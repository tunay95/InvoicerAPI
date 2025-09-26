using Invoicer.Business.DTOs.Base;

namespace Invoicer.Business.DTOs.UserDTOs;

public class UserRequestDTO : IPaginationBase, ISearchBase
{
	public DateTimeOffset? StartDate { get; set; }
	public DateTimeOffset? EndDate { get; set; }
	public string? Search { get; set; }
	public int? PageNumber { get; set; }
	public int? PageSize { get; set; }
}
