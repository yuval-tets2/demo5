namespace SupplierManagementService.APIs.Dtos;

public class SupplierUpdateInput
{
    public string? ContactInfo { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public List<string>? PurchaseOrders { get; set; }

    public List<string>? SupplyContracts { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
