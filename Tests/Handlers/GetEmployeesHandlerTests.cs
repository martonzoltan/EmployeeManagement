using API.Handlers;
using API.Requests;
using API.Services.Interfaces;
using AutoMapper;
using Moq;
using Shared.Models.DTO;
using Shared.Models.Entities;

namespace Tests.Handlers;

public class GetEmployeesHandlerTests
{
    private readonly Mock<IEmployeeRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetEmployeesHandler _handler;
    private readonly List<Employee> _employees;
    private readonly List<EmployeeDto> _employeeDtos;

    public GetEmployeesHandlerTests()
    {
        _mockRepository = new Mock<IEmployeeRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new GetEmployeesHandler(_mockRepository.Object, _mockMapper.Object);

        _employees = new List<Employee>
        {
            new() {Id = Guid.NewGuid()},
            new() {Id = Guid.NewGuid()}
        };

        _employeeDtos = _employees.Select(e => new EmployeeDto {Id = e.Id}).ToList();
    }

    [Fact]
    public async Task Handle_ShouldReturnEmployees()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetEmployees(It.IsAny<CancellationToken>())).ReturnsAsync(_employees);
        _mockMapper.Setup(m => m.Map<List<EmployeeDto>>(_employees)).Returns(_employeeDtos);

        // Act
        var result = await _handler.Handle(new GetEmployeesRequest(), new CancellationToken());

        // Assert
        _mockRepository.Verify(r => r.GetEmployees(It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal(_employeeDtos, result);
    }
}