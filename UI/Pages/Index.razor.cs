using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shared.Models.ViewModels;
using UI.Components;
using UI.Services.Interfaces;

namespace UI.Pages;

public partial class Index
{
    [Inject] private IEmployeeService EmployeeService { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }

    private AddEditEmployee _employeeModal;
    private List<EmployeeViewModel> _employees = new();
    private EmployeeViewModel? _selectedEmployee;
    private bool _isEdit;
    private string _searchTerm;

    protected override async Task OnInitializedAsync()
    {
        await GetEmployees();
    }

    private async Task GetEmployees()
    {
        try
        {
            _employees = await EmployeeService.GetAllEmployees();
        }

        catch (Exception ex)
        {
            Snackbar.Add($"Something went wrong {ex.Message}", Severity.Error);
        }
    }

    private void ShowAddEmployeeModal()
    {
        _selectedEmployee = null;
        _isEdit = false;
        _employeeModal.Open();
    }

    private void ShowEditEmployee(EmployeeViewModel employee)
    {
        _selectedEmployee = employee;
        _isEdit = true;
        _employeeModal.Open();
    }

    private async Task SaveEmployee(EmployeeViewModel viewModel)
    {
        try
        {
            if (_isEdit)
            {
                await EmployeeService.UpdateEmployee(viewModel);
                Snackbar.Add("Employee updated", Severity.Success);
            }
            else
            {
                await EmployeeService.AddEmployee(viewModel);
                Snackbar.Add("Employee created", Severity.Success);
            }

            await GetEmployees();
        }

        catch (Exception ex)
        {
            Snackbar.Add($"Something went wrong {ex.Message}", Severity.Error);
        }
    }

    private async Task DeleteEmployee(EmployeeViewModel viewModel)
    {
        try
        {
            await EmployeeService.DeleteEmployee(viewModel.Id);
            Snackbar.Add("Employee deleted", Severity.Success);
            await GetEmployees();
        }
        catch (Exception ex)
        {
            Snackbar.Add($"Something went wrong {ex.Message}", Severity.Error);
        }
    }

    private async Task HandleIntervalElapsed(string searchTerm)
    {
        _employees = string.IsNullOrWhiteSpace(searchTerm)
            ? await EmployeeService.GetAllEmployees()
            : await EmployeeService.SearchEmployees(searchTerm);
    }
}