using API.Requests;
using API.Services.Interfaces;
using AutoMapper;
using MediatR;
using Shared.Models.DTO;

namespace API.Handlers;

public class SearchEmployeeHandler : IRequestHandler<SearchEmployeeRequest, List<EmployeeDto>>
{
    private readonly IEmployeeRepository _repository;
    private readonly IMapper _mapper;

    public SearchEmployeeHandler(IEmployeeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<EmployeeDto>> Handle(SearchEmployeeRequest request, CancellationToken cancellationToken)
    {
        var employees = await _repository.SearchEmployees(request.searchTerm, cancellationToken);
        var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);
        return employeeDtos;
    }
}