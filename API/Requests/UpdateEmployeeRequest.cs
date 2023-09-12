using MediatR;
using Shared.Models.DTO;

namespace API.Requests;

public record UpdateEmployeeRequest(EmployeeDto Employee) : IRequest<EmployeeDto>;