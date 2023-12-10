using Coreplus.Sample.Api.Endpoints.Practitioner;
using Coreplus.Sample.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<PractitionerService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

var practitionerEndpoints = app.MapGroup("/practitioners");
practitionerEndpoints.MapPractitionerEndpoints();

app.Run();
