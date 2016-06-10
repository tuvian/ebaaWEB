using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using System.Data;
using BusinussLayer;
using System.IO;

namespace eMall
{
    public partial class students : System.Web.UI.Page
    {
        public string FileTypes = ".jpeg,.png,.btmap";
        private int MaxSize = 4194304;
        public string _filePath = "Files/";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("default.aspx");
            //ucFileUploader.uploadFileEvtArgs += new EventHandler(uploadedFileEvent);
            Master.StudentsMenu = "menubuttonselect";
            Master.StudentsLink = "linkselect";
            lblError.Text = "";
            if (!IsPostBack)
            {
                fillSearchScoolCodes();
                fillstudents();
                fillSchoolCodes();
                fillClass();
                fillBus();
            }
        }

        private void uploadedFileEvent(object sender, EventArgs e)
        {
            //lblItemImage.Text = ucFileUploader.UploadedFileNames;
            //imgItem.ImageUrl = "/Files/" + ucFileUploader.UploadedFileNames;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillstudents();
            Clearfields();
        }

        private void fillstudents()
        {
            eMallEntity.student objStudent = new eMallEntity.student();
            objStudent.schoolId = Convert.ToInt32(ddlSearchSchoolCodes.SelectedValue == "" ? "0" : ddlSearchSchoolCodes.SelectedValue);
            //objSearch.categoryID = Convert.ToInt32(ddlSearchCategory.SelectedValue == "" ? "0" : ddlSearchCategory.SelectedValue);
            //objSearch.subCategoryID = Convert.ToInt32(ddlSearchSubCategory.SelectedValue == "" ? "0" : ddlSearchSubCategory.SelectedValue);
            //objSearch.isActive = Convert.ToInt32(ddlActive.SelectedValue == "" ? "0" : ddlActive.SelectedValue);
            //objSearch.hasOffer = Convert.ToInt32(ddlHasOffer.SelectedValue == "" ? "0" : ddlHasOffer.SelectedValue);
            switch (ddlSearchBy.SelectedValue)
            {
                case "All":
                    objStudent.firstname = txtSearch.Text.Trim();
                    // objStudent.studentID =Convert.ToInt32(txtSearch.Text.Trim());
                    objStudent.mobile = txtSearch.Text.Trim();
                    objStudent.email = txtSearch.Text.Trim();
                    //objStudent.studentclass = txtSearch.Text.Trim();
                    objStudent.search_operator = "OR";
                    break;

                case "Name":
                    objStudent.firstname = txtSearch.Text.Trim();
                    objStudent.search_operator = "AND";
                    break;
                ////case "Code":
                ////    objStudent.studentID = txtSearch.Text.Trim();
                ////    objStudent.search_operator = "AND";
                ////    break;
                case "Mobile":
                    objStudent.mobile = txtSearch.Text.Trim();
                    objStudent.search_operator = "AND";
                    break;
                case "Email":
                    objStudent.email = txtSearch.Text.Trim();
                    objStudent.search_operator = "AND";
                    break;
            }
            //objSearch.userID = Convert.ToInt32(ddlUsers.SelectedValue == "" ? "0" : ddlUsers.SelectedValue);

            DataTable dtTbl = new DataTable();
            dtTbl = (new eMallBL()).searchStudents(objStudent);
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
                if (Session["usertype"] != null && Session["usertype"].ToString() != "1")  // Super Admin User type is 1 and School Admin is 2 and Teacher is 3
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
                if (Session["usertype"] != null && Session["usertype"].ToString() != "1")
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

        private void fillBus()
        {
            try
            {
                eMallEntity.bus objBus = new eMallEntity.bus();
                DataTable dtTbl = new DataTable();
                objBus.school_id = Convert.ToInt32(ddlSchoolCode.SelectedValue);
                dtTbl = (new eMallBL()).searchBus(objBus);
                ddlBus.DataSource = dtTbl;
                ddlBus.DataTextField = "bus_number";
                ddlBus.DataValueField = "id";
                ddlBus.DataBind();

                // Insert 'Select'
                ddlBus.Items.Insert(0, new ListItem("--Select--", "0"));
                // Set selected index
                ddlBus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void fillClass()
        {
            try
            {
                eMallEntity.Class objClass = new eMallEntity.Class();
                DataTable dtTbl = new DataTable();
                objClass.school_id = Convert.ToInt32(ddlSchoolCode.SelectedValue);
                if (objClass.school_id != 0)
                {
                    dtTbl = (new eMallBL()).getClasses(objClass);
                    ddlClass.DataSource = dtTbl;
                    ddlClass.DataTextField = "classname";
                    ddlClass.DataValueField = "id";
                    ddlClass.DataBind();
                }

                // Insert 'Select'
                ddlClass.Items.Insert(0, new ListItem("--Select--", "0"));
                // Set selected index
                ddlClass.SelectedIndex = 0;

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
                    eMallEntity.student objStudent = new eMallEntity.student();
                    objStudent.studentID = txtStudentID.Text.Trim();
                    objStudent.gender = ddlGender.SelectedValue;
                    objStudent.firstname = txtFirstName.Text.Trim().Replace("'", "''");
                    objStudent.middlename = txtMiddleName.Text.Trim().Replace("'", "''");
                    objStudent.familyname = txtFamilyName.Text.Trim().Replace("'", "''");
                    objStudent.mobile = txtMobile.Text.Trim().Replace("'", "''");
                    objStudent.wilayath = txtWilayath.Text.Trim().Replace("'", "''");
                    objStudent.waynumber = txtwaynumber.Text.Trim();
                    objStudent.contactmobile = txtContactMobile.Text.Trim().Replace("'", "''");
                    objStudent.fathername = txtFatherName.Text.Trim().Replace("'", "''");
                    objStudent.mothername = txtMotherName.Text.Trim().Replace("'", "''");
                    objStudent.email = txtEmail.Text.Trim().Replace("'", "''");
                    objStudent.contactemail = txtContactEmail.Text.Trim().Replace("'", "''");
                    objStudent.nationality = txtNationality.Text.Trim().Replace("'", "''");
                    objStudent.present_address = txtPresentAddress.Text.Trim().Replace("'", "''");
                    objStudent.permenant_address = txtPermenantAddress.Text.Trim().Replace("'", "''");
                    objStudent.status = chkActive.Checked;
                    objStudent.schoolId = Convert.ToInt32(ddlSchoolCode.SelectedValue);
                    objStudent.class_id = Convert.ToInt32(ddlClass.SelectedValue);
                    objStudent.busID = Convert.ToInt32(ddlBus.SelectedValue);

                    //objStudent.qualification = txtQualification.Text.Trim().Replace("'", "''");
                    //objStudent.qualification = txtQualification.Text.Trim().Replace("'", "''");
                    //objStudent.department_id = Convert.ToInt32(ddlDepartment.SelectedValue);
                    //objStudent.designation_id = Convert.ToInt32(ddlDesignation.SelectedValue);
                    //objStudent.image_path = lblItemImage.Text.Trim().Replace("'", "''");
                    objStudent.image_path = hdItemImage.Value.Trim().Replace("'", "''");
                    objStudent.ID = String.IsNullOrEmpty(hdItemID.Value) ? 0 : int.Parse(hdItemID.Value);

                    if (objBL.isStudentAlreadyExist(objStudent))
                    {
                        lblError.Text = "Student Already Exist";
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                            "Student Id Already Exist"), true);

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", string.Format("alert('{0}');",
                            "Student Id Code Already Exist"), true);
                        return;
                    }

                    //if (objBL.isItemNameAlreadyExist(objItem))
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                    //        "Item Name Already Exist"), true);
                    //    return;
                    //}

                    string result = objBL.InsertStudent(objStudent);
                    fillstudents();
                    if (result == "success")
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                            "Student details Added/Updated Successfully"), true);
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
                eMallBL.logErrors("UC_Items", ex.GetType().ToString(), ex.Message);
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Clearfields();
        }

        protected void GridItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridItems.PageIndex = e.NewPageIndex;
            fillstudents();
        }

        protected void GridItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int itemID = 0;
            int.TryParse(GridItems.DataKeys[e.RowIndex].Values["id"].ToString(), out itemID);
            //int ItemID = Convert.ToInt32(GridItems.DataKeys[e.RowIndex].Values["id"].ToString());
            eMallBL objBL = new eMallBL();
            string status = objBL.DeleteStudent(itemID);
            if (status == "success")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert('Records Deleted Successfully');", true);
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert('Error... Please try Again');", true);
            fillstudents();
            btnSave.Text = "Save";
            Clearfields();
        }

        private void Clearfields()
        {

            ddlGender.SelectedValue = "";
            txtFirstName.Text = "";
            txtMiddleName.Text = "";
            txtFamilyName.Text = "";
            txtPresentAddress.Text = "";
            ddlClass.SelectedIndex = 0;
            ddlBus.SelectedIndex = 0;
            txtPermenantAddress.Text = "";
            txtContactMobile.Text = "";
            txtContactEmail.Text = "";
            txtEmail.Text = "";
            txtWilayath.Text = "";
            txtwaynumber.Text = "";
            txtFirstName.Text = "";
            txtFatherName.Text = "";
            txtMotherName.Text = "";
            txtMobile.Text = "";
            btnSave.Text = "Save";
            hdItemID.Value = "";
            hdItemImage.Value = "";
            txtNationality.Text = "";
            imgItem.ImageUrl = "";
            chkActive.Checked = false;
            txtStudentID.Text = "";
            if (Session["usertype"] == null || Session["usertype"].ToString() != "2")
                ddlSchoolCode.SelectedValue = "0";
        }

        protected void GridItems_RowEditing(object sender, GridViewEditEventArgs e)
        {

            //DataKeyNames = "ID,Description,Overview,OfferDescription,CategoryID,SubCategoryID,IsActive,ReleaseDate,Specefications" >
            hdItemID.Value = GridItems.DataKeys[e.NewEditIndex].Values["id"].ToString();// GridItems.Rows[e.NewEditIndex].Cells[0].Text;
            //txtcode.Text = GridItems.DataKeys[e.NewEditIndex].Values["code"].ToString();
            txtFirstName.Text = GridItems.DataKeys[e.NewEditIndex].Values["first_name"].ToString();
            txtEmail.Text = GridItems.DataKeys[e.NewEditIndex].Values["email"].ToString();
            //filldesignation();
            //ddlDesignation.SelectedValue = GridItems.DataKeys[e.NewEditIndex].Values["designation_id"].ToString();
            //filldepartment();
            //ddlDepartment.SelectedValue = GridItems.DataKeys[e.NewEditIndex].Values["department_id"].ToString();
            // chkActive.Checked = GridItems.DataKeys[e.NewEditIndex].Values["status"].ToString() == "1" ? true : false;
            txtNationality.Text = GridItems.DataKeys[e.NewEditIndex].Values["nationality"].ToString();
            txtPermenantAddress.Text = GridItems.DataKeys[e.NewEditIndex].Values["permenant_address"].ToString();
            txtPresentAddress.Text = GridItems.DataKeys[e.NewEditIndex].Values["present_address"].ToString();
            //txtQualification.Text = GridItems.DataKeys[e.NewEditIndex].Values["qualification"].ToString();
            // txtMobile.Text = GridItems.DataKeys[e.NewEditIndex].Values["mobile"].ToString();
            //imgItem.ImageUrl = "/teacher_image/" + GridItems.DataKeys[e.NewEditIndex].Values["image_path"].ToString();
            //hdItemImage.Value = GridItems.DataKeys[e.NewEditIndex].Values["image_path"].ToString();
            //lblItemImage.Text = GridItems.DataKeys[e.NewEditIndex].Values["image_path"].ToString();
            txtStudentID.Text = GridItems.DataKeys[e.NewEditIndex].Values["student_id"].ToString();
            ddlGender.SelectedValue = GridItems.DataKeys[e.NewEditIndex].Values["gender"].ToString();
            txtFirstName.Text = GridItems.DataKeys[e.NewEditIndex].Values["first_name"].ToString();
            txtMiddleName.Text = GridItems.DataKeys[e.NewEditIndex].Values["middle_name"].ToString();
            txtFamilyName.Text = GridItems.DataKeys[e.NewEditIndex].Values["family_name"].ToString(); ;
            txtPresentAddress.Text = GridItems.DataKeys[e.NewEditIndex].Values["present_address"].ToString(); ;
            ddlClass.SelectedValue = GridItems.DataKeys[e.NewEditIndex].Values["class_id"].ToString();
            ddlBus.SelectedValue = GridItems.DataKeys[e.NewEditIndex].Values["bus_id"].ToString(); 
            txtPermenantAddress.Text = GridItems.DataKeys[e.NewEditIndex].Values["permenant_address"].ToString(); ;
            txtContactMobile.Text = GridItems.DataKeys[e.NewEditIndex].Values["contact_mobile"].ToString(); ;
            txtContactEmail.Text = GridItems.DataKeys[e.NewEditIndex].Values["contact_email"].ToString(); ;
            txtEmail.Text = GridItems.DataKeys[e.NewEditIndex].Values["email"].ToString();
            txtWilayath.Text = GridItems.DataKeys[e.NewEditIndex].Values["wilayath"].ToString();
            txtwaynumber.Text = GridItems.DataKeys[e.NewEditIndex].Values["waynumber"].ToString();

            txtFatherName.Text = GridItems.DataKeys[e.NewEditIndex].Values["father_name"].ToString();
            txtMotherName.Text = GridItems.DataKeys[e.NewEditIndex].Values["mother_name"].ToString();
            txtMobile.Text = GridItems.DataKeys[e.NewEditIndex].Values["mobile"].ToString();
            btnSave.Text = "Save";
            //hdItemID.Value = "";
            hdItemImage.Value = GridItems.DataKeys[e.NewEditIndex].Values["image_path"].ToString();
            //txtNationality.Text = "";
            //imgItem.ImageUrl = "";
            chkActive.Checked = Convert.ToBoolean(GridItems.DataKeys[e.NewEditIndex].Values["status"]);
            imgItem.ImageUrl = "teacher_image/" + hdItemImage.Value;
            ////from data bound
            //txtItemCode.Text = GridItems.Rows[e.NewEditIndex].Cells[0].Text;
            //txtName.Text = GridItems.Rows[e.NewEditIndex].Cells[1].Text;
            //txtPrice.Text = GridItems.Rows[e.NewEditIndex].Cells[2].Text;
            //imgItem.ImageUrl = "/Files/" + GridItems.Rows[e.NewEditIndex].Cells[3].Text;
            //lblItemImage.Text = GridItems.Rows[e.NewEditIndex].Cells[3].Text;
            //chkHasOffer.Checked = GridItems.Rows[e.NewEditIndex].Cells[4].Text == "Yes" ? true : false;
            //txtOfferPrice.Text = GridItems.Rows[e.NewEditIndex].Cells[5].Text;


            btnSave.Text = "Update";
            btnAddNew.Enabled = true;
            GridItems.EditIndex = -1;
            fillstudents();
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

        public static void AddConfirmDelete(GridView gv, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (DataControlField dcf in gv.Columns)
                {
                    if (dcf.ToString() == "CommandField")
                    {
                        if (((CommandField)dcf).ShowDeleteButton == true)
                        {
                            e.Row.Cells[gv.Columns.IndexOf(dcf)].Attributes
                            .Add("onclick", "return confirm(\"Are you sure?\")");
                        }
                    }
                }
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuTeacher.HasFile)
                {
                    try
                    {
                        if (fuTeacher.PostedFile.ContentType == "image/jpeg" || fuTeacher.PostedFile.ContentType == "image/png")
                        {
                            if (fuTeacher.PostedFile.ContentLength < 512000)
                            {
                                string filename = Path.GetFileName(fuTeacher.FileName);
                                fuTeacher.SaveAs(Server.MapPath("~/student_image/") + filename);
                                //lblItemImage.Text = filename;
                                hdItemImage.Value = filename;
                                imgItem.ImageUrl = "student_image/" + filename;
                            }
                            else
                                lblError.Text = "Upload status: The file has to be less than 500 kb!";
                        }
                        else
                            lblError.Text = "Upload status: Only JPEG/PNG files are accepted!";
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

        private bool isAllowedFileType(string fileType)
        {
            try
            {
                if (FileTypes != "")
                {
                    string[] allowedFileTypesArray = FileTypes.Split(',');
                    for (int i = 0; i < allowedFileTypesArray.Length; i++)
                    {
                        if (allowedFileTypesArray[i] == fileType)
                            return true;
                    }
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                eMallBL.logErrors("UC_FileUploader", ex.GetType().ToString(), ex.Message);
                return false;
            }
        }

        private bool isExceedFileSize()
        {
            int totalSize = 0;
            for (int j = 0; j < Request.Files.Count; j++)
            {
                HttpPostedFile PostedFile = Request.Files[j];
                totalSize += PostedFile.ContentLength;
            }
            if (MaxSize > 0 && totalSize > MaxSize)
                return true;
            return false;
        }

        protected void GridItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Change")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridItems.Rows[index];

                //===========================================================
                //DataKeyNames = "ID,Description,Overview,OfferDescription,CategoryID,SubCategoryID,IsActive,ReleaseDate,Specefications" >
                hdItemID.Value = GridItems.DataKeys[index].Values["id"].ToString();// GridItems.Rows[e.NewEditIndex].Cells[0].Text;
                //txtcode.Text = GridItems.DataKeys[e.NewEditIndex].Values["code"].ToString();
                txtFirstName.Text = GridItems.DataKeys[index].Values["first_name"].ToString();
                txtEmail.Text = GridItems.DataKeys[index].Values["email"].ToString();
                txtNationality.Text = GridItems.DataKeys[index].Values["nationality"].ToString();
                txtPermenantAddress.Text = GridItems.DataKeys[index].Values["permenant_address"].ToString();
                txtPresentAddress.Text = GridItems.DataKeys[index].Values["present_address"].ToString();
                txtStudentID.Text = GridItems.DataKeys[index].Values["student_id"].ToString();
                ddlGender.SelectedValue = GridItems.DataKeys[index].Values["gender"].ToString();
                txtFirstName.Text = GridItems.DataKeys[index].Values["first_name"].ToString();
                txtMiddleName.Text = GridItems.DataKeys[index].Values["middle_name"].ToString();
                txtFamilyName.Text = GridItems.DataKeys[index].Values["family_name"].ToString();
                txtPresentAddress.Text = GridItems.DataKeys[index].Values["present_address"].ToString();

                txtPermenantAddress.Text = GridItems.DataKeys[index].Values["permenant_address"].ToString();
                txtContactMobile.Text = GridItems.DataKeys[index].Values["contact_mobile"].ToString();
                txtContactEmail.Text = GridItems.DataKeys[index].Values["contact_email"].ToString();
                txtEmail.Text = GridItems.DataKeys[index].Values["email"].ToString();
                txtWilayath.Text = GridItems.DataKeys[index].Values["wilayath"].ToString();
                txtwaynumber.Text = GridItems.DataKeys[index].Values["waynumber"].ToString();

                txtFatherName.Text = GridItems.DataKeys[index].Values["father_name"].ToString();
                txtMotherName.Text = GridItems.DataKeys[index].Values["mother_name"].ToString();
                txtMobile.Text = GridItems.DataKeys[index].Values["mobile"].ToString();
                btnSave.Text = "Save";
                //hdItemID.Value = "";
                hdItemImage.Value = GridItems.DataKeys[index].Values["image_path"].ToString();
                //txtNationality.Text = "";
                //imgItem.ImageUrl = "";
                chkActive.Checked = Convert.ToBoolean(GridItems.DataKeys[index].Values["status"]);
                imgItem.ImageUrl = "teacher_image/" + hdItemImage.Value;
                //fillSchoolCodes();
                ddlSchoolCode.SelectedValue = GridItems.DataKeys[index].Values["school_id"].ToString();
                fillClass();
                ddlClass.SelectedValue = GridItems.DataKeys[index].Values["class_id"].ToString();
                fillBus();
                try
                {
                    ddlBus.SelectedValue = GridItems.DataKeys[index].Values["bus_id"].ToString() == "" ? "0" : GridItems.DataKeys[index].Values["bus_id"].ToString();
                }
                catch (Exception ex)
                {
                    ddlBus.SelectedValue = "0";
                }
                btnSave.Text = "Update";
                btnAddNew.Enabled = true;
                //=========================================================
            }
        }

        protected void ddlSchoolCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillClass();
        }

        protected void ddlBus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //fillDriver();
        }

        //protected void GridItems_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Edit")
        //    {
        //        int index = Convert.ToInt32(e.CommandArgument);

        //        // Get the last name of the selected author from the appropriate
        //        // cell in the GridView control.
        //        GridViewRow selectedRow = GridItems.Rows[index];
        //        TableCell contactName = selectedRow.Cells[1];
        //        string contact = contactName.Text;  
        //    }
        //}

    }
}