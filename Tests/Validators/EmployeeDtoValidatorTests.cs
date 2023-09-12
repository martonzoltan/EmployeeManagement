using Shared.Models.DTO;
using Shared.Validators;

namespace Tests.Validators;

public class EmployeeDtoValidatorTests
{
    [Fact]
    public void ShouldHaveValidationErrorForEmptyName()
    {
        var validator = new EmployeeDtoValidator();
        var employeeDto = new EmployeeDto
            {Name = "", Email = "test@example.com", DateOfBirth = DateTime.Now, Department = "IT"};

        var result = validator.Validate(employeeDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Name is required");
    }

    [Fact]
    public void ShouldHaveValidationErrorForInvalidEmail()
    {
        var validator = new EmployeeDtoValidator();
        var employeeDto = new EmployeeDto
            {Name = "John Doe", Email = "invalid-email", DateOfBirth = DateTime.Now, Department = "IT"};

        var result = validator.Validate(employeeDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Please enter valid email");
    }

    [Fact]
    public void ShouldHaveValidationErrorForNullDateOfBirth()
    {
        var validator = new EmployeeDtoValidator();
        var employeeDto = new EmployeeDto
            {Name = "John Doe", Email = "test@example.com", DateOfBirth = null, Department = "IT"};

        var result = validator.Validate(employeeDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Birthday is required");
    }

    [Fact]
    public void ShouldHaveValidationErrorForEmptyDepartment()
    {
        var validator = new EmployeeDtoValidator();
        var employeeDto = new EmployeeDto
            {Name = "John Doe", Email = "test@example.com", DateOfBirth = DateTime.Now, Department = ""};

        var result = validator.Validate(employeeDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Department is required");
    }

    [Fact]
    public void ShouldNotHaveValidationErrorForValidEmployeeDto()
    {
        var validator = new EmployeeDtoValidator();
        var employeeDto = new EmployeeDto
            {Name = "John Doe", Email = "test@example.com", DateOfBirth = DateTime.Now, Department = "IT"};

        var result = validator.Validate(employeeDto);

        Assert.True(result.IsValid);
    }
}