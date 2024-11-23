namespace CustomerService.APIs.Dtos;

public class CustomerCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    public List<Feedback>? Feedbacks { get; set; }

    public string? Id { get; set; }

    public List<Interaction>? Interactions { get; set; }

    public string? Name { get; set; }

    public List<SupportTicket>? SupportTickets { get; set; }

    public DateTime UpdatedAt { get; set; }
}
