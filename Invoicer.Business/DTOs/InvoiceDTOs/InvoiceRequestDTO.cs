using Invoicer.Business.DTOs.Base;

namespace Invoicer.Business.DTOs.InvoiceDTOs;

public class InvoiceRequestDTO : IPaginationBase
{
	public int? PageNumber { get; set; }
	public int? PageSize { get; set; }

	public DateTimeOffset? StartDate { get; set; }
	public DateTimeOffset? EndDate { get; set; }
}
