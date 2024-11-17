namespace OrderProcessingService.APIs.Dtos;

public class OrderItemWhereInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Order { get; set; }

    public string? Product { get; set; }

    public int? Quantity { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
