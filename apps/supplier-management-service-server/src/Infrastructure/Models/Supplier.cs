using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplierManagementService.Infrastructure.Models;

[Table("Suppliers")]
public class SupplierDbModel
{
    [StringLength(1000)]
    public string? ContactInfo { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    public List<PurchaseOrderDbModel>? PurchaseOrders { get; set; } =
        new List<PurchaseOrderDbModel>();

    public List<SupplyContractDbModel>? SupplyContracts { get; set; } =
        new List<SupplyContractDbModel>();

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
