using FluentValidation;
using Shared.Constants;
using Shared.Models.DTO;

namespace Shared.Validators;

public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
{
    public EmployeeDtoValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(dto => dto.Email).NotEmpty().WithMessage("Email address is required")
            .Matches(ConstantList.EmailValidatorRegex).WithMessage("Please enter valid email");
        RuleFor(dto => dto.DateOfBirth).NotNull().WithMessage("Birthday is required");
        RuleFor(dto => dto.Department).NotEmpty().WithMessage("Department is required");
    }
}