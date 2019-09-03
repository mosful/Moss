///<summary>
///========================================================================================================
///Author:  mosslin 
///Date:    2018-06-27
///Version: V
///CR No.:  #578635
///Purpose: 1.畫面增加顯示表單名稱「[FORM.550] BIS End-of-Life Management」與顯示EOL Type及FlowER單號
///         2.修改Function BindHeaderInfo()
///========================================================================================================
///Author:  mosslin 
///Date:    2018-07-05
///Version: V
///CR No.:  #xxxxxx
///Purpose: 1.參數增加Cancel Flower No欄位
///========================================================================================================
///Author:  mosslin 
///Date:    2018-08-08
///Version: V
///CR No.:  #xxxxxx
///Purpose: 1.Cancel時,Purpose改存PM_CANCEL_PURPOSE欄位,故要顯示該欄位資訊
///========================================================================================================
///Author:  mosslin 
///Date:    2018-12-07
///Version: V
///CR No.:  #xxxxxx
///Purpose: 1.增加AppCode欄位,IT BU時才顯示出來
///========================================================================================================
///Author:  mosslin 
///Date:    2019-07-18
///Version: V1.0.
///CR No.:  #635064
///Purpose: 提早轉Lock流程，
///         1.	如為提早轉Lock需求，畫面增加顯示LOCK_START_DATE、PM_WISH_LOCK_DATE
///         2.	修改BindHeaderInfo()，增加判斷如為提早轉Lock需求，需增加顯示LOCK_START_DATE、PM_WISH_LOCK_DATE
///========================================================================================================
///</summary>
using BISLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using NewEOL.Old_App_Code;
using System.Transactions;


namespace NewEOL.WebForm
{
    public partial class EOL_View : BISBase
    {
        string reqEOLNO = "";
        string reqFlowerNO = "";
        string reqCancelFlowerNo = "";
        string reqWebsite = "";
        string strFlowerNo = "";
        string strEOLType = "";
        string strWebSite = "";
        string strAppCode = "";
        //string reqPageCode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            reqEOLNO = Request["eol_no"].ToString();
            reqFlowerNO = Request["form_no"].ToString();
            reqWebsite = Request["BU"].ToString();
            //reqPageCode = Request["PageCode"].ToString();

            // 2018-07-05 mosslin add cancel_form_no
            if (Request["cancel_form_no"] != null && Request["cancel_form_no"].ToString() != "0")
                reqCancelFlowerNo = Request["cancel_form_no"].ToString();
            
            // 一律用strFlowerNo來帶值
            if (reqCancelFlowerNo.ToString().Trim() != "")
            {
                strFlowerNo = reqCancelFlowerNo;
                lblCancel.Visible = true;
                lblCancelTitle.Visible = true;
            }
            else
            {
                strFlowerNo = reqFlowerNO;
            }

