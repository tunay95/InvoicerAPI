using Invoicer.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoicer.DAL.Data;

public class InvoiceDbContext : DbContext
{
	public InvoiceDbContext(DbContextOptions options) : base(options)
	{ }

	public DbSet<User> Users => Set<User>();
	public DbSet<Invoice> Invoices => Set<Invoice>();
	public DbSet<InvoiceRow> InvoiceRows => Set<InvoiceRow>();
}
