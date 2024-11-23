using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplierManagementService.Infrastructure.Models;

[Table("SupplyContracts")]
public class SupplyContractDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [StringLength(1000)]
    public string? Details { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? SupplierId { get; set; }

    [ForeignKey(nameof(SupplierId))]
    public SupplierDbModel? Supplier { get; set; } = null;

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
