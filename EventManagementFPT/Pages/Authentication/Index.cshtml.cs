using EventManagementFPT.Model;
using EventManagementFPT.Modules.UserModule.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventManagementFPT.Pages.Authentication
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public string email { get; set; }
        public string password { get; set; }
        public string token { get; set; }
        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                TempData["noti"] = "You have already logged in";
                return RedirectToPage("../Index");
            }
            return Page();
        }
        public async Task<IActionResult> OnPost(string email, string password, string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    IConfiguration config = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", true, true)
                                    .Build();
                    string adminEmail = config["admin:email"];
                    string adminPassword = config["admin:password"];
                    if (email.Equals(adminEmail) && password.Equals(adminPassword))
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Role, "admin"),
                            new Claim(ClaimTypes.Email, email)
                        };
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);
                        TempData["success"] = "Welcome admin";
                        return RedirectToPage("../Index");
                    }
                }
                else
                {
                    TempData["error"] = "Empty field(s) detected, please try again";
                    return Page();
                }
            }
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(token);
                var emailData = jsonToken.Claims.First(claim => claim.Type == "email").Value;
                if (emailData.Split('@')[1].Equals("fpt.edu.vn") || emailData.Split('@')[1].Equals("fe.edu.vn"))
                {
                    bool isExist = _userService.isExist(emailData);
                    //create user if not exist
                    if(!isExist)
                    {
                        var nameData = jsonToken.Claims.First(claim => claim.Type == "name").Value;
                        var avatarData = jsonToken.Claims.First(claim => claim.Type == "picture").Value;
                        var user = new User(nameData, emailData, avatarData);
                        await _userService.AddNewUser(user);
                    }
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Role, "user"),
                            new Claim(ClaimTypes.Email, emailData)
                        };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    TempData["success"] = "Welcome " + jsonToken.Claims.First(claim => claim.Type == "name").Value;
                    return RedirectToPage("../Index");
                }
                else
                {
                    TempData["error"] = "Your email does not belong to this organization!";
                    return RedirectToPage("./Index");
                }
            }
            TempData["error"] = "Invalid credentials, please try again later!";
            return RedirectToPage("./Index");
        }
    }
}
