using Microsoft.AspNetCore.Mvc;
using SupplierManagementService.APIs;
using SupplierManagementService.APIs.Common;
using SupplierManagementService.APIs.Dtos;
using SupplierManagementService.APIs.Errors;

namespace SupplierManagementService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class SuppliersControllerBase : ControllerBase
{
    protected readonly ISuppliersService _service;

    public SuppliersControllerBase(ISuppliersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Supplier
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Supplier>> CreateSupplier(SupplierCreateInput input)
    {
        var supplier = await _service.CreateSupplier(input);

        return CreatedAtAction(nameof(Supplier), new { id = supplier.Id }, supplier);
    }

    /// <summary>
    /// Delete one Supplier
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteSupplier([FromRoute()] SupplierWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteSupplier(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Suppliers
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Supplier>>> Suppliers(
        [FromQuery()] SupplierFindManyArgs filter
    )
    {
        return Ok(await _service.Suppliers(filter));
    }

    /// <summary>
    /// Meta data about Supplier records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> SuppliersMeta(
        [FromQuery()] SupplierFindManyArgs filter
    )
    {
        return Ok(await _service.SuppliersMeta(filter));
    }

    /// <summary>
    /// Get one Supplier
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Supplier>> Supplier(
        [FromRoute()] SupplierWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Supplier(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Supplier
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateSupplier(
        [FromRoute()] SupplierWhereUniqueInput uniqueId,
        [FromQuery()] SupplierUpdateInput supplierUpdateDto
    )
    {
        try
        {
            await _service.UpdateSupplier(uniqueId, supplierUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple PurchaseOrders records to Supplier
    /// </summary>
    [HttpPost("{Id}/purchaseOrders")]
    public async Task<ActionResult> ConnectPurchaseOrders(
        [FromRoute()] SupplierWhereUniqueInput uniqueId,
        [FromQuery()] PurchaseOrderWhereUniqueInput[] purchaseOrdersId
    )
    {
        try
        {
            await _service.ConnectPurchaseOrders(uniqueId, purchaseOrdersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple PurchaseOrders records from Supplier
    /// </summary>
    [HttpDelete("{Id}/purchaseOrders")]
    public async Task<ActionResult> DisconnectPurchaseOrders(
        [FromRoute()] SupplierWhereUniqueInput uniqueId,
        [FromBody()] PurchaseOrderWhereUniqueInput[] purchaseOrdersId
    )
    {
        try
        {
            await _service.DisconnectPurchaseOrders(uniqueId, purchaseOrdersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple PurchaseOrders records for Supplier
    /// </summary>
    [HttpGet("{Id}/purchaseOrders")]
    public async Task<ActionResult<List<PurchaseOrder>>> FindPurchaseOrders(
        [FromRoute()] SupplierWhereUniqueInput uniqueId,
        [FromQuery()] PurchaseOrderFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindPurchaseOrders(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple PurchaseOrders records for Supplier
    /// </summary>
    [HttpPatch("{Id}/purchaseOrders")]
    public async Task<ActionResult> UpdatePurchaseOrders(
        [FromRoute()] SupplierWhereUniqueInput uniqueId,
        [FromBody()] PurchaseOrderWhereUniqueInput[] purchaseOrdersId
    )
    {
        try
        {
            await _service.UpdatePurchaseOrders(uniqueId, purchaseOrdersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple SupplyContracts records to Supplier
    /// </summary>
    [HttpPost("{Id}/supplyContracts")]
    public async Task<ActionResult> ConnectSupplyContracts(
        [FromRoute()] SupplierWhereUniqueInput uniqueId,
        [FromQuery()] SupplyContractWhereUniqueInput[] supplyContractsId
    )
    {
        try
        {
            await _service.ConnectSupplyContracts(uniqueId, supplyContractsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple SupplyContracts records from Supplier
    /// </summary>
    [HttpDelete("{Id}/supplyContracts")]
    public async Task<ActionResult> DisconnectSupplyContracts(
        [FromRoute()] SupplierWhereUniqueInput uniqueId,
        [FromBody()] SupplyContractWhereUniqueInput[] supplyContractsId
    )
    {
        try
        {
            await _service.DisconnectSupplyContracts(uniqueId, supplyContractsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple SupplyContracts records for Supplier
    /// </summary>
    [HttpGet("{Id}/supplyContracts")]
    public async Task<ActionResult<List<SupplyContract>>> FindSupplyContracts(
        [FromRoute()] SupplierWhereUniqueInput uniqueId,
        [FromQuery()] SupplyContractFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindSupplyContracts(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple SupplyContracts records for Supplier
    /// </summary>
    [HttpPatch("{Id}/supplyContracts")]
    public async Task<ActionResult> UpdateSupplyContracts(
        [FromRoute()] SupplierWhereUniqueInput uniqueId,
        [FromBody()] SupplyContractWhereUniqueInput[] supplyContractsId
    )
    {
        try
        {
            await _service.UpdateSupplyContracts(uniqueId, supplyContractsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
