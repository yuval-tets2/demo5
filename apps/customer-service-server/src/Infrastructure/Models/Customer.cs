using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerService.Infrastructure.Models;

[Table("Customers")]
public class CustomerDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    public List<FeedbackDbModel>? Feedbacks { get; set; } = new List<FeedbackDbModel>();

    [Key()]
    [Required()]
    public string Id { get; set; }

    public List<InteractionDbModel>? Interactions { get; set; } = new List<InteractionDbModel>();

    [StringLength(1000)]
    public string? Name { get; set; }

    public List<SupportTicketDbModel>? SupportTickets { get; set; } =
        new List<SupportTicketDbModel>();

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
