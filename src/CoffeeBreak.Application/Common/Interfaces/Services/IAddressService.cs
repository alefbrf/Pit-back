using CoffeeBreak.Application.DTOs.Request.Address;
using CoffeeBreak.Domain.Entities;

namespace CoffeeBreak.Application.Common.Interfaces.Services
{
    public interface IAddressService
    {
        List<Address> GetAll(int userId);
        Address? GetById(int userId, int id);
        Address Create(int userId, AddressDTO addressDTO);
        Address Update(int id, AddressDTO addressDTO);
        void Delete(int id);
    }
}
