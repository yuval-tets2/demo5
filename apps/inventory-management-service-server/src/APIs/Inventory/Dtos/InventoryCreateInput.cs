namespace InventoryManagementService.APIs.Dtos;

public class InventoryCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public Product? Product { get; set; }

    public int? Quantity { get; set; }

    public List<StockMovement>? StockMovements { get; set; }

    public DateTime UpdatedAt { get; set; }
}
