namespace CustomerService.APIs.Dtos;

public class Feedback
{
    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Customer { get; set; }

    public string Id { get; set; }

    public DateTime UpdatedAt { get; set; }
}
