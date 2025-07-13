using FluentValidation;
using LibraryMgmt.Books;
using LibraryMgmt.Core.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBooksServices();
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddProblemDetails();

builder.Services
    .AddExceptionHandler<BusinessExceptionHandler>()
    .AddExceptionHandler<UnhandledExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.AddBooksEndpoints();
app.UseExceptionHandler();
app.Run();

// Need for Integration testing via LibraryMgmt.Test.Integration
public partial class Program { }