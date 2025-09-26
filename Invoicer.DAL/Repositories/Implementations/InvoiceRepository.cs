using Invoicer.Core.Entities;
using Invoicer.DAL.Data;
using Invoicer.DAL.Repositories.Intrfaces;

namespace Invoicer.DAL.Repositories.Implementations;

public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
{
	public InvoiceRepository(InvoiceDbContext dbContext) : base(dbContext)
	{ }
}
