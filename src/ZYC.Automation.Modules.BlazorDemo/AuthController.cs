using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ZYC.Automation.Modules.BlazorDemo;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("out")]
    public async Task<IResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Results.Redirect("/Account/Login?ReturnUrl=%2F");
    }
}