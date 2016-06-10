using BusinussLayer;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eMall
{
    public partial class events : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("default.aspx");
            lblError.Text = "";
            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
            Master.AboutUsMenu = "menubuttonselect";
            Master.AboutUsLink = "linkselect";
            if (!IsPostBack)
            {
                fillSearchScoolCodes();
                fillEvents();
                fillSchoolCodes();
            }
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

        private void fillEvents()
        {
            eMallEntity.events objEvents = new eMallEntity.events();
            objEvents.status = Convert.ToInt32(ddlSearchStatus.SelectedValue == "" ? "0" : ddlSearchStatus.SelectedValue);
            objEvents.title = txtSearch.Text.Trim();
            objEvents.description = txtSearch.Text.Trim();
            objEvents.school_id = Convert.ToInt32(ddlSearchSchoolCodes.SelectedValue == "" ? "1" : ddlSearchSchoolCodes.SelectedValue);
            objEvents.search_operator = "OR";

            DataTable dtTbl = new DataTable();
            dtTbl = (new eMallBL()).searchEvents(objEvents);
            GridItems.PageSize = Convert.ToInt32(10);
            GridItems.DataSource = dtTbl;
            GridItems.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillEvents();
            Clearfields();
        }

        private void Clearfields()
        {
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            btnSave.Text = "Save";
            hdItemID.Value = "";
            ddlStatus.SelectedIndex = 0;
            if (Session["usertype"] == null || Session["usertype"].ToString() != "2")
                ddlSchoolCode.SelectedValue = "0";            
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Clearfields();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                    Response.Redirect("default.aspx");
                if (Page.IsValid)
                {
                    eMallBL objBL = new eMallBL();
                    eMallEntity.events objEvents = new eMallEntity.events();
                    objEvents.title = txtTitle.Text.Trim().Replace("'", "''");
                    objEvents.description = txtDescription.Text.Trim().Replace("'", "''");
                    objEvents.status = Convert.ToInt32(ddlStatus.SelectedValue);
                    objEvents.start_date = DateTime.ParseExact(txtStartDate.Text.Trim().Replace("'", "''"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    objEvents.end_date = DateTime.ParseExact(txtEndDate.Text.Trim().Replace("'", "''"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    objEvents.ID = String.IsNullOrEmpty(hdItemID.Value) ? 0 : int.Parse(hdItemID.Value);

                    objEvents.school_id = Convert.ToInt32(ddlSchoolCode.SelectedValue == "" ? "1" : ddlSchoolCode.SelectedValue);
                    objEvents.created_by = String.IsNullOrEmpty(Session["user_id"].ToString()) ? 0 : int.Parse(Session["user_id"].ToString());
                    
                    //if (objBL.isTeacherCodeAlreadyExist(objEvents))
                    //{
                    //    lblError.Text = "Teacher's Code Already Exist";
                    //    return;
                    //}

                    string result = objBL.insertEvent(objEvents);
                    fillEvents();
                    if (result == "success")
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                            "Event details Added/Updated Successfully"), true);
                        Clearfields();
                    }
                    else
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                            "Error Occured, Please try Again"), true);
                }
            }
            catch (Exception ex)
            {
                eMallBL.logErrors("btnSave_Click", ex.GetType().ToString(), ex.Message);
            }
        }

        protected void GridItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridItems.PageIndex = e.NewPageIndex;
            fillEvents();
        }

        protected void GridItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ItemID = Convert.ToInt32(GridItems.DataKeys[e.RowIndex].Values["id"].ToString());
            eMallBL objBL = new eMallBL();
            string status = objBL.deleteEvent(ItemID);
            if (status == "success")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert('Records Deleted Successfully');", true);
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert('Error... Please try Again');", true);
            fillEvents();
            btnSave.Text = "Save";
            Clearfields();
        }
        
        protected void GridItems_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ////hdItemID.Value = GridItems.DataKeys[index].Values["id"].ToString();
            ////txtTitle.Text = GridItems.DataKeys[e.NewEditIndex].Values["title"].ToString();
            ////txtDescription.Text = GridItems.DataKeys[e.NewEditIndex].Values["description"].ToString();
            ////ddlStatus.SelectedValue = GridItems.DataKeys[e.NewEditIndex].Values["status"].ToString();
            ////txtStartDate.Text = Convert.ToDateTime(GridItems.DataKeys[e.NewEditIndex].Values["start_date"].ToString()).ToString("dd/MM/yyy");
            ////txtEndDate.Text = Convert.ToDateTime(GridItems.DataKeys[e.NewEditIndex].Values["end_date"].ToString()).ToString("dd/MM/yyy");
            ////ddlSchoolCode.SelectedValue = GridItems.DataKeys[e.NewEditIndex].Values["school_id"].ToString();

            ////btnSave.Text = "Update";
            ////btnAddNew.Enabled = true;
        }

        protected void GridItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    foreach (DataControlField dcf in GridItems.Columns)
            //    {
            //        if (dcf.ToString() == "Delete")
            //        {
            //            if (((CommandField)dcf).ShowDeleteButton == true)
            //            {
            //                e.Row.Cells[GridItems.Columns.IndexOf(dcf)]. Attributes["onclick"] = "if(!confirm('Do you want to delete?')){ return false; };";
            //            }
            //        }
            //    }
            //}
        }

        protected void GridItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Change")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridItems.Rows[index];

                //===========================================================
                hdItemID.Value = GridItems.DataKeys[index].Values["id"].ToString();
                txtTitle.Text = GridItems.DataKeys[index].Values["title"].ToString();
                txtDescription.Text = GridItems.DataKeys[index].Values["description"].ToString();
                ddlStatus.SelectedValue = GridItems.DataKeys[index].Values["status"].ToString();
                txtStartDate.Text = Convert.ToDateTime(GridItems.DataKeys[index].Values["start_date"].ToString()).ToString("dd/MM/yyy");
                txtEndDate.Text = Convert.ToDateTime(GridItems.DataKeys[index].Values["end_date"].ToString()).ToString("dd/MM/yyy");
                ddlSchoolCode.SelectedValue = GridItems.DataKeys[index].Values["school_id"].ToString();

                btnSave.Text = "Update";
                btnAddNew.Enabled = true;
                //=========================================================
            }
        }
    }
}