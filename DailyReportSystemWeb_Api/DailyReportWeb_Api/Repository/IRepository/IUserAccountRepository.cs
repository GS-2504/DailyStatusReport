using DailyReportWeb_Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Repository.IRepository
{
   public interface IUserAccountRepository
   {
        void CreateUser(UserSignUp userSignup);

   }
}
