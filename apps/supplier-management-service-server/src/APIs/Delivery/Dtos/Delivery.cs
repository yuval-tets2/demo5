namespace SupplierManagementService.APIs.Dtos;

public class Delivery
{
    public DateTime CreatedAt { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public string Id { get; set; }

    public string? PurchaseOrder { get; set; }

    public DateTime UpdatedAt { get; set; }
}
