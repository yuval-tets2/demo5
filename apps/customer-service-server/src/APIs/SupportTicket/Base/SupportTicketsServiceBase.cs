using CustomerService.APIs;
using CustomerService.APIs.Common;
using CustomerService.APIs.Dtos;
using CustomerService.APIs.Errors;
using CustomerService.APIs.Extensions;
using CustomerService.Infrastructure;
using CustomerService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.APIs;

public abstract class SupportTicketsServiceBase : ISupportTicketsService
{
    protected readonly CustomerServiceDbContext _context;

    public SupportTicketsServiceBase(CustomerServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one SupportTicket
    /// </summary>
    public async Task<SupportTicket> CreateSupportTicket(SupportTicketCreateInput createDto)
    {
        var supportTicket = new SupportTicketDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Status = createDto.Status,
            Subject = createDto.Subject,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            supportTicket.Id = createDto.Id;
        }
        if (createDto.Customer != null)
        {
            supportTicket.Customer = await _context
                .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                .FirstOrDefaultAsync();
        }

        _context.SupportTickets.Add(supportTicket);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<SupportTicketDbModel>(supportTicket.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one SupportTicket
    /// </summary>
    public async Task DeleteSupportTicket(SupportTicketWhereUniqueInput uniqueId)
    {
        var supportTicket = await _context.SupportTickets.FindAsync(uniqueId.Id);
        if (supportTicket == null)
        {
            throw new NotFoundException();
        }

        _context.SupportTickets.Remove(supportTicket);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many SupportTickets
    /// </summary>
    public async Task<List<SupportTicket>> SupportTickets(SupportTicketFindManyArgs findManyArgs)
    {
        var supportTickets = await _context
            .SupportTickets.Include(x => x.Customer)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return supportTickets.ConvertAll(supportTicket => supportTicket.ToDto());
    }

    /// <summary>
    /// Meta data about SupportTicket records
    /// </summary>
    public async Task<MetadataDto> SupportTicketsMeta(SupportTicketFindManyArgs findManyArgs)
    {
        var count = await _context.SupportTickets.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one SupportTicket
    /// </summary>
    public async Task<SupportTicket> SupportTicket(SupportTicketWhereUniqueInput uniqueId)
    {
        var supportTickets = await this.SupportTickets(
            new SupportTicketFindManyArgs
            {
                Where = new SupportTicketWhereInput { Id = uniqueId.Id }
            }
        );
        var supportTicket = supportTickets.FirstOrDefault();
        if (supportTicket == null)
        {
            throw new NotFoundException();
        }

        return supportTicket;
    }

    /// <summary>
    /// Update one SupportTicket
    /// </summary>
    public async Task UpdateSupportTicket(
        SupportTicketWhereUniqueInput uniqueId,
        SupportTicketUpdateInput updateDto
    )
    {
        var supportTicket = updateDto.ToModel(uniqueId);

        if (updateDto.Customer != null)
        {
            supportTicket.Customer = await _context
                .Customers.Where(customer => updateDto.Customer == customer.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(supportTicket).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.SupportTickets.Any(e => e.Id == supportTicket.Id))
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
    /// Get a Customer record for SupportTicket
    /// </summary>
    public async Task<Customer> GetCustomer(SupportTicketWhereUniqueInput uniqueId)
    {
        var supportTicket = await _context
            .SupportTickets.Where(supportTicket => supportTicket.Id == uniqueId.Id)
            .Include(supportTicket => supportTicket.Customer)
            .FirstOrDefaultAsync();
        if (supportTicket == null)
        {
            throw new NotFoundException();
        }
        return supportTicket.Customer.ToDto();
    }
}
