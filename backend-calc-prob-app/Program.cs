using BackendCalcPropApp.Extensions;
using BackendCalcPropApp.Features.Intersection;
using BackendCalcPropApp.Features.Union;
using BackendCalcPropApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("*")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddScoped<IIntersectionCalculator, IntersectionCalculator>();
builder.Services.AddScoped<IUnionCalculator, UnionCalculator>();
builder.Services.AddSingleton<ICalculationLogger, CalculationLogger>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

app.MapCalculatorEndpoints();

app.Run();

/// <summary>
/// Partial Program class for integration testing.
/// </summary>
public partial class Program
{
}
