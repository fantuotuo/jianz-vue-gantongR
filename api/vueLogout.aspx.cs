using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class gantong_api_vueLogout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = true;

        Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        Response.AddHeader("Pragma", "No-Cache");
        HttpContext.Current.Request.Cookies.Clear();
        Session.Abandon();
        Session.Clear();
        System.Web.Security.FormsAuthentication.SignOut();

        Response.Write("{\"code\":1,\"msg\":\"退出成功\"}");
    }
}