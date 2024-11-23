using CustomerService.APIs;
using CustomerService.APIs.Common;
using CustomerService.APIs.Dtos;
using CustomerService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CustomersControllerBase : ControllerBase
{
    protected readonly ICustomersService _service;

    public CustomersControllerBase(ICustomersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Customer
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Customer>> CreateCustomer(CustomerCreateInput input)
    {
        var customer = await _service.CreateCustomer(input);

        return CreatedAtAction(nameof(Customer), new { id = customer.Id }, customer);
    }

    /// <summary>
    /// Delete one Customer
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteCustomer([FromRoute()] CustomerWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteCustomer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Customers
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Customer>>> Customers(
        [FromQuery()] CustomerFindManyArgs filter
    )
    {
        return Ok(await _service.Customers(filter));
    }

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CustomersMeta(
        [FromQuery()] CustomerFindManyArgs filter
    )
    {
        return Ok(await _service.CustomersMeta(filter));
    }

    /// <summary>
    /// Get one Customer
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Customer>> Customer(
        [FromRoute()] CustomerWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Customer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Customer
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateCustomer(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] CustomerUpdateInput customerUpdateDto
    )
    {
        try
        {
            await _service.UpdateCustomer(uniqueId, customerUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Feedbacks records to Customer
    /// </summary>
    [HttpPost("{Id}/feedbacks")]
    public async Task<ActionResult> ConnectFeedbacks(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] FeedbackWhereUniqueInput[] feedbacksId
    )
    {
        try
        {
            await _service.ConnectFeedbacks(uniqueId, feedbacksId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Feedbacks records from Customer
    /// </summary>
    [HttpDelete("{Id}/feedbacks")]
    public async Task<ActionResult> DisconnectFeedbacks(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] FeedbackWhereUniqueInput[] feedbacksId
    )
    {
        try
        {
            await _service.DisconnectFeedbacks(uniqueId, feedbacksId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Feedbacks records for Customer
    /// </summary>
    [HttpGet("{Id}/feedbacks")]
    public async Task<ActionResult<List<Feedback>>> FindFeedbacks(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] FeedbackFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindFeedbacks(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Feedbacks records for Customer
    /// </summary>
    [HttpPatch("{Id}/feedbacks")]
    public async Task<ActionResult> UpdateFeedbacks(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] FeedbackWhereUniqueInput[] feedbacksId
    )
    {
        try
        {
            await _service.UpdateFeedbacks(uniqueId, feedbacksId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Interactions records to Customer
    /// </summary>
    [HttpPost("{Id}/interactions")]
    public async Task<ActionResult> ConnectInteractions(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] InteractionWhereUniqueInput[] interactionsId
    )
    {
        try
        {
            await _service.ConnectInteractions(uniqueId, interactionsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Interactions records from Customer
    /// </summary>
    [HttpDelete("{Id}/interactions")]
    public async Task<ActionResult> DisconnectInteractions(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] InteractionWhereUniqueInput[] interactionsId
    )
    {
        try
        {
            await _service.DisconnectInteractions(uniqueId, interactionsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Interactions records for Customer
    /// </summary>
    [HttpGet("{Id}/interactions")]
    public async Task<ActionResult<List<Interaction>>> FindInteractions(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] InteractionFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindInteractions(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Interactions records for Customer
    /// </summary>
    [HttpPatch("{Id}/interactions")]
    public async Task<ActionResult> UpdateInteractions(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] InteractionWhereUniqueInput[] interactionsId
    )
    {
        try
        {
            await _service.UpdateInteractions(uniqueId, interactionsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple SupportTickets records to Customer
    /// </summary>
    [HttpPost("{Id}/supportTickets")]
    public async Task<ActionResult> ConnectSupportTickets(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] SupportTicketWhereUniqueInput[] supportTicketsId
    )
    {
        try
        {
            await _service.ConnectSupportTickets(uniqueId, supportTicketsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple SupportTickets records from Customer
    /// </summary>
    [HttpDelete("{Id}/supportTickets")]
    public async Task<ActionResult> DisconnectSupportTickets(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] SupportTicketWhereUniqueInput[] supportTicketsId
    )
    {
        try
        {
            await _service.DisconnectSupportTickets(uniqueId, supportTicketsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple SupportTickets records for Customer
    /// </summary>
    [HttpGet("{Id}/supportTickets")]
    public async Task<ActionResult<List<SupportTicket>>> FindSupportTickets(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] SupportTicketFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindSupportTickets(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple SupportTickets records for Customer
    /// </summary>
    [HttpPatch("{Id}/supportTickets")]
    public async Task<ActionResult> UpdateSupportTickets(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] SupportTicketWhereUniqueInput[] supportTicketsId
    )
    {
        try
        {
            await _service.UpdateSupportTickets(uniqueId, supportTicketsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
