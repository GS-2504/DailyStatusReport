using DailyReportWeb_Api.Identity;
using DailyReportWeb_Api.Model;
using DailyReportWeb_Api.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DailyReportWeb_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private IWebHostEnvironment _env;
        private AppSettings _appSettings;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
           ,RoleManager<ApplicationRole> roleManager, IEmailSender emailSender, IWebHostEnvironment env, IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _signInManager = signInManager;
            _env = env;
            _appSettings = appSettings.Value;
        }
        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody]ConfirmEmail confirmEmail)
        {
            var user = await _userManager.FindByIdAsync(confirmEmail.UserId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{confirmEmail.UserId}'.");
            }
            confirmEmail.Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(confirmEmail.Code));
            var result = await _userManager.ConfirmEmailAsync(user, confirmEmail.Code);
            if (!result.Succeeded)
            {
                return BadRequest($"Error confirming email for user with ID '{confirmEmail.UserId}':");
            }
            return Ok();
        }

       [HttpPost("RegisterUser")]
       public async Task<IActionResult> RegisterUser([FromBody]UserSignUp model)
        {
            if (!ModelState.IsValid && model==null) return BadRequest();
            var applicationUser = new ApplicationUser()
            {   
                UserName= model.Name,
                Email = model.Email,
            };
            var result =await _userManager.CreateAsync(applicationUser, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest("Something Went Wrong While creating Account");
            }
            if (!await _roleManager.RoleExistsAsync(SD.Role_Admin))
            {
                var role = new ApplicationRole();
                role.Name = SD.Role_Admin;
                await _roleManager.CreateAsync(role);
                await _userManager.AddToRoleAsync(applicationUser, SD.Role_Admin);
            }
            //if (!await _roleManager.RoleExistsAsync(SD.Role_Organization))
            //{
            //    var role = new ApplicationRole();
            //    role.Name = SD.Role_Organization;
            //    await _roleManager.CreateAsync(role);
            //    await _userManager.AddToRoleAsync(applicationUser, SD.Role_Organization);
            //}
            //if (!await _roleManager.RoleExistsAsync(SD.Role_TeamLeader))
            //{
            //    var role = new ApplicationRole();
            //    role.Name = SD.Role_TeamLeader;
            //    await _roleManager.CreateAsync(role);
            //    await _userManager.AddToRoleAsync(applicationUser, SD.Role_TeamLeader);
            //}
            //if (!await _roleManager.RoleExistsAsync(SD.Role_User))
            //{
            //    var role = new ApplicationRole();
            //    role.Name = SD.Role_User;
            //    await _roleManager.CreateAsync(role);
            //}
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Content("http://localhost:4200/confirmemail/?" + "UserId=" + applicationUser.Id + "&code=" + code);
            var pathToFile = _env.ContentRootPath + Path.DirectorySeparatorChar.ToString() + "Templates"
                       + Path.DirectorySeparatorChar.ToString()
                       + "EmailTemplate"
                       + Path.DirectorySeparatorChar.ToString()
                       + "Welcome_Email.html";
            string Message = "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>";
            var subject = "Confirm Account Registration";
            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }
            string messageBody = string.Format(builder.HtmlBody,
                    String.Format("{0:dddd, d MMMM yyyy}", DateTime.Now),
                    subject,
                    model.Email,
                    model.Name,
                    model.Password,
                    Message,
                    callbackUrl
                    );
            await _emailSender.SendEmailAsync(model.Email, subject, messageBody);
                      return Ok();
           
        }
        [HttpPost("UserSignIn")]
        public async Task<IActionResult> UserSignIn([FromBody] UserSignIn userSignIn)
        {
            if (!ModelState.IsValid) return BadRequest();
            var user = await _userManager.FindByEmailAsync(userSignIn.Email);
            if (user != null)
            {
                if (user.EmailConfirmed == false) return BadRequest(error: "Please Confirm your Email");
                var result = await _signInManager.PasswordSignInAsync(user.UserName, userSignIn.Password, false, false);
                if (result.Succeeded)
                {
                    if (await _userManager.IsInRoleAsync(user, SD.Role_Admin))
                        user.Role = SD.Role_Admin;
                    //
                    if (await _userManager.IsInRoleAsync(user, SD.Role_Organization))
                        user.Role = SD.Role_Organization;
                    var tokenhandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appSettings.Secretkey);
                    var tokenDescriptor = new SecurityTokenDescriptor()
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                          new Claim(ClaimTypes.Name, user.UserName),
                          new Claim(ClaimTypes.Email, user.Email),
                         // new Claim(ClaimTypes.Role,user.Role)
                        }),
                        Expires = DateTime.UtcNow.AddSeconds(15),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenhandler.CreateToken(tokenDescriptor);
                    var JwtToken = new Tokens()
                    {
                        JwtToken = tokenhandler.WriteToken(token)
                    };
                    return Ok(JwtToken);
                }
            }
            return BadRequest(error: "Invalid Credentials");
        }
        [HttpPost("ResendEmail")]
        public async Task<IActionResult> ResendEmail([FromBody] ResendEmail resendEmail)
        {
            if (string.IsNullOrEmpty(resendEmail.Email)) return BadRequest();
            var applicationUser = _userManager.FindByEmailAsync(resendEmail.Email);
            if (applicationUser == null) return BadRequest($"User Not Registred With this Email: {resendEmail.Email}");
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser.Result);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Content("http://localhost:4200/confirmemail/?" + "UserId=" + applicationUser.Result.Id + "&code=" + code);
            var pathToFile = _env.ContentRootPath + Path.DirectorySeparatorChar.ToString() + "Templates"
                       + Path.DirectorySeparatorChar.ToString()
                       + "EmailTemplate"
                       + Path.DirectorySeparatorChar.ToString()
                       + "Welcome_Email.html";
            string Message = "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>";
            var subject = "Confirm Account Registration";
            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }
            string messageBody = string.Format(builder.HtmlBody,
                    String.Format("{0:dddd, d MMMM yyyy}", DateTime.Now),
                    subject,
                    applicationUser.Result.Email,
                    applicationUser.Result.UserName,
                    applicationUser.Result.Email,
                    Message,
                    callbackUrl
                    );
            await _emailSender.SendEmailAsync(applicationUser.Result.Email, subject, messageBody);
            return Ok();
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPassword forgotPassword)
        {
            if (string.IsNullOrEmpty(forgotPassword.Email)) return BadRequest();
            var applicationUser = _userManager.FindByEmailAsync(forgotPassword.Email);
            if (applicationUser.Result == null) return NotFound();
            var code =await _userManager.GeneratePasswordResetTokenAsync(applicationUser.Result);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Content("http://localhost:4200/resetpassword?" + "UserId=" + applicationUser.Result.Id + "&code=" + code);
            var pathToFile = _env.ContentRootPath + Path.DirectorySeparatorChar.ToString() + "Templates"
                       + Path.DirectorySeparatorChar.ToString()
                       + "EmailTemplate"
                       + Path.DirectorySeparatorChar.ToString()
                       + "Welcome_Email.html";
            string Message = "Reset Your Password By Clicking Here<a href=\"" + callbackUrl + "\">here</a>";
            var subject = "Reset Your Password";
            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }
            string messageBody = string.Format(builder.HtmlBody,
                    String.Format("{0:dddd, d MMMM yyyy}", DateTime.Now),
                    subject,
                    applicationUser.Result.Email,
                    applicationUser.Result.UserName,
                    applicationUser.Result.Email,
                    Message,
                    callbackUrl
                    );
            await _emailSender.SendEmailAsync(applicationUser.Result.Email, subject, messageBody);
                      return Ok();
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassWord(ResetPassword resetPassword)
        {
            if (resetPassword==null && !ModelState.IsValid) return BadRequest();
            var user = _userManager.FindByIdAsync(resetPassword.UserId);
            if (user.Result == null) return BadRequest();
            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPassword.Token));
            var result =await _userManager.ResetPasswordAsync(user.Result,decodedToken,resetPassword.Password);
            if (result.Succeeded) return Ok();
            return BadRequest("Something Went Wrong While Reseting your Password");
        }
        }
    }


