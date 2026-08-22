using SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.Aggregates.RefreshToken
{
    public class RefreshToken : AggregateRoot
    {
        private RefreshToken(string tokenHash, Guid userId, DateTime expiresAt) : base(Guid.NewGuid())
        {
            TokenHash = tokenHash;
            UserId = userId;
            ExpiresAt = expiresAt;
        }

        public string TokenHash { get; private set; } 
        public Guid UserId { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public DateTime? RevokedAt { get; private set; }
        public Guid? ReplacedByTokenId { get; private set; }

        public static ResultT<RefreshToken> Create(string tokenHash, Guid userId, DateTime expiresAt)
        {
            if(tokenHash is null) 
                return RefreshTokenErrors.TokenHashCannotBeNull;

            return ResultT<RefreshToken>.Success(new RefreshToken(tokenHash, userId, expiresAt));
        }

        public void Revoke(Guid? replacedByTokenId)
        {
            RevokedAt = DateTime.UtcNow;
            ReplacedByTokenId = replacedByTokenId;
        }

        public bool isActive() => RevokedAt is null && ExpiresAt > DateTime.UtcNow;
    }
}
