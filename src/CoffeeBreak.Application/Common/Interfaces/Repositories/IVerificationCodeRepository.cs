using CoffeeBreak.Application.Common.Interfaces.Repositories;
using CoffeeBreak.Domain.Entities;

namespace CoffeeBreak.Application.Common.Interfaces
{
    public interface IVerificationCodeRepository : IBaseRepository<VerificationCode>
    {
        VerificationCode Create();
        Task<VerificationCode?> GetByUserIdByCode(int UserId, string code);
    }
}
