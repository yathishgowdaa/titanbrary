using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Titanbrary.WebAPI
{
    public partial class loginPage : System.Web.UI.Page
    {
        protected void signIn_Click(object sender, EventArgs e)
        {
            // Admin login == admin dashboard
            if (usernameTextbox.Text.ToLower() == "admin" && passwordTextbox.Text.ToLower() == "admin")
            {
                Response.Redirect("dashboardAdmin.aspx");
            }
            // else user login == user dashboard *************************** need to connect with database
            else
            {
                Response.Redirect("dashboardUser.aspx");
            }
            // TODO: keep that user/admin login until they logout
        }
    }
}