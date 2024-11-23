using Microsoft.AspNetCore.Mvc;
using SupplierManagementService.APIs;
using SupplierManagementService.APIs.Common;
using SupplierManagementService.APIs.Dtos;
using SupplierManagementService.APIs.Errors;

namespace SupplierManagementService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class SupplyContractsControllerBase : ControllerBase
{
    protected readonly ISupplyContractsService _service;

    public SupplyContractsControllerBase(ISupplyContractsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one SupplyContract
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<SupplyContract>> CreateSupplyContract(
        SupplyContractCreateInput input
    )
    {
        var supplyContract = await _service.CreateSupplyContract(input);

        return CreatedAtAction(
            nameof(SupplyContract),
            new { id = supplyContract.Id },
            supplyContract
        );
    }

    /// <summary>
    /// Delete one SupplyContract
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteSupplyContract(
        [FromRoute()] SupplyContractWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteSupplyContract(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many SupplyContracts
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<SupplyContract>>> SupplyContracts(
        [FromQuery()] SupplyContractFindManyArgs filter
    )
    {
        return Ok(await _service.SupplyContracts(filter));
    }

    /// <summary>
    /// Meta data about SupplyContract records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> SupplyContractsMeta(
        [FromQuery()] SupplyContractFindManyArgs filter
    )
    {
        return Ok(await _service.SupplyContractsMeta(filter));
    }

    /// <summary>
    /// Get one SupplyContract
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<SupplyContract>> SupplyContract(
        [FromRoute()] SupplyContractWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.SupplyContract(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one SupplyContract
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateSupplyContract(
        [FromRoute()] SupplyContractWhereUniqueInput uniqueId,
        [FromQuery()] SupplyContractUpdateInput supplyContractUpdateDto
    )
    {
        try
        {
            await _service.UpdateSupplyContract(uniqueId, supplyContractUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Supplier record for SupplyContract
    /// </summary>
    [HttpGet("{Id}/supplier")]
    public async Task<ActionResult<List<Supplier>>> GetSupplier(
        [FromRoute()] SupplyContractWhereUniqueInput uniqueId
    )
    {
        var supplier = await _service.GetSupplier(uniqueId);
        return Ok(supplier);
    }
}
