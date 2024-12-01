using CoffeeBreak.Application.Common.Exceptions;
using CoffeeBreak.Application.Common.Interfaces.Repositories;
using CoffeeBreak.Application.Common.Interfaces.Services;
using CoffeeBreak.Application.DTOs.Request.Address;
using CoffeeBreak.Domain.Entities;

namespace CoffeeBreak.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public List<Address> GetAll(int userId)
        {
            return _addressRepository.GetAllByUser(userId);
        }

        public Address? GetById(int userId, int id)
        {
            return _addressRepository.GetByIdByUser(userId, id);
        }

        public Address Create(int userId, AddressDTO addressDTO)
        {

            Address address = new Address()
            {
                UserId = userId,
                PostalCode = addressDTO.PostalCode,
                Neighborhood = addressDTO.Neighborhood,
                Street = addressDTO.Street,
                Number = addressDTO.Number,
                Complement = addressDTO.Complement
            };
            _addressRepository.Insert(address, true);
            return address;
        }

        public Address Update(int id, AddressDTO addressDTO)
        {
            var address = _addressRepository.GetById(id);

            if (address == null)
            {
                throw new BaseException("Endereço não encontrado", System.Net.HttpStatusCode.NotFound);
            }

            address.PostalCode = addressDTO.PostalCode;
            address.Neighborhood = addressDTO.Neighborhood;
            address.Street = addressDTO.Street;
            address.Number = addressDTO.Number;
            if (!String.IsNullOrWhiteSpace(addressDTO.Complement))
            {
                address.Complement = addressDTO.Complement;
            }
            _addressRepository.Commit();
            return address;
        }
        public void Delete(int id)
        {
            try
            {
                _addressRepository.DeleteWhere(address => address.Id == id);
            }
            catch (Exception)
            {
                throw new BaseException("Tentativa de apagar o endereço falhou, recarregue a página e tente novamente.", System.Net.HttpStatusCode.NotFound);
            }
        }

    }
}
