using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EncryptedParameterAuthentication
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HeadLoginView.Visible = false;
            if (Session["UserRegistrationID"] != null)
            {
                HeadLoginView.Visible = true;
                NavigationMenu.Items.RemoveAt(0);
                NavigationMenu.Items.RemoveAt(0);
            }
        }
        protected void HeadLoginStatus_Click(object sender, EventArgs e)
        {

            Session.Clear();
            Response.Redirect(Constants.PageDefault);
        }
    }
}
