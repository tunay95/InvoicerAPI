using Invoicer.Business.DTOs.Base;

namespace Invoicer.Business.DTOs.CustomerDTOs;

public record CustomerRequestDTO : ISearchBase, IPaginationBase
{
	public int? PageNumber { get; set; }
	public int? PageSize { get; set; }
	public string? Search { get; set; }
}
