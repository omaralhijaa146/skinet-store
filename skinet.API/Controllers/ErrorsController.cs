using Microsoft.AspNetCore.Mvc;
using skinet.API.Errors;

namespace skinet.API.Controllers;

[Route("errors/{code}")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController:BaseApiController
{
    private readonly IHttpContextAccessor _httpContext;

    public ErrorsController(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }
    public IActionResult Error(int code)
    {
        _httpContext.HttpContext.Response.StatusCode = code;
        return new ObjectResult(new ApiResponse(code));
    }
}