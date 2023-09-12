using MediatR;
using Shared.Models.DTO;

namespace API.Requests;

public record AddEmployeeRequest(EmployeeDto Employee) : IRequest<EmployeeDto>;