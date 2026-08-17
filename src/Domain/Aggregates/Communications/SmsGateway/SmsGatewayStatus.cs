using SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.Aggregates.Communications.SmsGateway
{
    public class SmsGatewayStatus : AggregateRoot
    {
        private SmsGatewayStatus(
            Guid id,
            string status,
            int creditsRemaining)
            : base(id)
        {
            Status = status;
            CreditsRemaining = creditsRemaining;
            LastUpdated = DateTime.UtcNow;
        }

        public string Status { get; private set; }

        public int CreditsRemaining { get; private set; }

        public int ErrorCount24h { get; private set; }

        public DateTime LastUpdated { get; private set; }

        public static ResultT<SmsGatewayStatus> Initialize(string status, int creditsRemaining)
        {
            if (string.IsNullOrWhiteSpace(status))
                return GeneralErrors.General.Empty(nameof(status));

            if (creditsRemaining < 0)
                return GeneralErrors.General.Invalid(nameof(creditsRemaining));

            return new SmsGatewayStatus(Guid.NewGuid(), status, creditsRemaining);
        }

        public Result RecordDeliveryFailure()
        {
            ErrorCount24h++;
            LastUpdated = DateTime.UtcNow;

            return Result.Success();
        }

        public Result ConsumeCredits(int amount)
        {
            if (amount <= 0)
                return GeneralErrors.General.Invalid(nameof(amount));

            if (amount > CreditsRemaining)
                return SmsGatewayStatusErrors.InsufficientCredit;

            CreditsRemaining -= amount;
            LastUpdated = DateTime.UtcNow;

            return Result.Success();
        }

        public Result TopUpCredits(int amount)
        {
            if (amount <= 0)
                return GeneralErrors.General.Invalid(nameof(amount));

            CreditsRemaining += amount;
            LastUpdated = DateTime.UtcNow;

            return Result.Success();
        }
    }

}
