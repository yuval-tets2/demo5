using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementService.Infrastructure.Models;

[Table("Warehouses")]
public class WarehouseDbModel
{
    [Range(-999999999, 999999999)]
    public int? Capacity { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Location { get; set; }

    public List<StockMovementDbModel>? StockMovements { get; set; } =
        new List<StockMovementDbModel>();

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
