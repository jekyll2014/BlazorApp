using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace BlazorApp.Server.Auth.BasicAuth
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IUserManager _manager;
        private const string BasicAuthorizationHeader = "Basic";
        public BasicAuthenticationHandler(
            IHttpContextAccessor httpContextAccessor,
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            IConfiguration configuration,
            ILoggerFactory logger,
            UrlEncoder encoder,
            IUserManager manager) :
            base(options, logger, encoder)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _manager = manager;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authorizationHeader = Request.Headers[HeaderNames.Authorization].ToString(); // "Authorization"
            if (authorizationHeader.StartsWith(BasicAuthorizationHeader, StringComparison.OrdinalIgnoreCase))
            {
                var authToken = authorizationHeader.Substring(BasicAuthorizationHeader.Length).Trim();
                var credentialsAsEncodedString = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                var credentials = credentialsAsEncodedString.Split(':');

                var user = _manager.GetUser(credentials[0]);
                if (user != null && user.Password == credentials[1])
                {
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                    };

                    var userRoles = _manager.GetRoles(user);
                    authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

                    var identity = new ClaimsIdentity(authClaims, BasicAuthorizationHeader);
                    var claimsPrincipal = new ClaimsPrincipal(identity);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        // Refreshing the authentication session should be allowed.

                        //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                        // The time at which the authentication ticket expires. A 
                        // value set here overrides the ExpireTimeSpan option of 
                        // CookieAuthenticationOptions set with AddCookie.

                        IsPersistent = true,
                        // Whether the authentication session is persisted across 
                        // multiple requests. When used with cookies, controls
                        // whether the cookie's lifetime is absolute (matching the
                        // lifetime of the authentication ticket) or session-based.

                        //IssuedUtc = <DateTimeOffset>,
                        // The time at which the authentication ticket was issued.

                        //RedirectUri = <string>
                        // The full path or absolute URI to be used as an http 
                        // redirect response value.
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        authClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await _httpContextAccessor.HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return await Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
                }
            }

            Response.StatusCode = 401;
            return await Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
        }
    }
}
