using CustomerService.Core.Enums;

namespace CustomerService.APIs.Dtos;

public class SupportTicketWhereInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Customer { get; set; }

    public string? Id { get; set; }

    public StatusEnum? Status { get; set; }

    public string? Subject { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
