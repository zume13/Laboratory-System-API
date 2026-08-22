using Domain.Aggregates.Identity.UserProfile.Enums;
using Domain.ValueObjects;
using SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.Aggregates.Identity.UserProfile
{
    public class User : AggregateRoot
    {
        private User(
            Guid id,
            Name firstName,
            Name lastName,
            Email email,
            PhoneNumber phone,
            string hashedPassword,
            UserRole role)
            : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phone;
            HashedPassword = hashedPassword;
            Role = role;
        }

        public Name FirstName { get; private set; }

        public Name LastName { get; private set; }

        public Email Email { get; private set; }

        public PhoneNumber PhoneNumber { get; private set; }

        public string HashedPassword { get; private set; }

        public UserRole Role { get; private set; } 

        public DateTime? LastLoginAt { get; private set; }

        public static ResultT<User> Create(
            Name firstName,
            Name lastName,
            Email email,
            PhoneNumber phone,
            string hashedPassword,
            UserRole role)
        {
            if (string.IsNullOrWhiteSpace(firstName.value))
                return GeneralErrors.General.Empty(nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName.value))
                return GeneralErrors.General.Empty(nameof(lastName));

            if (string.IsNullOrWhiteSpace(email.value))
                return GeneralErrors.General.Empty(nameof(email));

            if (string.IsNullOrWhiteSpace(hashedPassword))
                return GeneralErrors.General.Empty(nameof(hashedPassword));

            return new User(
                Guid.NewGuid(),
                firstName,
                lastName,
                email,
                phone,  
                hashedPassword,
                role);
        }

        public Result AssignToRole(UserRole role)
        {
            Role = role;

            return Result.Success();
        }

        public Result UpdateName(string firstName, string lastName)
        {
            var newFirstName = Name.Create(firstName);

            if (newFirstName.IsFailure)
                return newFirstName.Error;

            var newLastName = Name.Create(lastName);

            if (newLastName.IsFailure)
                return newLastName.Error;

            FirstName = newFirstName.value;
            LastName = newLastName.value;

            return Result.Success();
        }

        public Result UpdateEmail(string email)
        {
            var newEmail = Email.Create(email);

            if (newEmail.IsFailure)
                return newEmail.Error;

            Email = newEmail.value;

            return Result.Success();
        }

        public Result UpdatePassword(string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(hashedPassword))
                return GeneralErrors.General.Empty(nameof(hashedPassword));

            HashedPassword = hashedPassword;

            return Result.Success();
        }

        public Result RecordLogin()
        {
            LastLoginAt = DateTime.UtcNow;

            return Result.Success();
        }
    }
}
