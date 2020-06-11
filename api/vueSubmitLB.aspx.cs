using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class gantong_api_vueSubmitLB : System.Web.UI.Page
{
    private string scoreStr = "";
    private string[] scoreArr;

    private BrainTrain.BLL.bt_gantong_group groupBll = new BrainTrain.BLL.bt_gantong_group();
    private BrainTrain.BLL.bt_gantong_course courseBll = new BrainTrain.BLL.bt_gantong_course();
    private BrainTrain.BLL.bt_gantong_user uBll = new BrainTrain.BLL.bt_gantong_user();

    protected string name = "";
    protected string sex = "";
    protected string age = "";
    protected string remark = "";

    private string json_ret = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string user = Page.User.Identity.Name;

        if (Request.QueryString["name"] != null)
        {
            name = Request.QueryString["name"];
        }
        if (Request.QueryString["sex"] != null)
        {
            sex = Request.QueryString["sex"];
        }
        if (Request.QueryString["age"] != null)
        {
            age = Request.QueryString["age"];
        }
        if (Request.QueryString["remark"] != null)
        {
            remark = Request.QueryString["remark"];
        }
        name = removeSQLString(name);
        sex = removeSQLString(sex);
        age = removeSQLString(age);
        remark = removeSQLString(remark);

        if (name == "" || sex == "" || age == "")
        {
            json_ret = "{" + String.Format("\"msg\":\"个人信息不能为空！\",\"u_i\":-1") + "}";
            Response.Write(json_ret);
            return;
        }

        if (Request.QueryString["score"] != null)
        {
            scoreStr = Cgl.Common.TextHelper.RemoveSQLString(Request.QueryString["score"]);
        }
        scoreArr = scoreStr.Split('b');
        if (scoreArr.Length < 7)
        {
            json_ret = "{" + String.Format("\"msg\":\"请提交完整的量表！\",\"u_i\":-1") + "}";
            Response.Write(json_ret);
            return;
        }


        int[] bili = new int[] { 1, 1, 1, 1, 1, 1, 1 };
        bili[0] = getBiLi(new int[] { 17, 22, 28 }, Int32.Parse(scoreArr[0]));  // 前庭
        bili[1] = getBiLi(new int[] { 19, 25, 30 }, Int32.Parse(scoreArr[1]));  // 执行。。
        bili[2] = getBiLi(new int[] { 24, 31, 38 }, Int32.Parse(scoreArr[2]));  // 触觉
        bili[3] = getBiLi(new int[] { 17, 24, 30 }, Int32.Parse(scoreArr[3]));  // 发展
        bili[4] = getBiLi(new int[] { 8, 11, 15 }, Int32.Parse(scoreArr[4]));  // 视觉空间。。
        bili[5] = getBiLi(new int[] { 16, 23, 31 }, Int32.Parse(scoreArr[5]));  // 重力
        bili[6] = getBiLi(new int[] { 24, 31, 38 }, Int32.Parse(scoreArr[6]));  // 情绪。。
        //getComment(bili);

        int[] bili2 = new int[] { bili[0], bili[5], bili[2], bili[3] };
        int[] nArr = new int[] { 0, 0, 0, 0 };
        nArr[0] = getN(bili2, 0);    // 前庭
        nArr[1] = getN(bili2, 1);    // 重力
        nArr[2] = getN(bili2, 2);    // 触觉
        nArr[3] = getN(bili2, 3);    // 发展

        //Response.Write(bili[0]); Response.Write(bili[2]); Response.Write(bili[3]);
        //Response.Write(nArr[0]); Response.Write(nArr[2]); Response.Write(nArr[3]);

        List<string> grpList = new List<string>();
        //List<string> goalList = new List<string>();   .........goalList.........
        List<string> groupChoose = new List<string>();  // 当前年龄段包含的训练组
        /// 3,4,5岁
        /// FKGGHHLRJSFFCCTY HEQPPMWHHH GPUCCCVDXVV GGGB BBC
        /// 6-12岁
        /// LFKGGHHRJSCCFFY HEQPPMWHHH DXGPV、VVUCCC GGGB BBC
        if (Int32.Parse(age) < 6)
        {
            groupChoose.Add("F"); groupChoose.Add("K"); groupChoose.Add("GG"); groupChoose.Add("HH"); groupChoose.Add("L"); groupChoose.Add("R"); groupChoose.Add("J"); groupChoose.Add("S"); groupChoose.Add("FF"); groupChoose.Add("CC"); groupChoose.Add("T"); groupChoose.Add("Y");
            groupChoose.Add("H"); groupChoose.Add("E"); groupChoose.Add("Q"); groupChoose.Add("PP"); groupChoose.Add("M"); groupChoose.Add("W"); groupChoose.Add("HHH");
            groupChoose.Add("G"); groupChoose.Add("P"); groupChoose.Add("U"); groupChoose.Add("CCC"); groupChoose.Add("V"); groupChoose.Add("D"); groupChoose.Add("X"); groupChoose.Add("VV");
            groupChoose.Add("GGG"); groupChoose.Add("B");
            groupChoose.Add("BB"); groupChoose.Add("C");
        }
        else
        {
            groupChoose.Add("L"); groupChoose.Add("F"); groupChoose.Add("K"); groupChoose.Add("GG"); groupChoose.Add("HH"); groupChoose.Add("R"); groupChoose.Add("J"); groupChoose.Add("S"); groupChoose.Add("CC"); groupChoose.Add("FF"); groupChoose.Add("Y");
            groupChoose.Add("H"); groupChoose.Add("E"); groupChoose.Add("Q"); groupChoose.Add("PP"); groupChoose.Add("M"); groupChoose.Add("W"); groupChoose.Add("HHH");
            groupChoose.Add("D"); groupChoose.Add("X"); groupChoose.Add("G"); groupChoose.Add("P"); groupChoose.Add("V"); groupChoose.Add("VV"); groupChoose.Add("U"); groupChoose.Add("CCC");
            groupChoose.Add("GGG"); groupChoose.Add("B");
            groupChoose.Add("BB"); groupChoose.Add("C");
        }

        int added = 0;
        DataTable dt = groupBll.GetDataTable("modId = 5");
        for (int i = 0; i < groupChoose.Count; i++)
        {
            string grp = groupChoose[i];
            if (added >= 1)
            {
                break;
            }

            if (findGroup(dt, grp))
            {
                grpList.Add(grp);
                //goalList.Add("综合训练");
                added++;
            }
        }
        added = 0;
        dt = groupBll.GetDataTable("modId = 1");
        for (int i = 0; i < groupChoose.Count; i++)
        {
            string grp = groupChoose[i];
            if (added >= nArr[0])
            {
                break;
            }

            if (findGroup(dt, grp))
            {
                grpList.Add(grp);
                //goalList.Add("前庭平衡和大脑双侧分化");
                added++;
            }
        }
        added = 0;
        dt = groupBll.GetDataTable("modId = 2");
        for (int i = 0; i < groupChoose.Count; i++)
        {
            string grp = groupChoose[i];
            if (added >= nArr[1])
            {
                break;
            }

            if (findGroup(dt, grp))
            {
                grpList.Add(grp);
                //goalList.Add("重力不安");
                added++;
            }
        }
        added = 0;
        dt = groupBll.GetDataTable("modId = 3");
        for (int i = 0; i < groupChoose.Count; i++)
        {
            string grp = groupChoose[i];
            if (added >= nArr[2])
            {
                break;
            }

            if (findGroup(dt, grp))
            {
                grpList.Add(grp);
                //goalList.Add("触觉敏感");
                added++;
            }
        }
        added = 0;
        dt = groupBll.GetDataTable("modId = 4");
        for (int i = 0; i < groupChoose.Count; i++)
        {
            string grp = groupChoose[i];
            if (added >= nArr[3])
            {
                break;
            }

            if (findGroup(dt, grp))
            {
                grpList.Add(grp);
                //goalList.Add("发展性协调运动功能");
                added++;
            }
        }

        string trainList = "";
        //json = "[";
        added = 0;
        for (int i = 0; i < grpList.Count; i++)
        {
            string grp = grpList[i];
            DataTable dt2 = courseBll.GetDataTable("groupName=\"" + grp + "\" order by level asc");
            //DataTable dt3 = Cgl.Data.MysqlHelper.GetDataTable("select name from bt_gantong_module where modId=(select modId from bt_gantong_group where name=\"" + grp + "\" limit 1);");

            if (dt2.Rows.Count > 0)
            {
                DataRow dr = dt2.Rows[0];
                string level = dr["level"].ToString();
                level = level.Length > 1 ? level : "0" + level;
                string group = dr["groupName"].ToString();


                //json += added == 0 ? "" : ",";
                //json += String.Format("[\"{0}{1} {2}\",\"{5}\",\"{3}\",\"{4}\",\"{6}\"]", group, level, dr["name"], dr["aid"], dr["duration"], dt3.Rows[0]["name"], dr["tip"]);
                trainList += added == 0 ? "" : ";";
                trainList += group + "," + dr["level"].ToString();

                added++;
            }
        }
        //json += "]";
        int u_i = 0;
        dt = uBll.GetDataTable("name=\"" + user + "\" order by ui desc");
        if (dt.Rows.Count > 0)
        {
            u_i = Int32.Parse(dt.Rows[0]["ui"].ToString());
            u_i++;
        }

        BrainTrain.Model.bt_gantong_user uModel = new BrainTrain.Model.bt_gantong_user();
        uModel.name = user;
        uModel.ui = u_i;
        uModel.scores = scoreStr;
        uModel.trainListOri = trainList;
        uModel.trainList = trainList;
        uModel.time = DateTime.Now;
        uModel.nameCn = name;
        uModel.sex = sex;
        uModel.age = Int32.Parse(age);
        uModel.remark = remark;
        uBll.Add(uModel);

        json_ret = "{" + String.Format("\"msg\":\"训练方案生成成功！\",\"u_i\":{0}", u_i) + "}";
        Response.Write(json_ret);
    }


    private bool findGroup(DataTable dt, string grp)
    {
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["name"].ToString() == grp)
            {
                return true;
            }
        }
        return false;
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
    private int getN(int[] intArr, int index)
    {
        int ret = 0;

        int sum = 0;
        for (int i = 0; i < intArr.Length; i++)
        {
            sum += intArr[i];
        }
        if (sum > 0)
        {
            double minute = 50 * intArr[index] / sum;
            ret = Int32.Parse(Math.Floor(minute / 5).ToString());
        }

        return ret;
    }
    //private void getComment(int[] bili)
    //{
    //    jsonScore = "[";
    //    for (int i = 0; i < bili.Length; i++)
    //    {
    //        if (i > 0)
    //        {
    //            jsonScore += ",";
    //        }
    //        jsonScore += "[";
    //        jsonScore += scoreArr[i];
    //        jsonScore += "," + bili[i];
    //        jsonScore += "]";
    //    }
    //    jsonScore += "]";
    //}

    private string removeSQLString(string input)
    {
        char[] array = "!@#$%^&*()_+=~`[]\\;',./{}|:\"<>?".ToCharArray();
        string result;
        if (input == null)
        {
            result = null;
        }
        else
        {
            char[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                char c = array2[i];
                input = input.Replace(c.ToString(), string.Empty);
            }
            result = input;
        }
        return result;
    }
}