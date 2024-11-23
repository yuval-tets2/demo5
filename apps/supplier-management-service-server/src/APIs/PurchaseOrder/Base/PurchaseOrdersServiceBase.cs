using Microsoft.EntityFrameworkCore;
using SupplierManagementService.APIs;
using SupplierManagementService.APIs.Common;
using SupplierManagementService.APIs.Dtos;
using SupplierManagementService.APIs.Errors;
using SupplierManagementService.APIs.Extensions;
using SupplierManagementService.Infrastructure;
using SupplierManagementService.Infrastructure.Models;

namespace SupplierManagementService.APIs;

public abstract class PurchaseOrdersServiceBase : IPurchaseOrdersService
{
    protected readonly SupplierManagementServiceDbContext _context;

    public PurchaseOrdersServiceBase(SupplierManagementServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one PurchaseOrder
    /// </summary>
    public async Task<PurchaseOrder> CreatePurchaseOrder(PurchaseOrderCreateInput createDto)
    {
        var purchaseOrder = new PurchaseOrderDbModel
        {
            CreatedAt = createDto.CreatedAt,
            OrderDate = createDto.OrderDate,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            purchaseOrder.Id = createDto.Id;
        }
        if (createDto.Deliveries != null)
        {
            purchaseOrder.Deliveries = await _context
                .Deliveries.Where(delivery =>
                    createDto.Deliveries.Select(t => t.Id).Contains(delivery.Id)
                )
                .ToListAsync();
        }

        if (createDto.Supplier != null)
        {
            purchaseOrder.Supplier = await _context
                .Suppliers.Where(supplier => createDto.Supplier.Id == supplier.Id)
                .FirstOrDefaultAsync();
        }

        _context.PurchaseOrders.Add(purchaseOrder);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<PurchaseOrderDbModel>(purchaseOrder.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one PurchaseOrder
    /// </summary>
    public async Task DeletePurchaseOrder(PurchaseOrderWhereUniqueInput uniqueId)
    {
        var purchaseOrder = await _context.PurchaseOrders.FindAsync(uniqueId.Id);
        if (purchaseOrder == null)
        {
            throw new NotFoundException();
        }

        _context.PurchaseOrders.Remove(purchaseOrder);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many PurchaseOrders
    /// </summary>
    public async Task<List<PurchaseOrder>> PurchaseOrders(PurchaseOrderFindManyArgs findManyArgs)
    {
        var purchaseOrders = await _context
            .PurchaseOrders.Include(x => x.Supplier)
            .Include(x => x.Deliveries)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return purchaseOrders.ConvertAll(purchaseOrder => purchaseOrder.ToDto());
    }

    /// <summary>
    /// Meta data about PurchaseOrder records
    /// </summary>
    public async Task<MetadataDto> PurchaseOrdersMeta(PurchaseOrderFindManyArgs findManyArgs)
    {
        var count = await _context.PurchaseOrders.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one PurchaseOrder
    /// </summary>
    public async Task<PurchaseOrder> PurchaseOrder(PurchaseOrderWhereUniqueInput uniqueId)
    {
        var purchaseOrders = await this.PurchaseOrders(
            new PurchaseOrderFindManyArgs
            {
                Where = new PurchaseOrderWhereInput { Id = uniqueId.Id }
            }
        );
        var purchaseOrder = purchaseOrders.FirstOrDefault();
        if (purchaseOrder == null)
        {
            throw new NotFoundException();
        }

        return purchaseOrder;
    }

    /// <summary>
    /// Update one PurchaseOrder
    /// </summary>
    public async Task UpdatePurchaseOrder(
        PurchaseOrderWhereUniqueInput uniqueId,
        PurchaseOrderUpdateInput updateDto
    )
    {
        var purchaseOrder = updateDto.ToModel(uniqueId);

        if (updateDto.Deliveries != null)
        {
            purchaseOrder.Deliveries = await _context
                .Deliveries.Where(delivery =>
                    updateDto.Deliveries.Select(t => t).Contains(delivery.Id)
                )
                .ToListAsync();
        }

        if (updateDto.Supplier != null)
        {
            purchaseOrder.Supplier = await _context
                .Suppliers.Where(supplier => updateDto.Supplier == supplier.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(purchaseOrder).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.PurchaseOrders.Any(e => e.Id == purchaseOrder.Id))
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
    /// Connect multiple Deliveries records to PurchaseOrder
    /// </summary>
    public async Task ConnectDeliveries(
        PurchaseOrderWhereUniqueInput uniqueId,
        DeliveryWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .PurchaseOrders.Include(x => x.Deliveries)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Deliveries.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Deliveries);

        foreach (var child in childrenToConnect)
        {
            parent.Deliveries.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Deliveries records from PurchaseOrder
    /// </summary>
    public async Task DisconnectDeliveries(
        PurchaseOrderWhereUniqueInput uniqueId,
        DeliveryWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .PurchaseOrders.Include(x => x.Deliveries)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Deliveries.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Deliveries?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Deliveries records for PurchaseOrder
    /// </summary>
    public async Task<List<Delivery>> FindDeliveries(
        PurchaseOrderWhereUniqueInput uniqueId,
        DeliveryFindManyArgs purchaseOrderFindManyArgs
    )
    {
        var deliveries = await _context
            .Deliveries.Where(m => m.PurchaseOrderId == uniqueId.Id)
            .ApplyWhere(purchaseOrderFindManyArgs.Where)
            .ApplySkip(purchaseOrderFindManyArgs.Skip)
            .ApplyTake(purchaseOrderFindManyArgs.Take)
            .ApplyOrderBy(purchaseOrderFindManyArgs.SortBy)
            .ToListAsync();

        return deliveries.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Deliveries records for PurchaseOrder
    /// </summary>
    public async Task UpdateDeliveries(
        PurchaseOrderWhereUniqueInput uniqueId,
        DeliveryWhereUniqueInput[] childrenIds
    )
    {
        var purchaseOrder = await _context
            .PurchaseOrders.Include(t => t.Deliveries)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (purchaseOrder == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Deliveries.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        purchaseOrder.Deliveries = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Get a Supplier record for PurchaseOrder
    /// </summary>
    public async Task<Supplier> GetSupplier(PurchaseOrderWhereUniqueInput uniqueId)
    {
        var purchaseOrder = await _context
            .PurchaseOrders.Where(purchaseOrder => purchaseOrder.Id == uniqueId.Id)
            .Include(purchaseOrder => purchaseOrder.Supplier)
            .FirstOrDefaultAsync();
        if (purchaseOrder == null)
        {
            throw new NotFoundException();
        }
        return purchaseOrder.Supplier.ToDto();
    }
}
