using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class gt_api_vueUnLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("{\"unLogin\":true,\"msg\":\"用户未登录，请重新登陆\"}");
    }
}