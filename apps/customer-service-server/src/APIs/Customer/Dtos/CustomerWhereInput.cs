namespace CustomerService.APIs.Dtos;

public class CustomerWhereInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Email { get; set; }

    public List<string>? Feedbacks { get; set; }

    public string? Id { get; set; }

    public List<string>? Interactions { get; set; }

    public string? Name { get; set; }

    public List<string>? SupportTickets { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
