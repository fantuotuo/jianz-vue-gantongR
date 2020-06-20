using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class gantong_api_vueGetFangan : System.Web.UI.Page
{

    protected string json = "[]";
    protected string jsonScore = "[]";
    protected string jsonDates = "[]";
    protected string name = "";
    protected string age = "";
    protected string sex = "";

    protected int u_i = -1;
    private string scoreStr = "";
    private string[] scoreArr;

    private BrainTrain.BLL.bt_gantong_user uBll = new BrainTrain.BLL.bt_gantong_user();
    private BrainTrain.BLL.bt_gantong_course courseBll = new BrainTrain.BLL.bt_gantong_course();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.Identity.IsAuthenticated) {
            Response.Redirect("./vueUnLogin.aspx");
        }
        string user = Page.User.Identity.Name;

        try
        {
            if (Request.QueryString["u_i"] != null)
            {
                u_i = Int32.Parse(Request.QueryString["u_i"]);
            }
        }
        catch
        {

        }

        getDate(user);
        getData(user);

        string str = "{";
        str += String.Format("\"fangan\":{0},\"score\":{1},\"dates\":{2},\"name\":\"{3}\",\"age\":\"{4}\"", json, jsonScore, jsonDates, name, age);
        str += "}";
        Response.Write(str);
    }

    private int getBiLi(int[] boundArr, int score)
    {
        if (score < boundArr[0])
        {
            return 1;
        }
        else if (score < boundArr[1])
        {
            return 2;
        }
        else if (score < boundArr[2])
        {
            return 3;
        }
        else
        {
            return 4;
        }
    }
    private void getComment(int[] bili)
    {
        jsonScore = "[";
        for (int i = 0; i < bili.Length; i++)
        {
            if (i > 0)
            {
                jsonScore += ",";
            }
            jsonScore += "{\"i\":" + i + ",\"score\":\"" + scoreArr[i] + "\",\"comment\":\"" + bili[i] + "\"}";
            //jsonScore += "[";
            //jsonScore += scoreArr[i];
            //jsonScore += "," + bili[i];
            //jsonScore += "]";
        }
        jsonScore += "]";
    }
    protected void getData(string user)
    {
        DataTable dt_user = uBll.GetDataTable("name=\"" + user + "\" and ui=" + u_i);
        if (dt_user.Rows.Count <= 0) return;
        DataRow dr_user = dt_user.Rows[0];

        name = dr_user["nameCn"].ToString();
        sex = dr_user["sex"].ToString();
        age = dr_user["age"].ToString();
        scoreStr = dr_user["scores"].ToString();
        string trainList = dr_user["trainList"].ToString();
        scoreArr = scoreStr.Split('b');

        int[] bili = new int[] { 1, 1, 1, 1, 1, 1, 1 };
        bili[0] = getBiLi(new int[] { 17, 22, 28 }, Int32.Parse(scoreArr[0]));  // 前庭
        bili[1] = getBiLi(new int[] { 19, 25, 30 }, Int32.Parse(scoreArr[1]));  // 执行。。
        bili[2] = getBiLi(new int[] { 24, 31, 38 }, Int32.Parse(scoreArr[2]));  // 触觉
        bili[3] = getBiLi(new int[] { 17, 24, 30 }, Int32.Parse(scoreArr[3]));  // 发展
        bili[4] = getBiLi(new int[] { 8, 11, 15 }, Int32.Parse(scoreArr[4]));  // 视觉空间。。
        bili[5] = getBiLi(new int[] { 16, 23, 31 }, Int32.Parse(scoreArr[5]));  // 重力
        bili[6] = getBiLi(new int[] { 24, 31, 38 }, Int32.Parse(scoreArr[6]));  // 情绪。。
        getComment(bili);

        json = "[";
        int added = 0;
        string[] trainArr = trainList.Split(';');
        for (int i = 0; i < trainArr.Length; i++)
        {
            string train = trainArr[i];
            string[] trainArr2 = train.Split(',');
            if (trainArr2.Length >= 2)
            {
                string group = trainArr2[0];
                string level = trainArr2[1];
                DataTable dt = courseBll.GetDataTable("`groupName`=\"" + group + "\" and `level`=" + level);
                DataTable dt2 = Cgl.Data.MysqlHelper.GetDataTable("select name from bt_gantong_module where modId=(select modId from bt_gantong_group where name=\"" + group + "\" limit 1);");

                if (dt.Rows.Count > 0 && dt2.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    DataRow dr2 = dt2.Rows[0];
                    // 去掉换行符和其他符号，防止报错
                    string tip = dr["tip"].ToString();
                    tip = Cgl.Common.TextHelper.RemoveSQLString(tip);
                    tip = tip.Replace("\r\n", "<br>");

                    level = level.Length > 1 ? level : "0" + level;
                    json += added == 0 ? "" : ",";

                    json += "{" + String.Format("\"i\":{7},\"name\":\"{0}{1} {2}\",\"goal\":\"{5}\",\"tools\":\"{3}\",\"duration\":\"{4}\",\"tip\":\"{6}\",\"score\":-1", group, level, dr["name"], dr["aid"], dr["duration"], dr2["name"], tip, added) + "}";
                    //json += String.Format("[\"{0}{1} {2}\",\"{5}\",\"{3}\",\"{4}\",\"{6}\"]", group, level, dr["name"], dr["aid"], dr["duration"], dr2["name"], tip);

                    added++;
                }

            }
            else
            {
                json += added == 0 ? "" : ",";
                json += String.Format("[\"{0}{1} {2}\",\"{5}\",\"{3}\",\"{4}\",\"{6}\"]", "", "", "", "", "", "", "");

                added++;
            }
        }
        json += "]";

    }
    private void getDate(string user)
    {
        BrainTrain.BLL.bt_gantong_record rBll = new BrainTrain.BLL.bt_gantong_record();
        DataTable dt = rBll.GetDataTable("name=\"" + user + "\" and ui=" + u_i + " group by Date(time) order by time desc");

        jsonDates = "[";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (i > 0)
            {
                jsonDates += ",";
            }
            string date = Convert.ToDateTime(dt.Rows[i]["time"].ToString()).ToString("yyyy-MM-dd");
            jsonDates += "{\"i\":" + i + ",\"date\":\"" + date + "\"}";
            //jsonDates += "\"" + date + "\"";
        }
        jsonDates += "]";
    }
}