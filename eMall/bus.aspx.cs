﻿using BusinussLayer;
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
    public partial class bus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("default.aspx");

            Master.BusMenu = "menubuttonselect";
            Master.BusLink = "linkselect";

            if (!IsPostBack)
            {
                fillSearchScoolCodes();
                fillbus();
                fillDriver();
                fillSchoolCodes();
            }
        }

        private void fillDriver()
        {
            try
            {
                DataTable dtTbl = new DataTable();
                int school_id = Convert.ToInt32(ddlSchoolCode.SelectedValue == "" ? "0" : ddlSchoolCode.SelectedValue);
                dtTbl = (new eMallBL()).getDriver(school_id);
                ddlDriver.DataSource = dtTbl;
                ddlDriver.DataTextField = "name";
                ddlDriver.DataValueField = "id";
                ddlDriver.DataBind();

                // Insert 'Select'
                ddlDriver.Items.Insert(0, new ListItem("--Select--", "0"));
                // Set selected index
                ddlDriver.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw;
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

        private void fillbus()
        {
            eMallEntity.bus objBus = new eMallEntity.bus();
            //objEvents.status = Convert.ToInt32(ddlSearchStatus.SelectedValue == "" ? "0" : ddlSearchStatus.SelectedValue);
            objBus.bus_number = txtSearch.Text.Trim();
            objBus.rout = txtSearch.Text.Trim();
            objBus.school_id = Convert.ToInt32(ddlSearchSchoolCodes.SelectedValue == "" ? "1" : ddlSearchSchoolCodes.SelectedValue);
            objBus.driver_id = Convert.ToInt32(ddlDriver.SelectedValue == "" ? "0" : ddlDriver.SelectedValue);

            objBus.search_operator = "OR";

            DataTable dtTbl = new DataTable();
            dtTbl = (new eMallBL()).searchBus(objBus);
            GridItems.PageSize = Convert.ToInt32(10);
            GridItems.DataSource = dtTbl;
            GridItems.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillbus();
            Clearfields();
        }

        private void Clearfields()
        {
            txtBusNumber.Text = "";
            //txtDriver.Text = "";
            //txtMobile.Text = "";
            txtRout.Text = "";
            btnSave.Text = "Save";
            hdItemID.Value = "";
            ddlDriver.SelectedIndex = 0;
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
                    eMallEntity.bus objBus = new eMallEntity.bus();
                    objBus.bus_number = txtBusNumber.Text.Trim().Replace("'", "''");
                    //objBus.driver = txtDriver.Text.Trim().Replace("'", "''");
                    //objBus.mobile = txtMobile.Text.Trim().Replace("'", "''");
                    objBus.rout = txtRout.Text.Trim().Replace("'", "''");
                    objBus.school_id = Convert.ToInt32(ddlSchoolCode.SelectedValue == "" ? "1" : ddlSchoolCode.SelectedValue);
                    objBus.driver_id = Convert.ToInt32(ddlDriver.SelectedValue == "" ? "1" : ddlDriver.SelectedValue);
                    objBus.ID = String.IsNullOrEmpty(hdItemID.Value) ? 0 : int.Parse(hdItemID.Value);

                    string result = objBL.insertBus(objBus);
                    fillbus();
                    if (result == "success")
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                            "Bus details Added/Updated Successfully"), true);
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
            fillbus();
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
                    string status = objBL.deleteBus(ItemID);
                    if (status == "success")
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert('Records Deleted Successfully');", true);
                    else
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert('Error... Please try Again');", true);
                }
                fillbus();
                btnSave.Text = "Save";
                Clearfields();
            }
            catch (Exception ex)
            {                
                throw;
            }
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
                txtBusNumber.Text = GridItems.DataKeys[index].Values["bus_number"].ToString();
                txtRout.Text = GridItems.DataKeys[index].Values["rout"].ToString();

                //txtDriver.Text = GridItems.DataKeys[index].Values["driver"].ToString();
                //txtMobile.Text = GridItems.DataKeys[index].Values["driver_mobile"].ToString();
                ddlSchoolCode.SelectedValue = GridItems.DataKeys[index].Values["school_id"].ToString();

                fillDriver();
                ddlDriver.SelectedValue = GridItems.DataKeys[index].Values["driver_id"].ToString();

                btnSave.Text = "Update";
                btnAddNew.Enabled = true;
                //=========================================================
            }
        }

        protected void ddlSchoolCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillDriver();
        }
    }
}