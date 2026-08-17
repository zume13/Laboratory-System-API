using Application.Abstractions.Base;
using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Primitives;

namespace Infrastructure.Persistence.Repositories
{
    public abstract class Repository<TAggregate> : Application.Abstractions.Base.Repository<TAggregate> where TAggregate : AggregateRoot
    {
        protected readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }   

        public async Task<List<TAggregate>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<TAggregate>().AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task AddAsync(TAggregate aggregate, CancellationToken cancellationToken)
        {
            await _dbContext.Set<TAggregate>().AddAsync(aggregate, cancellationToken);
        }

        public async Task<TAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TAggregate>().FindAsync(new object[] { id }, cancellationToken);
        }

        public void Remove(TAggregate aggregate)
        {
            _dbContext.Set<TAggregate>().Remove(aggregate);
        }

        public virtual void Update(TAggregate aggregate)
        {
            _dbContext.Set<TAggregate>().Update(aggregate);
        }
    }
}
