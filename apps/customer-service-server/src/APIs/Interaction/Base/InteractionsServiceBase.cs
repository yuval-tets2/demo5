using CustomerService.APIs;
using CustomerService.APIs.Common;
using CustomerService.APIs.Dtos;
using CustomerService.APIs.Errors;
using CustomerService.APIs.Extensions;
using CustomerService.Infrastructure;
using CustomerService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.APIs;

public abstract class InteractionsServiceBase : IInteractionsService
{
    protected readonly CustomerServiceDbContext _context;

    public InteractionsServiceBase(CustomerServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Interaction
    /// </summary>
    public async Task<Interaction> CreateInteraction(InteractionCreateInput createDto)
    {
        var interaction = new InteractionDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Details = createDto.Details,
            TypeField = createDto.TypeField,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            interaction.Id = createDto.Id;
        }
        if (createDto.Customer != null)
        {
            interaction.Customer = await _context
                .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                .FirstOrDefaultAsync();
        }

        _context.Interactions.Add(interaction);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<InteractionDbModel>(interaction.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Interaction
    /// </summary>
    public async Task DeleteInteraction(InteractionWhereUniqueInput uniqueId)
    {
        var interaction = await _context.Interactions.FindAsync(uniqueId.Id);
        if (interaction == null)
        {
            throw new NotFoundException();
        }

        _context.Interactions.Remove(interaction);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Interactions
    /// </summary>
    public async Task<List<Interaction>> Interactions(InteractionFindManyArgs findManyArgs)
    {
        var interactions = await _context
            .Interactions.Include(x => x.Customer)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return interactions.ConvertAll(interaction => interaction.ToDto());
    }

    /// <summary>
    /// Meta data about Interaction records
    /// </summary>
    public async Task<MetadataDto> InteractionsMeta(InteractionFindManyArgs findManyArgs)
    {
        var count = await _context.Interactions.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Interaction
    /// </summary>
    public async Task<Interaction> Interaction(InteractionWhereUniqueInput uniqueId)
    {
        var interactions = await this.Interactions(
            new InteractionFindManyArgs { Where = new InteractionWhereInput { Id = uniqueId.Id } }
        );
        var interaction = interactions.FirstOrDefault();
        if (interaction == null)
        {
            throw new NotFoundException();
        }

        return interaction;
    }

    /// <summary>
    /// Update one Interaction
    /// </summary>
    public async Task UpdateInteraction(
        InteractionWhereUniqueInput uniqueId,
        InteractionUpdateInput updateDto
    )
    {
        var interaction = updateDto.ToModel(uniqueId);

        if (updateDto.Customer != null)
        {
            interaction.Customer = await _context
                .Customers.Where(customer => updateDto.Customer == customer.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(interaction).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Interactions.Any(e => e.Id == interaction.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a Customer record for Interaction
    /// </summary>
    public async Task<Customer> GetCustomer(InteractionWhereUniqueInput uniqueId)
    {
        var interaction = await _context
            .Interactions.Where(interaction => interaction.Id == uniqueId.Id)
            .Include(interaction => interaction.Customer)
            .FirstOrDefaultAsync();
        if (interaction == null)
        {
            throw new NotFoundException();
        }
        return interaction.Customer.ToDto();
    }
}
