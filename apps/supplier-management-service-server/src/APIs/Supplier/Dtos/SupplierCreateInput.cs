namespace SupplierManagementService.APIs.Dtos;

public class SupplierCreateInput
{
    public string? ContactInfo { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public List<PurchaseOrder>? PurchaseOrders { get; set; }

    public List<SupplyContract>? SupplyContracts { get; set; }

    public DateTime UpdatedAt { get; set; }
}
