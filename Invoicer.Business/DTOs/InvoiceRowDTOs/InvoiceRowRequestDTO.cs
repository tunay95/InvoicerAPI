using Invoicer.Business.DTOs.Base;

namespace Invoicer.Business.DTOs.InvoiceRowDTOs;

public class InvoiceRowRequestDTO : IPaginationBase, ISearchBase
{
	public string? Search { get; set; }
	public int? PageNumber { get; set; }
	public int? PageSize { get; set; }
	public decimal? MinSum { get; set; }
	public decimal? MaxSum { get; set; }

}
