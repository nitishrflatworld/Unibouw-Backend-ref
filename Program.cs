using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Unibouw.Services;
using Unibouw.Mapping;

var builder = WebApplication.CreateBuilder(args);

// -------------------------
// Add services to the container
// -------------------------
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddSingleton<IWorkItemService, WorkItemService>();
builder.Services.AddSingleton<ISubContractorService, SubContractorService>();


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
string[] allowedOrigins = builder.Environment.IsDevelopment()
    ? new[] { "http://localhost:4200" }              // Local Angular
    : builder.Environment.IsEnvironment("QA")
        ? new[] { "https://qa-portal.unibouw.com" } // QA
        : builder.Environment.IsEnvironment("UAT")
            ? new[] { "https://uat-portal.unibouw.com" } // UAT
            : builder.Environment.IsProduction()
                ? new[] { "https://portal.unibouw.com" } // Production
                : Array.Empty<string>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// -------------------------
// Build the app
// -------------------------
var app = builder.Build();

// Enable Swagger only in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS redirection
app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowSpecificOrigins");

// Authorization (if any)
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Run the app
app.Run();
