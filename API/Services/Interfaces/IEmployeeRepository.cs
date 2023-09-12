using Shared.Models.Entities;

namespace API.Services.Interfaces;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetEmployees(CancellationToken cancellationToken);
    Task<Employee?> GetEmployeeById(Guid employeeId, CancellationToken cancellationToken);
    Task<List<Employee>> SearchEmployees(string searchTerm, CancellationToken cancellationToken);
    Task<Employee> CreateEmployee(Employee employee, CancellationToken cancellationToken);
    Task<Employee> UpdateEmployee(Employee employee, CancellationToken cancellationToken);
    Task DeleteEmployee(Guid employeeId, CancellationToken cancellationToken);
}