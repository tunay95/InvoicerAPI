namespace Invoicer.Business.DTOs.Base;

public interface IPaginationBase
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}
