using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementService.Infrastructure.Models;

[Table("Inventories")]
public class InventoryDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? ProductId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public ProductDbModel? Product { get; set; } = null;

    [Range(-999999999, 999999999)]
    public int? Quantity { get; set; }

    public List<StockMovementDbModel>? StockMovements { get; set; } =
        new List<StockMovementDbModel>();

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
