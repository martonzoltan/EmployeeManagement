using MediatR;
using Shared.Models.DTO;

namespace API.Requests;

public record GetEmployeesRequest : IRequest<List<EmployeeDto>>;