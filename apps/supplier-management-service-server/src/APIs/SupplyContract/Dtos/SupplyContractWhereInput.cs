namespace SupplierManagementService.APIs.Dtos;

public class SupplyContractWhereInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Details { get; set; }

    public string? Id { get; set; }

    public string? Supplier { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
