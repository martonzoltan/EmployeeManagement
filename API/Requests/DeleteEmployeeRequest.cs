using MediatR;

namespace API.Requests;

public record DeleteEmployeeRequest(Guid EmployeeId) : IRequest;