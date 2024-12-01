using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementService.Infrastructure.Models;

[Table("StockMovements")]
public class StockMovementDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? InventoryId { get; set; }

    [ForeignKey(nameof(InventoryId))]
    public InventoryDbModel? Inventory { get; set; } = null;

    [Range(-999999999, 999999999)]
    public int? Quantity { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    public string? WarehouseId { get; set; }

    [ForeignKey(nameof(WarehouseId))]
    public WarehouseDbModel? Warehouse { get; set; } = null;
}
