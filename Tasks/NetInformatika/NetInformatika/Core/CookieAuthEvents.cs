using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace NetInformatika.Core;
public class XCookieAuthEvents : CookieAuthenticationEvents
{
    public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
    {
        context.RedirectUri = $"/login";
        return base.RedirectToLogin(context);
    }

    public override Task RedirectToLogout(RedirectContext<CookieAuthenticationOptions> context)
    {
        context.RedirectUri = $"/logout";
        return base.RedirectToLogout(context);
    }

    public override Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
    {
        context.RedirectUri = $"/login";
        return base.RedirectToAccessDenied(context);
    }

    public override Task RedirectToReturnUrl(RedirectContext<CookieAuthenticationOptions> context)
    {
        context.RedirectUri = $"/";
        return base.RedirectToReturnUrl(context);
    }
}
