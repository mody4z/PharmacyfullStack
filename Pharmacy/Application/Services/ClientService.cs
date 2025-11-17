using Pharmacy.Application.DTOs;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.Interfaces;

namespace Pharmacy.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IRepository<Client> _repository;

        public ClientService(IRepository<Client> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ClientDto>> GetAllAsync()
        {
            var clients = await _repository.GetAllAsync();
            return clients.Select(c => new ClientDto
            {
                Id = c.Id,
                Name = c.Name,
                PhoneNumber = c.PhoneNumber,
                Email = c.Email,
                Address = c.Address,
                CreatedDate = c.CreatedDate
            });
        }

        public async Task<ClientDto?> GetByIdAsync(int id)
        {
            var client = await _repository.GetByIdAsync(id);
            if (client == null) return null;

            return new ClientDto
            {
                Id = client.Id,
                Name = client.Name,
                PhoneNumber = client.PhoneNumber,
                Email = client.Email,
                Address = client.Address,
                CreatedDate = client.CreatedDate
            };
        }

        public async Task<ClientDto> CreateAsync(CreateClientDto dto)
        {
            var client = new Client
            {
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Address = dto.Address,
                CreatedDate = DateTime.Now
            };

            var created = await _repository.AddAsync(client);

            return new ClientDto
            {
                Id = created.Id,
                Name = created.Name,
                PhoneNumber = created.PhoneNumber,
                Email = created.Email,
                Address = created.Address,
                CreatedDate = created.CreatedDate
            };
        }

        public async Task UpdateAsync(int id, UpdateClientDto dto)
        {
            var client = await _repository.GetByIdAsync(id);
            if (client == null) throw new KeyNotFoundException($"Client with id {id} not found");

            client.Name = dto.Name;
            client.PhoneNumber = dto.PhoneNumber;
            client.Email = dto.Email;
            client.Address = dto.Address;

            await _repository.UpdateAsync(client);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
