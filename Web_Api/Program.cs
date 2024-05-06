using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy => policy.WithOrigins("http://localhost:3000")
                             .AllowAnyHeader()
                             .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                // Log the valid token or any claims you care about
                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                // Log the failure here
                Console.WriteLine("Authentication failed: " + context.Exception.Message);
                return Task.CompletedTask;
            }
        };
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = false,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKeyValidator = (key, token, parameters) =>
            {
                // Implement custom key validation logic here
                // You can check if the provided key matches your expected key
                return true; // Return true if the key is valid, false otherwise
            }

        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Bearer", policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication(); // Dodano za avtentikacijo z JWT žetoni

app.UseAuthorization();

app.MapControllers();

app.Run();