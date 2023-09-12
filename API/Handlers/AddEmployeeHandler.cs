using API.Requests;
using API.Services.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;
using Shared.Models.DTO;
using Shared.Models.Entities;

namespace API.Handlers;

public class AddEmployeeHandler : IRequestHandler<AddEmployeeRequest, EmployeeDto>
{
    private readonly IEmployeeRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<EmployeeDto> _validator;

    public AddEmployeeHandler(IEmployeeRepository repository, IMapper mapper, IValidator<EmployeeDto> validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<EmployeeDto> Handle(AddEmployeeRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Employee, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            throw new ValidationException(string.Join("\n", errors));
        }

        var employeeEntity = _mapper.Map<Employee>(request.Employee);
        employeeEntity.Id = Guid.NewGuid();
        employeeEntity.CreatedAt = DateTime.Now;
        var addResponse = await _repository.CreateEmployee(employeeEntity, cancellationToken);
        var employeeDto = _mapper.Map<EmployeeDto>(addResponse);
        return employeeDto;
    }
}