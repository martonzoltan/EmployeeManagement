using API.Context;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Entities;

namespace API.Services;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _dbContext;

    public EmployeeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Employee>> GetEmployees(CancellationToken cancellationToken)
    {
        return await _dbContext.Employees.Where(x => x.DeletedAt == null)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<Employee?> GetEmployeeById(Guid employeeId, CancellationToken cancellationToken)
    {
        return await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId,
            cancellationToken: cancellationToken);
    }

    public async Task<List<Employee>> SearchEmployees(string searchTerm, CancellationToken cancellationToken)
    {
        return await _dbContext.Employees
            .Where(e =>
                e.DeletedAt == null &&
                (EF.Functions.Like(e.Name, $"%{searchTerm}%") ||
                 EF.Functions.Like(e.Email, $"%{searchTerm}%") ||
                 EF.Functions.Like(e.Department, $"%{searchTerm}%"))
            )
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<Employee> CreateEmployee(Employee employee, CancellationToken cancellationToken)
    {
        var addResponse = await _dbContext.AddAsync(employee, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return addResponse.Entity;
    }

    public async Task<Employee> UpdateEmployee(Employee employee, CancellationToken cancellationToken)
    {
        var addResponse = _dbContext.Update(employee);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return addResponse.Entity;
    }

    public async Task DeleteEmployee(Guid employeeId, CancellationToken cancellationToken)
    {
        var employeeToDelete = await GetEmployeeById(employeeId, cancellationToken);
        if (employeeToDelete is null) throw new NullReferenceException();
        employeeToDelete.DeletedAt = DateTime.Now;
        await UpdateEmployee(employeeToDelete, cancellationToken);
    }
}