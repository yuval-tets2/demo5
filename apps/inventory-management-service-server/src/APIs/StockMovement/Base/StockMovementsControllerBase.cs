using InventoryManagementService.APIs;
using InventoryManagementService.APIs.Common;
using InventoryManagementService.APIs.Dtos;
using InventoryManagementService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class StockMovementsControllerBase : ControllerBase
{
    protected readonly IStockMovementsService _service;

    public StockMovementsControllerBase(IStockMovementsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one StockMovement
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<StockMovement>> CreateStockMovement(
        StockMovementCreateInput input
    )
    {
        var stockMovement = await _service.CreateStockMovement(input);

        return CreatedAtAction(nameof(StockMovement), new { id = stockMovement.Id }, stockMovement);
    }

    /// <summary>
    /// Delete one StockMovement
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteStockMovement(
        [FromRoute()] StockMovementWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteStockMovement(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many StockMovements
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<StockMovement>>> StockMovements(
        [FromQuery()] StockMovementFindManyArgs filter
    )
    {
        return Ok(await _service.StockMovements(filter));
    }

    /// <summary>
    /// Meta data about StockMovement records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> StockMovementsMeta(
        [FromQuery()] StockMovementFindManyArgs filter
    )
    {
        return Ok(await _service.StockMovementsMeta(filter));
    }

    /// <summary>
    /// Get one StockMovement
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<StockMovement>> StockMovement(
        [FromRoute()] StockMovementWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.StockMovement(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one StockMovement
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateStockMovement(
        [FromRoute()] StockMovementWhereUniqueInput uniqueId,
        [FromQuery()] StockMovementUpdateInput stockMovementUpdateDto
    )
    {
        try
        {
            await _service.UpdateStockMovement(uniqueId, stockMovementUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Inventory record for StockMovement
    /// </summary>
    [HttpGet("{Id}/inventory")]
    public async Task<ActionResult<List<Inventory>>> GetInventory(
        [FromRoute()] StockMovementWhereUniqueInput uniqueId
    )
    {
        var inventory = await _service.GetInventory(uniqueId);
        return Ok(inventory);
    }

    /// <summary>
    /// Get a Warehouse record for StockMovement
    /// </summary>
    [HttpGet("{Id}/warehouse")]
    public async Task<ActionResult<List<Warehouse>>> GetWarehouse(
        [FromRoute()] StockMovementWhereUniqueInput uniqueId
    )
    {
        var warehouse = await _service.GetWarehouse(uniqueId);
        return Ok(warehouse);
    }
}
