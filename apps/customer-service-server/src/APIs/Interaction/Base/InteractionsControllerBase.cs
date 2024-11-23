using CustomerService.APIs;
using CustomerService.APIs.Common;
using CustomerService.APIs.Dtos;
using CustomerService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class InteractionsControllerBase : ControllerBase
{
    protected readonly IInteractionsService _service;

    public InteractionsControllerBase(IInteractionsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Interaction
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Interaction>> CreateInteraction(InteractionCreateInput input)
    {
        var interaction = await _service.CreateInteraction(input);

        return CreatedAtAction(nameof(Interaction), new { id = interaction.Id }, interaction);
    }

    /// <summary>
    /// Delete one Interaction
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteInteraction(
        [FromRoute()] InteractionWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteInteraction(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Interactions
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Interaction>>> Interactions(
        [FromQuery()] InteractionFindManyArgs filter
    )
    {
        return Ok(await _service.Interactions(filter));
    }

    /// <summary>
    /// Meta data about Interaction records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> InteractionsMeta(
        [FromQuery()] InteractionFindManyArgs filter
    )
    {
        return Ok(await _service.InteractionsMeta(filter));
    }

    /// <summary>
    /// Get one Interaction
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Interaction>> Interaction(
        [FromRoute()] InteractionWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Interaction(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Interaction
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateInteraction(
        [FromRoute()] InteractionWhereUniqueInput uniqueId,
        [FromQuery()] InteractionUpdateInput interactionUpdateDto
    )
    {
        try
        {
            await _service.UpdateInteraction(uniqueId, interactionUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Customer record for Interaction
    /// </summary>
    [HttpGet("{Id}/customer")]
    public async Task<ActionResult<List<Customer>>> GetCustomer(
        [FromRoute()] InteractionWhereUniqueInput uniqueId
    )
    {
        var customer = await _service.GetCustomer(uniqueId);
        return Ok(customer);
    }
}
