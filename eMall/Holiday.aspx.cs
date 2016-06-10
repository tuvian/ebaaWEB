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
    public partial class Holiday : System.Web.UI.Page
    {
        /// <summary>
        /// page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserName"] == null)
                Response.Redirect("default.aspx");

            Master.HolidayMenu = "menubuttonselect";
            Master.HolidaLink = "linkselect";

            if (!IsPostBack)
            {
                FillData();
                fillSchoolCodes();
                fillSearchScoolCodes();
            }
        }

        /// <summary>
        /// Save Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    HolidayBL objBL = new HolidayBL();
                    eMallEntity.Holiday objHoliday = new eMallEntity.Holiday();
                    objHoliday.Name = txtHolidayName.Text.Trim().Replace("'", "''");
                    objHoliday.Desccription = txtDescription.Text.Trim().Replace("'", "''");
                    objHoliday.Status = Convert.ToInt32(ddlStatus.SelectedValue);
                    objHoliday.FromDate = DateTime.ParseExact(txtFromDate.Text.Trim().Replace("'", "''"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    objHoliday.ToDate = DateTime.ParseExact(txtToDate.Text.Trim().Replace("'", "''"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    objHoliday.ID = String.IsNullOrEmpty(hdItemID.Value) ? 0 : int.Parse(hdItemID.Value);

                    objHoliday.SchoolId = Convert.ToInt32(ddlSchoolCode.SelectedValue == "" ? "1" : ddlSchoolCode.SelectedValue);
                    objHoliday.Type = 1;
                    string userId = string.Empty;
                    if (Session["user_id"] != null)
                        userId = String.IsNullOrEmpty(Session["user_id"].ToString()) ? string.Empty : Session["user_id"].ToString();
                    objHoliday.CreatedBy = objHoliday.UpdatedBy = userId;

                    //if (objBL.isTeacherCodeAlreadyExist(objEvents))
                    //{
                    //    lblError.Text = "Teacher's Code Already Exist";
                    //    return;
                    //}
                    int result = 0;
                    if(objHoliday.ID == 0)
                        result = objBL.Insert(objHoliday);
                    else
                        result = objBL.Update(objHoliday);

                    FillData();
                    if (result > 0 )
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                            "Holiday details Added/Updated Successfully"), true);
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

        /// <summary>
        /// Add New Clear all functions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Clearfields();
        }

        /// <summary>
        /// Seatch based on filter criteria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int status = Convert.ToInt32(ddlSearchStatus.SelectedValue);
            int schoolId = Convert.ToInt32(ddlSearchSchoolCodes.SelectedValue);
            string searchText = txtSearch.Text.Trim();
            if (status == 0 && schoolId == 0 && txtSearch.Text.Trim() == string.Empty)
                FillData();
            else         
            {
                DataTable holidays = new HolidayBL().GetHolidaysByFilter(status, schoolId, searchText);
                GridItems.DataSource = holidays;
                GridItems.DataBind();
            }
           
            
            Clearfields();
        }

        #region Private Methods

        /// <summary>
        /// Fill DAta 
        /// </summary>
        private void FillData()
        {            
            DataTable holidays = new HolidayBL().GetHolidays();
            GridItems.DataSource = holidays;
            GridItems.DataBind();
        }

        protected void GridItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridItems.PageIndex = e.NewPageIndex;
            FillData();
        }

        /// <summary>
        /// Delete the row..
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ItemID = Convert.ToInt32(GridItems.DataKeys[e.RowIndex].Values["id"].ToString());
            HolidayBL objBL = new HolidayBL();
            int status = objBL.Delete(ItemID);
            if (status >= 1)
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert('Records Deleted Successfully');", true);
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert('Error... Please try Again');", true);
            FillData();
            btnSave.Text = "Save";
            Clearfields();
            FillData();
        }

        protected void GridItems_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //hdItemID.Value = GridItems.DataKeys[index].Values["id"].ToString();
            //txtHolidayName.Text = GridItems.DataKeys[index].Values["name"].ToString();
            //txtDescription.Text = GridItems.DataKeys[e.NewEditIndex].Values["description"].ToString();
            //ddlStatus.SelectedValue = GridItems.DataKeys[e.NewEditIndex].Values["status"].ToString();
            //txtFromDate.Text =  !string.IsNullOrEmpty(GridItems.DataKeys[e.NewEditIndex].Values["fromDate"].ToString()) ?
            //     Convert.ToDateTime(GridItems.DataKeys[e.NewEditIndex].Values["fromDate"].ToString()).ToString("dd/MM/yyy") : string.Empty;

            //txtToDate.Text = !string.IsNullOrEmpty(GridItems.DataKeys[e.NewEditIndex].Values["toDate"].ToString()) ?
            //    Convert.ToDateTime(GridItems.DataKeys[e.NewEditIndex].Values["toDate"].ToString()).ToString("dd/MM/yyy") : string.Empty;
            //ddlSchoolCode.SelectedValue = GridItems.DataKeys[e.NewEditIndex].Values["schoolId"].ToString();

            //btnSave.Text = "Update";
            //btnAddNew.Enabled = true;
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
            //                e.Row.Cells[GridItems.Columns.IndexOf(dcf)].Attributes["onclick"] = "if(!confirm('Do you want to delete?')){ return false; };";
            //            }
            //        }
            //    }
            //}
        }


        /// <summary>
        /// Clear Fields
        /// </summary>
        private void Clearfields()
        {
            txtHolidayName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            btnSave.Text = "Save";
            hdItemID.Value = string.Empty;
            ddlStatus.SelectedIndex = 0;
            if (Session["usertype"] == null || Session["usertype"].ToString() != "2")
                ddlSchoolCode.SelectedValue = "0";
        }

        /// <summary>
        /// Fill Search school codes
        /// </summary>
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


        /// <summary>
        /// Fill School Codes
        /// </summary>
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

        #endregion

        protected void GridItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Change")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridItems.Rows[index];

                //===========================================================
                hdItemID.Value = GridItems.DataKeys[index].Values["id"].ToString();
                txtHolidayName.Text = GridItems.DataKeys[index].Values["name"].ToString();
                txtDescription.Text = GridItems.DataKeys[index].Values["description"].ToString();
                ddlStatus.SelectedValue = GridItems.DataKeys[index].Values["status"].ToString();
                txtFromDate.Text = !string.IsNullOrEmpty(GridItems.DataKeys[index].Values["fromDate"].ToString()) ?
                     Convert.ToDateTime(GridItems.DataKeys[index].Values["fromDate"].ToString()).ToString("dd/MM/yyy") : string.Empty;

                txtToDate.Text = !string.IsNullOrEmpty(GridItems.DataKeys[index].Values["toDate"].ToString()) ?
                    Convert.ToDateTime(GridItems.DataKeys[index].Values["toDate"].ToString()).ToString("dd/MM/yyy") : string.Empty;
                ddlSchoolCode.SelectedValue = GridItems.DataKeys[index].Values["schoolId"].ToString();

                btnSave.Text = "Update";
                btnAddNew.Enabled = true;
                //=========================================================
            }
        }
    }
}