            if (!IsPostBack)
            {
                if (reqEOLNO.ToString().Trim() != "" || reqFlowerNO.ToString().Trim() != "")
                {
                    //Bind Header
                    BindHeaderInfo();

                    //Bind Line
                    BindLineInfo();

                    //Bind File
                    BindFileInfo();

                    //Bind Shipment
                    switch (strEOLType.ToString().Trim())
                    {
                        case "LETTER":
                        case "NOTICE":
                            BindShipInfo(this.lblNO.Text.ToString().Trim(), lblApplyDate.Text.ToString().Trim());
                            break;
                        case "LOCK":
                            BindCDMSInfo(this.lblNO.Text.ToString().Trim(), lblApplyDate.Text.ToString().Trim());
                            break;
                    }
                }

                //有flower單號則顯示簽核關卡
                //if (reqFlowerNO.ToString().Trim() != "")
                //{
                //    string approveHisUrl = ConfigurationManager.AppSettings["FlowerApproverUrl"];
                //    string formkind = ConfigurationManager.AppSettings["FlowerFormKind"];
                //    approveHisUrl = approveHisUrl + "?FORM_KIND=" + formkind + "&FORM_NO=" + reqFlowerNO;

                //    trFlower.Visible = true;
                //    IFRAME_APPROVE_HISTORY.Visible = true;
                //    this.IFRAME_APPROVE_HISTORY.Attributes.Add("src", approveHisUrl);
                //}
                //else
                //{
                //    trFlower.Visible = false;
                //    IFRAME_APPROVE_HISTORY.Visible = false;
                //}

                //預設先關起來
                trFlower.Visible = false;
                IFRAME_APPROVE_HISTORY.Visible = false;

                string approveHisUrl = ConfigurationManager.AppSettings["FlowerApproverUrl"];
                string formkind = ConfigurationManager.AppSettings["FlowerFormKind"];

                if (strFlowerNo.Trim() != "")
                    approveHisUrl = approveHisUrl + "?FORM_KIND=" + formkind + "&FORM_NO=" + strFlowerNo.ToString().Trim();

                if (reqFlowerNO.ToString().Trim() != "" || reqCancelFlowerNo.ToString().Trim() != "")
                {
                    trFlower.Visible = true;
                    IFRAME_APPROVE_HISTORY.Visible = true;
                    this.IFRAME_APPROVE_HISTORY.Attributes.Add("src", approveHisUrl);
                }
            }

            //profile = (Profile)Session["profile_tmp"];
        }

