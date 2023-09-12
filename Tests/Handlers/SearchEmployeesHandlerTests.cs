using API.Handlers;
using API.Requests;
using API.Services.Interfaces;
using AutoMapper;
using Moq;
using Shared.Models.DTO;
using Shared.Models.Entities;

namespace Tests.Handlers;

public class SearchEmployeeHandlerTests
{
    private readonly Mock<IEmployeeRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly SearchEmployeeHandler _handler;
    private readonly List<Guid> _guids;

    public SearchEmployeeHandlerTests()
    {
        _mockRepository = new Mock<IEmployeeRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new SearchEmployeeHandler(_mockRepository.Object, _mockMapper.Object);

        _guids = new List<Guid>
        {
            Guid.NewGuid(),
            Guid.NewGuid()
        };
    }

    [Theory]
    [InlineData("John", 0)]
    [InlineData("Jane", 1)]
    public async Task Handle_ShouldReturnEmployees(string searchTerm, int guidIndex)
    {
        // Arrange
        var employees = new List<Employee>
        {
            new() {Id = _guids[0], CreatedAt = DateTime.Now, Name = "John"},
            new() {Id = _guids[1], CreatedAt = DateTime.Now, Name = "Jane"}
        };
        _mockRepository.Setup(r => r.SearchEmployees(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(employees.Where(e => e.Name == searchTerm).ToList());
        _mockMapper.Setup(m => m.Map<List<EmployeeDto>>(It.IsAny<List<Employee>>())).Returns((List<Employee> source) =>
            source.Select(e => new EmployeeDto {Id = e.Id, Name = e.Name}).ToList());

        // Act
        var result =
            await _handler.Handle(new SearchEmployeeRequest(searchTerm), new CancellationToken());

        // Assert
        _mockRepository.Verify(r => r.SearchEmployees(searchTerm, It.IsAny<CancellationToken>()), Times.Once);
        Assert.Single(result);
        Assert.Equal(_guids[guidIndex], result.First().Id);
    }
}