using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using shotnet.Data;
using shotnet.ViewModels;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace shotnet.Controllers{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller {
        
        private readonly UserManager<ApplicationUser> _userManager;
        public LoginController(UserManager<ApplicationUser> userManager){
            _userManager = userManager;
        }

        private async Task<string> tokenInitializer(ApplicationUser user){
            var key = Encoding.ASCII.GetBytes("secretKeyApplicationUser");
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var role = await _userManager.GetRolesAsync(user);
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, role[0]),
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = signingCredentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            return token;
        }

        [HttpPost]
        public async Task<IActionResult> login(LoginViewModel model){
            if(ModelState.IsValid){
                var user = await _userManager.FindByEmailAsync(model.email);
                if(user == null){
                    var newUser = new ApplicationUser{ UserName = model.email, Email = model.email };
                    var result =  await _userManager.CreateAsync(newUser, model.password);
                    if(result.Succeeded){
                        if(_userManager.Users.Count() == 1) await _userManager.AddToRoleAsync(newUser, "admin");
                        else await _userManager.AddToRoleAsync(newUser, "staff");
                    }
                    return Ok(new {token = await tokenInitializer(newUser)});
                }
                return Ok(new {token = await tokenInitializer(user)});
            }
            return Unauthorized();
        }

        [HttpPost("checkUserExists")]
        public async Task<bool> checkUserExists(LoginViewModel model){  
            var user = await _userManager.FindByEmailAsync(model.email);
            if(user != null && !(await _userManager.CheckPasswordAsync(user, model.password))) return true;
            return false;
        }

        [HttpGet("userExists/{email}")]
        public async Task<bool> userExists(string email){
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null) return true;
            return false;
        }

        [HttpGet("sendEmailConfirmation/{email}")]
        public async Task sendEmailConfirmation(string email){
            var user = await _userManager.FindByEmailAsync(email);
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("support", "nelsontorresl2019@gmail.com"));
            message.To.Add(new MailboxAddress("password reset confirmation", email));
            message.Subject = "please use this link to reset your password";
            message.Body = new TextPart("plain"){
                Text = "https://localhost:5001/reset-password/" + (await _userManager.GeneratePasswordResetTokenAsync(user)) 
            };
            using(var client = new SmtpClient()){
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("nelsontorresl2019@gmail.com", "32GnawFilosofica2019");
                client.Send(message);
                client.Disconnect(true);
            }
        }

        [HttpPut("resetPassword/{token}")]
        public async Task<IActionResult> resetPassword(string token, LoginViewModel model){
            if(ModelState.IsValid){
                var user = await _userManager.FindByEmailAsync(model.email);
                await _userManager.ResetPasswordAsync(user, token, model.password);
                return Ok();
            }return Unauthorized();
        }
    }
}