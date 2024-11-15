namespace InventoryManagementService.APIs.Dtos;

public class StockMovementUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Inventory { get; set; }

    public int? Quantity { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Warehouse { get; set; }
}
