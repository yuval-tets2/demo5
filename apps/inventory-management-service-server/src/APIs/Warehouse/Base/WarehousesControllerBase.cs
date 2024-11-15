using InventoryManagementService.APIs;
using InventoryManagementService.APIs.Common;
using InventoryManagementService.APIs.Dtos;
using InventoryManagementService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class WarehousesControllerBase : ControllerBase
{
    protected readonly IWarehousesService _service;

    public WarehousesControllerBase(IWarehousesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Warehouse
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Warehouse>> CreateWarehouse(WarehouseCreateInput input)
    {
        var warehouse = await _service.CreateWarehouse(input);

        return CreatedAtAction(nameof(Warehouse), new { id = warehouse.Id }, warehouse);
    }

    /// <summary>
    /// Delete one Warehouse
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteWarehouse(
        [FromRoute()] WarehouseWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteWarehouse(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Warehouses
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Warehouse>>> Warehouses(
        [FromQuery()] WarehouseFindManyArgs filter
    )
    {
        return Ok(await _service.Warehouses(filter));
    }

    /// <summary>
    /// Meta data about Warehouse records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> WarehousesMeta(
        [FromQuery()] WarehouseFindManyArgs filter
    )
    {
        return Ok(await _service.WarehousesMeta(filter));
    }

    /// <summary>
    /// Get one Warehouse
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Warehouse>> Warehouse(
        [FromRoute()] WarehouseWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Warehouse(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Warehouse
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateWarehouse(
        [FromRoute()] WarehouseWhereUniqueInput uniqueId,
        [FromQuery()] WarehouseUpdateInput warehouseUpdateDto
    )
    {
        try
        {
            await _service.UpdateWarehouse(uniqueId, warehouseUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple StockMovements records to Warehouse
    /// </summary>
    [HttpPost("{Id}/stockMovements")]
    public async Task<ActionResult> ConnectStockMovements(
        [FromRoute()] WarehouseWhereUniqueInput uniqueId,
        [FromQuery()] StockMovementWhereUniqueInput[] stockMovementsId
    )
    {
        try
        {
            await _service.ConnectStockMovements(uniqueId, stockMovementsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple StockMovements records from Warehouse
    /// </summary>
    [HttpDelete("{Id}/stockMovements")]
    public async Task<ActionResult> DisconnectStockMovements(
        [FromRoute()] WarehouseWhereUniqueInput uniqueId,
        [FromBody()] StockMovementWhereUniqueInput[] stockMovementsId
    )
    {
        try
        {
            await _service.DisconnectStockMovements(uniqueId, stockMovementsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple StockMovements records for Warehouse
    /// </summary>
    [HttpGet("{Id}/stockMovements")]
    public async Task<ActionResult<List<StockMovement>>> FindStockMovements(
        [FromRoute()] WarehouseWhereUniqueInput uniqueId,
        [FromQuery()] StockMovementFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindStockMovements(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple StockMovements records for Warehouse
    /// </summary>
    [HttpPatch("{Id}/stockMovements")]
    public async Task<ActionResult> UpdateStockMovements(
        [FromRoute()] WarehouseWhereUniqueInput uniqueId,
        [FromBody()] StockMovementWhereUniqueInput[] stockMovementsId
    )
    {
        try
        {
            await _service.UpdateStockMovements(uniqueId, stockMovementsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
