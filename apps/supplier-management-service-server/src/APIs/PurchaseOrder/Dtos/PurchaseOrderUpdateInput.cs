namespace SupplierManagementService.APIs.Dtos;

public class PurchaseOrderUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public List<string>? Deliveries { get; set; }

    public string? Id { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? Supplier { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
