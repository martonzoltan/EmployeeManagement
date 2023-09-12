using API.Handlers;
using API.Requests;
using API.Services.Interfaces;
using Moq;

namespace Tests.Handlers;

public class DeleteEmployeeHandlerTests
{
    private readonly Mock<IEmployeeRepository> _mockRepository;
    private readonly DeleteEmployeeHandler _handler;
    private readonly Guid _employeeId;
    private readonly DeleteEmployeeRequest _request;

    public DeleteEmployeeHandlerTests()
    {
        _mockRepository = new Mock<IEmployeeRepository>();
        _handler = new DeleteEmployeeHandler(_mockRepository.Object);
        _employeeId = Guid.NewGuid();
        _request = new DeleteEmployeeRequest(_employeeId);
    }

    [Fact]
    public async Task Handle_ShouldDeleteEmployee()
    {
        // Arrange
        _mockRepository.Setup(r => r.DeleteEmployee(_request.EmployeeId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(_request, new CancellationToken());

        // Assert
        _mockRepository.Verify(r => r.DeleteEmployee(_employeeId, It.IsAny<CancellationToken>()), Times.Once);
    }
}