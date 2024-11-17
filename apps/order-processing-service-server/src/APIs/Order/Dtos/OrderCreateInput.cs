namespace OrderProcessingService.APIs.Dtos;

public class OrderCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Customer { get; set; }

    public string? Id { get; set; }

    public DateTime? OrderDate { get; set; }

    public List<OrderItem>? OrderItems { get; set; }

    public List<Payment>? Payments { get; set; }

    public List<Shipment>? Shipments { get; set; }

    public DateTime UpdatedAt { get; set; }
}
