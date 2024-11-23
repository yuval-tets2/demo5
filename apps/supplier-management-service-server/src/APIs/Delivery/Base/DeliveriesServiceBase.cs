using Microsoft.EntityFrameworkCore;
using SupplierManagementService.APIs;
using SupplierManagementService.APIs.Common;
using SupplierManagementService.APIs.Dtos;
using SupplierManagementService.APIs.Errors;
using SupplierManagementService.APIs.Extensions;
using SupplierManagementService.Infrastructure;
using SupplierManagementService.Infrastructure.Models;

namespace SupplierManagementService.APIs;

public abstract class DeliveriesServiceBase : IDeliveriesService
{
    protected readonly SupplierManagementServiceDbContext _context;

    public DeliveriesServiceBase(SupplierManagementServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Delivery
    /// </summary>
    public async Task<Delivery> CreateDelivery(DeliveryCreateInput createDto)
    {
        var delivery = new DeliveryDbModel
        {
            CreatedAt = createDto.CreatedAt,
            DeliveryDate = createDto.DeliveryDate,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            delivery.Id = createDto.Id;
        }
        if (createDto.PurchaseOrder != null)
        {
            delivery.PurchaseOrder = await _context
                .PurchaseOrders.Where(purchaseOrder =>
                    createDto.PurchaseOrder.Id == purchaseOrder.Id
                )
                .FirstOrDefaultAsync();
        }

        _context.Deliveries.Add(delivery);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<DeliveryDbModel>(delivery.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Delivery
    /// </summary>
    public async Task DeleteDelivery(DeliveryWhereUniqueInput uniqueId)
    {
        var delivery = await _context.Deliveries.FindAsync(uniqueId.Id);
        if (delivery == null)
        {
            throw new NotFoundException();
        }

        _context.Deliveries.Remove(delivery);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Deliveries
    /// </summary>
    public async Task<List<Delivery>> Deliveries(DeliveryFindManyArgs findManyArgs)
    {
        var deliveries = await _context
            .Deliveries.Include(x => x.PurchaseOrder)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return deliveries.ConvertAll(delivery => delivery.ToDto());
    }

    /// <summary>
    /// Meta data about Delivery records
    /// </summary>
    public async Task<MetadataDto> DeliveriesMeta(DeliveryFindManyArgs findManyArgs)
    {
        var count = await _context.Deliveries.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Delivery
    /// </summary>
    public async Task<Delivery> Delivery(DeliveryWhereUniqueInput uniqueId)
    {
        var deliveries = await this.Deliveries(
            new DeliveryFindManyArgs { Where = new DeliveryWhereInput { Id = uniqueId.Id } }
        );
        var delivery = deliveries.FirstOrDefault();
        if (delivery == null)
        {
            throw new NotFoundException();
        }

        return delivery;
    }

    /// <summary>
    /// Update one Delivery
    /// </summary>
    public async Task UpdateDelivery(
        DeliveryWhereUniqueInput uniqueId,
        DeliveryUpdateInput updateDto
    )
    {
        var delivery = updateDto.ToModel(uniqueId);

        if (updateDto.PurchaseOrder != null)
        {
            delivery.PurchaseOrder = await _context
                .PurchaseOrders.Where(purchaseOrder => updateDto.PurchaseOrder == purchaseOrder.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(delivery).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Deliveries.Any(e => e.Id == delivery.Id))
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
    /// Get a PurchaseOrder record for Delivery
    /// </summary>
    public async Task<PurchaseOrder> GetPurchaseOrder(DeliveryWhereUniqueInput uniqueId)
    {
        var delivery = await _context
            .Deliveries.Where(delivery => delivery.Id == uniqueId.Id)
            .Include(delivery => delivery.PurchaseOrder)
            .FirstOrDefaultAsync();
        if (delivery == null)
        {
            throw new NotFoundException();
        }
        return delivery.PurchaseOrder.ToDto();
    }
}
