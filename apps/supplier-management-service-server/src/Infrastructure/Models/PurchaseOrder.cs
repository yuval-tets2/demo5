using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplierManagementService.Infrastructure.Models;

[Table("PurchaseOrders")]
public class PurchaseOrderDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public List<DeliveryDbModel>? Deliveries { get; set; } = new List<DeliveryDbModel>();

    [Key()]
    [Required()]
    public string Id { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? SupplierId { get; set; }

    [ForeignKey(nameof(SupplierId))]
    public SupplierDbModel? Supplier { get; set; } = null;

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
