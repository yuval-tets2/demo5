using InventoryManagementService.APIs;
using InventoryManagementService.APIs.Common;
using InventoryManagementService.APIs.Dtos;
using InventoryManagementService.APIs.Errors;
using InventoryManagementService.APIs.Extensions;
using InventoryManagementService.Infrastructure;
using InventoryManagementService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementService.APIs;

public abstract class WarehousesServiceBase : IWarehousesService
{
    protected readonly InventoryManagementServiceDbContext _context;

    public WarehousesServiceBase(InventoryManagementServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Warehouse
    /// </summary>
    public async Task<Warehouse> CreateWarehouse(WarehouseCreateInput createDto)
    {
        var warehouse = new WarehouseDbModel
        {
            Capacity = createDto.Capacity,
            CreatedAt = createDto.CreatedAt,
            Location = createDto.Location,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            warehouse.Id = createDto.Id;
        }
        if (createDto.StockMovements != null)
        {
            warehouse.StockMovements = await _context
                .StockMovements.Where(stockMovement =>
                    createDto.StockMovements.Select(t => t.Id).Contains(stockMovement.Id)
                )
                .ToListAsync();
        }

        _context.Warehouses.Add(warehouse);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<WarehouseDbModel>(warehouse.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Warehouse
    /// </summary>
    public async Task DeleteWarehouse(WarehouseWhereUniqueInput uniqueId)
    {
        var warehouse = await _context.Warehouses.FindAsync(uniqueId.Id);
        if (warehouse == null)
        {
            throw new NotFoundException();
        }

        _context.Warehouses.Remove(warehouse);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Warehouses
    /// </summary>
    public async Task<List<Warehouse>> Warehouses(WarehouseFindManyArgs findManyArgs)
    {
        var warehouses = await _context
            .Warehouses.Include(x => x.StockMovements)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return warehouses.ConvertAll(warehouse => warehouse.ToDto());
    }

    /// <summary>
    /// Meta data about Warehouse records
    /// </summary>
    public async Task<MetadataDto> WarehousesMeta(WarehouseFindManyArgs findManyArgs)
    {
        var count = await _context.Warehouses.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Warehouse
    /// </summary>
    public async Task<Warehouse> Warehouse(WarehouseWhereUniqueInput uniqueId)
    {
        var warehouses = await this.Warehouses(
            new WarehouseFindManyArgs { Where = new WarehouseWhereInput { Id = uniqueId.Id } }
        );
        var warehouse = warehouses.FirstOrDefault();
        if (warehouse == null)
        {
            throw new NotFoundException();
        }

        return warehouse;
    }

    /// <summary>
    /// Update one Warehouse
    /// </summary>
    public async Task UpdateWarehouse(
        WarehouseWhereUniqueInput uniqueId,
        WarehouseUpdateInput updateDto
    )
    {
        var warehouse = updateDto.ToModel(uniqueId);

        if (updateDto.StockMovements != null)
        {
            warehouse.StockMovements = await _context
                .StockMovements.Where(stockMovement =>
                    updateDto.StockMovements.Select(t => t).Contains(stockMovement.Id)
                )
                .ToListAsync();
        }

        _context.Entry(warehouse).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Warehouses.Any(e => e.Id == warehouse.Id))
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
    /// Connect multiple StockMovements records to Warehouse
    /// </summary>
    public async Task ConnectStockMovements(
        WarehouseWhereUniqueInput uniqueId,
        StockMovementWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Warehouses.Include(x => x.StockMovements)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .StockMovements.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.StockMovements);

        foreach (var child in childrenToConnect)
        {
            parent.StockMovements.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple StockMovements records from Warehouse
    /// </summary>
    public async Task DisconnectStockMovements(
        WarehouseWhereUniqueInput uniqueId,
        StockMovementWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Warehouses.Include(x => x.StockMovements)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .StockMovements.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.StockMovements?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple StockMovements records for Warehouse
    /// </summary>
    public async Task<List<StockMovement>> FindStockMovements(
        WarehouseWhereUniqueInput uniqueId,
        StockMovementFindManyArgs warehouseFindManyArgs
    )
    {
        var stockMovements = await _context
            .StockMovements.Where(m => m.WarehouseId == uniqueId.Id)
            .ApplyWhere(warehouseFindManyArgs.Where)
            .ApplySkip(warehouseFindManyArgs.Skip)
            .ApplyTake(warehouseFindManyArgs.Take)
            .ApplyOrderBy(warehouseFindManyArgs.SortBy)
            .ToListAsync();

        return stockMovements.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple StockMovements records for Warehouse
    /// </summary>
    public async Task UpdateStockMovements(
        WarehouseWhereUniqueInput uniqueId,
        StockMovementWhereUniqueInput[] childrenIds
    )
    {
        var warehouse = await _context
            .Warehouses.Include(t => t.StockMovements)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (warehouse == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .StockMovements.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        warehouse.StockMovements = children;
        await _context.SaveChangesAsync();
    }
}
