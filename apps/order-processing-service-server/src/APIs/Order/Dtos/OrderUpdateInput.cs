namespace OrderProcessingService.APIs.Dtos;

public class OrderUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Customer { get; set; }

    public string? Id { get; set; }

    public DateTime? OrderDate { get; set; }

    public List<string>? OrderItems { get; set; }

    public List<string>? Payments { get; set; }

    public List<string>? Shipments { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
