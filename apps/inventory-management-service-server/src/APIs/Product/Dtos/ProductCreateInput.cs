namespace InventoryManagementService.APIs.Dtos;

public class ProductCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Description { get; set; }

    public string? Id { get; set; }

    public List<Inventory>? Inventories { get; set; }

    public string? Name { get; set; }

    public double? Price { get; set; }

    public DateTime UpdatedAt { get; set; }
}
