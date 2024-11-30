using CoffeeBreak.Domain.Entities;

namespace CoffeeBreak.Application.Common.Interfaces.Repositories
{
    public interface IConfigsRepository : IBaseRepository<Configs>
    {
        Configs Update(Configs configs);
        Configs? GetConfig();
    }
}
