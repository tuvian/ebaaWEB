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
    public partial class driver : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("default.aspx");
            lblError.Text = "";            
            Master.DriverMenu = "menubuttonselect";
            Master.DriverLink = "linkselect";
            if (!IsPostBack)
            {
                fillSearchScoolCodes();
                fillDrivers();
                fillSchoolCodes();
            }
        }

        private void fillDrivers()
        {
            eMallEntity.driver objDriver = new eMallEntity.driver();
            objDriver.school_id = Convert.ToInt32(ddlSearchSchoolCodes.SelectedValue == "" ? "1" : ddlSearchSchoolCodes.SelectedValue);

            DataTable dtTbl = new DataTable();
            dtTbl = (new eMallBL()).searchDriver(objDriver);
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
            fillDrivers();
            Clearfields();
        }

        private void Clearfields()
        {
            txtName.Text = "";
            txtMobile.Text = "";
            txtAddress.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            btnSave.Text = "Save";
            hdItemID.Value = "";
            //ddlSchoolCode.SelectedIndex = 0;
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
                    eMallEntity.driver objDriver = new eMallEntity.driver();
                    objDriver.name = txtName.Text.Trim().Replace("'", "''");
                    objDriver.mobile = txtMobile.Text.Trim().Replace("'", "''");
                    objDriver.address = txtAddress.Text.Trim().Replace("'", "''");
                    objDriver.username = txtUsername.Text.Trim().Replace("'", "''");
                    objDriver.password = txtPassword.Text.Trim().Replace("'", "''");
                    objDriver.ID = String.IsNullOrEmpty(hdItemID.Value) ? 0 : int.Parse(hdItemID.Value);
                    objDriver.school_id = Convert.ToInt32(ddlSchoolCode.SelectedValue == "" ? "1" : ddlSchoolCode.SelectedValue);
                    //objDriver.created_by = String.IsNullOrEmpty(Session["user_id"].ToString()) ? 0 : int.Parse(Session["user_id"].ToString());

                    //if (objBL.isTeacherCodeAlreadyExist(objEvents))
                    //{
                    //    lblError.Text = "Teacher's Code Already Exist";
                    //    return;
                    //}

                    if (objBL.isUserNameAlreadyExist(objDriver.username, objDriver.school_id,objDriver.ID))
                    {
                        lblError.Text = "Username Already Exist, Please select another username.";
                        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                        //    "Teacher's Code Already Exist"), true);

                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", string.Format("alert('{0}');",
                        //    "Teacher's Code Already Exist"), true);
                        return;
                    }

                    string result = objBL.insertDriver(objDriver);
                    fillDrivers();
                    if (result == "success")
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                            "Driver details Added/Updated Successfully"), true);
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
            fillDrivers();
        }

        protected void GridItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ItemID = Convert.ToInt32(GridItems.DataKeys[e.RowIndex].Values["id"].ToString());
            int login_id = Convert.ToInt32(GridItems.DataKeys[e.RowIndex].Values["login_id"].ToString() == "" ? "0" : GridItems.DataKeys[e.RowIndex].Values["login_id"].ToString());
            eMallBL objBL = new eMallBL();
            string status = objBL.deleteDriver(ItemID,login_id);
            if (status == "success")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert('Records Deleted Successfully');", true);
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert('Error... Please try Again');", true);
            fillDrivers();
            btnSave.Text = "Save";
            Clearfields();
        }

        protected void GridItems_RowEditing(object sender, GridViewEditEventArgs e)
        {
           
        }

        protected void GridItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        protected void GridItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Change")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridItems.Rows[index];

                //===========================================================
                hdItemID.Value = GridItems.DataKeys[index].Values["id"].ToString();
                txtName.Text = GridItems.DataKeys[index].Values["name"].ToString();
                txtMobile.Text = GridItems.DataKeys[index].Values["mobile"].ToString();
                txtAddress.Text = GridItems.DataKeys[index].Values["address"].ToString();
                txtUsername.Text = GridItems.DataKeys[index].Values["username"].ToString();
                txtPassword.Text = GridItems.DataKeys[index].Values["password"].ToString();

                ddlSchoolCode.SelectedValue = GridItems.DataKeys[index].Values["school_id"].ToString();

                btnSave.Text = "Update";
                btnAddNew.Enabled = true;
                //=========================================================
            }
        }
    }
}