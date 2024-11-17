using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderProcessingService.Infrastructure.Models;

[Table("Orders")]
public class OrderDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [StringLength(1000)]
    public string? Customer { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public DateTime? OrderDate { get; set; }

    public List<OrderItemDbModel>? OrderItems { get; set; } = new List<OrderItemDbModel>();

    public List<PaymentDbModel>? Payments { get; set; } = new List<PaymentDbModel>();

    public List<ShipmentDbModel>? Shipments { get; set; } = new List<ShipmentDbModel>();

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
