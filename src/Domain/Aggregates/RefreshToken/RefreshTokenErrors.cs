
using SharedKernel.Shared;

namespace Domain.Aggregates.RefreshToken
{
    public static class RefreshTokenErrors
    {
            public static Error TokenHashCannotBeNull => Error.Conflict("TokenHash.Null", "Token hash cannot be null.");
    }
}
