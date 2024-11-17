using Microsoft.EntityFrameworkCore;
using OrderProcessingService.APIs;
using OrderProcessingService.APIs.Common;
using OrderProcessingService.APIs.Dtos;
using OrderProcessingService.APIs.Errors;
using OrderProcessingService.APIs.Extensions;
using OrderProcessingService.Infrastructure;
using OrderProcessingService.Infrastructure.Models;

namespace OrderProcessingService.APIs;

public abstract class ShipmentsServiceBase : IShipmentsService
{
    protected readonly OrderProcessingServiceDbContext _context;

    public ShipmentsServiceBase(OrderProcessingServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Shipment
    /// </summary>
    public async Task<Shipment> CreateShipment(ShipmentCreateInput createDto)
    {
        var shipment = new ShipmentDbModel
        {
            CreatedAt = createDto.CreatedAt,
            ShipmentDate = createDto.ShipmentDate,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            shipment.Id = createDto.Id;
        }
        if (createDto.Order != null)
        {
            shipment.Order = await _context
                .Orders.Where(order => createDto.Order.Id == order.Id)
                .FirstOrDefaultAsync();
        }

        _context.Shipments.Add(shipment);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ShipmentDbModel>(shipment.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Shipment
    /// </summary>
    public async Task DeleteShipment(ShipmentWhereUniqueInput uniqueId)
    {
        var shipment = await _context.Shipments.FindAsync(uniqueId.Id);
        if (shipment == null)
        {
            throw new NotFoundException();
        }

        _context.Shipments.Remove(shipment);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Shipments
    /// </summary>
    public async Task<List<Shipment>> Shipments(ShipmentFindManyArgs findManyArgs)
    {
        var shipments = await _context
            .Shipments.Include(x => x.Order)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return shipments.ConvertAll(shipment => shipment.ToDto());
    }

    /// <summary>
    /// Meta data about Shipment records
    /// </summary>
    public async Task<MetadataDto> ShipmentsMeta(ShipmentFindManyArgs findManyArgs)
    {
        var count = await _context.Shipments.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Shipment
    /// </summary>
    public async Task<Shipment> Shipment(ShipmentWhereUniqueInput uniqueId)
    {
        var shipments = await this.Shipments(
            new ShipmentFindManyArgs { Where = new ShipmentWhereInput { Id = uniqueId.Id } }
        );
        var shipment = shipments.FirstOrDefault();
        if (shipment == null)
        {
            throw new NotFoundException();
        }

        return shipment;
    }

    /// <summary>
    /// Update one Shipment
    /// </summary>
    public async Task UpdateShipment(
        ShipmentWhereUniqueInput uniqueId,
        ShipmentUpdateInput updateDto
    )
    {
        var shipment = updateDto.ToModel(uniqueId);

        if (updateDto.Order != null)
        {
            shipment.Order = await _context
                .Orders.Where(order => updateDto.Order == order.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(shipment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Shipments.Any(e => e.Id == shipment.Id))
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
    /// Get a Order record for Shipment
    /// </summary>
    public async Task<Order> GetOrder(ShipmentWhereUniqueInput uniqueId)
    {
        var shipment = await _context
            .Shipments.Where(shipment => shipment.Id == uniqueId.Id)
            .Include(shipment => shipment.Order)
            .FirstOrDefaultAsync();
        if (shipment == null)
        {
            throw new NotFoundException();
        }
        return shipment.Order.ToDto();
    }
}