        //Bind Header
        #region BindHeaderInfo
        public void BindHeaderInfo()
        {
            // 2018-07-05 mosslin modify 參數改傳strFlowerNo
            //DataTable dt = SysConfig.FetchEOLHeader(reqEOLNO, reqFlowerNO); //dbFun.GetDataTableBySql(sbSQL.ToString(), "BU_DB");
            DataTable dt = SysConfig.FetchEOLHeader(reqEOLNO, strFlowerNo);
            if (dt.Rows.Count > 0)
            {
                // 2018-12-07 mosslin Add Application Code選項                    
                string strShowAppCode = SysConfig.FetchFormat(this.profile.webSite, "ALL", "SWITCH", "SHOW_APPCODE");

                if (strShowAppCode == "Y" && this.profile.webSite == "IT")
                {
                    trApplicationCode.Visible = true;
                    this.lblAppCode.Text = dt.Rows[0]["APP_CODE"].ToString().Trim();                    
                }

                //Title -EOL Type
                this.lblTitle.Text = dt.Rows[0]["TITLE_DESC"].ToString().Trim();
                this.lblTitleField.Text = dt.Rows[0]["TITLE_DESC"].ToString().Trim();
                this.lblTitleType.Text = dt.Rows[0]["TITLE_DESC"].ToString().Trim();

                // 2018-06-27 mosslin add 畫面增加顯示表單名稱「[FORM.550] BIS End-of-Life Management」與顯示EOL Type及FlowER單號
                //this.lblFormTitle.Text = " － " + dt.Rows[0]["TITLE_DESC"].ToString().Trim() + " [" + dt.Rows[0]["FLOWER_NO"].ToString().Trim() + "]";
                if (reqCancelFlowerNo.ToString().Trim() != "")
                    this.lblFormTitle.Text = " － " + dt.Rows[0]["TITLE_DESC"].ToString().Trim() + " Cancel" + " [" + reqCancelFlowerNo.ToString().Trim() + "]";
                else
                    this.lblFormTitle.Text = " － " + dt.Rows[0]["TITLE_DESC"].ToString().Trim() + " [" + dt.Rows[0]["FLOWER_NO"].ToString().Trim() + "]";

                //Apply Date
                this.lblApplyDate.Text = dt.Rows[0]["APPLY_DATE"].ToString().Trim();

                //EOL NO
                lblNO.Text = dt.Rows[0]["EOL_NO"].ToString().Trim();
                
                //申請人中英文名(不用工號)
                lblEmp.Text = dt.Rows[0]["EMP_ENAME"].ToString().Trim() + "-" + dt.Rows[0]["NOTE_NAME"].ToString().Trim();
                //申請人部門
                lblEmpDept.Text = dt.Rows[0]["NOTE_DEPT"].ToString().Trim() + "-" + dt.Rows[0]["ORG_CNAME"].ToString().Trim();
                //Model Version
                lblModel.Text = dt.Rows[0]["MODEL_VERSION"].ToString().Trim();
                //Purpose
                // 2018-08-08 mosslin modify 如果PM_CANCEL_PURPOSE不為空,則改秀PM_CANCEL_PURPOSE
                //lblPurpose.Text = dt.Rows[0]["PM_REMARK"].ToString().Trim().Replace("\n", "<br/>");
                if (dt.Rows[0]["PM_CANCEL_PURPOSE"] != null && dt.Rows[0]["PM_CANCEL_PURPOSE"].ToString() != "")
                    lblPurpose.Text = dt.Rows[0]["PM_CANCEL_PURPOSE"].ToString().Trim().Replace("\n", "<br/>");
                else
                    lblPurpose.Text = dt.Rows[0]["PM_REMARK"].ToString().Trim().Replace("\n", "<br/>");

                //FlowER No
                // 2018-07-05 mosslin modify FlowER No改由傳入參數顯示
                //lblFlowerNo.Text = dt.Rows[0]["FLOWER_NO"].ToString().Trim();
                lblFlowerNo.Text = strFlowerNo.ToString().Trim();
                //Status
                lblStatus.Text = dt.Rows[0]["STATUS"].ToString().Trim();
                //PageCode
                lblPageCode.Text = dt.Rows[0]["PAGECODE"].ToString().Trim();

                //2019-07-18 mosslin add PM 提早轉Lock需求
                lblEarlyLock.Text = dt.Rows[0]["IS_EARLY_LOCK"].ToString().Trim();
                lblLockStartDate.Text = dt.Rows[0]["LOCK_START_DATE"].ToString().Trim();
                lblPMWishLockDate.Text = dt.Rows[0]["PM_WISH_LOCK_DATE"].ToString().Trim();

                #region 控制畫面顯示
                strWebSite = dt.Rows[0]["WEBSITE"].ToString().Trim();
                strAppCode = dt.Rows[0]["APP_CODE"].ToString().Trim();
                strEOLType = dt.Rows[0]["EOL_TYPE"].ToString().Trim();

                switch (strEOLType)
                {
                    case "PRE_EOL":
                        this.trReplace.Visible = false;
                        this.trSchedule.Visible = false;
                        this.trCDMS.Visible = false;    //CDMS for Lock
                        this.trShip.Visible = false;    //Ship for Notice
                        this.trCustomer.Visible = false;    //Customer for Letter
                        break;
                    case "LETTER":
                    case "NOTICE":
                        if (strEOLType == "LETTER")
                        {
                            trCustomer.Visible = true;
                            lblCustomer.Text = dt.Rows[0]["CUSTOMER"].ToString().Trim();
                        }
                        else
                        {
                            trCustomer.Visible = false;
                        }
                        lblOfficial.Text = dt.Rows[0]["OFFICIAL"].ToString().Trim();
                        lblFeedback.Text = dt.Rows[0]["FEEDBACK"].ToString().Trim();
                        lblLastBuy.Text = dt.Rows[0]["LAST_BUY"].ToString().Trim();
                        //檢查是否需顯示EOL_DATE
                        DataTable dtEOL = SysConfig.FetchFormatTable(strWebSite, strAppCode, "EOL_DATE");
                        if (dtEOL != null && dtEOL.Rows.Count > 0)
                        {
                            this.trEOLDate.Visible = true; //顯示EOL_DATE
                            lblEOLDate.Text = dt.Rows[0]["EOL_DATE"].ToString().Trim();
                        }
                        else
                        {
                            this.trEOLDate.Visible = false; //隱藏
                        }
                        this.trCDMS.Visible = false;    //CDMS for Lock
                        this.trShip.Visible = true;    //Ship for Notice
                        //Replacement Model
                        if (dt.Rows[0]["SHOW_REPLACE"].ToString().Trim() != "")
                        {
                            //Show on EOL document
                            if (dt.Rows[0]["SHOW_REPLACE"].ToString().Trim() == "Y")
                                chkShowDoc.Checked = true;
                            else
                                chkShowDoc.Checked = false;
                            chkShowDoc.Enabled = false; //不可異動

                            List<SysConfig.ReplaceModel> lsReplace = new List<SysConfig.ReplaceModel>(20);

                            //拆解多筆資料，以逗號分隔
                            string[] strReplace = dt.Rows[0]["REPLACE_MODEL"].ToString().Trim().Split(',');

                            //寫入資料
                            foreach (string s in strReplace)
                            {
                                SysConfig.ReplaceModel objReplace = new SysConfig.ReplaceModel();
                                if (s.IndexOf("_") > 0)
                                {
                                    objReplace.MODEL_VERSION = s.Substring(0, s.IndexOf("_"));
                                    objReplace.PART_NO = s.Substring(s.IndexOf("_") + 1);
                                    lsReplace.Add(objReplace);
                                }
                            }

                            //Bind
                            gvReplace.DataSource = lsReplace;// Session["TEMP_REPLACE_MODEL"];
                            gvReplace.DataBind();
                        }
                        else
                        {
                            gvReplace.DataSource = null;
                            gvReplace.DataBind();
                            chkShowDoc.Visible = false;
                        }
                        break;
                    case "LOCK":
                        this.trReplace.Visible = false;
                        // 2018-07-05 mosslin open trSchedule,show Last Buy Date
                        //this.trSchedule.Visible = false; 
                        trOfficialEOLNotice.Visible = false;
                        trFeedback.Visible = false;
                        lblLastBuy.Text = dt.Rows[0]["LAST_BUY"].ToString().Trim();
                        //檢查是否需顯示EOL_DATE
                        DataTable dtEOL2 = SysConfig.FetchFormatTable(strWebSite, strAppCode, "EOL_DATE");
                        if (dtEOL2 != null && dtEOL2.Rows.Count > 0)
                        {
                            this.trEOLDate.Visible = true; //顯示EOL_DATE
                            lblEOLDate.Text = dt.Rows[0]["EOL_DATE"].ToString().Trim();
                        }
                        else
                        {
                            this.trEOLDate.Visible = false; //隱藏
                        }
                        this.trCDMS.Visible = true;    //CDMS for Lock
                        this.trShip.Visible = false;    //Ship for Notice
                        this.trCustomer.Visible = false;    //Customer for Letter
                        //2019-07-18 mosslin add
                        if (lblEarlyLock.Text == "Y")
                        {
                            trLockStartDate.Visible = true;
                            trPMWishLockDate.Visible = true;
                        }
                        break;
                    default:
                        break;
                }
                #endregion 

                //SetProfile();
            }
        }
        #endregion

