using Microsoft.AspNetCore.Mvc;
using SupplierManagementService.APIs;
using SupplierManagementService.APIs.Common;
using SupplierManagementService.APIs.Dtos;
using SupplierManagementService.APIs.Errors;

namespace SupplierManagementService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class DeliveriesControllerBase : ControllerBase
{
    protected readonly IDeliveriesService _service;

    public DeliveriesControllerBase(IDeliveriesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Delivery
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Delivery>> CreateDelivery(DeliveryCreateInput input)
    {
        var delivery = await _service.CreateDelivery(input);

        return CreatedAtAction(nameof(Delivery), new { id = delivery.Id }, delivery);
    }

    /// <summary>
    /// Delete one Delivery
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteDelivery([FromRoute()] DeliveryWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteDelivery(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Deliveries
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Delivery>>> Deliveries(
        [FromQuery()] DeliveryFindManyArgs filter
    )
    {
        return Ok(await _service.Deliveries(filter));
    }

    /// <summary>
    /// Meta data about Delivery records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> DeliveriesMeta(
        [FromQuery()] DeliveryFindManyArgs filter
    )
    {
        return Ok(await _service.DeliveriesMeta(filter));
    }

    /// <summary>
    /// Get one Delivery
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Delivery>> Delivery(
        [FromRoute()] DeliveryWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Delivery(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Delivery
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateDelivery(
        [FromRoute()] DeliveryWhereUniqueInput uniqueId,
        [FromQuery()] DeliveryUpdateInput deliveryUpdateDto
    )
    {
        try
        {
            await _service.UpdateDelivery(uniqueId, deliveryUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a PurchaseOrder record for Delivery
    /// </summary>
    [HttpGet("{Id}/purchaseOrder")]
    public async Task<ActionResult<List<PurchaseOrder>>> GetPurchaseOrder(
        [FromRoute()] DeliveryWhereUniqueInput uniqueId
    )
    {
        var purchaseOrder = await _service.GetPurchaseOrder(uniqueId);
        return Ok(purchaseOrder);
    }
}
