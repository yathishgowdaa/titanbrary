using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanbrary.Common.Models;

namespace Titanbrary.Common.Interfaces.Data
{
    public interface IAccountDAC
    {
        UserModel GetUserInfoDAC(ApplicationUser currentUser);
        bool SaveAccountDAC(UserModel model);
    }
}
