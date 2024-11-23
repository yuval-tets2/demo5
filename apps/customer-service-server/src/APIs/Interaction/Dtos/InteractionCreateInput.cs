using CustomerService.Core.Enums;

namespace CustomerService.APIs.Dtos;

public class InteractionCreateInput
{
    public DateTime CreatedAt { get; set; }

    public Customer? Customer { get; set; }

    public string? Details { get; set; }

    public string? Id { get; set; }

    public TypeFieldEnum? TypeField { get; set; }

    public DateTime UpdatedAt { get; set; }
}
