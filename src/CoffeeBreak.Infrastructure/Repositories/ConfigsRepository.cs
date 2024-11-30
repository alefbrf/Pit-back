using CoffeeBreak.Application.Common.Interfaces.Repositories;
using CoffeeBreak.Domain.Entities;
using CoffeeBreak.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoffeeBreak.Infrastructure.Repositories
{
    public class ConfigsRepository : BaseRepository<Configs>, IConfigsRepository
    {
        private AppDbContext _context;

        public ConfigsRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Configs Update(Configs configs)
        {
            _context.Update(configs);
            _context.SaveChanges();
            return configs;
        }

        public Configs? GetConfig()
        {
            return _context.Configs.AsNoTracking().FirstOrDefault();
        }
    }
}
