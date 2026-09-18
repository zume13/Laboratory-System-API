using Application.Abstractions.Events;
using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;
using SharedKernel.DomainEvent;

namespace Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class ProcessOutboxMessageJob : IJob
    {
        private readonly ApplicationDbContext _context;
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly IDomainEventTypeRegistry _registry;
        private readonly IOutboxMessageSerializer _serializer;
        private readonly ILogger<ProcessOutboxMessageJob> _logger;

        public ProcessOutboxMessageJob(
            ApplicationDbContext context,
            IDomainEventDispatcher dispatcher,
            IDomainEventTypeRegistry registry,
            IOutboxMessageSerializer serializer,
            ILogger<ProcessOutboxMessageJob> logger)
        {
            _context = context;
            _dispatcher = dispatcher;
            _registry = registry;
            _serializer = serializer;
            _logger = logger;
        }

        public async ValueTask Execute(
            IJobExecutionContext context,
            CancellationToken cancellationToken)
        {
            var messages = await _context.OutboxMessages
                .Where(m => m.ProcessedOn == null && m.RetryCount < 5)
                .Take(20)
                .ToListAsync(cancellationToken);

            foreach (var message in messages)
            {
                try
                {
                    var eventType = _registry.Resolve(message.EventName);

                    if (eventType.IsFailure)
                    {
                        _logger.LogError(
                            "Failed to resolve event type for '{EventName}': {Error}",
                            message.EventName,
                            eventType.Error);

                        continue;
                    }

                    var domainEvent =
                        (IDomainEvent)_serializer.Deserialize(
                            message.Content,
                            eventType.value);

                    await _dispatcher.DispatchAsync([domainEvent]);

                    message.ProcessedOn = DateTime.UtcNow;
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Error dispatching outbox message {MessageId}",
                        message.Id);

                    message.Error = ex.Message;
                    message.RetryCount++;
                }

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}