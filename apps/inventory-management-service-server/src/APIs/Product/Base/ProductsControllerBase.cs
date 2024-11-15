using InventoryManagementService.APIs;
using InventoryManagementService.APIs.Common;
using InventoryManagementService.APIs.Dtos;
using InventoryManagementService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ProductsControllerBase : ControllerBase
{
    protected readonly IProductsService _service;

    public ProductsControllerBase(IProductsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Product
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Product>> CreateProduct(ProductCreateInput input)
    {
        var product = await _service.CreateProduct(input);

        return CreatedAtAction(nameof(Product), new { id = product.Id }, product);
    }

    /// <summary>
    /// Delete one Product
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteProduct([FromRoute()] ProductWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteProduct(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Products
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Product>>> Products(
        [FromQuery()] ProductFindManyArgs filter
    )
    {
        return Ok(await _service.Products(filter));
    }

    /// <summary>
    /// Meta data about Product records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ProductsMeta(
        [FromQuery()] ProductFindManyArgs filter
    )
    {
        return Ok(await _service.ProductsMeta(filter));
    }

    /// <summary>
    /// Get one Product
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Product>> Product([FromRoute()] ProductWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Product(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Product
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateProduct(
        [FromRoute()] ProductWhereUniqueInput uniqueId,
        [FromQuery()] ProductUpdateInput productUpdateDto
    )
    {
        try
        {
            await _service.UpdateProduct(uniqueId, productUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Inventories records to Product
    /// </summary>
    [HttpPost("{Id}/inventories")]
    public async Task<ActionResult> ConnectInventories(
        [FromRoute()] ProductWhereUniqueInput uniqueId,
        [FromQuery()] InventoryWhereUniqueInput[] inventoriesId
    )
    {
        try
        {
            await _service.ConnectInventories(uniqueId, inventoriesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Inventories records from Product
    /// </summary>
    [HttpDelete("{Id}/inventories")]
    public async Task<ActionResult> DisconnectInventories(
        [FromRoute()] ProductWhereUniqueInput uniqueId,
        [FromBody()] InventoryWhereUniqueInput[] inventoriesId
    )
    {
        try
        {
            await _service.DisconnectInventories(uniqueId, inventoriesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Inventories records for Product
    /// </summary>
    [HttpGet("{Id}/inventories")]
    public async Task<ActionResult<List<Inventory>>> FindInventories(
        [FromRoute()] ProductWhereUniqueInput uniqueId,
        [FromQuery()] InventoryFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindInventories(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Inventories records for Product
    /// </summary>
    [HttpPatch("{Id}/inventories")]
    public async Task<ActionResult> UpdateInventories(
        [FromRoute()] ProductWhereUniqueInput uniqueId,
        [FromBody()] InventoryWhereUniqueInput[] inventoriesId
    )
    {
        try
        {
            await _service.UpdateInventories(uniqueId, inventoriesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
