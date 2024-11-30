using CoffeeBreak.Domain.Entities;

namespace CoffeeBreak.Application.Common.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository <User>
    {
        User? Create(User user);
        Task<User?> GetByEmail(string email);
        Task Approve(User user);
        List<User> GetDeliveryMans();
    }
}
