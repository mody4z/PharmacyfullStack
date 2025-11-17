using Pharmacy.Application.DTOs;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.Interfaces;

namespace Pharmacy.Application.Services
{
    public class InOutService : IInOutService
    {
        private readonly IInOutRepository _repository;
        private readonly IMedicineRepository _medicineRepository;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Client> _clientRepository;

        public InOutService(
            IInOutRepository repository, 
            IMedicineRepository medicineRepository,
            IRepository<Employee> employeeRepository,
            IRepository<Client> clientRepository)
        {
            _repository = repository;
            _medicineRepository = medicineRepository;
            _employeeRepository = employeeRepository;
            _clientRepository = clientRepository;
        }

        public async Task<IEnumerable<InOutDto>> GetAllAsync()
        {
            var transactions = await _repository.GetAllAsync();
            var result = new List<InOutDto>();
            
            foreach (var transaction in transactions)
            {
                result.Add(await MapToDtoAsync(transaction));
            }
            
            return result;
        }

        public async Task<InOutDto?> GetByIdAsync(int id)
        {
            var transaction = await _repository.GetByIdAsync(id);
            return transaction == null ? null : await MapToDtoAsync(transaction);
        }

        public async Task<IEnumerable<InOutDto>> GetByMedicineIdAsync(int medicineId)
        {
            var transactions = await _repository.GetByMedicineIdAsync(medicineId);
            var result = new List<InOutDto>();
            
            foreach (var transaction in transactions)
            {
                result.Add(await MapToDtoAsync(transaction));
            }
            
            return result;
        }

        public async Task<InOutDto> CreateTransactionAsync(CreateInOutDto dto)
        {
            if (dto.TransactionType != "In" && dto.TransactionType != "Out")
                throw new ArgumentException("TransactionType must be 'In' or 'Out'");

            var medicine = await _medicineRepository.GetByIdAsync(dto.MedicineId);
            if (medicine == null)
                throw new KeyNotFoundException("Medicine not found");

            if (dto.TransactionType == "Out" && medicine.StockQuantity < dto.Quantity)
                throw new InvalidOperationException("Insufficient stock");

            var transaction = new InOut
            {
                MedicineId = dto.MedicineId,
                EmployeeId = dto.EmployeeId,
                ClientId = dto.ClientId,
                TransactionType = dto.TransactionType,
                Quantity = dto.Quantity,
                Notes = dto.Notes,
                TransactionDate = DateTime.Now
            };

            if (dto.TransactionType == "In")
                medicine.StockQuantity += dto.Quantity;
            else
                medicine.StockQuantity -= dto.Quantity;

            await _medicineRepository.UpdateAsync(medicine);
            var created = await _repository.AddAsync(transaction);

            return await MapToDtoAsync(created);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        private async Task<InOutDto> MapToDtoAsync(InOut transaction)
        {
            var medicine = transaction.Medicine ?? await _medicineRepository.GetByIdAsync(transaction.MedicineId);
            var employee = transaction.Employee;
            var client = transaction.Client;
            
            if (transaction.EmployeeId.HasValue && employee == null)
            {
                employee = await _employeeRepository.GetByIdAsync(transaction.EmployeeId.Value);
            }
            
            if (transaction.ClientId.HasValue && client == null)
            {
                client = await _clientRepository.GetByIdAsync(transaction.ClientId.Value);
            }
            
            return new InOutDto
            {
                Id = transaction.Id,
                MedicineId = transaction.MedicineId,
                MedicineName = medicine?.Name,
                EmployeeId = transaction.EmployeeId,
                EmployeeName = employee?.Name,
                ClientId = transaction.ClientId,
                ClientName = client?.Name,
                TransactionType = transaction.TransactionType,
                Quantity = transaction.Quantity,
                TransactionDate = transaction.TransactionDate,
                Notes = transaction.Notes
            };
        }
    }
}
