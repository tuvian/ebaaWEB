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
    public partial class teachers : System.Web.UI.Page
    {
        public string FileTypes = ".jpeg,.png,.btmap";
        private int MaxSize = 4194304;
        public string _filePath = "Files/";

        protected void Page_Load(object sender, EventArgs e)
        {
            //ucFileUploader.uploadFileEvtArgs += new EventHandler(uploadedFileEvent);
            Master.HomeMenu = "menubuttonselect";
            Master.HomeLink = "linkselect";
            lblError.Text = "";
            if (!IsPostBack)
            {
                fillteachers();
                filldepartment();
                filldesignation();
                filldepartmentsearch();
                fillSchoolCodes();
                fillClass();
            }
        }

        private void uploadedFileEvent(object sender, EventArgs e)
        {
            //lblItemImage.Text = ucFileUploader.UploadedFileNames;
            //imgItem.ImageUrl = "/Files/" + ucFileUploader.UploadedFileNames;
        }

        private void filldesignation()
        {
            try
            {
                DataTable dtTbl = new DataTable();
                dtTbl = (new eMallBL()).getDesignation(0);
                ddlDesignation.DataSource = dtTbl;
                ddlDesignation.DataTextField = "name";
                ddlDesignation.DataValueField = "id";
                ddlDesignation.DataBind();

                // Insert 'Select'
                ddlDesignation.Items.Insert(0, new ListItem("--Select--", "0"));
                // Set selected index
                ddlDesignation.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void filldepartment()
        {
            try
            {
                DataTable dtTbl = new DataTable();
                dtTbl = (new eMallBL()).getDepartment(0);
                ddlDepartment.DataSource = dtTbl;
                ddlDepartment.DataTextField = "name";
                ddlDepartment.DataValueField = "id";
                ddlDepartment.DataBind();

                // Insert 'Select'
                ddlDepartment.Items.Insert(0, new ListItem("--Select--", "0"));
                // Set selected index
                ddlDepartment.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void filldepartmentsearch()
        {
            try
            {
                DataTable dtTbl = new DataTable();
                dtTbl = (new eMallBL()).getDepartment(0);
                ddlSearchDepartment.DataSource = dtTbl;
                ddlSearchDepartment.DataTextField = "name";
                ddlSearchDepartment.DataValueField = "id";
                ddlSearchDepartment.DataBind();

                // Insert 'Select'
                ddlSearchDepartment.Items.Insert(0, new ListItem("--All--", "0"));
                // Set selected index
                ddlSearchDepartment.SelectedIndex = 0;
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
                    
                    chkClassForTeacher.DataSource = dtTbl;
                    chkClassForTeacher.DataTextField = "classname";
                    chkClassForTeacher.DataValueField = "id";
                    chkClassForTeacher.DataBind();
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillteachers();
            Clearfields();
        }

        private void fillteachers()
        {
            eMallEntity.teacher objTeacher = new eMallEntity.teacher();
            //objSearch.categoryID = Convert.ToInt32(ddlSearchCategory.SelectedValue == "" ? "0" : ddlSearchCategory.SelectedValue);
            //objSearch.subCategoryID = Convert.ToInt32(ddlSearchSubCategory.SelectedValue == "" ? "0" : ddlSearchSubCategory.SelectedValue);
            //objSearch.isActive = Convert.ToInt32(ddlActive.SelectedValue == "" ? "0" : ddlActive.SelectedValue);
            //objSearch.hasOffer = Convert.ToInt32(ddlHasOffer.SelectedValue == "" ? "0" : ddlHasOffer.SelectedValue);
            objTeacher.department_id = Convert.ToInt32(ddlSearchDepartment.SelectedValue == "" ? "0" : ddlSearchDepartment.SelectedValue);
            switch (ddlSearchBy.SelectedValue)
            {
                case "All":
                    objTeacher.name = txtSearch.Text.Trim();
                    objTeacher.mobile = txtSearch.Text.Trim();
                    objTeacher.email = txtSearch.Text.Trim();
                    objTeacher.code = txtSearch.Text.Trim();
                    objTeacher.search_operator = "OR";
                    break;
                case "Name":
                    objTeacher.name = txtSearch.Text.Trim();
                    objTeacher.search_operator = "AND";
                    break;
                case "Code":
                    objTeacher.code = txtSearch.Text.Trim();
                    objTeacher.search_operator = "AND";
                    break;
                case "Mobile":
                    objTeacher.mobile = txtSearch.Text.Trim();
                    objTeacher.search_operator = "AND";
                    break;
                case "Email":
                    objTeacher.email = txtSearch.Text.Trim();
                    objTeacher.search_operator = "AND";
                    break;
            }
            //objSearch.userID = Convert.ToInt32(ddlUsers.SelectedValue == "" ? "0" : ddlUsers.SelectedValue);

            DataTable dtTbl = new DataTable();
            
            dtTbl = (new eMallBL()).searchTeachers(objTeacher);
            GridItems.PageSize = Convert.ToInt32(10);
            GridItems.DataSource = dtTbl;
            GridItems.DataBind();
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
                    eMallEntity.teacher objTeacher = new eMallEntity.teacher();
                    objTeacher.code = txtcode.Text.Trim().Replace("'", "''");
                    objTeacher.name = txtName.Text.Trim().Replace("'", "''");
                    objTeacher.mobile = txtMobile.Text.Trim().Replace("'", "''");
                    objTeacher.email = txtEmail.Text.Trim().Replace("'", "''");
                    objTeacher.present_address = txtPresentAddress.Text.Trim().Replace("'", "''");
                    objTeacher.status = chkActive.Checked;
                    objTeacher.permenant_address = txtPermenantAddress.Text.Trim().Replace("'", "''");
                    objTeacher.qualification = txtQualification.Text.Trim().Replace("'", "''");
                    objTeacher.department_id = Convert.ToInt32(ddlDepartment.SelectedValue);
                    objTeacher.designation_id = Convert.ToInt32(ddlDesignation.SelectedValue);
                    objTeacher.nationality = txtNationality.Text.Trim().Replace("'", "''");
                    objTeacher.image_path = lblItemImage.Text.Trim().Replace("'", "''");
                    //objTeacher.image_path = hdItemImage.Value.Trim().Replace("'", "''");
                    objTeacher.ID = String.IsNullOrEmpty(hdItemID.Value) ? 0 : int.Parse(hdItemID.Value);

                    objTeacher.school_id = Convert.ToInt32(ddlSchoolCode.SelectedValue);
                    objTeacher.class_id = Convert.ToInt32(ddlClass.SelectedValue);

                    if (objBL.isTeacherCodeAlreadyExist(objTeacher))
                    {
                        lblError.Text = "Teacher's Code Already Exist";
                        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                        //    "Teacher's Code Already Exist"), true);

                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", string.Format("alert('{0}');",
                        //    "Teacher's Code Already Exist"), true);
                        return;
                    }

                    //if (objBL.isItemNameAlreadyExist(objItem))
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                    //        "Item Name Already Exist"), true);
                    //    return;
                    //}

                    string result = objBL.InsertTeacher(objTeacher);
                    fillteachers();
                    if (result == "success")
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                            "Teacher details Added/Updated Successfully"), true);
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
            fillteachers();
        }

        protected void GridItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ItemID = Convert.ToInt32(GridItems.DataKeys[e.RowIndex].Values["id"].ToString());
            eMallBL objBL = new eMallBL();
            string status = objBL.DeleteTeacher(ItemID);
            if (status == "success")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert('Records Deleted Successfully');", true);
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert('Error... Please try Again');", true);
            fillteachers();
            btnSave.Text = "Save";
            Clearfields();
        }

        private void Clearfields()
        {
            txtPresentAddress.Text = "";
            txtPermenantAddress.Text = "";
            txtEmail.Text = "";
            txtName.Text = "";
            txtQualification.Text = "";
            txtcode.Text = "";
            txtMobile.Text = "";
            btnSave.Text = "Save";
            hdItemID.Value = "";
            //hdItemImage.Value = "";
            lblItemImage.Text = "";
            txtNationality.Text = "";
            imgItem.ImageUrl = "";
            ddlDepartment.SelectedIndex = 0;
            ddlDesignation.SelectedIndex = 0;
            ddlClass.SelectedIndex = 0;
            ddlSchoolCode.SelectedIndex = 0;
        }

        protected void GridItems_RowEditing(object sender, GridViewEditEventArgs e)
        {
            hdItemID.Value = GridItems.DataKeys[e.NewEditIndex].Values["id"].ToString();// GridItems.Rows[e.NewEditIndex].Cells[0].Text;
            txtcode.Text = GridItems.DataKeys[e.NewEditIndex].Values["code"].ToString();
            txtName.Text = GridItems.DataKeys[e.NewEditIndex].Values["name"].ToString();
            txtEmail.Text = GridItems.DataKeys[e.NewEditIndex].Values["email"].ToString();
            filldesignation();
            ddlDesignation.SelectedValue = GridItems.DataKeys[e.NewEditIndex].Values["designation_id"].ToString();
            filldepartment();
            ddlDepartment.SelectedValue = GridItems.DataKeys[e.NewEditIndex].Values["department_id"].ToString();
            chkActive.Checked = GridItems.DataKeys[e.NewEditIndex].Values["status"].ToString() == "1" ? true : false;
            txtNationality.Text = GridItems.DataKeys[e.NewEditIndex].Values["nationality"].ToString();
            txtPermenantAddress.Text = GridItems.DataKeys[e.NewEditIndex].Values["permenant_address"].ToString();
            txtPresentAddress.Text = GridItems.DataKeys[e.NewEditIndex].Values["present_address"].ToString();
            txtQualification.Text = GridItems.DataKeys[e.NewEditIndex].Values["qualification"].ToString();
            txtMobile.Text = GridItems.DataKeys[e.NewEditIndex].Values["mobile"].ToString();
            imgItem.ImageUrl = "/teacher_image/" + GridItems.DataKeys[e.NewEditIndex].Values["image_path"].ToString();
            lblItemImage.Text = GridItems.DataKeys[e.NewEditIndex].Values["image_path"].ToString();

            btnSave.Text = "Update";
            btnAddNew.Enabled = true;
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
                            if (fuTeacher.PostedFile.ContentLength < 1024000)
                            {
                                string filename = Path.GetFileName(fuTeacher.FileName);
                                fuTeacher.SaveAs(Server.MapPath("~/teacher_image/") + filename);
                                lblItemImage.Text = filename;
                                //hdItemImage.Value = filename;
                                imgItem.ImageUrl = "teacher_image/" + filename;
                            }
                            else
                                lblError.Text = "Upload status: The file has to be less than 1 MB!";
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
                hdItemID.Value = GridItems.DataKeys[index].Values["id"].ToString();// GridItems.Rows[index].Cells[0].Text;
                txtcode.Text = GridItems.DataKeys[index].Values["code"].ToString();
                txtName.Text = GridItems.DataKeys[index].Values["name"].ToString();
                txtEmail.Text = GridItems.DataKeys[index].Values["email"].ToString();
                filldesignation();
                ddlDesignation.SelectedValue = GridItems.DataKeys[index].Values["designation_id"].ToString();
                filldepartment();
                ddlDepartment.SelectedValue = GridItems.DataKeys[index].Values["department_id"].ToString();
                chkActive.Checked = GridItems.DataKeys[index].Values["status"].ToString() == "1" ? true : false;
                txtNationality.Text = GridItems.DataKeys[index].Values["nationality"].ToString();
                txtPermenantAddress.Text = GridItems.DataKeys[index].Values["permenant_address"].ToString();
                txtPresentAddress.Text = GridItems.DataKeys[index].Values["present_address"].ToString();
                txtQualification.Text = GridItems.DataKeys[index].Values["qualification"].ToString();
                txtMobile.Text = GridItems.DataKeys[index].Values["mobile"].ToString();
                imgItem.ImageUrl = "/teacher_image/" + GridItems.DataKeys[index].Values["image_path"].ToString();
                lblItemImage.Text = GridItems.DataKeys[index].Values["image_path"].ToString();

                ddlSchoolCode.SelectedValue = GridItems.DataKeys[index].Values["school_id"].ToString();
                fillClass();
                ddlClass.SelectedValue = GridItems.DataKeys[index].Values["class_id"].ToString() == "" ? "0" : GridItems.DataKeys[index].Values["class_id"].ToString(); 

                btnSave.Text = "Update";
                btnAddNew.Enabled = true;
                //=========================================================
            }
        }

        protected void ddlSchoolCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillClass();
        }
    }
}