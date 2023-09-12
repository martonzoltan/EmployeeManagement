using Shared.Models.ViewModels;

namespace UI.Services.Interfaces;

public interface IEmployeeService
{
    Task<List<EmployeeViewModel>> GetAllEmployees();
    Task<EmployeeViewModel> AddEmployee(EmployeeViewModel employee);
    Task<EmployeeViewModel> UpdateEmployee(EmployeeViewModel employee);
    Task<bool> DeleteEmployee(Guid employeeId);
    Task<List<EmployeeViewModel>> SearchEmployees(string searchTerm);
}