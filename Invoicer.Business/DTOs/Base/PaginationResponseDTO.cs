namespace Invoicer.Business.DTOs.Base;

public class PaginationResponseDTO<TEntity>
{
	public IEnumerable<TEntity> Entities { get; set; } = new List<TEntity>();
	public int TotalCount { get; set; }
	public int PageNumber { get; set; }
	public int PageSize { get; set; }
	public int TotalPages => Convert.ToInt32(Math.Ceiling(Convert.ToDouble(TotalCount) / PageSize));
}
