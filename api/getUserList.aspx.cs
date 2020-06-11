using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class gantong_api_getUserList : System.Web.UI.Page
{
    private string user = "";
    private BrainTrain.BLL.bt_gantong_user uBll = new BrainTrain.BLL.bt_gantong_user();

    protected void Page_Load(object sender, EventArgs e)
    {
        user = Page.User.Identity.Name;
        DataTable dt = uBll.GetDataTable("name=\"" + user + "\"");

        string json = "[";
        for (int i = 0; i < dt.Rows.Count; i++) {
            if (i > 0) {
                json += ",";
            }
            DataRow dr=dt.Rows[i];
            json+="{";
            json += String.Format("\"name\":\"{0}\",\"date\":\"{1}\",\"u_i\":\"{2}\"", dr["nameCn"], DateTime.Parse(dr["time"].ToString()).ToString("yyyy-MM-dd"), dr["ui"]);
            json+="}";
        }
        json += "]";
        Response.Write(json);
    }
}