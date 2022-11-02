using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BussinessObject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.BakeryModules.UserModule.Interface;

namespace CookingBakery.Pages.Authentication
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly CookingBakeryContext _context;

        public CreateModel(IUserService userService, CookingBakeryContext context)
        {
            _userService = userService;
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                TempData["noti"] = "You have already logged in";
                return RedirectToPage("/Home/Index");
            }
            return Page();
        }

        [BindProperty] public User NewUser { get; set; }
        public string token { get; set; }

        public async Task<IActionResult> OnPost(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                if (!string.IsNullOrEmpty(NewUser.Email) && !string.IsNullOrEmpty(NewUser.Password) && !string.IsNullOrEmpty(NewUser.Name))
                {
                    NewUser.Avatar = "~/image/default.png";
                    NewUser.Role = RoleEnum.User;

                    await _userService.AddNewUser(NewUser);
                    TempData["noti"] = "Account created";
                    return RedirectToPage("/Index");
                }
                else
                {
                    //TempData["error"] = "Empty field(s) detected, please try again";
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
                    return Page();
                }
            }

            return Page();
        }
    }
}