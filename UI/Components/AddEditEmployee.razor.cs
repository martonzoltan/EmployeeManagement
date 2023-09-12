using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using Shared.Models.Entities;
using Shared.Models.ViewModels;
using UI.Helpers;

namespace UI.Components;

public partial class AddEditEmployee
{
    [Parameter] public EmployeeViewModel? employee { get; set; }
    [Parameter] public EventCallback<EmployeeViewModel> OnSave { get; set; }
    [Parameter] public EventCallback<EmployeeViewModel> OnDelete { get; set; }
    [Parameter] public bool IsEdit { get; set; }

    private bool _isOpen = false;
    private EmployeeViewModel _localEmployee = new();
    private DialogOptions _dialogOptions = new() {DisableBackdropClick = true};

    protected override Task OnParametersSetAsync()
    {
        if (employee is not null)
        {
            IsEdit = true;
            _localEmployee = employee.Clone();
        }
        else
        {
            IsEdit = false;
            _localEmployee = new EmployeeViewModel();
        }

        return Task.FromResult(Task.CompletedTask);
    }

    public void Open()
    {
        _isOpen = true;
    }

    private async Task HandleValidSubmit()
    {
        await OnSave.InvokeAsync(_localEmployee);
        _isOpen = false;
    }

    private async Task Delete()
    {
        await OnDelete.InvokeAsync(_localEmployee);
        _isOpen = false;
    }

    private void Cancel()
    {
        _isOpen = false;
    }
}