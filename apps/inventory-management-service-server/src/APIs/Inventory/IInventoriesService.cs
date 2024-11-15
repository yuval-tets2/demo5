using InventoryManagementService.APIs.Common;
using InventoryManagementService.APIs.Dtos;

namespace InventoryManagementService.APIs;

public interface IInventoriesService
{
    /// <summary>
    /// Create one Inventory
    /// </summary>
    public Task<Inventory> CreateInventory(InventoryCreateInput inventory);

    /// <summary>
    /// Delete one Inventory
    /// </summary>
    public Task DeleteInventory(InventoryWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Inventories
    /// </summary>
    public Task<List<Inventory>> Inventories(InventoryFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Inventory records
    /// </summary>
    public Task<MetadataDto> InventoriesMeta(InventoryFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Inventory
    /// </summary>
    public Task<Inventory> Inventory(InventoryWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Inventory
    /// </summary>
    public Task UpdateInventory(InventoryWhereUniqueInput uniqueId, InventoryUpdateInput updateDto);

    /// <summary>
    /// Get a Product record for Inventory
    /// </summary>
    public Task<Product> GetProduct(InventoryWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple StockMovements records to Inventory
    /// </summary>
    public Task ConnectStockMovements(
        InventoryWhereUniqueInput uniqueId,
        StockMovementWhereUniqueInput[] stockMovementsId
    );

    /// <summary>
    /// Disconnect multiple StockMovements records from Inventory
    /// </summary>
    public Task DisconnectStockMovements(
        InventoryWhereUniqueInput uniqueId,
        StockMovementWhereUniqueInput[] stockMovementsId
    );

    /// <summary>
    /// Find multiple StockMovements records for Inventory
    /// </summary>
    public Task<List<StockMovement>> FindStockMovements(
        InventoryWhereUniqueInput uniqueId,
        StockMovementFindManyArgs StockMovementFindManyArgs
    );

    /// <summary>
    /// Update multiple StockMovements records for Inventory
    /// </summary>
    public Task UpdateStockMovements(
        InventoryWhereUniqueInput uniqueId,
        StockMovementWhereUniqueInput[] stockMovementsId
    );
}