        //Bind Line
        #region BindLineInfo
        public void BindLineInfo()
        {
            DBFun dbFun = new DBFun();
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT PART_NO,REF_EOL_NO,REF_EOL_TYPE,REF_EOL_STATUS,GPOS_STATUS ");
            sbSQL.Append("  FROM EOL_LINE");
            sbSQL.Append(" WHERE EOL_NO = '" + lblNO.Text.ToString().Trim() + "'");
            sbSQL.Append("   AND WEBSITE= '" + reqWebsite.Trim() + "'");             //2019-03-11 mosslin 加卡BU,避免出現重複資料
            DataTable dt = dbFun.GetDataTableBySql(sbSQL.ToString(), "BU_DB");
            if (dt.Rows.Count > 0)
            {
                gvPartNo.DataSource = dt;
            }
            else
            {
                gvPartNo.DataSource = null;
            }

            gvPartNo.DataBind();
        }
        #endregion

        //Bind File
        #region BindFileInfo
        public void BindFileInfo()
        {
            //Bind File
            DataTable dt = SysConfig.FetchFileInfo(lblNO.Text.ToString().Trim(), "FILE");
            if (dt.Rows.Count > 0)
            {
                this.gvFile.DataSource = dt;
            }
            else
            {
                this.gvFile.DataSource = null;
            }
            this.gvFile.DataBind();

            //Bind Picture
            DataTable dtPic = SysConfig.FetchFileInfo(lblNO.Text.ToString().Trim(), "PIC");
            if (dtPic.Rows.Count > 0)
            {
                this.gvPic.DataSource = dtPic;
                this.trPic.Visible = true;
            }
            else
            {
                this.gvPic.DataSource = null;
                this.trPic.Visible = false;
            }
            this.gvPic.DataBind();
        }
        #endregion

