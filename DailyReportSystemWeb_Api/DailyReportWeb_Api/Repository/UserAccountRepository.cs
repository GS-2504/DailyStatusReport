using DailyReportWeb_Api.Identity;
using DailyReportWeb_Api.Model;
using DailyReportWeb_Api.Repository.IRepository;
using DailyReportWeb_Api.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Repository
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IWebHostEnvironment _environment;
        private readonly IEmailSender _emailSender;
        public UserAccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
           , RoleManager<ApplicationRole> roleManager, IWebHostEnvironment environment, IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _environment = environment;
            _emailSender = emailSender;
        }
        public string SignInUser(UserSignIn userSignIn)
        {
            return "hello";
        }
        public async Task<bool> SignUpUser(UserSignUp userSignUp)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = userSignUp.Name,
                Email = userSignUp.Email,
                OrganizationId= userSignUp.OrganizationId
            };
            var result = await _userManager.CreateAsync(applicationUser, userSignUp.Password);
            if (!result.Succeeded)
            {
                return false;
            }
            if (!await _roleManager.RoleExistsAsync(StandardRoles.Role_Admin))
            {
                var role = new ApplicationRole();
                role.Name = StandardRoles.Role_Admin;
                await _roleManager.CreateAsync(role);
                await _userManager.AddToRoleAsync(applicationUser, StandardRoles.Role_Admin);
            }
            if (!await _roleManager.RoleExistsAsync(StandardRoles.Role_Organization))
            {
                var role = new ApplicationRole();
                role.Name = StandardRoles.Role_Organization;
                await _roleManager.CreateAsync(role);
                await _userManager.AddToRoleAsync(applicationUser, StandardRoles.Role_Organization);
            }
            if (!await _roleManager.RoleExistsAsync(StandardRoles.Role_TeamLeader))
            {
                var role = new ApplicationRole();
                role.Name = StandardRoles.Role_TeamLeader;
                await _roleManager.CreateAsync(role);
                await _userManager.AddToRoleAsync(applicationUser, StandardRoles.Role_TeamLeader);
            }
            if (!await _roleManager.RoleExistsAsync(StandardRoles.Role_User))
            {
                var role = new ApplicationRole();
                role.Name = StandardRoles.Role_User;
                await _roleManager.CreateAsync(role);
            }
            await _userManager.AddToRoleAsync(applicationUser, StandardRoles.Role_User);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("http://localhost:4200/confirmemail/?");
            stringBuilder.Append("UserId=" + applicationUser.Id + "&");
            stringBuilder.Append("code=" + code);
            var callbackUrl = stringBuilder.ToString();
            //get the path of the WelcomeEmailTemplate
            var pathToFile = _environment.ContentRootPath
                + Path.DirectorySeparatorChar
                + "Templates"
                + Path.DirectorySeparatorChar
                + "EmailTemplate"
                + Path.DirectorySeparatorChar
                + "Welcome_Email.html";

            stringBuilder.Clear();
            stringBuilder.Append("Please confirm your account by clicking ");
            stringBuilder.Append("<a href=\"" + callbackUrl + "\">here</a>");
            string Message = stringBuilder.ToString();

            var subject = "Confirm Account Registration";
            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }
            string messageBody = string.Format(builder.HtmlBody,
                    String.Format("{0:dddd, d MMMM yyyy}", DateTime.Now),
                    subject,
                    userSignUp.Email,
                    userSignUp.Name,
                    userSignUp.Password,
                    Message,
                    callbackUrl
                    );
            await _emailSender.SendEmailAsync(userSignUp.Email, subject, messageBody);
                   return true;
        }
        public bool ConfirmUserEmail(ConfirmEmail confirmEmail)
        {
            throw new NotImplementedException();
        }

        public bool ResendEmailToUser(ResendEmail resendEmail)
        {
            throw new NotImplementedException();
        }

        public bool ResetUserPassword(ResetPassword resetPassword)
        {
            throw new NotImplementedException();
        }

        public bool SendForgotPasswordEmailToUser(ForgotPassword forgotPassword)
        {
            throw new NotImplementedException();
        }

       
       
    }
}
