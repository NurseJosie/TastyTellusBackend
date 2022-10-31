using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TastyTellusBackend.Data;

// tim corey https://www.youtube.com/watch?v=rhydGmLxfjQ

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TastyTellusBackendContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TastyTellusBackendContext") ?? throw new InvalidOperationException("Connection string 'TastyTellusBackendContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();