        //Bind Shipment (for Notice)
        #region BindShipment
        public void BindShipInfo(string strEOLNo, string strApplyDate)
        {
            string strMon = "", strPivot = "", strPvtHeader = "";
            DateTime dateFcst = Convert.ToDateTime(strApplyDate);
            DateTime dateShip = Convert.ToDateTime(strApplyDate).AddMonths(-1);
            #region Shipment
            //月份 過去12個月(不含當月)
            for (int i = 0; i < 12; i++)
            {
                strMon = dateShip.Year.ToString("0000") + dateShip.Month.ToString("00");
                strPivot = "[" + strMon + "]," + strPivot;
                strPvtHeader = "CEILING([" + strMon + "]) AS MON" + (12 - i).ToString() + "," + strPvtHeader;
                dateShip = dateShip.AddMonths(-1);

                //指定Gridview Header
                gvShipment.Columns[17 - i].HeaderText = strMon;
            }
            //去掉最後的逗號
            strPivot = strPivot.Substring(0, strPivot.Length - 1);
            strPvtHeader = strPvtHeader.Substring(0, strPvtHeader.Length - 1);

            //Shipment
            DataTable dt = SysConfig.FetchEOLShipPlan("Shipment", strEOLNo, strPivot, strPvtHeader);
            if (dt.Rows.Count > 0)
            {
                this.gvShipment.DataSource = dt;
                this.lblShipment.Visible = true;
            }
            else
            {
                this.gvShipment.DataSource = null;
                this.lblShipment.Visible = false;
            }
            gvShipment.DataBind();
            #endregion

            //Allocation
            #region Allocation/Forecast/BP Customer
            strMon = ""; strPivot = ""; strPvtHeader = "";
            //月份 未來六個月(含當月)
            for (int i = 0; i < 6; i++)
            {
                strMon = dateFcst.Year.ToString("0000") + dateFcst.Month.ToString("00");
                strPivot += "[" + strMon + "],";
                strPvtHeader += "CEILING([" + strMon + "]) AS MON" + (i + 1).ToString() + ",";
                dateFcst = dateFcst.AddMonths(1);

                //指定Gridview Header
                gvAllocation.Columns[5 + i].HeaderText = strMon;
                //gvForecast.Columns[5 + i].HeaderText = strMon;
                //gvBP.Columns[5 + i].HeaderText = strMon;
            }
            //去掉最後的逗號                
            strPivot = strPivot.Substring(0, strPivot.Length - 1);
            strPvtHeader = strPvtHeader.Substring(0, strPvtHeader.Length - 1);
            DataTable dtA = SysConfig.FetchEOLShipPlan("Allocation", strEOLNo, strPivot, strPvtHeader);
            if (dtA.Rows.Count > 0)
            {
                this.gvAllocation.DataSource = dtA;
                this.lblAllocation.Visible = true;
            }
            else
            {
                this.gvAllocation.DataSource = null;
                this.lblAllocation.Visible = false;
            }
            gvAllocation.DataBind();


            ////Forecast
            //DataTable dtF = SysConfig.FetchEOLShipPlan("Forecast", strEOLNo, strPivot, strPvtHeader);
            //if (dtF.Rows.Count > 0)
            //{
            //    this.gvForecast.DataSource = dtF;
            //    this.lblForecast.Visible = true;
            //}
            //else
            //{
            //    this.gvForecast.DataSource = null;
            //    this.lblForecast.Visible = false;
            //}
            //gvForecast.DataBind();

            ////BP Customer
            //DataTable dtB = SysConfig.FetchEOLShipPlan("BP", strEOLNo, strPivot, strPvtHeader);
            //if (dtB.Rows.Count > 0)
            //{
            //    this.gvBP.DataSource = dtB;
            //    this.lblBP.Visible = true;
            //}
            //else
            //{
            //    this.gvBP.DataSource = null;
            //    this.lblBP.Visible = false;
            //}
            //gvBP.DataBind();
            #endregion

        }
        #endregion

