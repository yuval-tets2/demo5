using InventoryManagementService.APIs;
using InventoryManagementService.APIs.Common;
using InventoryManagementService.APIs.Dtos;
using InventoryManagementService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class InventoriesControllerBase : ControllerBase
{
    protected readonly IInventoriesService _service;

    public InventoriesControllerBase(IInventoriesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Inventory
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Inventory>> CreateInventory(InventoryCreateInput input)
    {
        var inventory = await _service.CreateInventory(input);

        return CreatedAtAction(nameof(Inventory), new { id = inventory.Id }, inventory);
    }

    /// <summary>
    /// Delete one Inventory
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteInventory(
        [FromRoute()] InventoryWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteInventory(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Inventories
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Inventory>>> Inventories(
        [FromQuery()] InventoryFindManyArgs filter
    )
    {
        return Ok(await _service.Inventories(filter));
    }

    /// <summary>
    /// Meta data about Inventory records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> InventoriesMeta(
        [FromQuery()] InventoryFindManyArgs filter
    )
    {
        return Ok(await _service.InventoriesMeta(filter));
    }

    /// <summary>
    /// Get one Inventory
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Inventory>> Inventory(
        [FromRoute()] InventoryWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Inventory(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Inventory
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateInventory(
        [FromRoute()] InventoryWhereUniqueInput uniqueId,
        [FromQuery()] InventoryUpdateInput inventoryUpdateDto
    )
    {
        try
        {
            await _service.UpdateInventory(uniqueId, inventoryUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Product record for Inventory
    /// </summary>
    [HttpGet("{Id}/product")]
    public async Task<ActionResult<List<Product>>> GetProduct(
        [FromRoute()] InventoryWhereUniqueInput uniqueId
    )
    {
        var product = await _service.GetProduct(uniqueId);
        return Ok(product);
    }

    /// <summary>
    /// Connect multiple StockMovements records to Inventory
    /// </summary>
    [HttpPost("{Id}/stockMovements")]
    public async Task<ActionResult> ConnectStockMovements(
        [FromRoute()] InventoryWhereUniqueInput uniqueId,
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
    /// Disconnect multiple StockMovements records from Inventory
    /// </summary>
    [HttpDelete("{Id}/stockMovements")]
    public async Task<ActionResult> DisconnectStockMovements(
        [FromRoute()] InventoryWhereUniqueInput uniqueId,
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
    /// Find multiple StockMovements records for Inventory
    /// </summary>
    [HttpGet("{Id}/stockMovements")]
    public async Task<ActionResult<List<StockMovement>>> FindStockMovements(
        [FromRoute()] InventoryWhereUniqueInput uniqueId,
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
    /// Update multiple StockMovements records for Inventory
    /// </summary>
    [HttpPatch("{Id}/stockMovements")]
    public async Task<ActionResult> UpdateStockMovements(
        [FromRoute()] InventoryWhereUniqueInput uniqueId,
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
