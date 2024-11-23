using CustomerService.Core.Enums;

namespace CustomerService.APIs.Dtos;

public class InteractionUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Customer { get; set; }

    public string? Details { get; set; }

    public string? Id { get; set; }

    public TypeFieldEnum? TypeField { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
