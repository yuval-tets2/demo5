using CustomerService.APIs.Dtos;
using CustomerService.Infrastructure.Models;

namespace CustomerService.APIs.Extensions;

public static class SupportTicketsExtensions
{
    public static SupportTicket ToDto(this SupportTicketDbModel model)
    {
        return new SupportTicket
        {
            CreatedAt = model.CreatedAt,
            Customer = model.CustomerId,
            Id = model.Id,
            Status = model.Status,
            Subject = model.Subject,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static SupportTicketDbModel ToModel(
        this SupportTicketUpdateInput updateDto,
        SupportTicketWhereUniqueInput uniqueId
    )
    {
        var supportTicket = new SupportTicketDbModel
        {
            Id = uniqueId.Id,
            Status = updateDto.Status,
            Subject = updateDto.Subject
        };

        if (updateDto.CreatedAt != null)
        {
            supportTicket.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Customer != null)
        {
            supportTicket.CustomerId = updateDto.Customer;
        }
        if (updateDto.UpdatedAt != null)
        {
            supportTicket.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return supportTicket;
    }
}
