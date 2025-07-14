using BackendCalcPropApp.Extensions;
using BackendCalcPropApp.Features.Intersection;
using BackendCalcPropApp.Features.Union;
using BackendCalcPropApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();              // Add this line

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Your frontend URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddScoped<IIntersectionCalculator, IntersectionCalculator>();
builder.Services.AddScoped<IUnionCalculator, UnionCalculator>();
builder.Services.AddSingleton<ICalculationLogger, CalculationLogger>();  // Add this line

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
    app.UseSwagger();                          // Now this will work
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
