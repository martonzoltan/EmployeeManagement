using System.Reflection;
using API.Context;
using API.Requests;
using API.Services;
using API.Services.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Mappers;
using Shared.Models.DTO;
using Shared.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddValidatorsFromAssemblyContaining<EmployeeDtoValidator>();

builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();

var app = builder.Build();
app.UseCors(b => b
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.MapGet("/getEmployees", async (IMediator mediator) =>
{
    var response = await mediator.Send(new GetEmployeesRequest());
    return Results.Ok(response);
});

app.MapPost("/addEmployee", async (IMediator mediator, EmployeeDto employee) =>
{
    var response = await mediator.Send(new AddEmployeeRequest(employee));
    return Results.Ok(response);
});

app.MapPut("/updateEmployee", async (IMediator mediator, EmployeeDto employee) =>
{
    var response = await mediator.Send(new UpdateEmployeeRequest(employee));
    return Results.Ok(response);
});

app.MapDelete("/deleteEmployee/{employeeId:guid}", async (IMediator mediator, Guid employeeId) =>
{
    await mediator.Send(new DeleteEmployeeRequest(employeeId));
    return Results.Ok();
});

app.MapGet("/searchEmployees/{searchTerm}", async (IMediator mediator, string searchTerm) =>
{
    var response = await mediator.Send(new SearchEmployeeRequest(searchTerm));
    return Results.Ok(response);
});

app.Run();