using Invoicer.Core.Entities;
using Invoicer.DAL.Data;
using Invoicer.DAL.Repositories.Intrfaces;

namespace Invoicer.DAL.Repositories.Implementations;

public class InvoiceRowRepository : Repository<InvoiceRow>, IInvoiceRowRepository
{
	public InvoiceRowRepository(InvoiceDbContext dbContext) : base(dbContext)
	{ }
}
