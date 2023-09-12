using API.Requests;
using API.Services.Interfaces;
using AutoMapper;
using MediatR;
using Shared.Models.DTO;
using Shared.Models.Entities;

namespace API.Handlers;

public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeRequest, EmployeeDto>
{
    private readonly IEmployeeRepository _repository;
    private readonly IMapper _mapper;

    public UpdateEmployeeHandler(IEmployeeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<EmployeeDto> Handle(UpdateEmployeeRequest request, CancellationToken cancellationToken)
    {
        var employeeEntity = _mapper.Map<Employee>(request.Employee);
        var addResponse = await _repository.UpdateEmployee(employeeEntity, cancellationToken);
        var employeeDto = _mapper.Map<EmployeeDto>(addResponse);
        return employeeDto;
    }
}