        //Bind CDMS (for Lock)
        #region BindCDMS
        public void BindCDMSInfo(string strEOLNo, string strApplyDate)
        {
            string strMon = "", strPivot = "", strPvtHeader = "";
            DateTime dateFcst = Convert.ToDateTime(strApplyDate);

            //Allocation
            #region Allocation/Forecast/BP Customer
            strMon = ""; strPivot = ""; strPvtHeader = "";
            //月份 未來六個月(含當月)
            for (int i = 0; i < 6; i++)
            {
                strMon = dateFcst.Year.ToString("0000") + dateFcst.Month.ToString("00");
                strPivot += "[" + strMon + "],";
                strPvtHeader += "[" + strMon + "] AS MON" + (i + 1).ToString() + ",";
                dateFcst = dateFcst.AddMonths(1);

                //指定Gridview Header
                gvCDMS.Columns[5 + i].HeaderText = strMon;
            }
            //去掉最後的逗號                
            strPivot = strPivot.Substring(0, strPivot.Length - 1);
            strPvtHeader = strPvtHeader.Substring(0, strPvtHeader.Length - 1);

            //Forecast
            DataTable dtF = SysConfig.FetchEOLShipPlan("Forecast", strEOLNo, strPivot, strPvtHeader);
            if (dtF.Rows.Count > 0)
            {
                this.gvCDMS.DataSource = dtF;
            }
            else
            {
                this.gvCDMS.DataSource = null;
            }
            gvCDMS.DataBind();

            #endregion

        }
        #endregion

