using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class gantong_api_vueGetUserList : System.Web.UI.Page
{
    private string user = "";
    private BrainTrain.BLL.bt_gantong_user uBll = new BrainTrain.BLL.bt_gantong_user();
    private BrainTrain.BLL.bt_gantong_curriculum cBll = new BrainTrain.BLL.bt_gantong_curriculum();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.Identity.IsAuthenticated)
        {
            Response.Redirect("./vueUnLogin.aspx");
        }

        user = Page.User.Identity.Name;
        DataTable dt = uBll.GetDataTable("name=\"" + user + "\"");
        
        string json = "{\"user\":\"" + user + "\",\"users\":[";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (i > 0)
            {
                json += ",";
            }
            DataRow dr = dt.Rows[i];
            json += "{";
            json += String.Format("\"i\":{3},\"name\":\"{0}\",\"date\":\"{1}\",\"u_i\":\"{2}\"", dr["nameCn"], DateTime.Parse(dr["time"].ToString()).ToString("yyyy-MM-dd"), dr["ui"],i);
            json += "}";
        }
        json += "]";
        int times = 0;
        BrainTrain.Model.bt_gantong_curriculum c_m = cBll.GetModel(user);
        if (c_m != null) {
            times = c_m.times;
        }
        json += ",\"times\":" + times + "}";

        Response.Write(json);
    }
}