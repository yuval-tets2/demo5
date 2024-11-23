using CustomerService.APIs.Common;
using CustomerService.APIs.Dtos;

namespace CustomerService.APIs;

public interface IInteractionsService
{
    /// <summary>
    /// Create one Interaction
    /// </summary>
    public Task<Interaction> CreateInteraction(InteractionCreateInput interaction);

    /// <summary>
    /// Delete one Interaction
    /// </summary>
    public Task DeleteInteraction(InteractionWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Interactions
    /// </summary>
    public Task<List<Interaction>> Interactions(InteractionFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Interaction records
    /// </summary>
    public Task<MetadataDto> InteractionsMeta(InteractionFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Interaction
    /// </summary>
    public Task<Interaction> Interaction(InteractionWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Interaction
    /// </summary>
    public Task UpdateInteraction(
        InteractionWhereUniqueInput uniqueId,
        InteractionUpdateInput updateDto
    );

    /// <summary>
    /// Get a Customer record for Interaction
    /// </summary>
    public Task<Customer> GetCustomer(InteractionWhereUniqueInput uniqueId);
}
