namespace OrderProcessingService.APIs.Dtos;

public class ShipmentWhereInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Order { get; set; }

    public DateTime? ShipmentDate { get; set; }

    public DateTime? UpdatedAt { get; set; }
}