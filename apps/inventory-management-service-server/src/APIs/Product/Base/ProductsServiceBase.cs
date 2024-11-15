using InventoryManagementService.APIs;
using InventoryManagementService.APIs.Common;
using InventoryManagementService.APIs.Dtos;
using InventoryManagementService.APIs.Errors;
using InventoryManagementService.APIs.Extensions;
using InventoryManagementService.Infrastructure;
using InventoryManagementService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementService.APIs;

public abstract class ProductsServiceBase : IProductsService
{
    protected readonly InventoryManagementServiceDbContext _context;

    public ProductsServiceBase(InventoryManagementServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Product
    /// </summary>
    public async Task<Product> CreateProduct(ProductCreateInput createDto)
    {
        var product = new ProductDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Description = createDto.Description,
            Name = createDto.Name,
            Price = createDto.Price,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            product.Id = createDto.Id;
        }
        if (createDto.Inventories != null)
        {
            product.Inventories = await _context
                .Inventories.Where(inventory =>
                    createDto.Inventories.Select(t => t.Id).Contains(inventory.Id)
                )
                .ToListAsync();
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ProductDbModel>(product.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Product
    /// </summary>
    public async Task DeleteProduct(ProductWhereUniqueInput uniqueId)
    {
        var product = await _context.Products.FindAsync(uniqueId.Id);
        if (product == null)
        {
            throw new NotFoundException();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Products
    /// </summary>
    public async Task<List<Product>> Products(ProductFindManyArgs findManyArgs)
    {
        var products = await _context
            .Products.Include(x => x.Inventories)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return products.ConvertAll(product => product.ToDto());
    }

    /// <summary>
    /// Meta data about Product records
    /// </summary>
    public async Task<MetadataDto> ProductsMeta(ProductFindManyArgs findManyArgs)
    {
        var count = await _context.Products.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Product
    /// </summary>
    public async Task<Product> Product(ProductWhereUniqueInput uniqueId)
    {
        var products = await this.Products(
            new ProductFindManyArgs { Where = new ProductWhereInput { Id = uniqueId.Id } }
        );
        var product = products.FirstOrDefault();
        if (product == null)
        {
            throw new NotFoundException();
        }

        return product;
    }

    /// <summary>
    /// Update one Product
    /// </summary>
    public async Task UpdateProduct(ProductWhereUniqueInput uniqueId, ProductUpdateInput updateDto)
    {
        var product = updateDto.ToModel(uniqueId);

        if (updateDto.Inventories != null)
        {
            product.Inventories = await _context
                .Inventories.Where(inventory =>
                    updateDto.Inventories.Select(t => t).Contains(inventory.Id)
                )
                .ToListAsync();
        }

        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Products.Any(e => e.Id == product.Id))
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
    /// Connect multiple Inventories records to Product
    /// </summary>
    public async Task ConnectInventories(
        ProductWhereUniqueInput uniqueId,
        InventoryWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Products.Include(x => x.Inventories)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Inventories.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Inventories);

        foreach (var child in childrenToConnect)
        {
            parent.Inventories.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Inventories records from Product
    /// </summary>
    public async Task DisconnectInventories(
        ProductWhereUniqueInput uniqueId,
        InventoryWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Products.Include(x => x.Inventories)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Inventories.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Inventories?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Inventories records for Product
    /// </summary>
    public async Task<List<Inventory>> FindInventories(
        ProductWhereUniqueInput uniqueId,
        InventoryFindManyArgs productFindManyArgs
    )
    {
        var inventories = await _context
            .Inventories.Where(m => m.ProductId == uniqueId.Id)
            .ApplyWhere(productFindManyArgs.Where)
            .ApplySkip(productFindManyArgs.Skip)
            .ApplyTake(productFindManyArgs.Take)
            .ApplyOrderBy(productFindManyArgs.SortBy)
            .ToListAsync();

        return inventories.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Inventories records for Product
    /// </summary>
    public async Task UpdateInventories(
        ProductWhereUniqueInput uniqueId,
        InventoryWhereUniqueInput[] childrenIds
    )
    {
        var product = await _context
            .Products.Include(t => t.Inventories)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (product == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Inventories.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        product.Inventories = children;
        await _context.SaveChangesAsync();
    }
}
