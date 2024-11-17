using Microsoft.AspNetCore.Mvc;
using OrderProcessingService.APIs;
using OrderProcessingService.APIs.Common;
using OrderProcessingService.APIs.Dtos;
using OrderProcessingService.APIs.Errors;

namespace OrderProcessingService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ShipmentsControllerBase : ControllerBase
{
    protected readonly IShipmentsService _service;

    public ShipmentsControllerBase(IShipmentsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Shipment
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Shipment>> CreateShipment(ShipmentCreateInput input)
    {
        var shipment = await _service.CreateShipment(input);

        return CreatedAtAction(nameof(Shipment), new { id = shipment.Id }, shipment);
    }

    /// <summary>
    /// Delete one Shipment
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteShipment([FromRoute()] ShipmentWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteShipment(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Shipments
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Shipment>>> Shipments(
        [FromQuery()] ShipmentFindManyArgs filter
    )
    {
        return Ok(await _service.Shipments(filter));
    }

    /// <summary>
    /// Meta data about Shipment records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ShipmentsMeta(
        [FromQuery()] ShipmentFindManyArgs filter
    )
    {
        return Ok(await _service.ShipmentsMeta(filter));
    }

    /// <summary>
    /// Get one Shipment
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Shipment>> Shipment(
        [FromRoute()] ShipmentWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Shipment(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Shipment
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateShipment(
        [FromRoute()] ShipmentWhereUniqueInput uniqueId,
        [FromQuery()] ShipmentUpdateInput shipmentUpdateDto
    )
    {
        try
        {
            await _service.UpdateShipment(uniqueId, shipmentUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Order record for Shipment
    /// </summary>
    [HttpGet("{Id}/order")]
    public async Task<ActionResult<List<Order>>> GetOrder(
        [FromRoute()] ShipmentWhereUniqueInput uniqueId
    )
    {
        var order = await _service.GetOrder(uniqueId);
        return Ok(order);
    }
}
