using Invoicer.Core.Entities.Common;

namespace Invoicer.Core.Entities;

public class InvoiceRow: BaseEntity
{
    public string Service { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal Amount { get; set; }
    public decimal Sum { get; set; }


    public Guid InvoiceId { get; set; }
    public Invoice Invoice { get; set; }
}
