namespace OrderProcessingService.APIs.Dtos;

public class Payment
{
    public double? Amount { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Id { get; set; }

    public string? Order { get; set; }

    public DateTime UpdatedAt { get; set; }
}
