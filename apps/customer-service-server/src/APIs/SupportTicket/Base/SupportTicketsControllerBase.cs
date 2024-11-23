using CustomerService.APIs;
using CustomerService.APIs.Common;
using CustomerService.APIs.Dtos;
using CustomerService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class SupportTicketsControllerBase : ControllerBase
{
    protected readonly ISupportTicketsService _service;

    public SupportTicketsControllerBase(ISupportTicketsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one SupportTicket
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<SupportTicket>> CreateSupportTicket(
        SupportTicketCreateInput input
    )
    {
        var supportTicket = await _service.CreateSupportTicket(input);

        return CreatedAtAction(nameof(SupportTicket), new { id = supportTicket.Id }, supportTicket);
    }

    /// <summary>
    /// Delete one SupportTicket
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteSupportTicket(
        [FromRoute()] SupportTicketWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteSupportTicket(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many SupportTickets
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<SupportTicket>>> SupportTickets(
        [FromQuery()] SupportTicketFindManyArgs filter
    )
    {
        return Ok(await _service.SupportTickets(filter));
    }

    /// <summary>
    /// Meta data about SupportTicket records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> SupportTicketsMeta(
        [FromQuery()] SupportTicketFindManyArgs filter
    )
    {
        return Ok(await _service.SupportTicketsMeta(filter));
    }

    /// <summary>
    /// Get one SupportTicket
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<SupportTicket>> SupportTicket(
        [FromRoute()] SupportTicketWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.SupportTicket(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one SupportTicket
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateSupportTicket(
        [FromRoute()] SupportTicketWhereUniqueInput uniqueId,
        [FromQuery()] SupportTicketUpdateInput supportTicketUpdateDto
    )
    {
        try
        {
            await _service.UpdateSupportTicket(uniqueId, supportTicketUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Customer record for SupportTicket
    /// </summary>
    [HttpGet("{Id}/customer")]
    public async Task<ActionResult<List<Customer>>> GetCustomer(
        [FromRoute()] SupportTicketWhereUniqueInput uniqueId
    )
    {
        var customer = await _service.GetCustomer(uniqueId);
        return Ok(customer);
    }
}
