using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Titanbrary.WebAPI
{
    public partial class register : System.Web.UI.Page
    {
        // count for 'Pick Date' button -- must be static, otherwise the value gets lost
        static int count = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void pickDateBtn_Click(object sender, EventArgs e)
        {
            
            if (count % 2 == 0)
            {
                pickDateBtn.Text = "Hide Calendar";
                calendar.Visible = true;
            }
            else
            {
                pickDateBtn.Text = "Pick Date";
                calendar.Visible = false;
            }
            count++;
        }

        protected void calendar_SelectionChanged(object sender, EventArgs e)
        {
            datePickedTextbox.Text = calendar.SelectedDate.ToString("dd-MMM-yyyy");
            calendar.Visible = false;
        }
    }
}