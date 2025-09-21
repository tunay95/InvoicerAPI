using AutoMapper;
using Invoicer.Business.Services.Implementations;
using Invoicer.Business.Services.Interfaces;
using Invoicer.Core.MappingProfiles;
using Invoicer.DAL.Data;
using Invoicer.DAL.Repositories.Implementations;
using Invoicer.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<InvoiceDbContext>(opt =>
{
	opt.UseSqlServer(builder.Configuration.GetConnectionString("InvDB"));
});


builder.Services.AddAutoMapper(cfg =>
{
	cfg.AddProfile<UserMP>();
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
