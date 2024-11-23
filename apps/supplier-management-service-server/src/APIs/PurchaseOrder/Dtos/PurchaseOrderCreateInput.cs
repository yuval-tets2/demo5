namespace SupplierManagementService.APIs.Dtos;

public class PurchaseOrderCreateInput
{
    public DateTime CreatedAt { get; set; }

    public List<Delivery>? Deliveries { get; set; }

    public string? Id { get; set; }

    public DateTime? OrderDate { get; set; }

    public Supplier? Supplier { get; set; }

    public DateTime UpdatedAt { get; set; }
}
