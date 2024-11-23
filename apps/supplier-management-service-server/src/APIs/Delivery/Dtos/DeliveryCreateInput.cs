namespace SupplierManagementService.APIs.Dtos;

public class DeliveryCreateInput
{
    public DateTime CreatedAt { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public string? Id { get; set; }

    public PurchaseOrder? PurchaseOrder { get; set; }

    public DateTime UpdatedAt { get; set; }
}
