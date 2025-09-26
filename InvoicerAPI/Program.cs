using Invoicer.Business.MappingProfiles;
using Invoicer.Business.Services.Implementations;
using Invoicer.Business.Services.Interfaces;
using Invoicer.Core.MappingProfiles;
using Invoicer.DAL.Data;
using Invoicer.DAL.Repositories.Implementations;
using Invoicer.DAL.Repositories.Interfaces;
using Invoicer.DAL.Repositories.Intrfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllers()
	.AddJsonOptions(options =>					// ENUM-ın json kimi cixmasi ucun
{
	options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
}); ;

builder.Services.AddDbContext<InvoiceDbContext>(opt =>
{
	opt.UseSqlServer(builder.Configuration.GetConnectionString("InvDB"));
});


builder.Services.AddAutoMapper(
	cfg =>
{
	cfg.AddProfile<UserMP>();
	cfg.AddProfile<InvoiceMP>();
}, typeof(UserMP));


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceRowRepository, InvoiceRowRepository>();
builder.Services.AddScoped<IInvoiceRowService, InvoiceRowService>();


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
