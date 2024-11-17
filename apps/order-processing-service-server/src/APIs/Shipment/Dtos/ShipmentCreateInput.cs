namespace OrderProcessingService.APIs.Dtos;

public class ShipmentCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public Order? Order { get; set; }

    public DateTime? ShipmentDate { get; set; }

    public DateTime UpdatedAt { get; set; }
}
