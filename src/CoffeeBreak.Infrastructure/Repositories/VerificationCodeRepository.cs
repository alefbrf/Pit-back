using CoffeeBreak.Application.Common.Interfaces;
using CoffeeBreak.Domain.Entities;
using CoffeeBreak.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoffeeBreak.Infrastructure.Repositories
{
    public class VerificationCodeRepository : BaseRepository<VerificationCode>, IVerificationCodeRepository
    {
        private AppDbContext _context;
        public VerificationCodeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public VerificationCode Create()
        {
            Random Random = new Random();
            int RandomNumber = Random.Next(1000000);
            string sixDigitNumber = RandomNumber.ToString("D6");

            VerificationCode verificationCode = new VerificationCode();
            verificationCode.Code = sixDigitNumber;
            verificationCode.Valid = DateTime.Now.AddMinutes(30);

            return verificationCode;
        }

        public async Task<VerificationCode?> GetByUserIdByCode(int UserId, string code)
        {
            return await (
                from codes in _context.VerificationCodes
                where
                    codes.UserId == UserId &&
                    codes.Code == code &&
                    codes.Valid >= DateTime.Now
                select codes
            ).FirstOrDefaultAsync();
        }
    }
}
