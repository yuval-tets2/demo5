using Microsoft.AspNetCore.Mvc;
using SupplierManagementService.APIs;
using SupplierManagementService.APIs.Common;
using SupplierManagementService.APIs.Dtos;
using SupplierManagementService.APIs.Errors;

namespace SupplierManagementService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class PurchaseOrdersControllerBase : ControllerBase
{
    protected readonly IPurchaseOrdersService _service;

    public PurchaseOrdersControllerBase(IPurchaseOrdersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one PurchaseOrder
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<PurchaseOrder>> CreatePurchaseOrder(
        PurchaseOrderCreateInput input
    )
    {
        var purchaseOrder = await _service.CreatePurchaseOrder(input);

        return CreatedAtAction(nameof(PurchaseOrder), new { id = purchaseOrder.Id }, purchaseOrder);
    }

    /// <summary>
    /// Delete one PurchaseOrder
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeletePurchaseOrder(
        [FromRoute()] PurchaseOrderWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeletePurchaseOrder(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many PurchaseOrders
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<PurchaseOrder>>> PurchaseOrders(
        [FromQuery()] PurchaseOrderFindManyArgs filter
    )
    {
        return Ok(await _service.PurchaseOrders(filter));
    }

    /// <summary>
    /// Meta data about PurchaseOrder records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> PurchaseOrdersMeta(
        [FromQuery()] PurchaseOrderFindManyArgs filter
    )
    {
        return Ok(await _service.PurchaseOrdersMeta(filter));
    }

    /// <summary>
    /// Get one PurchaseOrder
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<PurchaseOrder>> PurchaseOrder(
        [FromRoute()] PurchaseOrderWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.PurchaseOrder(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one PurchaseOrder
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdatePurchaseOrder(
        [FromRoute()] PurchaseOrderWhereUniqueInput uniqueId,
        [FromQuery()] PurchaseOrderUpdateInput purchaseOrderUpdateDto
    )
    {
        try
        {
            await _service.UpdatePurchaseOrder(uniqueId, purchaseOrderUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Deliveries records to PurchaseOrder
    /// </summary>
    [HttpPost("{Id}/deliveries")]
    public async Task<ActionResult> ConnectDeliveries(
        [FromRoute()] PurchaseOrderWhereUniqueInput uniqueId,
        [FromQuery()] DeliveryWhereUniqueInput[] deliveriesId
    )
    {
        try
        {
            await _service.ConnectDeliveries(uniqueId, deliveriesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Deliveries records from PurchaseOrder
    /// </summary>
    [HttpDelete("{Id}/deliveries")]
    public async Task<ActionResult> DisconnectDeliveries(
        [FromRoute()] PurchaseOrderWhereUniqueInput uniqueId,
        [FromBody()] DeliveryWhereUniqueInput[] deliveriesId
    )
    {
        try
        {
            await _service.DisconnectDeliveries(uniqueId, deliveriesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Deliveries records for PurchaseOrder
    /// </summary>
    [HttpGet("{Id}/deliveries")]
    public async Task<ActionResult<List<Delivery>>> FindDeliveries(
        [FromRoute()] PurchaseOrderWhereUniqueInput uniqueId,
        [FromQuery()] DeliveryFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindDeliveries(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Deliveries records for PurchaseOrder
    /// </summary>
    [HttpPatch("{Id}/deliveries")]
    public async Task<ActionResult> UpdateDeliveries(
        [FromRoute()] PurchaseOrderWhereUniqueInput uniqueId,
        [FromBody()] DeliveryWhereUniqueInput[] deliveriesId
    )
    {
        try
        {
            await _service.UpdateDeliveries(uniqueId, deliveriesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Supplier record for PurchaseOrder
    /// </summary>
    [HttpGet("{Id}/supplier")]
    public async Task<ActionResult<List<Supplier>>> GetSupplier(
        [FromRoute()] PurchaseOrderWhereUniqueInput uniqueId
    )
    {
        var supplier = await _service.GetSupplier(uniqueId);
        return Ok(supplier);
    }
}
