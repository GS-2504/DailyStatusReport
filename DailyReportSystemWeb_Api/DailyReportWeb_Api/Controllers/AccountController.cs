﻿using DailyReportWeb_Api.Identity;
using DailyReportWeb_Api.Model;
using DailyReportWeb_Api.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationRoleManager _applicationRoleManager;
        private readonly IEmailSender _emailSender;
        private IWebHostEnvironment _env;
        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,
           ApplicationRoleManager applicationRoleManager, IEmailSender emailSender, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationRoleManager = applicationRoleManager;
            _emailSender = emailSender;
            _env = env;
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
            }

            return null;
        }

       [HttpPost("RegisterUser")]
      // [ValidateAntiForgeryToken]
       public async Task<IActionResult> RegisterUser([FromBody]UserSignUp model)
        {
            if (!ModelState.IsValid) return BadRequest();
            var applicationUser = new ApplicationUser()
            {   
                UserName= model.Name,
                Email = model.Email,
            };
            var result =await _userManager.CreateAsync(applicationUser, model.Password);
            if (result.Succeeded)
            {   
                if (!await _applicationRoleManager.RoleExistsAsync(SD.Role_Admin))
                {
                    var role = new ApplicationRole();
                    role.Name = SD.Role_Admin;
                    await _applicationRoleManager.CreateAsync(role);
                }
                if (!await _applicationRoleManager.RoleExistsAsync(SD.Role_Organization))
                {
                    var role = new ApplicationRole();
                    role.Name = SD.Role_Organization;

                    await _applicationRoleManager.CreateAsync(role);
                }
                if (!await _applicationRoleManager.RoleExistsAsync(SD.Role_TeamLeader))
                {
                    var role = new ApplicationRole();
                    role.Name = SD.Role_TeamLeader;

                    await _applicationRoleManager.CreateAsync(role);
                }
                if (!await _applicationRoleManager.RoleExistsAsync(SD.Role_User))
                {
                    var role = new ApplicationRole();
                    role.Name = SD.Role_User;

                    await _applicationRoleManager.CreateAsync(role);
                }
                if (model.Role == null)
                {
                    await _userManager.AddToRoleAsync(applicationUser, SD.Role_User);
                }
                else
                {

                }
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Action(nameof(ConfirmEmail), "Account", new { userId = applicationUser.Id, code = code }, protocol: HttpContext.Request.Scheme);
                
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
            }
                  return Ok();
        }
    }
}