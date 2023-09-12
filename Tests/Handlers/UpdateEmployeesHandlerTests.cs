using API.Handlers;
using API.Requests;
using API.Services.Interfaces;
using AutoMapper;
using Moq;
using Shared.Models.DTO;
using Shared.Models.Entities;

namespace Tests.Handlers;

public class UpdateEmployeeHandlerTests
{
    private readonly Mock<IEmployeeRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly UpdateEmployeeHandler _handler;
    private readonly Employee _employee;
    private readonly EmployeeDto _employeeDto;
    private readonly UpdateEmployeeRequest _request;

    public UpdateEmployeeHandlerTests()
    {
        _mockRepository = new Mock<IEmployeeRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new UpdateEmployeeHandler(_mockRepository.Object, _mockMapper.Object);

        _employee = new Employee
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now
        };

        _employeeDto = new EmployeeDto
        {
            Id = _employee.Id,
        };

        _request = new UpdateEmployeeRequest(_employeeDto);
    }

    [Fact]
    public async Task Handle_ShouldUpdateEmployee()
    {
        // Arrange
        _mockMapper.Setup(m => m.Map<Employee>(_request.Employee)).Returns(_employee);
        _mockRepository.Setup(r => r.UpdateEmployee(_employee, It.IsAny<CancellationToken>())).ReturnsAsync(_employee);
        _mockMapper.Setup(m => m.Map<EmployeeDto>(_employee)).Returns(_employeeDto);

        // Act
        var result = await _handler.Handle(_request, new CancellationToken());

        // Assert
        _mockRepository.Verify(r => r.UpdateEmployee(_employee, It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal(_employeeDto, result);
    }
}