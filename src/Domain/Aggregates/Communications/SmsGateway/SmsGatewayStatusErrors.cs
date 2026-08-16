using SharedKernel.Shared;

namespace Domain.Aggregates.Communications.SmsGateway
{
    public static class SmsGatewayStatusErrors
    {
        public static readonly Error InsufficientCredit = Error.Conflict("Status.InsufficientCredit", "Insufficient credits available.");
    }
}
