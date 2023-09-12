using API.Requests;
using API.Services.Interfaces;
using MediatR;

namespace API.Handlers;

public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeRequest>
{
    private readonly IEmployeeRepository _repository;

    public DeleteEmployeeHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteEmployeeRequest request, CancellationToken cancellationToken)
    {
        await _repository.DeleteEmployee(request.EmployeeId, cancellationToken);
    }
}