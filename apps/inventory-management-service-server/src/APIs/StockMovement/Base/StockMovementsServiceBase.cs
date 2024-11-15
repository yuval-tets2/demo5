using InventoryManagementService.APIs;
using InventoryManagementService.APIs.Common;
using InventoryManagementService.APIs.Dtos;
using InventoryManagementService.APIs.Errors;
using InventoryManagementService.APIs.Extensions;
using InventoryManagementService.Infrastructure;
using InventoryManagementService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementService.APIs;

public abstract class StockMovementsServiceBase : IStockMovementsService
{
    protected readonly InventoryManagementServiceDbContext _context;

    public StockMovementsServiceBase(InventoryManagementServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one StockMovement
    /// </summary>
    public async Task<StockMovement> CreateStockMovement(StockMovementCreateInput createDto)
    {
        var stockMovement = new StockMovementDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Quantity = createDto.Quantity,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            stockMovement.Id = createDto.Id;
        }
        if (createDto.Inventory != null)
        {
            stockMovement.Inventory = await _context
                .Inventories.Where(inventory => createDto.Inventory.Id == inventory.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Warehouse != null)
        {
            stockMovement.Warehouse = await _context
                .Warehouses.Where(warehouse => createDto.Warehouse.Id == warehouse.Id)
                .FirstOrDefaultAsync();
        }

        _context.StockMovements.Add(stockMovement);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<StockMovementDbModel>(stockMovement.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one StockMovement
    /// </summary>
    public async Task DeleteStockMovement(StockMovementWhereUniqueInput uniqueId)
    {
        var stockMovement = await _context.StockMovements.FindAsync(uniqueId.Id);
        if (stockMovement == null)
        {
            throw new NotFoundException();
        }

        _context.StockMovements.Remove(stockMovement);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many StockMovements
    /// </summary>
    public async Task<List<StockMovement>> StockMovements(StockMovementFindManyArgs findManyArgs)
    {
        var stockMovements = await _context
            .StockMovements.Include(x => x.Inventory)
            .Include(x => x.Warehouse)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return stockMovements.ConvertAll(stockMovement => stockMovement.ToDto());
    }

    /// <summary>
    /// Meta data about StockMovement records
    /// </summary>
    public async Task<MetadataDto> StockMovementsMeta(StockMovementFindManyArgs findManyArgs)
    {
        var count = await _context.StockMovements.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one StockMovement
    /// </summary>
    public async Task<StockMovement> StockMovement(StockMovementWhereUniqueInput uniqueId)
    {
        var stockMovements = await this.StockMovements(
            new StockMovementFindManyArgs
            {
                Where = new StockMovementWhereInput { Id = uniqueId.Id }
            }
        );
        var stockMovement = stockMovements.FirstOrDefault();
        if (stockMovement == null)
        {
            throw new NotFoundException();
        }

        return stockMovement;
    }

    /// <summary>
    /// Update one StockMovement
    /// </summary>
    public async Task UpdateStockMovement(
        StockMovementWhereUniqueInput uniqueId,
        StockMovementUpdateInput updateDto
    )
    {
        var stockMovement = updateDto.ToModel(uniqueId);

        if (updateDto.Inventory != null)
        {
            stockMovement.Inventory = await _context
                .Inventories.Where(inventory => updateDto.Inventory == inventory.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Warehouse != null)
        {
            stockMovement.Warehouse = await _context
                .Warehouses.Where(warehouse => updateDto.Warehouse == warehouse.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(stockMovement).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.StockMovements.Any(e => e.Id == stockMovement.Id))
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
    /// Get a Inventory record for StockMovement
    /// </summary>
    public async Task<Inventory> GetInventory(StockMovementWhereUniqueInput uniqueId)
    {
        var stockMovement = await _context
            .StockMovements.Where(stockMovement => stockMovement.Id == uniqueId.Id)
            .Include(stockMovement => stockMovement.Inventory)
            .FirstOrDefaultAsync();
        if (stockMovement == null)
        {
            throw new NotFoundException();
        }
        return stockMovement.Inventory.ToDto();
    }

    /// <summary>
    /// Get a Warehouse record for StockMovement
    /// </summary>
    public async Task<Warehouse> GetWarehouse(StockMovementWhereUniqueInput uniqueId)
    {
        var stockMovement = await _context
            .StockMovements.Where(stockMovement => stockMovement.Id == uniqueId.Id)
            .Include(stockMovement => stockMovement.Warehouse)
            .FirstOrDefaultAsync();
        if (stockMovement == null)
        {
            throw new NotFoundException();
        }
        return stockMovement.Warehouse.ToDto();
    }
}
