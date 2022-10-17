using CookingBakery.BakeryModules.UserModule.Interface;
using CookingBakery.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CookingBakery.Pages.Authentication
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
                return RedirectToPage("/Home/Index");
            }
            return Page();
        }
        public async Task<IActionResult> OnPost(string email, string password, string token)
        {
            //Login with username and password -> check if it is admin
            if (string.IsNullOrEmpty(token))
            {
                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    //IConfiguration config = new ConfigurationBuilder()
                    //                .SetBasePath(Directory.GetCurrentDirectory())
                    //                .AddJsonFile("appsettings.json", true, true)
                    //                .Build();
                    //string adminEmail = config["admin:email"];
                    //string adminPassword = config["admin:password"];
                    ////check admin
                    //if (email.Equals(adminEmail) && password.Equals(adminPassword))
                    //{
                    //    var claims = new List<Claim>
                    //    {
                    //        new Claim(ClaimTypes.Role, "admin"),
                    //        new Claim(ClaimTypes.Email, email),
                    //        new Claim(ClaimTypes.NameIdentifier, config["admin:guid"])
                    //    };
                    //    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    //    await HttpContext.SignInAsync(claimsPrincipal);
                    //    TempData["success"] = "Welcome admin";
                    //    return RedirectToPage("/EventPage/Index");
                    //}
                    //check user
                    var User = await _userService.Authenticate(email, password);
                    if (User != null)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Role, User.Role.ToString()),
                            new Claim(ClaimTypes.Email, User.Email),
                            new Claim(ClaimTypes.NameIdentifier, User.UserId.ToString()),
                            new Claim("avatar-url", User.Avatar ?? ""),
                            new Claim("google-authen", "false")
                        };
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);
                        TempData["success"] = "Welcome " + User.Name;
                        return RedirectToPage("/Home/Index");
                    }
                    else
                    {
                        TempData["error"] = "Invalid credentials";
                        return Page();
                    }
                }
                else
                {
                    TempData["error"] = "Empty field(s) detected, please try again";
                    return Page();
                }
            }
            //Login with Google token -> check if the email is valid
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(token);
                var emailData = jsonToken.Claims.First(claim => claim.Type == "email").Value;
                if (emailData.Split('@')[1].Equals("fpt.edu.vn") || emailData.Split('@')[1].Equals("fe.edu.vn"))
                {
                    bool isExist = _userService.isExist(emailData);
                    //create user if not exist
                    if (!isExist)
                    {
                        var nameData = jsonToken.Claims.First(claim => claim.Type == "name").Value;
                        var avatarData = jsonToken.Claims.First(claim => claim.Type == "picture").Value;
                        var newUser = new User(nameData, emailData, avatarData, true, RoleEnum.User);
                        await _userService.AddNewUser(newUser);
                    }
                    User user = await _userService.GetUserByEmail(emailData);
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Role, user.Role.ToString()),
                            new Claim(ClaimTypes.Email, emailData),
                            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                            new Claim("avatar-url", user.Avatar),
                            new Claim("google-authen", "true")
                        };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    TempData["success"] = "Welcome " + jsonToken.Claims.First(claim => claim.Type == "name").Value;
                    return RedirectToPage("/Home/Index");
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
