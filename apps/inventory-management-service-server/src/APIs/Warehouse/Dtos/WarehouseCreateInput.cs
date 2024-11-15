namespace InventoryManagementService.APIs.Dtos;

public class WarehouseCreateInput
{
    public int? Capacity { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Location { get; set; }

    public List<StockMovement>? StockMovements { get; set; }

    public DateTime UpdatedAt { get; set; }
}
