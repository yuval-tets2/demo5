using CustomerService.APIs;
using CustomerService.APIs.Common;
using CustomerService.APIs.Dtos;
using CustomerService.APIs.Errors;
using CustomerService.APIs.Extensions;
using CustomerService.Infrastructure;
using CustomerService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.APIs;

public abstract class CustomersServiceBase : ICustomersService
{
    protected readonly CustomerServiceDbContext _context;

    public CustomersServiceBase(CustomerServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Customer
    /// </summary>
    public async Task<Customer> CreateCustomer(CustomerCreateInput createDto)
    {
        var customer = new CustomerDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Email = createDto.Email,
            Name = createDto.Name,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            customer.Id = createDto.Id;
        }
        if (createDto.Feedbacks != null)
        {
            customer.Feedbacks = await _context
                .Feedbacks.Where(feedback =>
                    createDto.Feedbacks.Select(t => t.Id).Contains(feedback.Id)
                )
                .ToListAsync();
        }

        if (createDto.Interactions != null)
        {
            customer.Interactions = await _context
                .Interactions.Where(interaction =>
                    createDto.Interactions.Select(t => t.Id).Contains(interaction.Id)
                )
                .ToListAsync();
        }

        if (createDto.SupportTickets != null)
        {
            customer.SupportTickets = await _context
                .SupportTickets.Where(supportTicket =>
                    createDto.SupportTickets.Select(t => t.Id).Contains(supportTicket.Id)
                )
                .ToListAsync();
        }

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<CustomerDbModel>(customer.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Customer
    /// </summary>
    public async Task DeleteCustomer(CustomerWhereUniqueInput uniqueId)
    {
        var customer = await _context.Customers.FindAsync(uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Customers
    /// </summary>
    public async Task<List<Customer>> Customers(CustomerFindManyArgs findManyArgs)
    {
        var customers = await _context
            .Customers.Include(x => x.Interactions)
            .Include(x => x.SupportTickets)
            .Include(x => x.Feedbacks)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return customers.ConvertAll(customer => customer.ToDto());
    }

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    public async Task<MetadataDto> CustomersMeta(CustomerFindManyArgs findManyArgs)
    {
        var count = await _context.Customers.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Customer
    /// </summary>
    public async Task<Customer> Customer(CustomerWhereUniqueInput uniqueId)
    {
        var customers = await this.Customers(
            new CustomerFindManyArgs { Where = new CustomerWhereInput { Id = uniqueId.Id } }
        );
        var customer = customers.FirstOrDefault();
        if (customer == null)
        {
            throw new NotFoundException();
        }

        return customer;
    }

    /// <summary>
    /// Update one Customer
    /// </summary>
    public async Task UpdateCustomer(
        CustomerWhereUniqueInput uniqueId,
        CustomerUpdateInput updateDto
    )
    {
        var customer = updateDto.ToModel(uniqueId);

        if (updateDto.Feedbacks != null)
        {
            customer.Feedbacks = await _context
                .Feedbacks.Where(feedback =>
                    updateDto.Feedbacks.Select(t => t).Contains(feedback.Id)
                )
                .ToListAsync();
        }

        if (updateDto.Interactions != null)
        {
            customer.Interactions = await _context
                .Interactions.Where(interaction =>
                    updateDto.Interactions.Select(t => t).Contains(interaction.Id)
                )
                .ToListAsync();
        }

        if (updateDto.SupportTickets != null)
        {
            customer.SupportTickets = await _context
                .SupportTickets.Where(supportTicket =>
                    updateDto.SupportTickets.Select(t => t).Contains(supportTicket.Id)
                )
                .ToListAsync();
        }

        _context.Entry(customer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Customers.Any(e => e.Id == customer.Id))
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
    /// Connect multiple Feedbacks records to Customer
    /// </summary>
    public async Task ConnectFeedbacks(
        CustomerWhereUniqueInput uniqueId,
        FeedbackWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Feedbacks)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Feedbacks.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Feedbacks);

        foreach (var child in childrenToConnect)
        {
            parent.Feedbacks.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Feedbacks records from Customer
    /// </summary>
    public async Task DisconnectFeedbacks(
        CustomerWhereUniqueInput uniqueId,
        FeedbackWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Feedbacks)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Feedbacks.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Feedbacks?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Feedbacks records for Customer
    /// </summary>
    public async Task<List<Feedback>> FindFeedbacks(
        CustomerWhereUniqueInput uniqueId,
        FeedbackFindManyArgs customerFindManyArgs
    )
    {
        var feedbacks = await _context
            .Feedbacks.Where(m => m.CustomerId == uniqueId.Id)
            .ApplyWhere(customerFindManyArgs.Where)
            .ApplySkip(customerFindManyArgs.Skip)
            .ApplyTake(customerFindManyArgs.Take)
            .ApplyOrderBy(customerFindManyArgs.SortBy)
            .ToListAsync();

        return feedbacks.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Feedbacks records for Customer
    /// </summary>
    public async Task UpdateFeedbacks(
        CustomerWhereUniqueInput uniqueId,
        FeedbackWhereUniqueInput[] childrenIds
    )
    {
        var customer = await _context
            .Customers.Include(t => t.Feedbacks)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Feedbacks.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        customer.Feedbacks = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple Interactions records to Customer
    /// </summary>
    public async Task ConnectInteractions(
        CustomerWhereUniqueInput uniqueId,
        InteractionWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Interactions)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Interactions.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Interactions);

        foreach (var child in childrenToConnect)
        {
            parent.Interactions.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Interactions records from Customer
    /// </summary>
    public async Task DisconnectInteractions(
        CustomerWhereUniqueInput uniqueId,
        InteractionWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Interactions)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Interactions.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Interactions?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Interactions records for Customer
    /// </summary>
    public async Task<List<Interaction>> FindInteractions(
        CustomerWhereUniqueInput uniqueId,
        InteractionFindManyArgs customerFindManyArgs
    )
    {
        var interactions = await _context
            .Interactions.Where(m => m.CustomerId == uniqueId.Id)
            .ApplyWhere(customerFindManyArgs.Where)
            .ApplySkip(customerFindManyArgs.Skip)
            .ApplyTake(customerFindManyArgs.Take)
            .ApplyOrderBy(customerFindManyArgs.SortBy)
            .ToListAsync();

        return interactions.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Interactions records for Customer
    /// </summary>
    public async Task UpdateInteractions(
        CustomerWhereUniqueInput uniqueId,
        InteractionWhereUniqueInput[] childrenIds
    )
    {
        var customer = await _context
            .Customers.Include(t => t.Interactions)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Interactions.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        customer.Interactions = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple SupportTickets records to Customer
    /// </summary>
    public async Task ConnectSupportTickets(
        CustomerWhereUniqueInput uniqueId,
        SupportTicketWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.SupportTickets)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .SupportTickets.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.SupportTickets);

        foreach (var child in childrenToConnect)
        {
            parent.SupportTickets.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple SupportTickets records from Customer
    /// </summary>
    public async Task DisconnectSupportTickets(
        CustomerWhereUniqueInput uniqueId,
        SupportTicketWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.SupportTickets)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .SupportTickets.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.SupportTickets?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple SupportTickets records for Customer
    /// </summary>
    public async Task<List<SupportTicket>> FindSupportTickets(
        CustomerWhereUniqueInput uniqueId,
        SupportTicketFindManyArgs customerFindManyArgs
    )
    {
        var supportTickets = await _context
            .SupportTickets.Where(m => m.CustomerId == uniqueId.Id)
            .ApplyWhere(customerFindManyArgs.Where)
            .ApplySkip(customerFindManyArgs.Skip)
            .ApplyTake(customerFindManyArgs.Take)
            .ApplyOrderBy(customerFindManyArgs.SortBy)
            .ToListAsync();

        return supportTickets.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple SupportTickets records for Customer
    /// </summary>
    public async Task UpdateSupportTickets(
        CustomerWhereUniqueInput uniqueId,
        SupportTicketWhereUniqueInput[] childrenIds
    )
    {
        var customer = await _context
            .Customers.Include(t => t.SupportTickets)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .SupportTickets.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        customer.SupportTickets = children;
        await _context.SaveChangesAsync();
    }
}
