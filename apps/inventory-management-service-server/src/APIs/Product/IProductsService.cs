using InventoryManagementService.APIs.Common;
using InventoryManagementService.APIs.Dtos;

namespace InventoryManagementService.APIs;

public interface IProductsService
{
    /// <summary>
    /// Create one Product
    /// </summary>
    public Task<Product> CreateProduct(ProductCreateInput product);

    /// <summary>
    /// Delete one Product
    /// </summary>
    public Task DeleteProduct(ProductWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Products
    /// </summary>
    public Task<List<Product>> Products(ProductFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Product records
    /// </summary>
    public Task<MetadataDto> ProductsMeta(ProductFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Product
    /// </summary>
    public Task<Product> Product(ProductWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Product
    /// </summary>
    public Task UpdateProduct(ProductWhereUniqueInput uniqueId, ProductUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Inventories records to Product
    /// </summary>
    public Task ConnectInventories(
        ProductWhereUniqueInput uniqueId,
        InventoryWhereUniqueInput[] inventoriesId
    );

    /// <summary>
    /// Disconnect multiple Inventories records from Product
    /// </summary>
    public Task DisconnectInventories(
        ProductWhereUniqueInput uniqueId,
        InventoryWhereUniqueInput[] inventoriesId
    );

    /// <summary>
    /// Find multiple Inventories records for Product
    /// </summary>
    public Task<List<Inventory>> FindInventories(
        ProductWhereUniqueInput uniqueId,
        InventoryFindManyArgs InventoryFindManyArgs
    );

    /// <summary>
    /// Update multiple Inventories records for Product
    /// </summary>
    public Task UpdateInventories(
        ProductWhereUniqueInput uniqueId,
        InventoryWhereUniqueInput[] inventoriesId
    );
}
