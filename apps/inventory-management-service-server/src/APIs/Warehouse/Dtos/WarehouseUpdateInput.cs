namespace InventoryManagementService.APIs.Dtos;

public class WarehouseUpdateInput
{
    public int? Capacity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Location { get; set; }

    public List<string>? StockMovements { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
