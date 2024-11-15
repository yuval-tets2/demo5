using InventoryManagementService.APIs.Common;
using InventoryManagementService.APIs.Dtos;

namespace InventoryManagementService.APIs;

public interface IWarehousesService
{
    /// <summary>
    /// Create one Warehouse
    /// </summary>
    public Task<Warehouse> CreateWarehouse(WarehouseCreateInput warehouse);

    /// <summary>
    /// Delete one Warehouse
    /// </summary>
    public Task DeleteWarehouse(WarehouseWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Warehouses
    /// </summary>
    public Task<List<Warehouse>> Warehouses(WarehouseFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Warehouse records
    /// </summary>
    public Task<MetadataDto> WarehousesMeta(WarehouseFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Warehouse
    /// </summary>
    public Task<Warehouse> Warehouse(WarehouseWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Warehouse
    /// </summary>
    public Task UpdateWarehouse(WarehouseWhereUniqueInput uniqueId, WarehouseUpdateInput updateDto);

    /// <summary>
    /// Connect multiple StockMovements records to Warehouse
    /// </summary>
    public Task ConnectStockMovements(
        WarehouseWhereUniqueInput uniqueId,
        StockMovementWhereUniqueInput[] stockMovementsId
    );

    /// <summary>
    /// Disconnect multiple StockMovements records from Warehouse
    /// </summary>
    public Task DisconnectStockMovements(
        WarehouseWhereUniqueInput uniqueId,
        StockMovementWhereUniqueInput[] stockMovementsId
    );

    /// <summary>
    /// Find multiple StockMovements records for Warehouse
    /// </summary>
    public Task<List<StockMovement>> FindStockMovements(
        WarehouseWhereUniqueInput uniqueId,
        StockMovementFindManyArgs StockMovementFindManyArgs
    );

    /// <summary>
    /// Update multiple StockMovements records for Warehouse
    /// </summary>
    public Task UpdateStockMovements(
        WarehouseWhereUniqueInput uniqueId,
        StockMovementWhereUniqueInput[] stockMovementsId
    );
}
