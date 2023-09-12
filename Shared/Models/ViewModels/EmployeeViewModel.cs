using System.ComponentModel.DataAnnotations;
using Shared.Constants;

namespace Shared.Models.ViewModels;

public class EmployeeViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email address is required")]
    [RegularExpression(ConstantList.EmailValidatorRegex, ErrorMessage = "Please enter valid email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Birthday is required")]
    public DateTime? DateOfBirth { get; set; }

    [Required(ErrorMessage = "Department is required")]
    public string Department { get; set; } = string.Empty;

    public string? DateOfBirthFormatted => DateOfBirth?.ToString("MM/dd/yyyy");
}