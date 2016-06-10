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
    public partial class teacherlogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
                Response.Redirect("default.aspx");
            Master.ContactUsMenu = "menubuttonselect";
            Master.ContactUsLink = "linkselect";
            btnAddNew.Visible = false;
            btnSave.Text = "Update";
            ddlSchoolCode.Enabled = false;
            ddlTeacherCode.Enabled = false;
            if (!IsPostBack)
            {
                fillSchoolCodes();
                fillSearchSchoolCodes();
                fillteacherCodes();
                fillTeacherLogins();
                fetchSelectedTeacherdetails();
            }
        }

        private void fillteacherCodes()
        {
            try
            {
                eMallEntity.teacher objTeacher = new eMallEntity.teacher();
                DataTable dtTbl = new DataTable();
                dtTbl = (new eMallBL()).searchTeachers(objTeacher);
                ddlTeacherCode.DataSource = dtTbl;
                ddlTeacherCode.DataTextField = "code";
                ddlTeacherCode.DataValueField = "id";
                ddlTeacherCode.DataBind();

                // Insert 'Select'
                ddlTeacherCode.Items.Insert(0, new ListItem("--Select--", "0"));
                // Set selected index
                ddlTeacherCode.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw;
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
                    eMallEntity.teacherlogin objteacherLogin = new eMallEntity.teacherlogin();
                    objteacherLogin.schoolcode = ddlSchoolCode.Text.Trim().Replace("'", "''");
                    objteacherLogin.teachername = txtTeacherName.Text.Trim().Replace("'", "''");
                    objteacherLogin.mobile = txtMobile.Text.Trim().Replace("'", "''");
                    objteacherLogin.email = txtEmail.Text.Trim().Replace("'", "''");
                    objteacherLogin.schoolID = Convert.ToInt32(ddlSchoolCode.SelectedValue);
                    objteacherLogin.teacherid = Convert.ToInt32(ddlTeacherCode.SelectedValue);
                    objteacherLogin.username = txtUserName.Text.Trim().Replace("'", "''");
                    objteacherLogin.password = txtPassword.Text.Trim().Replace("'", "''");
                    objteacherLogin.type = "3";

                    objteacherLogin.ID = String.IsNullOrEmpty(hdItemID.Value) ? 0 : int.Parse(hdItemID.Value);

                    if (objBL.isTeacherUserNameAlreadyExist(objteacherLogin))
                    {
                        lblError.Text = "Teacher's login ID Already Exist";
                        return;
                    }

                    string result = objBL.InsertTeacherLogin(objteacherLogin);
                    fillTeacherLogins();
                    if (result == "success")
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                            "Teacher login details Added/Updated Successfully"), true);
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
                eMallBL.logErrors("Teacher", ex.GetType().ToString(), ex.Message);
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Clearfields();
        }

        protected void GridItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridItems.PageIndex = e.NewPageIndex;
            fillTeacherLogins();
        }

        protected void GridItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = Convert.ToInt32(GridItems.DataKeys[e.RowIndex].Values["id"].ToString());
            eMallBL objBL = new eMallBL();
            string status = objBL.DeleteUserLogin(ID);
            if (status == "success")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert('Records Deleted Successfully');", true);
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert('Error... Please try Again');", true);
            fillTeacherLogins();
            btnSave.Text = "Save";
            Clearfields();
        }

        protected void GridItems_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //hdItemID.Value = GridItems.DataKeys[index].Values["id"].ToString();// GridItems.Rows[e.NewEditIndex].Cells[0].Text;
            //txtUserName.Text = GridItems.DataKeys[e.NewEditIndex].Values["username"].ToString();
            //txtPassword.Text = GridItems.DataKeys[e.NewEditIndex].Values["password"].ToString();
            //txtTeacherName.Text = GridItems.DataKeys[e.NewEditIndex].Values["name"].ToString();
            //txtEmail.Text = GridItems.DataKeys[e.NewEditIndex].Values["email"].ToString();
            //fillSchoolCodes();
            //ddlSchoolCode.SelectedValue = GridItems.DataKeys[e.NewEditIndex].Values["school_id"].ToString();
            //fillteacherCodes();
            //ddlTeacherCode.SelectedValue = GridItems.DataKeys[e.NewEditIndex].Values["teacher_id"].ToString();
            //txtMobile.Text = GridItems.DataKeys[e.NewEditIndex].Values["mobile"].ToString();

            //btnSave.Text = "Update";
            //btnAddNew.Enabled = true;
            //ddlSchoolCode.Enabled = false;
            //ddlTeacherCode.Enabled = false;
        }

        protected void GridItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;

                int loginID = 0;
                int.TryParse(rowView["id"].ToString(), out loginID);
                if (loginID == 0)
                {
                    Button deleteButton = (Button)e.Row.FindControl("deleteButton");
                    deleteButton.Visible = false;
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillTeacherLogins();
            Clearfields();
        }

        private void fillTeacherLogins()
        {
            eMallEntity.teacherlogin objteacherlogin = new eMallEntity.teacherlogin();
            objteacherlogin.schoolID = ddlSearchSchool.SelectedValue == "" ? 0 : Convert.ToInt32(ddlSearchSchool.SelectedValue);

            switch (ddlSearchBy.SelectedValue)
            {
                case "All":
                    objteacherlogin.teachername = txtSearch.Text.Trim();
                    objteacherlogin.teachercode = txtSearch.Text.Trim();
                    objteacherlogin.email = txtSearch.Text.Trim();
                    objteacherlogin.mobile = txtSearch.Text.Trim();
                    objteacherlogin.search_operator = "OR";
                    break;
                case "Name":
                    objteacherlogin.teachername = txtSearch.Text.Trim();
                    objteacherlogin.search_operator = "AND";
                    break;
                case "Code":
                    objteacherlogin.teachercode = txtSearch.Text.Trim();
                    objteacherlogin.search_operator = "AND";
                    break;
            }

            DataTable dtTbl = new DataTable();
            dtTbl = (new eMallBL()).searchTeacherLogin(objteacherlogin);
            GridItems.PageSize = Convert.ToInt32(10);
            GridItems.DataSource = dtTbl;
            GridItems.DataBind();
        }

        private void Clearfields()
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
            //btnSave.Text = "Save";
            hdItemID.Value = "";
            //ddlSchoolCode.SelectedIndex = 0;            
            //if (Session["usertype"] != null && Session["usertype"].ToString() == "2")
            //{
            //    ddlSchoolCode.SelectedValue = Session["school_id"].ToString();
            //    ddlSchoolCode.Enabled = false;
            //    ddlSchoolCode.SelectedValue = Session["school_id"].ToString();
            //}
            //else
            //{
                ddlSchoolCode.SelectedIndex = 0;
                ddlSchoolCode.Enabled = false;
                ddlTeacherCode.SelectedIndex = 0;
                ddlTeacherCode.Enabled = false;
                txtTeacherName.Text = "";
                txtMobile.Text = "";
                txtEmail.Text = "";
            //}
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
            //fetchSelectedTeacherdetails();
        }

        protected void ddTeacherCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //fetchSelectedTeacherdetails();
        }
        
        private void fetchSelectedTeacherdetails()
        {
            if (ddlSchoolCode.SelectedValue != "" && ddlSchoolCode.SelectedValue != "0")
            {
                eMallEntity.teacher objTeacher = new eMallEntity.teacher();
                objTeacher.school_id = ddlSchoolCode.SelectedValue == "" ? 0 : Convert.ToInt32(ddlSchoolCode.SelectedValue);
                objTeacher.ID = ddlTeacherCode.SelectedValue == "" ? 0 : Convert.ToInt32(ddlTeacherCode.SelectedValue);

                DataTable dtTbl = new DataTable();
                dtTbl = (new eMallBL()).searchTeachers(objTeacher);

                if (objTeacher.ID != 0)
                {
                    txtEmail.Text = dtTbl.Rows[0]["email"].ToString();
                    txtMobile.Text = dtTbl.Rows[0]["mobile"].ToString();
                    txtTeacherName.Text = dtTbl.Rows[0]["name"].ToString();
                }
            }
            else
            {
                txtEmail.Text = "";
                txtMobile.Text = "";
                txtTeacherName.Text = "";
            }
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
                txtTeacherName.Text = GridItems.DataKeys[index].Values["name"].ToString();
                txtEmail.Text = GridItems.DataKeys[index].Values["email"].ToString();
                fillSchoolCodes();
                ddlSchoolCode.SelectedValue = GridItems.DataKeys[index].Values["school_id"].ToString();
                fillteacherCodes();
                ddlTeacherCode.SelectedValue = GridItems.DataKeys[index].Values["teacher_id"].ToString();
                txtMobile.Text = GridItems.DataKeys[index].Values["mobile"].ToString();

                btnSave.Text = "Update";
                btnAddNew.Enabled = true;
                ddlSchoolCode.Enabled = false;
                ddlTeacherCode.Enabled = false;
                //=========================================================
            }
        }
    }
}