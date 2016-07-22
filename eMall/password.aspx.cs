using BusinussLayer;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eMall
{
    public partial class password : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("default.aspx");

            Master.PwdMenu = "menubuttonselect";
            Master.PwdLink = "linkselect";

            if (!IsPostBack)
            {
                fillSearchScoolCodes();
                fillPassword();
                fillSchoolCodes();
                fillSchoolCodes();
            }
        }

        protected void GridItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridItems.PageIndex = e.NewPageIndex;
            fillPassword();
        }

        protected void GridItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int ItemID = Convert.ToInt32(GridItems.DataKeys[e.RowIndex].Values["id"].ToString());
                eMallBL objBL = new eMallBL();
                string referenceIDMsg = objBL.isReferenceExist(ItemID, "bus_id", "student");
                if (referenceIDMsg != "NO")
                    Response.Write("<script>alert('" + Server.HtmlEncode(referenceIDMsg) + "')</script>");
                else
                {
                    string status = objBL.deletePassword(ItemID);
                    if (status == "success")
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert('Records Deleted Successfully');", true);
                    else
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert('Error... Please try Again');", true);
                }
                fillPassword();
                //btnSave.Text = "Save";
                //Clearfields();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void fillPassword()
        {
            int school_id = 0;
            school_id = Convert.ToInt32(ddlSearchSchoolCodes.SelectedValue == "" ? "1" : ddlSearchSchoolCodes.SelectedValue);

            DataTable dtTbl = new DataTable();
            dtTbl = (new eMallBL()).searchPwd(school_id);
            GridItems.PageSize = Convert.ToInt32(10);
            GridItems.DataSource = dtTbl;
            GridItems.DataBind();
        }

        private void fillSearchScoolCodes()
        {
            try
            {
                eMallEntity.school objSchool = new eMallEntity.school();
                DataTable dtTbl = new DataTable();
                dtTbl = (new eMallBL()).searchSchool(objSchool);
                ddlSearchSchoolCodes.DataSource = dtTbl;
                ddlSearchSchoolCodes.DataTextField = "code";
                ddlSearchSchoolCodes.DataValueField = "id";
                ddlSearchSchoolCodes.DataBind();

                // Insert 'Select'
                ddlSearchSchoolCodes.Items.Insert(0, new ListItem("--Select--", "0"));
                // Set selected index
                ddlSearchSchoolCodes.SelectedIndex = 0;
                if (Session["usertype"] != null && Session["usertype"].ToString() == "2")
                {
                    ddlSearchSchoolCodes.SelectedValue = Session["school_id"].ToString();
                    ddlSearchSchoolCodes.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void fillSchoolCodes()
        {
            try
            {
                eMallEntity.school objSchool = new eMallEntity.school();
                DataTable dtTbl = new DataTable();
                dtTbl = (new eMallBL()).searchSchool(objSchool);
                ddlSchoolCode.DataSource = dtTbl;
                ddlSchoolCode.DataTextField = "code";
                ddlSchoolCode.DataValueField = "id";
                ddlSchoolCode.DataBind();

                // Insert 'Select'
                ddlSchoolCode.Items.Insert(0, new ListItem("--Select--", "0"));
                // Set selected index
                ddlSchoolCode.SelectedIndex = 0;
                if (Session["usertype"] != null && Session["usertype"].ToString() == "2")
                {
                    ddlSchoolCode.SelectedValue = Session["school_id"].ToString();
                    ddlSchoolCode.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillPassword();
            Clearfields();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }


        //Export to Excel from a GridView
        protected void ExportToExcel()
        {
            ////Response.Clear();
            ////Response.Buffer = true;
            ////Response.ContentType = "application/vnd.ms-excel";
            ////Response.AddHeader("content-disposition", "attachment;filename=passwords.xls");
            ////Response.Charset = "";
            ////this.EnableViewState = false;

            ////System.IO.StringWriter sw = new System.IO.StringWriter();
            ////System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

            ////GridItems.RenderControl(htw);

            ////Response.Write(sw.ToString());
            ////Response.End();


            int school_id = 0;
            school_id = Convert.ToInt32(ddlSearchSchoolCodes.SelectedValue == "" ? "1" : ddlSearchSchoolCodes.SelectedValue);

            DataTable dtTbl = new DataTable();
            dtTbl = (new eMallBL()).searchPwd(school_id);
            GridItems.PageSize = Convert.ToInt32(10);
            GridItems.DataSource = dtTbl;
            GridItems.DataBind();

            //dt = city.GetAllCity();//your datatable
            string attachment = "attachment; filename=pwds.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            //foreach (DataColumn dc in dtTbl.Columns)
            //{
            //    if (dc.ColumnName == "password")
            //    {
            //        Response.Write(tab + dc.ColumnName);
            //        tab = "\t";
            //    }
            //}
            Response.Write("\n");
            //int i;
            foreach (DataRow dr in dtTbl.Rows)
            {
                tab = "";
                //for (i = 0; i < dtTbl.Columns.Count; i++)
                //{
                Response.Write(tab + dr["password"].ToString());
                tab = "\t";
                //}
                Response.Write("\n");
            }
            Response.End();

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }

        private void Clearfields()
        {
            txtPwdPrefix.Text = "";
            txtCount.Text = "";
            btnSave.Enabled = true;
            ////txtMobile.Text = "";
            //txtRout.Text = "";
            //btnSave.Text = "Save";
            //hdItemID.Value = "";
            //ddlDriver.SelectedIndex = 0;
            //if (Session["usertype"] == null || Session["usertype"].ToString() != "2")
            //    ddlSchoolCode.SelectedValue = "0";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                    Response.Redirect("default.aspx");
                if (Page.IsValid)
                {
                    btnSave.Enabled = false;
                    eMallBL objBL = new eMallBL();
                    eMallEntity.password objPwd = new eMallEntity.password();
                    string prefix = txtPwdPrefix.Text.Trim();
                    int count = 0;
                    int.TryParse(txtCount.Text.Trim(), out count);
                    int school_id = 0;
                    int.TryParse(ddlSchoolCode.SelectedValue, out school_id);
                    if (count > 50)
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                                "Maximum 50 passwords will be generated in a single request"), true);
                    }
                    else if (prefix.Length > 2 && prefix.Contains(" "))
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                                "Please enter prefix without space"), true);
                    }
                    else if (school_id != 0 && count > 0)
                    {
                        string result = objBL.generatePwd(prefix, count, school_id);
                        fillPassword();

                        if (result == "success")
                        {
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                                "Passwords generated Successfully"), true);
                            Clearfields();
                        }
                        else
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                                "Error Occured, Please try Again"), true);
                    }
                    btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                eMallBL.logErrors("btnSave_Click", ex.GetType().ToString(), ex.Message);
            }
        }
    }
}