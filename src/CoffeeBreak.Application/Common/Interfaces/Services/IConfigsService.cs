using CoffeeBreak.Domain.Entities;

namespace CoffeeBreak.Application.Common.Interfaces.Services
{
    public interface IConfigsService
    {
        Configs? GetConfigs();
        Configs Update(string address, decimal deliveryTax);
    }
}
