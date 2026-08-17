using Application.Abstractions.Base;
using Domain.Aggregates.Appointment;
using Domain.Aggregates.AppointmentSlot;
using Domain.Aggregates.Communications.Notification;
using Domain.Aggregates.Communications.SmsGateway;
using Domain.Aggregates.Identity.AdministratorProfile;
using Domain.Aggregates.Identity.ClinicalStaffProfile;
using Domain.Aggregates.Identity.PatientProfile;
using Domain.Aggregates.Identity.User;
using Domain.Aggregates.Laboratory.LaboratoryRequest;
using Domain.Aggregates.Laboratory.TestCategory;
using Domain.Aggregates.Monitoring.ActivityLog;
using Domain.Aggregates.Monitoring.StorageStatus;
using Domain.Aggregates.Monitoring.SystemConfig;
using Domain.Aggregates.SlotCapacity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Database
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Identity
        public DbSet<User> Users => Set<User>();
        public DbSet<PatientProfile> PatientProfiles => Set<PatientProfile>();
        public DbSet<ClinicalStaffProfile> ClinicalStaffProfiles => Set<ClinicalStaffProfile>();
        public DbSet<AdministratorProfile> AdministratorProfiles => Set<AdministratorProfile>();

        // Laboratory
        public DbSet<TestCategory> TestCategories => Set<TestCategory>();
        public DbSet<LaboratoryRequest> LabRequests => Set<LaboratoryRequest>();
        // LabResult intentionally has no DbSet — only reachable via LabRequest.Results.

        // Notifications
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<SmsGatewayStatus> SmsGatewayStatuses => Set<SmsGatewayStatus>();

        // Monitoring
        public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();
        public DbSet<StorageStatus> StorageStatuses => Set<StorageStatus>();
        public DbSet<SystemConfig> SystemConfigs => Set<SystemConfig>();

        // Appointments
        public DbSet<AppointmentSlot> AppointmentSlots => Set<AppointmentSlot>();
        public DbSet<SlotCapacityConfig> SlotCapacityConfigs => Set<SlotCapacityConfig>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        // AppointmentReminder intentionally has no DbSet — only reachable via Appointment.Reminders.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        // IUnitOfWork — DbContext.SaveChangesAsync returns Task<int>; the interface
        // only cares that the transaction committed, so this adapts the signature.
        Task IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
            => base.SaveChangesAsync(cancellationToken);
    }   
}
