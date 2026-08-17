using Domain.Aggregates.Identity.User;
using Infrastructure.Persistence.Database;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
