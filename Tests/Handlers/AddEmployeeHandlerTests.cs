using API.Handlers;
using API.Requests;
using API.Services.Interfaces;
using AutoMapper;
using FluentValidation;
using Moq;
using Shared.Models.DTO;
using Shared.Models.Entities;

namespace Tests.Handlers;

public class AddEmployeeHandlerTests
{
    private readonly Mock<IEmployeeRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IValidator<EmployeeDto>> _mockValidator;
    private readonly AddEmployeeHandler _handler;
    private readonly Employee _employee;
    private readonly EmployeeDto _employeeDto;
    private readonly AddEmployeeRequest _request;

    public AddEmployeeHandlerTests()
    {
        _mockRepository = new Mock<IEmployeeRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockValidator = new Mock<IValidator<EmployeeDto>>();
        _handler = new AddEmployeeHandler(_mockRepository.Object, _mockMapper.Object, _mockValidator.Object);

        _employee = new Employee
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now
        };

        _employeeDto = new EmployeeDto
        {
            Id = _employee.Id,
        };

        _request = new AddEmployeeRequest(_employeeDto);
    }

    [Fact]
    public async Task Handle_ShouldAddEmployee()
    {
        // Arrange
        _mockMapper.Setup(m => m.Map<Employee>(_request.Employee)).Returns(_employee);
        _mockRepository.Setup(r => r.CreateEmployee(_employee, It.IsAny<CancellationToken>())).ReturnsAsync(_employee);
        _mockMapper.Setup(m => m.Map<EmployeeDto>(_employee)).Returns(_employeeDto);
        _mockValidator.Setup(v => v.ValidateAsync(It.IsAny<EmployeeDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        // Act
        var result = await _handler.Handle(_request, new CancellationToken());

        // Assert
        _mockRepository.Verify(r => r.CreateEmployee(_employee, It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal(_employeeDto, result);
    }


    [Fact]
    public async Task Handle_InvalidRequest_ShouldThrowValidationException()
    {
        // Arrange
        _mockMapper.Setup(m => m.Map<Employee>(_request.Employee)).Returns(_employee);
        _mockRepository.Setup(r => r.CreateEmployee(_employee, It.IsAny<CancellationToken>())).ReturnsAsync(_employee);
        _mockMapper.Setup(m => m.Map<EmployeeDto>(_employee)).Returns(_employeeDto);
        var validationFailures = new List<FluentValidation.Results.ValidationFailure>
        {
            new("Name", "Name is required")
        };
        _mockValidator.Setup(v => v.ValidateAsync(It.IsAny<EmployeeDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult(validationFailures));

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(_request, CancellationToken.None));
    }
}