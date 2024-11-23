using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplierManagementService.Infrastructure.Models;

[Table("Deliveries")]
public class DeliveryDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public DateTime? DeliveryDate { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? PurchaseOrderId { get; set; }

    [ForeignKey(nameof(PurchaseOrderId))]
    public PurchaseOrderDbModel? PurchaseOrder { get; set; } = null;

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
