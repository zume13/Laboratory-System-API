using Application.Abstractions.Base;
using Domain.Aggregates.Identity.AdministratorProfile;
using Domain.Aggregates.Identity.UserProfile;
using Domain.Aggregates.Identity.UserProfile.Enums;
using Domain.ValueObjects;
using Infrastructure.Persistence.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services
{
    public sealed class AdminSeederService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly AdminSeedOptions _options;

        public AdminSeederService(
            ApplicationDbContext dbContext,
            IOptions<AdminSeedOptions> options)
        {
            _dbContext = dbContext;
            _options = options.Value;
        }

        public async Task SeedAsync()
        {
            var adminExists = await _dbContext.Users.AnyAsync(u => u.Role == UserRole.Admin);

            if (adminExists)
                return;

            var firstName = Name.Create(_options.FirstName);

            if(firstName.IsFailure)
                throw new InvalidOperationException(
                    firstName.Error.message);

            var lastName = Name.Create(_options.LastName);

            if(lastName.IsFailure)
                throw new InvalidOperationException(
                    lastName.Error.message);

            var email = Email.Create(_options.Email);

            if(email.IsFailure)
                throw new InvalidOperationException(
                    email.Error.message);

            var phoneNumber = PhoneNumber.Create(_options.PhoneNumber);

            if(phoneNumber.IsFailure)
                throw new InvalidOperationException(
                    phoneNumber.Error.message);

            if (string.IsNullOrEmpty(_options.Password))
                throw new InvalidOperationException("Null admin password");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(_options.Password);

            var userResult = User.Create(firstName.value, lastName.value, email.value, phoneNumber.value, hashedPassword, UserRole.Admin);

            if(userResult.IsFailure)
                throw new InvalidCastException(userResult.Error.message);

            var user = userResult.value;

            var adminProfileResult = AdministratorProfile.Create(user.Id);

            if(adminProfileResult.IsFailure)
                throw new InvalidOperationException(
                    adminProfileResult.Error.message);

            var adminProfile = adminProfileResult.value;

            _dbContext.Users.Add(user);

            _dbContext.AdministratorProfiles.Add(adminProfile);

            await _dbContext.SaveChangesAsync();
        }
    }
}
