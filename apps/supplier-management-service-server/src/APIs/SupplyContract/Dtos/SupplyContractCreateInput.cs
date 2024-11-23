namespace SupplierManagementService.APIs.Dtos;

public class SupplyContractCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Details { get; set; }

    public string? Id { get; set; }

    public Supplier? Supplier { get; set; }

    public DateTime UpdatedAt { get; set; }
}
