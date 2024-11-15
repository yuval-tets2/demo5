using InventoryManagementService.APIs.Common;
using InventoryManagementService.APIs.Dtos;

namespace InventoryManagementService.APIs;

public interface IStockMovementsService
{
    /// <summary>
    /// Create one StockMovement
    /// </summary>
    public Task<StockMovement> CreateStockMovement(StockMovementCreateInput stockmovement);

    /// <summary>
    /// Delete one StockMovement
    /// </summary>
    public Task DeleteStockMovement(StockMovementWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many StockMovements
    /// </summary>
    public Task<List<StockMovement>> StockMovements(StockMovementFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about StockMovement records
    /// </summary>
    public Task<MetadataDto> StockMovementsMeta(StockMovementFindManyArgs findManyArgs);

    /// <summary>
    /// Get one StockMovement
    /// </summary>
    public Task<StockMovement> StockMovement(StockMovementWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one StockMovement
    /// </summary>
    public Task UpdateStockMovement(
        StockMovementWhereUniqueInput uniqueId,
        StockMovementUpdateInput updateDto
    );

    /// <summary>
    /// Get a Inventory record for StockMovement
    /// </summary>
    public Task<Inventory> GetInventory(StockMovementWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a Warehouse record for StockMovement
    /// </summary>
    public Task<Warehouse> GetWarehouse(StockMovementWhereUniqueInput uniqueId);
}
