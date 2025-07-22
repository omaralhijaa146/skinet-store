using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using skinet.API.Errors;
using skinet.Infrastructure.Data;

namespace skinet.API.Controllers;

public class BuggyController:BaseApiController
{
    private readonly StoreContext _context;

    public BuggyController(StoreContext context)
    {
        _context = context;
    }

    [HttpGet("testauth")]
    [Authorize]
    public async Task<ActionResult<string>> GetSecretText()
    {
        return "secret text";
    }
    
    
    [HttpGet("notfound")]
    public ActionResult GetNotFoundRequest()
    {
        var thing = _context.Products.Find(42);
        
        if(thing is null)
            return NotFound(new ApiResponse(404));
        
        return Ok();
    }
    
    [HttpGet("servererror")]
    public ActionResult GetServerError()
    {
        var thing = _context.Products.Find(42);
        
        var thingToReturn =thing.ToString();
        
        return Ok();
    }
    
    [HttpGet("badrequest")]
    public ActionResult BadRequest()
    {
        return BadRequest(new ApiResponse(400));
    }
    
    [HttpGet("badrequest/{id}")]
    public ActionResult BadRequest(int id)
    {
        return BadRequest();
    }
}