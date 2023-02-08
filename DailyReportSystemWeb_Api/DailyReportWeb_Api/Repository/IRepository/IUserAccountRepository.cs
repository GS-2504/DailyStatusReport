using DailyReportWeb_Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Repository.IRepository
{
   public interface IUserAccountRepository
   {
         string SignInUser(UserSignIn userSignIn);
         Task<bool> SignUpUser(UserSignUp userSignUp);
         bool ResendEmailToUser(ResendEmail resendEmail);
         bool SendForgotPasswordEmailToUser(ForgotPassword forgotPassword);
         bool ResetUserPassword(ResetPassword resetPassword);
         bool ConfirmUserEmail(ConfirmEmail confirmEmail);
   }
}
