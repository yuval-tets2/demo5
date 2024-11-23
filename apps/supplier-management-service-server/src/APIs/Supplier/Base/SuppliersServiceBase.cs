using Microsoft.EntityFrameworkCore;
using SupplierManagementService.APIs;
using SupplierManagementService.APIs.Common;
using SupplierManagementService.APIs.Dtos;
using SupplierManagementService.APIs.Errors;
using SupplierManagementService.APIs.Extensions;
using SupplierManagementService.Infrastructure;
using SupplierManagementService.Infrastructure.Models;

namespace SupplierManagementService.APIs;

public abstract class SuppliersServiceBase : ISuppliersService
{
    protected readonly SupplierManagementServiceDbContext _context;

    public SuppliersServiceBase(SupplierManagementServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Supplier
    /// </summary>
    public async Task<Supplier> CreateSupplier(SupplierCreateInput createDto)
    {
        var supplier = new SupplierDbModel
        {
            ContactInfo = createDto.ContactInfo,
            CreatedAt = createDto.CreatedAt,
            Name = createDto.Name,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            supplier.Id = createDto.Id;
        }
        if (createDto.PurchaseOrders != null)
        {
            supplier.PurchaseOrders = await _context
                .PurchaseOrders.Where(purchaseOrder =>
                    createDto.PurchaseOrders.Select(t => t.Id).Contains(purchaseOrder.Id)
                )
                .ToListAsync();
        }

        if (createDto.SupplyContracts != null)
        {
            supplier.SupplyContracts = await _context
                .SupplyContracts.Where(supplyContract =>
                    createDto.SupplyContracts.Select(t => t.Id).Contains(supplyContract.Id)
                )
                .ToListAsync();
        }

        _context.Suppliers.Add(supplier);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<SupplierDbModel>(supplier.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Supplier
    /// </summary>
    public async Task DeleteSupplier(SupplierWhereUniqueInput uniqueId)
    {
        var supplier = await _context.Suppliers.FindAsync(uniqueId.Id);
        if (supplier == null)
        {
            throw new NotFoundException();
        }

        _context.Suppliers.Remove(supplier);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Suppliers
    /// </summary>
    public async Task<List<Supplier>> Suppliers(SupplierFindManyArgs findManyArgs)
    {
        var suppliers = await _context
            .Suppliers.Include(x => x.SupplyContracts)
            .Include(x => x.PurchaseOrders)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return suppliers.ConvertAll(supplier => supplier.ToDto());
    }

    /// <summary>
    /// Meta data about Supplier records
    /// </summary>
    public async Task<MetadataDto> SuppliersMeta(SupplierFindManyArgs findManyArgs)
    {
        var count = await _context.Suppliers.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Supplier
    /// </summary>
    public async Task<Supplier> Supplier(SupplierWhereUniqueInput uniqueId)
    {
        var suppliers = await this.Suppliers(
            new SupplierFindManyArgs { Where = new SupplierWhereInput { Id = uniqueId.Id } }
        );
        var supplier = suppliers.FirstOrDefault();
        if (supplier == null)
        {
            throw new NotFoundException();
        }

        return supplier;
    }

    /// <summary>
    /// Update one Supplier
    /// </summary>
    public async Task UpdateSupplier(
        SupplierWhereUniqueInput uniqueId,
        SupplierUpdateInput updateDto
    )
    {
        var supplier = updateDto.ToModel(uniqueId);

        if (updateDto.PurchaseOrders != null)
        {
            supplier.PurchaseOrders = await _context
                .PurchaseOrders.Where(purchaseOrder =>
                    updateDto.PurchaseOrders.Select(t => t).Contains(purchaseOrder.Id)
                )
                .ToListAsync();
        }

        if (updateDto.SupplyContracts != null)
        {
            supplier.SupplyContracts = await _context
                .SupplyContracts.Where(supplyContract =>
                    updateDto.SupplyContracts.Select(t => t).Contains(supplyContract.Id)
                )
                .ToListAsync();
        }

        _context.Entry(supplier).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Suppliers.Any(e => e.Id == supplier.Id))
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
    /// Connect multiple PurchaseOrders records to Supplier
    /// </summary>
    public async Task ConnectPurchaseOrders(
        SupplierWhereUniqueInput uniqueId,
        PurchaseOrderWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Suppliers.Include(x => x.PurchaseOrders)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .PurchaseOrders.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.PurchaseOrders);

        foreach (var child in childrenToConnect)
        {
            parent.PurchaseOrders.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple PurchaseOrders records from Supplier
    /// </summary>
    public async Task DisconnectPurchaseOrders(
        SupplierWhereUniqueInput uniqueId,
        PurchaseOrderWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Suppliers.Include(x => x.PurchaseOrders)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .PurchaseOrders.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.PurchaseOrders?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple PurchaseOrders records for Supplier
    /// </summary>
    public async Task<List<PurchaseOrder>> FindPurchaseOrders(
        SupplierWhereUniqueInput uniqueId,
        PurchaseOrderFindManyArgs supplierFindManyArgs
    )
    {
        var purchaseOrders = await _context
            .PurchaseOrders.Where(m => m.SupplierId == uniqueId.Id)
            .ApplyWhere(supplierFindManyArgs.Where)
            .ApplySkip(supplierFindManyArgs.Skip)
            .ApplyTake(supplierFindManyArgs.Take)
            .ApplyOrderBy(supplierFindManyArgs.SortBy)
            .ToListAsync();

        return purchaseOrders.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple PurchaseOrders records for Supplier
    /// </summary>
    public async Task UpdatePurchaseOrders(
        SupplierWhereUniqueInput uniqueId,
        PurchaseOrderWhereUniqueInput[] childrenIds
    )
    {
        var supplier = await _context
            .Suppliers.Include(t => t.PurchaseOrders)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (supplier == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .PurchaseOrders.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        supplier.PurchaseOrders = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple SupplyContracts records to Supplier
    /// </summary>
    public async Task ConnectSupplyContracts(
        SupplierWhereUniqueInput uniqueId,
        SupplyContractWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Suppliers.Include(x => x.SupplyContracts)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .SupplyContracts.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.SupplyContracts);

        foreach (var child in childrenToConnect)
        {
            parent.SupplyContracts.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple SupplyContracts records from Supplier
    /// </summary>
    public async Task DisconnectSupplyContracts(
        SupplierWhereUniqueInput uniqueId,
        SupplyContractWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Suppliers.Include(x => x.SupplyContracts)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .SupplyContracts.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.SupplyContracts?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple SupplyContracts records for Supplier
    /// </summary>
    public async Task<List<SupplyContract>> FindSupplyContracts(
        SupplierWhereUniqueInput uniqueId,
        SupplyContractFindManyArgs supplierFindManyArgs
    )
    {
        var supplyContracts = await _context
            .SupplyContracts.Where(m => m.SupplierId == uniqueId.Id)
            .ApplyWhere(supplierFindManyArgs.Where)
            .ApplySkip(supplierFindManyArgs.Skip)
            .ApplyTake(supplierFindManyArgs.Take)
            .ApplyOrderBy(supplierFindManyArgs.SortBy)
            .ToListAsync();

        return supplyContracts.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple SupplyContracts records for Supplier
    /// </summary>
    public async Task UpdateSupplyContracts(
        SupplierWhereUniqueInput uniqueId,
        SupplyContractWhereUniqueInput[] childrenIds
    )
    {
        var supplier = await _context
            .Suppliers.Include(t => t.SupplyContracts)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (supplier == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .SupplyContracts.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        supplier.SupplyContracts = children;
        await _context.SaveChangesAsync();
    }
}
