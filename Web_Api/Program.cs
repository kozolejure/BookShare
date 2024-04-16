var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy => policy.WithOrigins("http://localhost:3000") // Dovoli izvor React aplikacije
                             .AllowAnyHeader()
                             .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting(); // To je potrebno dodati

app.UseCors("AllowSpecificOrigin"); // To mora biti po UseRouting in pred UseAuthorization

app.UseAuthorization();

app.MapControllers();

app.Run();
