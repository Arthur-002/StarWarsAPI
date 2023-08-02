using DevTownCodeChallenge.Middleware;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ServiceName", Version = "1" });
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
{
    Name = "ApiKey",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.ApiKey,
    Description = "Authorization by ApiKey inside request's header",
    Scheme = "ApiKeyScheme"
});

var key = new OpenApiSecurityScheme()
{
    Reference = new OpenApiReference
    {
        Type = ReferenceType.SecurityScheme,
        Id = "ApiKey"
    },
    In = ParameterLocation.Header
};

var requirement = new OpenApiSecurityRequirement{{ key, new List<string>()}};

c.AddSecurityRequirement(requirement);
});var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
