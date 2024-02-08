using BlazorApp.Server.Auth;
using BlazorApp.Shared;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

using System.Net;
using System.Security.Claims;

namespace BlazorApp.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticateController : ControllerBase
{
    private readonly IUserManager _manager;

    public AuthenticateController(IUserManager manager)
    {
        _manager = manager;
    }

    [HttpPost("Login")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserInfoModel))]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = _manager.GetUser(model.UserName ?? "");
        if (user != null && user.Password == model.Password)
        {
            var userRoles = _manager.GetRoles(user).ToArray();

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authProperties = new AuthenticationProperties
            {
                // Refreshing the authentication session should be allowed.
                AllowRefresh = true,

                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),

                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.
                IsPersistent = model.RememberLogin,

                // The time at which the authentication ticket was issued.
                IssuedUtc = DateTimeOffset.Now,
            };

            // The full path or absolute URI to be used as an http 
            // redirect response value.
            if (string.IsNullOrEmpty(model.RedirectUri))
                authProperties.RedirectUri = model.RedirectUri;

            var claimsIdentity = new ClaimsIdentity(
                authClaims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            if (!string.IsNullOrEmpty(model.RedirectUri))
                return Redirect(model.RedirectUri);

            return Ok(new UserInfoModel { UserName = user.Name, Roles = userRoles });
        }

        return Unauthorized();
    }

    [HttpGet("IsLoggedIn")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserInfoModel))]
    public IActionResult IsLoggedIn()
    {
        if (HttpContext.User.Identity?.IsAuthenticated ?? false)
        {
            var name = HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Name)?.Value;
            var roles = HttpContext.User.FindAll(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

            return Ok(new UserInfoModel { UserName = name, Roles = roles });
        }

        return BadRequest();
    }

    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();

        return Ok();
    }
}