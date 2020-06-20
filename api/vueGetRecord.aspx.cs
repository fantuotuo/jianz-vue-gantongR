using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class gantong_api_vueGetRecord : System.Web.UI.Page
{
    private string user = "";
    private string json = "";
    private int page = 1;
    private int page_size = 20;
    private int u_i = -1;
    private string date = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.Identity.IsAuthenticated)
        {
            Response.Redirect("./vueUnLogin.aspx");
        }
        if (User.Identity.IsAuthenticated)
        {
            user = Page.User.Identity.Name;
            if (Request.QueryString["page"] != null)
            {
                page = Int32.Parse(Request.QueryString["page"]);
            }
            if (Request.QueryString["u_i"] != null)
            {
                u_i = Int32.Parse(Request.QueryString["u_i"]);
            }
            if (Request.QueryString["date"] != null)
            {
                date = Request.QueryString["date"];
            }
            try
            {
                date = Convert.ToDateTime(date).ToString("yyyy-MM-dd");
            }
            catch
            {
                date = DateTime.Now.ToString("yyyy-MM-dd");
            }

            int start = (page - 1) * page_size,
                count = page_size;
            string str = "select r.*,c.name as c_name,m.name as m_name from bt_gantong_record r left join ( select * from bt_gantong_course group by groupName,level) as c on (r.groupName=c.groupName and r.level=c.level) left join bt_gantong_module m on (r.modId=m.modId) where (r.name=\"" + user + "\" and ui=" + u_i + " and date(r.time)=date(\"" + date + "\")) order by r.time desc limit " + start + "," + count + ";";
            DataTable dt = Cgl.Data.MysqlHelper.GetDataTable(str);

            string record = "[";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                if (i > 0)
                {
                    record += ",";
                }
                string level = dr["level"].ToString();
                level = level.Length > 1 ? level : "0" + level;
                string date_row = Convert.ToDateTime(dr["time"].ToString()).ToString("yyyy-MM-dd");

                record += "{" + String.Format("\"i\":{6},\"name\":\"{0}{1} {2}\",\"score\":\"{3}\",\"goal\":\"{4}\",\"time\":\"{5}\"", dr["groupName"], level, dr["c_name"], dr["score"], dr["m_name"], dr["time"], i) + "}";
                //record += String.Format("[\"{0}{1} {2}\",\"{3}\",\"{4}\",\"{5}\"]", dr["groupName"], level, dr["c_name"], dr["score"], dr["m_name"], dr["time"]);
            }
            record += "]";

            json = "{\"code\":1,\"record\":" + record + ",\"date\":\"" + date + "\"}";

            Response.Write(json);
        }
    }

    private void inValid()
    {
        json = "{\"code\":0,\"record\":[]},\"date\":\"" + date + "\"";
    }
}