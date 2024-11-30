using CoffeeBreak.Application.Common.Enums;
using CoffeeBreak.Application.Common.Interfaces.Repositories;
using CoffeeBreak.Domain.Entities;
using CoffeeBreak.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoffeeBreak.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private AppDbContext _context;
        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<User?> GetByEmail(string email)
        {
            User? user = await FilterQuery(p => p.Email == email).FirstOrDefaultAsync();

            return user;
        }
        public User? Create(User user)
        {
            Insert(user);
            return user;
        }
        public async Task Approve(User user)
        {
            user.Verified = true;
            _context.Entry(user).Property(p => p.Verified).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public List<User> GetDeliveryMans()
        {
            return _context.Users.Where(p => p.Role == (byte)Role.DeliveryMan).AsNoTracking().ToList();
        }
    }
}
