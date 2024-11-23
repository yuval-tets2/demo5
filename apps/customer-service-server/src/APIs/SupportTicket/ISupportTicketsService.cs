using CustomerService.APIs.Common;
using CustomerService.APIs.Dtos;

namespace CustomerService.APIs;

public interface ISupportTicketsService
{
    /// <summary>
    /// Create one SupportTicket
    /// </summary>
    public Task<SupportTicket> CreateSupportTicket(SupportTicketCreateInput supportticket);

    /// <summary>
    /// Delete one SupportTicket
    /// </summary>
    public Task DeleteSupportTicket(SupportTicketWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many SupportTickets
    /// </summary>
    public Task<List<SupportTicket>> SupportTickets(SupportTicketFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about SupportTicket records
    /// </summary>
    public Task<MetadataDto> SupportTicketsMeta(SupportTicketFindManyArgs findManyArgs);

    /// <summary>
    /// Get one SupportTicket
    /// </summary>
    public Task<SupportTicket> SupportTicket(SupportTicketWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one SupportTicket
    /// </summary>
    public Task UpdateSupportTicket(
        SupportTicketWhereUniqueInput uniqueId,
        SupportTicketUpdateInput updateDto
    );

    /// <summary>
    /// Get a Customer record for SupportTicket
    /// </summary>
    public Task<Customer> GetCustomer(SupportTicketWhereUniqueInput uniqueId);
}
