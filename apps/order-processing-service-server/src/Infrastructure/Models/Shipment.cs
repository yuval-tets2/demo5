using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderProcessingService.Infrastructure.Models;

[Table("Shipments")]
public class ShipmentDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    public OrderDbModel? Order { get; set; } = null;

    public DateTime? ShipmentDate { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
