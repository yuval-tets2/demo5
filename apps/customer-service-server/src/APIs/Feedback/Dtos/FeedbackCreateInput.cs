namespace CustomerService.APIs.Dtos;

public class FeedbackCreateInput
{
    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public Customer? Customer { get; set; }

    public string? Id { get; set; }

    public DateTime UpdatedAt { get; set; }
}
