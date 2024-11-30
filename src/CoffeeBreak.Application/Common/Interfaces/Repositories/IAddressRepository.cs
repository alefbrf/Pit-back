using CoffeeBreak.Domain.Entities;

namespace CoffeeBreak.Application.Common.Interfaces.Repositories
{
    public interface IAddressRepository : IBaseRepository<Address>
    {
        List<Address> GetAllByUser(int userId);
        Address? GetByIdByUser(int userId, int id);
    }
}
