using Application.Abstractions.Base;
using Domain.Aggregates.Identity.User;

namespace Application.Abstractions.Repositories
{
    public interface IUserRepository : Repository<User>
    {
    }
}