        //gvFile Controls
        #region gvFile Controls
        protected void gvFile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = ((GridView)sender).Rows[int.Parse(e.CommandArgument.ToString())];
            if (row != null)
            {
                FileFun fnFile = new FileFun();

                switch (e.CommandName)
                {
                    #region DownLoad
                    case ("DownLoad"):
                        try
                        {
                            string strFileID = row.Cells[3].Text.ToString().Trim();
                            string strFileName = row.Cells[1].Text.ToString().Trim();
                            byte[] stream = fnFile.GetFLMFile(strFileID, strFileName, profile, "Y");

                            if (stream.Length > 0)
                            {
                                Response.Clear();
                                Response.Charset = "BIG-5";
                                Response.ContentEncoding = Encoding.UTF7;
                                Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(strFileName, Encoding.UTF8).Replace("+", "_"));
                                Response.ContentType = "applicant/octet-stream";
                                Page.Response.BinaryWrite(stream);
                                Page.Response.End();
                            }
                        }
                        catch (Exception ex)
                        {
                            //new DBFun().DealLog(sFunction + "_DownLoad", "DI_NO=" + strProjectId + ", fail Msg=" + ex.ToString());
                            //Response.Write("Download Error :" + ex.Message);
                        }
                    break;

                #endregion
                }
            }
        }
        
        protected void gvFile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    ((ImageButton)e.Row.FindControl("lnkDownLoad")).CommandArgument = e.Row.RowIndex.ToString();
                }
            }
            //隱藏FILE_ID
            e.Row.Cells[3].Visible = false;
        }
        #endregion

        //gvPic Controls
        #region gvPic Controls
        protected void gvPic_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string sFunction = "gvPic_RowCommand";
            GridViewRow row = ((GridView)sender).Rows[int.Parse(e.CommandArgument.ToString())];
            if (row != null)
            {
                FileFun fnFile = new FileFun();

                switch (e.CommandName)
                {
                    #region DownLoad
                    case ("DownLoad"):
                        try
                        {
                            string strFileID = row.Cells[3].Text.ToString().Trim();
                            string strFileName = row.Cells[1].Text.ToString().Trim();
                            byte[] stream = fnFile.GetFLMFile(strFileID, strFileName, profile, "Y");

                            if (stream.Length > 0)
                            {
                                Response.Clear();
                                Response.Charset = "BIG-5";
                                Response.ContentEncoding = Encoding.UTF7;
                                Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(strFileName, Encoding.UTF8).Replace("+", "_"));
                                Response.ContentType = "applicant/octet-stream";
                                Page.Response.BinaryWrite(stream);
                                Page.Response.End();
                            }
                        }
                        catch (Exception ex)
                        {
                            new DBFun().DealLog(sFunction + "_DownLoad", "EOL_NO=" + lblNO.Text.ToString().Trim() + ", fail Msg=" + ex.ToString());
                            //Response.Write("Download Error :" + ex.Message);
                        }
                        break;

                    #endregion
                }
            }
        }

        protected void gvPic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    ((ImageButton)e.Row.FindControl("lnkDownLoad")).CommandArgument = e.Row.RowIndex.ToString();
                }
            }
            //隱藏FILE_ID
            e.Row.Cells[3].Visible = false;
        }
        #endregion


        #region gvPartNo_RowDataBound
        protected void gvPartNo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //EOL簽核完成才顯示CheckBox
            if (lblStatus.Text.ToString().Trim() != "Approved")
            {
                e.Row.Cells[0].Visible = false;
                //btnToNotice.Visible = false;
            }
            else
            {
                e.Row.Cells[0].Visible = true;
                //btnToNotice.Visible = true;

                //Check EOL Status
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //判斷gpos狀態，只有在non-EOL(N) or unlock(U)狀態可才以申請
                    string strApply = SysConfig.FetchFormat("ALL", "ALL", "TRANS_TO_NOTICE", e.Row.Cells[5].Text.ToString().Trim());
                    if (strApply == "N")    //N表示不可轉Notice
                    {
                        //Disable Checkbox
                        e.Row.Cells[0].Enabled = false;
                        e.Row.Cells[1].Enabled = false;
                    }
                    else
                    {
                        //Checkbox
                        e.Row.Cells[0].Enabled = true;
                        e.Row.Cells[1].Enabled = true;
                    }
                }
            }
        }
        #endregion

        #region SetProfile
        private void SetProfile()
        {
            FlowERUtilityHelper FlowER_Helper = new FlowERUtilityHelper();
            string strApprover = FlowER_Helper.GetCurrentApprover("AUO.FORM.550", Convert.ToInt32(this.lblFlowerNo.Text.ToString().Trim()));
            
            //get pageCode
            string pageCode = lblPageCode.Text.ToString().Trim();

            //Get Login_name
            DataTable dtUser = SysConfig.FetchUser(strApprover);
            string strEmpName = "";
            if (dtUser.Rows.Count > 0)
                strEmpName = dtUser.Rows[0]["login_name"].ToString();

            // 取得傳送內容
            Profile profile = new Profile();
            profile.Set_Profile(strEmpName, pageCode);
            Session["profile_tmp"] = profile;

        }
        #endregion

    }
}