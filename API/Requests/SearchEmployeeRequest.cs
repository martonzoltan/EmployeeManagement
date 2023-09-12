using MediatR;
using Shared.Models.DTO;

namespace API.Requests;

public record SearchEmployeeRequest(string searchTerm) : IRequest<List<EmployeeDto>>;