using CustomerService.APIs.Common;
using CustomerService.APIs.Dtos;

namespace CustomerService.APIs;

public interface ICustomersService
{
    /// <summary>
    /// Create one Customer
    /// </summary>
    public Task<Customer> CreateCustomer(CustomerCreateInput customer);

    /// <summary>
    /// Delete one Customer
    /// </summary>
    public Task DeleteCustomer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Customers
    /// </summary>
    public Task<List<Customer>> Customers(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    public Task<MetadataDto> CustomersMeta(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Customer
    /// </summary>
    public Task<Customer> Customer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Customer
    /// </summary>
    public Task UpdateCustomer(CustomerWhereUniqueInput uniqueId, CustomerUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Feedbacks records to Customer
    /// </summary>
    public Task ConnectFeedbacks(
        CustomerWhereUniqueInput uniqueId,
        FeedbackWhereUniqueInput[] feedbacksId
    );

    /// <summary>
    /// Disconnect multiple Feedbacks records from Customer
    /// </summary>
    public Task DisconnectFeedbacks(
        CustomerWhereUniqueInput uniqueId,
        FeedbackWhereUniqueInput[] feedbacksId
    );

    /// <summary>
    /// Find multiple Feedbacks records for Customer
    /// </summary>
    public Task<List<Feedback>> FindFeedbacks(
        CustomerWhereUniqueInput uniqueId,
        FeedbackFindManyArgs FeedbackFindManyArgs
    );

    /// <summary>
    /// Update multiple Feedbacks records for Customer
    /// </summary>
    public Task UpdateFeedbacks(
        CustomerWhereUniqueInput uniqueId,
        FeedbackWhereUniqueInput[] feedbacksId
    );

    /// <summary>
    /// Connect multiple Interactions records to Customer
    /// </summary>
    public Task ConnectInteractions(
        CustomerWhereUniqueInput uniqueId,
        InteractionWhereUniqueInput[] interactionsId
    );

    /// <summary>
    /// Disconnect multiple Interactions records from Customer
    /// </summary>
    public Task DisconnectInteractions(
        CustomerWhereUniqueInput uniqueId,
        InteractionWhereUniqueInput[] interactionsId
    );

    /// <summary>
    /// Find multiple Interactions records for Customer
    /// </summary>
    public Task<List<Interaction>> FindInteractions(
        CustomerWhereUniqueInput uniqueId,
        InteractionFindManyArgs InteractionFindManyArgs
    );

    /// <summary>
    /// Update multiple Interactions records for Customer
    /// </summary>
    public Task UpdateInteractions(
        CustomerWhereUniqueInput uniqueId,
        InteractionWhereUniqueInput[] interactionsId
    );

    /// <summary>
    /// Connect multiple SupportTickets records to Customer
    /// </summary>
    public Task ConnectSupportTickets(
        CustomerWhereUniqueInput uniqueId,
        SupportTicketWhereUniqueInput[] supportTicketsId
    );

    /// <summary>
    /// Disconnect multiple SupportTickets records from Customer
    /// </summary>
    public Task DisconnectSupportTickets(
        CustomerWhereUniqueInput uniqueId,
        SupportTicketWhereUniqueInput[] supportTicketsId
    );

    /// <summary>
    /// Find multiple SupportTickets records for Customer
    /// </summary>
    public Task<List<SupportTicket>> FindSupportTickets(
        CustomerWhereUniqueInput uniqueId,
        SupportTicketFindManyArgs SupportTicketFindManyArgs
    );

    /// <summary>
    /// Update multiple SupportTickets records for Customer
    /// </summary>
    public Task UpdateSupportTickets(
        CustomerWhereUniqueInput uniqueId,
        SupportTicketWhereUniqueInput[] supportTicketsId
    );
}
