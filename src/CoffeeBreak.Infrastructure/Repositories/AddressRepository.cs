using CoffeeBreak.Application.Common.Interfaces.Repositories;
using CoffeeBreak.Domain.Entities;
using CoffeeBreak.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoffeeBreak.Infrastructure.Repositories
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        private AppDbContext _context;

        public AddressRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public List<Address> GetAllByUser(int userId)
        {
            return _context.Addresses.Where(address => address.UserId == userId).AsNoTracking().ToList();
        }

        public Address? GetByIdByUser(int userId, int id)
        {
            return _context.Addresses.Where(address => address.Id == id && address.UserId == userId).AsNoTracking().FirstOrDefault();
        }
    }
}
