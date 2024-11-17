using Microsoft.AspNetCore.Mvc;
using ServiceFromPrivateTemplate.APIs;
using ServiceFromPrivateTemplate.APIs.Common;
using ServiceFromPrivateTemplate.APIs.Dtos;
using ServiceFromPrivateTemplate.APIs.Errors;

namespace ServiceFromPrivateTemplate.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class FffsControllerBase : ControllerBase
{
    protected readonly IFffsService _service;

    public FffsControllerBase(IFffsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one fff
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Fff>> CreateFff(FffCreateInput input)
    {
        var fff = await _service.CreateFff(input);

        return CreatedAtAction(nameof(Fff), new { id = fff.Id }, fff);
    }

    /// <summary>
    /// Delete one fff
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteFff([FromRoute()] FffWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteFff(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many fffs
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Fff>>> Fffs([FromQuery()] FffFindManyArgs filter)
    {
        return Ok(await _service.Fffs(filter));
    }

    /// <summary>
    /// Meta data about fff records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> FffsMeta([FromQuery()] FffFindManyArgs filter)
    {
        return Ok(await _service.FffsMeta(filter));
    }

    /// <summary>
    /// Get one fff
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Fff>> Fff([FromRoute()] FffWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Fff(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one fff
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateFff(
        [FromRoute()] FffWhereUniqueInput uniqueId,
        [FromQuery()] FffUpdateInput fffUpdateDto
    )
    {
        try
        {
            await _service.UpdateFff(uniqueId, fffUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
