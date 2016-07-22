using BusinussLayer;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using PushSharp;
//using PushSharp.Android;
using PushSharp.Apple;
using PushSharp.Core;
using System.IO;

namespace eMall
{
    public partial class notification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("default.aspx");
            lblError.Text = "";
            Master.NotificationMenu = "menubuttonselect";
            Master.NotificationLink = "linkselect";
            if (!IsPostBack)
            {
                //fillSearchScoolCodes();
                //fillNotifications();
                fillSchoolCodes();
                fillClassForStudents();
            }
        }

        private void fillClassForStudents()
        {
            try
            {
                eMallEntity.Class objClass = new eMallEntity.Class();
                DataTable dtTbl = new DataTable();
                objClass.school_id = Convert.ToInt32(ddlSchoolCode.SelectedValue);
                if (objClass.school_id != 0)
                {
                    dtTbl = (new eMallBL()).getClasses(objClass);
                    chkClassForStudent.DataSource = dtTbl;
                    chkClassForStudent.DataTextField = "classname";
                    chkClassForStudent.DataValueField = "id";
                    chkClassForStudent.DataBind();

                    chkClassForTeacher.DataSource = dtTbl;
                    chkClassForTeacher.DataTextField = "classname";
                    chkClassForTeacher.DataValueField = "id";
                    chkClassForTeacher.DataBind();
                }
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
                //ddlSearchSchoolCodes.DataSource = dtTbl;
                //ddlSearchSchoolCodes.DataTextField = "code";
                //ddlSearchSchoolCodes.DataValueField = "id";
                //ddlSearchSchoolCodes.DataBind();

                //// Insert 'Select'
                //ddlSearchSchoolCodes.Items.Insert(0, new ListItem("--Select--", "0"));
                //// Set selected index
                //ddlSearchSchoolCodes.SelectedIndex = 0;
                //if (Session["usertype"] != null && Session["usertype"].ToString() == "2")
                //{
                //    ddlSearchSchoolCodes.SelectedValue = Session["school_id"].ToString();
                //    ddlSearchSchoolCodes.Enabled = false;
                //}

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

        private void fillNotifications()
        {
            int school_id = 0;
            //int.TryParse(ddlSearchSchoolCodes.SelectedValue, out school_id);
            DataTable dtTbl = new DataTable();
            dtTbl = (new eMallBL()).searchNotifications(school_id);
            ////GridItems.PageSize = Convert.ToInt32(10);
            ////GridItems.DataSource = dtTbl;
            ////GridItems.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillNotifications();
            Clearfields();
        }

        private void Clearfields()
        {
            btnSave.Text = "Save";
            hdItemID.Value = "";
            txtMessage.Text = "";
            txtSubject.Text = "";
            //if (Session["usertype"] == null || Session["usertype"].ToString() != "2")
            ddlSchoolCode.SelectedValue = "0";
            chkClassForStudent.DataSource = null;
            chkClassForStudent.DataBind();
            chkClassForTeacher.DataSource = null;
            chkClassForTeacher.DataBind();
            chkClassForStudent.ClearSelection();
            chkClassForTeacher.ClearSelection();
            
        }

        protected void GridItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //GridItems.PageIndex = e.NewPageIndex;
            fillNotifications();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                List<String> teacherclassList = new List<String>();
                foreach (ListItem item in chkClassForTeacher.Items)
                {
                    if (item.Selected)
                        teacherclassList.Add(item.Value);
                }
                string teacherClasses = String.Join(",", teacherclassList.ToArray()) == "" ? "0" : String.Join(",", teacherclassList.ToArray());

                List<String> studentclassList = new List<String>();
                foreach (ListItem item in chkClassForStudent.Items)
                {
                    if (item.Selected)
                        studentclassList.Add(item.Value);
                }
                string studentClasses = String.Join(",", studentclassList.ToArray()) == "" ? "0" : String.Join(",", studentclassList.ToArray());

                if (teacherclassList.Count < 1 && studentclassList.Count < 1)
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                                            "Please select atleast one class for student or teachers"), true);
                    return;
                }

                string sub = txtSubject.Text.Replace("'", "''");
                string msg = txtMessage.Text.Replace("'", "''");
                string file = lblFileName.Text.Replace("'", "''");
                string fname = Session["employee_name"] == null ? "" : Session["employee_name"].ToString();
                string fid = Session["employee_id"] == null ? "" : Session["employee_id"].ToString();
                string ftype = Session["usertype"] == null ? "" : Session["usertype"].ToString();

                eMallBL objBL = new eMallBL();

                int school_id = Convert.ToInt32(ddlSchoolCode.SelectedValue);
                //Send Push Notification
                string pushResult = objBL.sendPushNotification(fname, fid, ftype, file, sub, msg, school_id, teacherClasses, studentClasses);

                //objBL.sendemailfortesting();
                string result = objBL.sendEmailNotification(sub, msg, fid, teacherClasses, studentClasses);
                if (result == "success")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                        "Notification initaited successfully"), true);
                    Clearfields();
                }
                else
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                        "Error Occured, Please try Again"), true);

                Clearfields();
            }
            catch (Exception ex)
            {
                throw ex;
                //eMallBL.logErrors("btnSave_Click", ex.GetType().ToString(), ex.Message);
            }
        }

        protected void ddlSchoolCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillClassForStudents();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clearfields();

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuTeacher.HasFile)
                {
                    try
                    {
                        ////if (fuTeacher.PostedFile.ContentType == "image/jpeg" || fuTeacher.PostedFile.ContentType == "image/png")
                        ////{
                        if (fuTeacher.PostedFile.ContentLength < 512000)
                        {
                            string filename = Path.GetFileName(fuTeacher.FileName);
                            fuTeacher.SaveAs(Server.MapPath("~/notification_file/") + filename);
                            //lblItemImage.Text = filename;
                            hdItemImage.Value = filename;
                            lblFileName.Text = filename;
                            //imgItem.ImageUrl = "student_image/" + filename;
                        }
                        else
                            lblError.Text = "Upload status: The file has to be less than 500 kb!";
                        //}
                        //else
                        //    lblError.Text = "Upload status: Only JPEG/PNG files are accepted!";
                    }
                    catch (Exception ex)
                    {
                        lblError.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                eMallBL.logErrors("UC_FileUploader", ex.GetType().ToString(), ex.Message);
            }
        }
    }
}