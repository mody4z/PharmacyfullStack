using Pharmacy.Application.DTOs;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.Interfaces;

namespace Pharmacy.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _repository;

        public EmployeeService(IRepository<Employee> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await _repository.GetAllAsync();
            return employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                Position = e.Position,
                PhoneNumber = e.PhoneNumber,
                Email = e.Email,
                Salary = e.Salary,
                HireDate = e.HireDate
            });
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null) return null;

            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Position = employee.Position,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
                Salary = employee.Salary,
                HireDate = employee.HireDate
            };
        }

        public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto)
        {
            var employee = new Employee
            {
                Name = dto.Name,
                Position = dto.Position,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Salary = dto.Salary,
                HireDate = DateTime.Now
            };

            var created = await _repository.AddAsync(employee);

            return new EmployeeDto
            {
                Id = created.Id,
                Name = created.Name,
                Position = created.Position,
                PhoneNumber = created.PhoneNumber,
                Email = created.Email,
                Salary = created.Salary,
                HireDate = created.HireDate
            };
        }

        public async Task UpdateAsync(int id, UpdateEmployeeDto dto)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null) throw new KeyNotFoundException($"Employee with id {id} not found");

            employee.Name = dto.Name;
            employee.Position = dto.Position;
            employee.PhoneNumber = dto.PhoneNumber;
            employee.Email = dto.Email;
            employee.Salary = dto.Salary;

            await _repository.UpdateAsync(employee);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
