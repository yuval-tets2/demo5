using InventoryManagementService.APIs;
using InventoryManagementService.APIs.Common;
using InventoryManagementService.APIs.Dtos;
using InventoryManagementService.APIs.Errors;
using InventoryManagementService.APIs.Extensions;
using InventoryManagementService.Infrastructure;
using InventoryManagementService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementService.APIs;

public abstract class InventoriesServiceBase : IInventoriesService
{
    protected readonly InventoryManagementServiceDbContext _context;

    public InventoriesServiceBase(InventoryManagementServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Inventory
    /// </summary>
    public async Task<Inventory> CreateInventory(InventoryCreateInput createDto)
    {
        var inventory = new InventoryDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Quantity = createDto.Quantity,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            inventory.Id = createDto.Id;
        }
        if (createDto.Product != null)
        {
            inventory.Product = await _context
                .Products.Where(product => createDto.Product.Id == product.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.StockMovements != null)
        {
            inventory.StockMovements = await _context
                .StockMovements.Where(stockMovement =>
                    createDto.StockMovements.Select(t => t.Id).Contains(stockMovement.Id)
                )
                .ToListAsync();
        }

        _context.Inventories.Add(inventory);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<InventoryDbModel>(inventory.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Inventory
    /// </summary>
    public async Task DeleteInventory(InventoryWhereUniqueInput uniqueId)
    {
        var inventory = await _context.Inventories.FindAsync(uniqueId.Id);
        if (inventory == null)
        {
            throw new NotFoundException();
        }

        _context.Inventories.Remove(inventory);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Inventories
    /// </summary>
    public async Task<List<Inventory>> Inventories(InventoryFindManyArgs findManyArgs)
    {
        var inventories = await _context
            .Inventories.Include(x => x.Product)
            .Include(x => x.StockMovements)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return inventories.ConvertAll(inventory => inventory.ToDto());
    }

    /// <summary>
    /// Meta data about Inventory records
    /// </summary>
    public async Task<MetadataDto> InventoriesMeta(InventoryFindManyArgs findManyArgs)
    {
        var count = await _context.Inventories.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Inventory
    /// </summary>
    public async Task<Inventory> Inventory(InventoryWhereUniqueInput uniqueId)
    {
        var inventories = await this.Inventories(
            new InventoryFindManyArgs { Where = new InventoryWhereInput { Id = uniqueId.Id } }
        );
        var inventory = inventories.FirstOrDefault();
        if (inventory == null)
        {
            throw new NotFoundException();
        }

        return inventory;
    }

    /// <summary>
    /// Update one Inventory
    /// </summary>
    public async Task UpdateInventory(
        InventoryWhereUniqueInput uniqueId,
        InventoryUpdateInput updateDto
    )
    {
        var inventory = updateDto.ToModel(uniqueId);

        if (updateDto.Product != null)
        {
            inventory.Product = await _context
                .Products.Where(product => updateDto.Product == product.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.StockMovements != null)
        {
            inventory.StockMovements = await _context
                .StockMovements.Where(stockMovement =>
                    updateDto.StockMovements.Select(t => t).Contains(stockMovement.Id)
                )
                .ToListAsync();
        }

        _context.Entry(inventory).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Inventories.Any(e => e.Id == inventory.Id))
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
    /// Get a Product record for Inventory
    /// </summary>
    public async Task<Product> GetProduct(InventoryWhereUniqueInput uniqueId)
    {
        var inventory = await _context
            .Inventories.Where(inventory => inventory.Id == uniqueId.Id)
            .Include(inventory => inventory.Product)
            .FirstOrDefaultAsync();
        if (inventory == null)
        {
            throw new NotFoundException();
        }
        return inventory.Product.ToDto();
    }

    /// <summary>
    /// Connect multiple StockMovements records to Inventory
    /// </summary>
    public async Task ConnectStockMovements(
        InventoryWhereUniqueInput uniqueId,
        StockMovementWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Inventories.Include(x => x.StockMovements)
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
    /// Disconnect multiple StockMovements records from Inventory
    /// </summary>
    public async Task DisconnectStockMovements(
        InventoryWhereUniqueInput uniqueId,
        StockMovementWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Inventories.Include(x => x.StockMovements)
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
    /// Find multiple StockMovements records for Inventory
    /// </summary>
    public async Task<List<StockMovement>> FindStockMovements(
        InventoryWhereUniqueInput uniqueId,
        StockMovementFindManyArgs inventoryFindManyArgs
    )
    {
        var stockMovements = await _context
            .StockMovements.Where(m => m.InventoryId == uniqueId.Id)
            .ApplyWhere(inventoryFindManyArgs.Where)
            .ApplySkip(inventoryFindManyArgs.Skip)
            .ApplyTake(inventoryFindManyArgs.Take)
            .ApplyOrderBy(inventoryFindManyArgs.SortBy)
            .ToListAsync();

        return stockMovements.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple StockMovements records for Inventory
    /// </summary>
    public async Task UpdateStockMovements(
        InventoryWhereUniqueInput uniqueId,
        StockMovementWhereUniqueInput[] childrenIds
    )
    {
        var inventory = await _context
            .Inventories.Include(t => t.StockMovements)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (inventory == null)
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

        inventory.StockMovements = children;
        await _context.SaveChangesAsync();
    }
}
