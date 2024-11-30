using CoffeeBreak.Application.Common.Interfaces.Repositories;
using CoffeeBreak.Application.Common.Interfaces.Services;
using CoffeeBreak.Domain.Entities;

namespace CoffeeBreak.Application.Services
{
    public class ConfigsService : IConfigsService
    {
        private readonly IConfigsRepository _configsRepository;

        public ConfigsService(IConfigsRepository configsRepository)
        {
            _configsRepository = configsRepository;
        }

        public Configs? GetConfigs()
        {
            return _configsRepository.GetConfig();
        }

        public Configs Update(string address, decimal deliveryTax)
        {
            var config = _configsRepository.GetConfig();
            if (config == null)
            {
                config = new()
                {
                    Address = address,
                    DeliveryTax = deliveryTax
                };

                _configsRepository.Insert(config, true);
                return config;
            }

            config.Address = address;
            config.DeliveryTax = deliveryTax;
            _configsRepository.Update(config);
            return config;
        }
    }
}
