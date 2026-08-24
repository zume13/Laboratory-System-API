using Application.Abstractions.Auth;
using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Identity.UserProfile;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' was not found.");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IActivityLogRepository, ActivityLogRepository>();
            services.AddScoped<IAdministratorProfileRepository, AdministratorProfileRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IAppointmentSlotRepository, AppointmentSlotRepository>();
            services.AddScoped<IClinicalProfileRepository, ClinicalProfileRepository>();
            services.AddScoped<ILaboratoryRequestRepository, LaboratoryRequestRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IPatientProfileRepository, PatientProfileRepository>();
            services.AddScoped<ISlotCapacityRepository, SlotCapacityRepository>();
            services.AddScoped<ISmsGatewayRepository, SmsGatewayRepository>();
            services.AddScoped<IStorageStatusRepository, StorageStatusRepository>();
            services.AddScoped<ISystemConfigRepository, SystemConfigRepository>();
            services.AddScoped<ITestCategoryRepository, TestCategoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            return services;
        }
    }
}
