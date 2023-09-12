using System.Net.Http.Json;
using AutoMapper;
using Shared.Models.DTO;
using Shared.Models.ViewModels;
using UI.Services.Interfaces;

namespace UI.Services;

public class EmployeeService : IEmployeeService
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;

    public EmployeeService(HttpClient httpClient, IMapper mapper)
    {
        _httpClient = httpClient;
        _mapper = mapper;
    }

    public async Task<List<EmployeeViewModel>> GetAllEmployees()
    {
        var response = await _httpClient.GetAsync("/getEmployees");
        if (response.IsSuccessStatusCode)
        {
            var employeeDtos = await response.Content.ReadFromJsonAsync<List<EmployeeDto>>();
            return _mapper.Map<List<EmployeeViewModel>>(employeeDtos)!;
        }

        throw new HttpRequestException($"Error: {response.StatusCode}");
    }

    public async Task<EmployeeViewModel> AddEmployee(EmployeeViewModel employee)
    {
        var employeeCreateDto = _mapper.Map<EmployeeDto>(employee);
        var content = JsonContent.Create(employeeCreateDto);
        var response = await _httpClient.PostAsync("/addEmployee", content);
        if (response.IsSuccessStatusCode)
        {
            var employeeDto = await response.Content.ReadFromJsonAsync<EmployeeDto>();
            return _mapper.Map<EmployeeViewModel>(employeeDto)!;
        }

        throw new HttpRequestException($"Error: {response.ReasonPhrase}");
    }

    public async Task<EmployeeViewModel> UpdateEmployee(EmployeeViewModel employee)
    {
        var employeeCreateDto = _mapper.Map<EmployeeDto>(employee);
        var content = JsonContent.Create(employeeCreateDto);
        var response = await _httpClient.PutAsync("/updateEmployee", content);
        if (response.IsSuccessStatusCode)
        {
            var employeeDto = await response.Content.ReadFromJsonAsync<EmployeeDto>();
            return _mapper.Map<EmployeeViewModel>(employeeDto)!;
        }

        throw new HttpRequestException($"Error: {response.ReasonPhrase}");
    }

    public async Task<bool> DeleteEmployee(Guid employeeId)
    {
        var response = await _httpClient.DeleteAsync($"/deleteEmployee/{employeeId}");
        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        throw new HttpRequestException($"Error: {response.ReasonPhrase}");
    }

    public async Task<List<EmployeeViewModel>> SearchEmployees(string searchTerm)
    {
        var response = await _httpClient.GetAsync($"/searchEmployees/{searchTerm}");
        if (response.IsSuccessStatusCode)
        {
            var employeeDtos = await response.Content.ReadFromJsonAsync<List<EmployeeDto>>();
            return _mapper.Map<List<EmployeeViewModel>>(employeeDtos)!;
        }

        throw new HttpRequestException($"Error: {response.StatusCode}");
    }
}