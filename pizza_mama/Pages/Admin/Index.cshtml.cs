using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.Extensions.Hosting;

namespace pizza_mama.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public bool DisplayInvalidAccountMessage = false;
        public bool IsDevelopmentMode = false;
        IConfiguration configuration;
        public IndexModel(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.configuration = configuration;

            if (env.IsDevelopment())
                
            {
                IsDevelopmentMode = true;
            }
        }
        public IActionResult OnGet()
        {
            Console.WriteLine(HttpContext.User);
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/Admin/Pizzas");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost(string username, string password, string ReturnUrl)
        {
            var authSection = configuration.GetSection("Auth");
            var adminLogin = authSection["AdminLogin"];
            var adminPassword = authSection["AdminPassword"];

            if ((username == adminLogin) &&(password == adminPassword))
            {
                DisplayInvalidAccountMessage = true;
                var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Name, username)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, "Login");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new
                ClaimsPrincipal(claimsIdentity));
                return Redirect(ReturnUrl == null ? "/Admin/Pizzas" : ReturnUrl);
            }
            DisplayInvalidAccountMessage = true;
            return Page();
        }
        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Admin");
        }
    }
}
