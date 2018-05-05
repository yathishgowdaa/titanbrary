using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Titanbrary.Common.Interfaces.BusinessObjects;
using Titanbrary.Common.Interfaces.Data;
using Titanbrary.Common.Models;
using Titanbrary.Data;
using Titanbrary.Data.DACs;

namespace Titanbrary.BusinessObjects
{
    public class AccountManager : IAccount
    {
        //private readonly IAccountDAC _AccountDAC;

        private AccountDAC accountDAC = new AccountDAC();

        //public AccountManager(IAccountDAC accountDAC)
        //{
        //    _AccountDAC = accountDAC;
        //}

        public AccountManager()
        {
        }

        public string UserName
        {
            get { return ((ClaimsPrincipal)HttpContext.Current.User).FindFirst(ClaimTypes.Name).Value; }
        }

        public string BearerToken
        {
            get { return ((ClaimsPrincipal)HttpContext.Current.User).FindFirst("AcessToken").Value; }
        }

        public UserModel GetUserInfo(ApplicationUser currentUser)
        {
            return accountDAC.GetUserInfoDAC(currentUser);
        }

        public bool SaveAccount(UserModel model)
        {
            bool result = accountDAC.SaveAccountDAC(model);           

            return result;
        }

       
    }
}
