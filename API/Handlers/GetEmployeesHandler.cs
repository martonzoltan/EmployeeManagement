using API.Requests;
using API.Services.Interfaces;
using AutoMapper;
using MediatR;
using Shared.Models.DTO;

namespace API.Handlers;

public class GetEmployeesHandler : IRequestHandler<GetEmployeesRequest, List<EmployeeDto>>
{
    private readonly IEmployeeRepository _repository;
    private readonly IMapper _mapper;

    public GetEmployeesHandler(IEmployeeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<EmployeeDto>> Handle(GetEmployeesRequest request, CancellationToken cancellationToken)
    {
        var employees = await _repository.GetEmployees(cancellationToken);
        var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);
        return employeeDtos;
    }
}