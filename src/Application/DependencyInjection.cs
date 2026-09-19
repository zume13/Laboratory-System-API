using Application.Behaviors;
using Application.Behaviours.Validations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.DomainEvent;
using Application.Features.LabOrder.Events.LabOrderCreatedEvent;
using Application.Features.LabOrder.Events.LabResultReleasedEvent;
using Application.Features.Appointments.Events.AppointmentBookedEvent;
using Application.Features.Appointments.Events.AppointmentCancelledEvent;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

           services.AddScoped<IDomainEventHandler<Domain.Aggregates.Laboratory.LaboratoryOrder.Events.LabOrderCreatedEvent>, LogLabOrderCreatedHandler>();

           services.AddScoped<IDomainEventHandler<Domain.Aggregates.Laboratory.LaboratoryOrder.Events.LabResultReleasedEvent>, LogLabResultReleasedHandler>();
           services.AddScoped<IDomainEventHandler<Domain.Aggregates.Laboratory.LaboratoryOrder.Events.LabResultReleasedEvent>, NotifyPatientLabResultReleasedHandler>();

             services.AddScoped<IDomainEventHandler<Domain.Aggregates.Appointment.Events.AppointmentBookedEvent>, LogAppointmentBookedHandler>();
             services.AddScoped<IDomainEventHandler<Domain.Aggregates.Appointment.Events.AppointmentBookedEvent>, NotifyPatientAppointmentBookedHandler>();

             services.AddScoped<IDomainEventHandler<Domain.Aggregates.Appointment.Events.AppointmentCancelledEvent>, LogAppointmentCancelledHandler>();
             services.AddScoped<IDomainEventHandler<Domain.Aggregates.Appointment.Events.AppointmentCancelledEvent>, NotifyPatientAppointmentCancelledHandler>();

            return services;
        }
    }
}
