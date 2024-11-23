using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CustomerService.Core.Enums;

namespace CustomerService.Infrastructure.Models;

[Table("SupportTickets")]
public class SupportTicketDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public CustomerDbModel? Customer { get; set; } = null;

    [Key()]
    [Required()]
    public string Id { get; set; }

    public StatusEnum? Status { get; set; }

    [StringLength(1000)]
    public string? Subject { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
