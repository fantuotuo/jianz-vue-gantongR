using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class gantong_api_submit : System.Web.UI.Page
{
    private string user = "";
    private int age = 0;
    private int row = -1;
    private string groupName = "";
    private string level = "";
    private int score = 0;
    private int modId = -1;

    private string json = "";
    private BrainTrain.BLL.bt_gantong_record rBll = new BrainTrain.BLL.bt_gantong_record();
    private BrainTrain.BLL.bt_gantong_user uBll = new BrainTrain.BLL.bt_gantong_user();
    private BrainTrain.BLL.bt_gantong_group gBll = new BrainTrain.BLL.bt_gantong_group();
    private BrainTrain.BLL.bt_gantong_course cBll = new BrainTrain.BLL.bt_gantong_course();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated) {
            if (checkValid())
            {
                //try
                //{
                    // 添加记录
                    addRecord();

                    // 更新用户当前训练表格
                    if (score <= 1 || score >= 5)
                    {
                        updateTrainList();
                    }

                    // 返回json字符串
                    json = "{\"code\":1,\"score\":" + score + ",\"row\":" + row + "}";
                //}
                //catch
                //{
                //    inValid();
                //}
            }
            else {
                inValid();
            }
            Response.Write(json);
        }
    }
    private void inValid() {
        json = "{\"code\":0,\"score\":" + 0 + ",\"row\":" + (-1) + "}";
    }

    private bool checkValid(){
        user = Page.User.Identity.Name;

        if (Request.QueryString["row"] != null)
        {
            row = Int32.Parse(Request.QueryString["row"]);
        }
        if (Request.QueryString["score"] != null)
        {
            score = Int32.Parse(Request.QueryString["score"]);
        }
        score = score < 1 ? 1 : score;
        score = score > 5 ? 5 : score;

        BrainTrain.Model.bt_gantong_user uM = uBll.GetModel(user);
        if (uM != null) {
            string trainList = uM.trainList;
            string[] trainListArr = trainList.Split(';');
            if (trainListArr.Length > row) {
                string[] trainListArr2 = trainListArr[row].Split(',');
                if (trainListArr2.Length >= 2) {
                    groupName = trainListArr2[0];
                    level = trainListArr2[1];
                };
            }
            age = uM.age;
        }

        //if (Request.QueryString["groupName"] != null)
        //{
        //    groupName = Request.QueryString["groupName"];
        //}
        //if (Request.QueryString["level"] != null)
        //{
        //    level = Request.QueryString["level"];
        //}

        DataTable dt = gBll.GetDataTable("name=\"" + groupName + "\"");
        if (dt.Rows.Count > 0)
        {
            modId = Int32.Parse(dt.Rows[0]["modId"].ToString());
        }


        if (user == "" || row < 0 || groupName == "" || level == "" || modId < 0 || age<=0) {
            return false;
        }
        return true;
    }

    private void addRecord() {
        BrainTrain.Model.bt_gantong_record rM = new BrainTrain.Model.bt_gantong_record();
        rM.name = user;
        rM.groupName = groupName;
        rM.level = Int32.Parse(level);
        rM.modId = modId;
        rM.score = score.ToString();
        rM.time = DateTime.Now;
        rBll.Add(rM);
    }

    private void updateTrainList() {
        List<string> groupChoose = new List<string>();  // 当前年龄段包含的训练组
        // 这里的顺序决定了训练的顺序
        if (age < 6)
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
        // 获取已经训练的分组
        List<string> groupChooseNo = new List<string>();
        DataTable dt = rBll.GetDataTable("name=\"" + user + "\" group by groupName");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            groupChooseNo.Add(dt.Rows[i]["groupName"].ToString());
        }

        BrainTrain.Model.bt_gantong_user uM = uBll.GetModel(user);
        string trainList = uM.trainList;
        string[] trainListArr = trainList.Split(';');

        for (int i = 0; i < trainListArr.Length; i++)
        {
            string[] trainListArr2 = trainListArr[i].Split(',');
            if (trainListArr2.Length >= 2)
            {
                groupChooseNo.Add(trainListArr2[0]);
            }
        }

        // 获取新的训练的组号与难度
        string groupNameNew = "";
        int levelNew = -1;

        dt = cBll.GetDataTable("groupName=\"" + groupName + "\" AND level>" + level + " order by level asc");
        if (dt.Rows.Count > 0 && score>=5)
        {
            // 只需要更新难度
            groupNameNew = dt.Rows[0]["groupName"].ToString();
            levelNew = Int32.Parse(dt.Rows[0]["level"].ToString());
        }
        else
        {
            // 需要更新组号与难度
            List<string> groupChoose2 = new List<string>();  // 当前可以分配的训练组
            dt = gBll.GetDataTable("modId=" + modId + "");
            for (int i = 0; i < groupChoose.Count; i++) {
                string grp = groupChoose[i];
                if (findGroup(dt, grp) && groupChooseNo.IndexOf(grp) < 0)
                {
                    groupChoose2.Add(grp);
                }
            }

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    string grp = dt.Rows[i]["name"].ToString();
            //    if (groupChoose.IndexOf(grp) >= 0 && groupChooseNo.IndexOf(grp) < 0)
            //    {
            //        groupChoose2.Add(grp);
            //    }
            //}

            if (groupChoose2.Count > 0)
            {
                groupNameNew = groupChoose2[0];
                dt = cBll.GetDataTable("groupName=\"" + groupNameNew + "\" order by level asc");
                if (dt.Rows.Count > 0)
                {
                    levelNew = Int32.Parse(dt.Rows[0]["level"].ToString());
                }
            }
        }

        // 更新trainList
        trainList = "";
        for (int i = 0; i < trainListArr.Length; i++)
        {
            if (i != row || levelNew<=0)
            {
                // 当没有搜索到课程时，继续保持原来的课程表
                trainList += trainListArr[i];
            }
            else
            {
                trainList += groupNameNew + "," + levelNew;
            }

            if (i < trainListArr.Length-1)
            {
                trainList += ";";
            }
        }
        uM.trainList = trainList;
        uBll.Update(uM);
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
}