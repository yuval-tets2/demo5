namespace InventoryManagementService.APIs.Dtos;

public class StockMovementCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public Inventory? Inventory { get; set; }

    public int? Quantity { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Warehouse? Warehouse { get; set; }
}
