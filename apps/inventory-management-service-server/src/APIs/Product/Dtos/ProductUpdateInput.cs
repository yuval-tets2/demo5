namespace InventoryManagementService.APIs.Dtos;

public class ProductUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Description { get; set; }

    public string? Id { get; set; }

    public List<string>? Inventories { get; set; }

    public string? Name { get; set; }

    public double? Price { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
