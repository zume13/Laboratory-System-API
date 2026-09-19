using SharedKernel.Primitives;

namespace Domain.Aggregates.Otp
{
    public class Otp : AggregateRoot
    {
        public Guid UserId { get; private set; }

        public string OtpHash { get; private set; } = null!;

        public DateTime ExpiresAt { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? ConsumedAt { get; private set; }

        public int AttemptCount { get; private set; }

        private Otp() { }

        private Otp(
            Guid id,
            Guid userId,
            string otpHash,
            DateTime expiresAt)
        {
            Id = id;
            UserId = userId;
            OtpHash = otpHash;
            ExpiresAt = expiresAt;
            CreatedAt = DateTime.UtcNow;
        }

        public static Otp Create(
            Guid userId,
            string otpHash,
            DateTime expiresAt)
        {
            return new Otp(
                Guid.NewGuid(),
                userId,
                otpHash,
                expiresAt);
        }

        public bool IsExpired()
            => DateTime.UtcNow >= ExpiresAt;

        public bool IsConsumed()
            => ConsumedAt.HasValue;

        public void Consume()
        {
            ConsumedAt = DateTime.UtcNow;
        }

        public void IncrementAttempt()
        {
            AttemptCount++;
        }
    }
}
