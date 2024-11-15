namespace InventoryManagementService.APIs.Dtos;

public class InventoryUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Product { get; set; }

    public int? Quantity { get; set; }

    public List<string>? StockMovements { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
