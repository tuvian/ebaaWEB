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
    public partial class studentlogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
                Response.Redirect("default.aspx");
            Master.StudentLoginMenu = "menubuttonselect";
            Master.StudentLoginLink = "linkselect";
            btnAddNew.Visible = false;
            btnSave.Text = "Update";
            ddlSchoolCode.Enabled = false;
            ddlStudentCode.Enabled = false;
            if (!IsPostBack)
            {
                fillSchoolCodes();
                fillSearchSchoolCodes();
                fillStudentCodes();
                fillStudentLogins();
                fetchSelectedStudentdetails();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillStudentLogins();
            Clearfields();
        }

        private void fillStudentCodes()
        {
            try
            {
                eMallEntity.student objStudent = new eMallEntity.student();
                DataTable dtTbl = new DataTable();
                dtTbl = (new eMallBL()).getschoolCodes(Convert.ToInt32(ddlSchoolCode.SelectedValue));
                ddlStudentCode.DataSource = dtTbl;
                ddlStudentCode.DataTextField = "code";
                ddlStudentCode.DataValueField = "id";
                ddlStudentCode.DataBind();

                // Insert 'Select'
                ddlStudentCode.Items.Insert(0, new ListItem("--Select--", "0"));
                // Set selected index
                ddlStudentCode.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Clearfields()
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtDeviceID.Text = "";
            hdItemID.Value = "";
            ddlSchoolCode.SelectedIndex = 0;
            ddlSchoolCode.Enabled = false;
            ddlStudentCode.SelectedIndex = 0;
            txtTeacherName.Text = "";
            txtMobile.Text = "";
            txtEmail.Text = "";
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

        protected void ddlStudentCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //fetchSelectedTeacherdetails();
        }

        protected void GridItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridItems.PageIndex = e.NewPageIndex;
            fillStudentLogins();
        }

        protected void GridItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = Convert.ToInt32(GridItems.DataKeys[e.RowIndex].Values["login_id"].ToString());
            eMallBL objBL = new eMallBL();
            string status = objBL.DeleteUserLogin(ID);
            if (status == "success")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert('Records Deleted Successfully');", true);
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert('Error... Please try Again');", true);
            fillStudentLogins();
            btnSave.Text = "Save";
            Clearfields();
        }

        private void fillStudentLogins()
        {
            eMallEntity.studentlogin objteacherlogin = new eMallEntity.studentlogin();
            objteacherlogin.schoolID = ddlSearchSchool.SelectedValue == "" ? 0 : Convert.ToInt32(ddlSearchSchool.SelectedValue);

            //switch (ddlSearchBy.SelectedValue)
            //{
            //    case "All":
            //        objteacherlogin.teachername = txtSearch.Text.Trim();
            //        objteacherlogin.teachercode = txtSearch.Text.Trim();
            //        objteacherlogin.email = txtSearch.Text.Trim();
            //        objteacherlogin.mobile = txtSearch.Text.Trim();
            //        objteacherlogin.search_operator = "OR";
            //        break;
            //    case "Name":
            //        objteacherlogin.teachername = txtSearch.Text.Trim();
            //        objteacherlogin.search_operator = "AND";
            //        break;
            //    case "Code":
            //        objteacherlogin.teachercode = txtSearch.Text.Trim();
            //        objteacherlogin.search_operator = "AND";
            //        break;
            //}

            DataTable dtTbl = new DataTable();
            dtTbl = (new eMallBL()).searchStudentLogin(objteacherlogin);
            GridItems.PageSize = Convert.ToInt32(10);
            GridItems.DataSource = dtTbl;
            GridItems.DataBind();
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
                int.TryParse(rowView["login_id"].ToString(), out loginID);
                if (loginID == 0)
                {
                    Button deleteButton = (Button)e.Row.FindControl("deleteButton");
                    deleteButton.Visible = false;
                }
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
                    eMallEntity.studentlogin objStudentLogin = new eMallEntity.studentlogin();
                    objStudentLogin.schoolcode = ddlSchoolCode.Text.Trim().Replace("'", "''");
                    objStudentLogin.studentname = txtTeacherName.Text.Trim().Replace("'", "''");
                    objStudentLogin.mobile = txtMobile.Text.Trim().Replace("'", "''");
                    objStudentLogin.email = txtEmail.Text.Trim().Replace("'", "''");
                    objStudentLogin.schoolID = Convert.ToInt32(ddlSchoolCode.SelectedValue);
                    objStudentLogin.student_id = Convert.ToInt32(ddlStudentCode.SelectedValue);
                    objStudentLogin.username = txtUserName.Text.Trim().Replace("'", "''");
                    objStudentLogin.password = txtPassword.Text.Trim().Replace("'", "''");
                    objStudentLogin.device_id = txtDeviceID.Text.Trim().Replace("'", "''");
                    objStudentLogin.type = "4";

                    objStudentLogin.loginID = String.IsNullOrEmpty(hdItemID.Value) ? 0 : int.Parse(hdItemID.Value);

                    if (objBL.isStudentUserNameAlreadyExist(objStudentLogin))
                    {
                        lblError.Text = "Student's login ID Already Exist";
                        return;
                    }

                    string result = objBL.InsertStudentLogin(objStudentLogin);
                    fillStudentLogins();
                    if (result == "success")
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                            "Student login details Added/Updated Successfully"), true);
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

        private void fetchSelectedStudentdetails()
        {
            if (ddlSchoolCode.SelectedValue != "" && ddlSchoolCode.SelectedValue != "0")
            {
                eMallEntity.student objStudent = new eMallEntity.student();
                objStudent.schoolId = ddlSchoolCode.SelectedValue == "" ? 0 : Convert.ToInt32(ddlSchoolCode.SelectedValue);
                objStudent.ID = ddlStudentCode.SelectedValue == "" ? 0 : Convert.ToInt32(ddlStudentCode.SelectedValue);

                DataTable dtTbl = new DataTable();
                dtTbl = (new eMallBL()).searchStudents(objStudent);

                if (objStudent.ID != 0)
                {
                    txtEmail.Text = dtTbl.Rows[0]["email"].ToString();
                    txtMobile.Text = dtTbl.Rows[0]["mobile"].ToString();
                    txtTeacherName.Text = dtTbl.Rows[0]["first_name"].ToString();
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
            try
            {
                if (e.CommandName == "Change")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = GridItems.Rows[index];
                    fillSchoolCodes();

                    //===========================================================                
                    ddlSchoolCode.SelectedValue = GridItems.DataKeys[index].Values["school_id"].ToString();
                    hdItemID.Value = GridItems.DataKeys[index].Values["login_id"].ToString();// GridItems.Rows[index].Cells[0].Text;
                    txtUserName.Text = GridItems.DataKeys[index].Values["student_code"].ToString();
                    txtPassword.Text = GridItems.DataKeys[index].Values["password"].ToString();
                    txtTeacherName.Text = GridItems.DataKeys[index].Values["name"].ToString();
                    txtEmail.Text = GridItems.DataKeys[index].Values["email"].ToString();
                    fillStudentCodes();
                    ddlStudentCode.SelectedValue = GridItems.DataKeys[index].Values["student_id"].ToString();
                    txtMobile.Text = GridItems.DataKeys[index].Values["mobile"].ToString();
                    txtDeviceID.Text = GridItems.DataKeys[index].Values["device_id"].ToString();

                    btnSave.Text = "Update";
                    btnAddNew.Enabled = true;
                    ddlSchoolCode.Enabled = false;
                    ddlStudentCode.Enabled = false;
                    //=========================================================
                }
            }
            catch (Exception ex)
            {
                eMallBL.logErrors(this.Page.ToString().Replace("'","''"), ex.Message.Replace("'","''"));
                throw;
            }
        }
    }
}