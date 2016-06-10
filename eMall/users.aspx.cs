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
    public partial class users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
                Response.Redirect("default.aspx");
            Master.UserMenu = "menubuttonselect";
            Master.UserLink = "linkselect";       
            if (!IsPostBack)
            {                
                fillSchoolCodes();
                fillSearchSchoolCodes();
                fillSchoolAdmin();
                fetchSelectedSchooldetails();
                //fillRegisterDates();
                //fillExpireDates();
            }
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
                    eMallEntity.schooluser objSchoolLogin = new eMallEntity.schooluser();
                    objSchoolLogin.code = ddlSchoolCode.Text.Trim().Replace("'", "''");
                    objSchoolLogin.name = txtName.Text.Trim().Replace("'", "''");
                    objSchoolLogin.mobile = txtMobile.Text.Trim().Replace("'", "''");
                    objSchoolLogin.email = txtEmail.Text.Trim().Replace("'", "''");
                    objSchoolLogin.schoolID = Convert.ToInt32(ddlSchoolCode.SelectedValue);
                    objSchoolLogin.username = txtUserName.Text.Trim().Replace("'", "''");
                    objSchoolLogin.password = txtPassword.Text.Trim().Replace("'", "''");
                    objSchoolLogin.type = "2";

                    objSchoolLogin.ID = String.IsNullOrEmpty(hdItemID.Value) ? 0 : int.Parse(hdItemID.Value);

                    if (objBL.isSchoolUserNameAlreadyExist(objSchoolLogin))
                    {
                        lblError.Text = "Schools's login ID Already Exist";
                        return;
                    }

                    //if (objBL.isItemNameAlreadyExist(objItem))
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                    //        "Item Name Already Exist"), true);
                    //    return;
                    //}

                    string result = objBL.InsertSchoolLogin(objSchoolLogin);
                    fillSchoolAdmin();
                    if (result == "success")
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                            "School login details Added/Updated Successfully"), true);
                        //lblError.Text = "New Teacher details added successfully";
                        Clearfields();
                    }
                    else
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                            "Error Occured, Please try Again"), true);
                }
            }
            catch (Exception ex)
            {
                eMallBL.logErrors("Schools", ex.GetType().ToString(), ex.Message);
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Clearfields();
        }

        protected void GridItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridItems.PageIndex = e.NewPageIndex;
            fillSchoolAdmin();
        }

        protected void GridItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = Convert.ToInt32(GridItems.DataKeys[e.RowIndex].Values["id"].ToString());
            eMallBL objBL = new eMallBL();
            string status = objBL.DeleteSchoolLogin(ID);
            if (status == "success")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert('Records Deleted Successfully');", true);
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert('Error... Please try Again');", true);
            fillSchoolAdmin();
            btnSave.Text = "Save";
            Clearfields();
        }
                
        protected void GridItems_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //hdItemID.Value = GridItems.DataKeys[e.NewEditIndex].Values["id"].ToString();// GridItems.Rows[e.NewEditIndex].Cells[0].Text;
            //txtUserName.Text = GridItems.DataKeys[e.NewEditIndex].Values["username"].ToString();
            //txtPassword.Text = GridItems.DataKeys[e.NewEditIndex].Values["password"].ToString();
            //txtName.Text = GridItems.DataKeys[e.NewEditIndex].Values["name"].ToString();
            //txtEmail.Text = GridItems.DataKeys[e.NewEditIndex].Values["email"].ToString();
            //fillSchoolCodes();
            //ddlSchoolCode.SelectedValue = GridItems.DataKeys[e.NewEditIndex].Values["school_id"].ToString();
            //txtMobile.Text = GridItems.DataKeys[e.NewEditIndex].Values["mobile"].ToString();
            
            //btnSave.Text = "Update";
            //btnAddNew.Enabled = true;
            //ddlSchoolCode.Enabled = false;
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillSchoolAdmin();
            Clearfields();
        }

        private void Clearfields()
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
            btnSave.Text = "Save";
            hdItemID.Value = "";
            //ddlSchoolCode.SelectedIndex = 0;            
            if (Session["usertype"] != null && Session["usertype"].ToString() == "2")
            {
                ddlSchoolCode.SelectedValue = Session["school_id"].ToString();
                ddlSchoolCode.Enabled = false;
                ddlSchoolCode.SelectedValue = Session["school_id"].ToString();
            }
            else
            {
                ddlSchoolCode.SelectedIndex = 0;
                ddlSearchSchool.Enabled = true;                
                txtName.Text = "";
                txtMobile.Text = "";
                txtEmail.Text = "";
            }
        }

        private void fillSchoolAdmin()
        {
            eMallEntity.schooluser objSchooluser = new eMallEntity.schooluser();
            objSchooluser.schoolID = ddlSearchSchool.SelectedValue == "" ? 0 : Convert.ToInt32(ddlSearchSchool.SelectedValue);

            DataTable dtTbl = new DataTable();
            dtTbl = (new eMallBL()).searchSchoolLogin(objSchooluser);
            GridItems.PageSize = Convert.ToInt32(10);
            GridItems.DataSource = dtTbl;
            GridItems.DataBind();
        }

        private void fillSearchSchoolCodes()
        {
            try
            {
                eMallEntity.school objSchool = new eMallEntity.school();
                DataTable dtTbl = new DataTable();
                dtTbl = (new eMallBL()).searchSchool(objSchool);
                ddlSearchSchool.DataSource = dtTbl;
                ddlSearchSchool.DataTextField = "code";
                ddlSearchSchool.DataValueField = "id";
                ddlSearchSchool.DataBind();

                // Insert 'Select'
                ddlSearchSchool.Items.Insert(0, new ListItem("--All--", "0"));
                // Set selected index
                ddlSearchSchool.SelectedIndex = 0;

                if (Session["usertype"] != null && Session["usertype"].ToString() == "2")
                {
                    ddlSearchSchool.SelectedValue = Session["school_id"].ToString();
                    ddlSearchSchool.Enabled = false;
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

        protected void ddlSchoolCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            fetchSelectedSchooldetails();
        }

        private void fetchSelectedSchooldetails()
        {
            eMallEntity.school objSchool = new eMallEntity.school();
            objSchool.ID = ddlSchoolCode.SelectedValue == "" ? 0 : Convert.ToInt32(ddlSchoolCode.SelectedValue);

            DataTable dtTbl = new DataTable();
            dtTbl = (new eMallBL()).searchSchool(objSchool);

            txtEmail.Text = dtTbl.Rows[0]["email"].ToString();
            txtMobile.Text = dtTbl.Rows[0]["mobile"].ToString();
            txtName.Text = dtTbl.Rows[0]["name"].ToString();
        }

        protected void GridItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Change")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridItems.Rows[index];

                //===========================================================
                hdItemID.Value = GridItems.DataKeys[index].Values["id"].ToString();// GridItems.Rows[index].Cells[0].Text;
                txtUserName.Text = GridItems.DataKeys[index].Values["username"].ToString();
                txtPassword.Text = GridItems.DataKeys[index].Values["password"].ToString();
                txtName.Text = GridItems.DataKeys[index].Values["name"].ToString();
                txtEmail.Text = GridItems.DataKeys[index].Values["email"].ToString();
                fillSchoolCodes();
                ddlSchoolCode.SelectedValue = GridItems.DataKeys[index].Values["school_id"].ToString();
                txtMobile.Text = GridItems.DataKeys[index].Values["mobile"].ToString();

                btnSave.Text = "Update";
                btnAddNew.Enabled = true;
                ddlSchoolCode.Enabled = false;
                //=========================================================
            }
        }

    }
}

