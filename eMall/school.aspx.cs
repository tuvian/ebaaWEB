using BusinussLayer;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eMall
{
    public partial class school : System.Web.UI.Page
    {
        #region  public variables

        public string FileTypes = ".jpeg,.png,.btmap";
        private int MaxSize = 4194304;
        public string _filePath = "Files/";

        #endregion

        #region events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("default.aspx");
            Master.SchoolMenu = "menubuttonselect";
            Master.SchoolLink = "linkselect";
            lblError.Text = "";
            txtRegisterDate.Attributes.Add("readonly", "readonly");
            txtExpireDate.Attributes.Add("readonly", "readonly");
            if (!IsPostBack)
            {
                fillSchools();
                fillPackage();
                fillPackagesearch();
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
                    eMallEntity.school objSchool = new eMallEntity.school();
                    objSchool.code = txtcode.Text.Trim().Replace("'", "''");
                    objSchool.name = txtName.Text.Trim().Replace("'", "''");
                    objSchool.siteurl = txtSiteURL.Text.Trim().Replace("'", "''");
                    objSchool.school_address = txtSchoolAddress.Text.Trim().Replace("'", "''");
                    objSchool.contact_person = txtContactPerson.Text.Trim().Replace("'", "''");
                    objSchool.mobile = txtMobile.Text.Trim().Replace("'", "''");
                    objSchool.email = txtEmail.Text.Trim().Replace("'", "''");
                    objSchool.phone = txtPhone.Text.Trim().Replace("'", "''");
                    objSchool.contact_address = txtContactAddres.Text.Trim().Replace("'", "''");
                    objSchool.nationality = txtNationality.Text.Trim().Replace("'", "''");
                    objSchool.package_id = Convert.ToInt32(ddlPackage.SelectedValue);
                    //objSchool.register_date = Convert.ToDateTime(txtRegisterDate.Text.Trim().Replace("'", "''"));
                    //objSchool.expire_date = Convert.ToDateTime(txtExpire.Text.Trim().Replace("'", "''"));
                    objSchool.logo = lblItemImage.Text.Trim().Replace("'", "''");
                    objSchool.notes = txtNotes.Text.Trim().Replace("'", "''");
                    objSchool.status = chkActive.Checked;
                    objSchool.wilayath = txtWilayath.Text.Trim().Replace("'", "''");
                    objSchool.waynumber = txtWayNymber.Text.Trim().Replace("'", "''");

                    //objSchool.register_date = DateTime.ParseExact(txtRegisterDate.Text.Trim().Replace("'", "''"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    //objSchool.expire_date = DateTime.ParseExact(txtExpire.Text.Trim().Replace("'", "''"), "yyyy-MM-dd", CultureInfo.InvariantCulture);

                    objSchool.register_date = DateTime.ParseExact(txtRegisterDate.Text.Trim().Replace("'", "''"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    objSchool.expire_date = DateTime.ParseExact(txtExpireDate.Text.Trim().Replace("'", "''"), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    //objSchool.register_date = new DateTime(Convert.ToInt32(ddlRegisterYear.Text), Convert.ToInt32(ddlRegisterMonth.Text), Convert.ToInt32(ddlRegisterDay.Text));
                    //objSchool.expire_date = new DateTime(Convert.ToInt32(ddlExpireYear.Text), Convert.ToInt32(ddlExpireMonth.Text), Convert.ToInt32(ddlExpireDay.Text));

                    //objTeacher.present_address = txtPresentAddress.Text.Trim().Replace("'", "''");
                    //objTeacher.status = chkActive.Checked;
                    //objTeacher.permenant_address = txtPermenantAddress.Text.Trim().Replace("'", "''");
                    //objTeacher.qualification = txtQualification.Text.Trim().Replace("'", "''");
                    //objTeacher.department_id = Convert.ToInt32(ddlDepartment.SelectedValue);
                    //objTeacher.designation_id = Convert.ToInt32(ddlDesignation.SelectedValue);
                    //objTeacher.nationality = txtNationality.Text.Trim().Replace("'", "''");
                    //objTeacher.image_path = lblItemImage.Text.Trim().Replace("'", "''");
                    //objTeacher.image_path = hdItemImage.Value.Trim().Replace("'", "''");
                    objSchool.ID = String.IsNullOrEmpty(hdItemID.Value) ? 0 : int.Parse(hdItemID.Value);

                    if (objBL.isSchoolCodeAlreadyExist(objSchool))
                    {
                        lblError.Text = "Schools's Code Already Exist";
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

                    string result = objBL.InsertSchool(objSchool);
                    fillSchools();
                    if (result == "success")
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                            "School details Added/Updated Successfully"), true);
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
            fillSchools();
        }

        protected void GridItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = Convert.ToInt32(GridItems.DataKeys[e.RowIndex].Values["id"].ToString());
            eMallBL objBL = new eMallBL();
            string referenceIDMsg = objBL.isReferenceExist(ID, "School_id", "student,teacher,meeting,driver");
            if(referenceIDMsg != "NO")
                Response.Write("<script>alert('" + Server.HtmlEncode(referenceIDMsg) + "')</script>");
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert(" + Server.HtmlEncode(referenceIDMsg) + ");", true);
            else
            {
                //string status = objBL.DeleteSchool(ID);
                string status = "";
                if (status == "success")
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert('Records Deleted Successfully');", true);
                else
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert('Error... Please try Again');", true);
            }
            fillSchools();
            btnSave.Text = "Save";
            Clearfields();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuSchooLogo.HasFile)
                {
                    try
                    {
                        if (fuSchooLogo.PostedFile.ContentType == "image/jpeg" || fuSchooLogo.PostedFile.ContentType == "image/png")
                        {
                            if (fuSchooLogo.PostedFile.ContentLength < 1024000)
                            {
                                string filename = Path.GetFileName(fuSchooLogo.FileName);
                                fuSchooLogo.SaveAs(Server.MapPath("~/school_logo/") + filename);
                                lblItemImage.Text = filename;
                                //hdItemImage.Value = filename;
                                imgItem.ImageUrl = "school_logo/" + filename;
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

        protected void GridItems_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //DataKeyNames = "ID,Description,Overview,OfferDescription,CategoryID,SubCategoryID,IsActive,ReleaseDate,Specefications" >
            ////hdItemID.Value = GridItems.DataKeys[e.NewEditIndex].Values["id"].ToString();// GridItems.Rows[e.NewEditIndex].Cells[0].Text;
            ////txtcode.Text = GridItems.DataKeys[e.NewEditIndex].Values["code"].ToString();
            ////txtName.Text = GridItems.DataKeys[e.NewEditIndex].Values["name"].ToString();
            ////txtEmail.Text = GridItems.DataKeys[e.NewEditIndex].Values["email"].ToString();
            ////fillPackage();
            ////ddlPackage.SelectedValue = GridItems.DataKeys[e.NewEditIndex].Values["package_id"].ToString();
            ////chkActive.Checked = GridItems.DataKeys[e.NewEditIndex].Values["status"].ToString() == "1" ? true : false;
            ////txtNationality.Text = GridItems.DataKeys[e.NewEditIndex].Values["nationality"].ToString();
            ////txtSchoolAddress.Text = GridItems.DataKeys[e.NewEditIndex].Values["address"].ToString();
            ////txtContactAddres.Text = GridItems.DataKeys[e.NewEditIndex].Values["contact_address"].ToString();
            ////txtPhone.Text = GridItems.DataKeys[e.NewEditIndex].Values["phone"].ToString();
            ////txtMobile.Text = GridItems.DataKeys[e.NewEditIndex].Values["mobile"].ToString();
            ////imgItem.ImageUrl = "/school_logo/" + GridItems.DataKeys[e.NewEditIndex].Values["logo"].ToString();
            ////txtSiteURL.Text = GridItems.DataKeys[e.NewEditIndex].Values["site_url"].ToString();
            ////txtContactPerson.Text = GridItems.DataKeys[e.NewEditIndex].Values["contact_person"].ToString();
            ////txtRegisterDate.Text = GridItems.DataKeys[e.NewEditIndex].Values["register_date"].ToString();
            ////txtExpireDate.Text = GridItems.DataKeys[e.NewEditIndex].Values["expire_date"].ToString();
            ////lblItemImage.Text = GridItems.DataKeys[e.NewEditIndex].Values["logo"].ToString();
            ////txtNotes.Text = GridItems.DataKeys[e.NewEditIndex].Values["notes"].ToString();

            //////ddlRegisterYear.SelectedValue = Convert.ToDateTime(GridItems.DataKeys[e.NewEditIndex].Values["register_date"].ToString()).Year.ToString();
            //////ddlRegisterMonth.SelectedValue = Convert.ToDateTime(GridItems.DataKeys[e.NewEditIndex].Values["register_date"].ToString()).Month.ToString();
            //////ddlRegisterDay.SelectedValue = Convert.ToDateTime(GridItems.DataKeys[e.NewEditIndex].Values["register_date"].ToString()).Day.ToString();

            //////ddlExpireYear.SelectedValue = Convert.ToDateTime(GridItems.DataKeys[e.NewEditIndex].Values["expire_date"].ToString()).Year.ToString();
            //////ddlExpireMonth.SelectedValue = Convert.ToDateTime(GridItems.DataKeys[e.NewEditIndex].Values["expire_date"].ToString()).Month.ToString();
            //////ddlExpireDay.SelectedValue = Convert.ToDateTime(GridItems.DataKeys[e.NewEditIndex].Values["expire_date"].ToString()).Day.ToString();

            ////txtRegisterDate.Text = Convert.ToDateTime(GridItems.DataKeys[e.NewEditIndex].Values["register_date"].ToString()).ToString("dd/MM/yyy");
            ////txtExpireDate.Text = Convert.ToDateTime(GridItems.DataKeys[e.NewEditIndex].Values["expire_date"].ToString()).ToString("dd/MM/yyy");

            ////txtWilayath.Text = GridItems.DataKeys[e.NewEditIndex].Values["wilayath"].ToString();
            ////txtWayNymber.Text = GridItems.DataKeys[e.NewEditIndex].Values["waynumber"].ToString();


            ////////from data bound
            //////txtItemCode.Text = GridItems.Rows[e.NewEditIndex].Cells[0].Text;
            //////txtName.Text = GridItems.Rows[e.NewEditIndex].Cells[1].Text;
            //////txtPrice.Text = GridItems.Rows[e.NewEditIndex].Cells[2].Text;
            //////imgItem.ImageUrl = "/Files/" + GridItems.Rows[e.NewEditIndex].Cells[3].Text;
            //////lblItemImage.Text = GridItems.Rows[e.NewEditIndex].Cells[3].Text;
            //////chkHasOffer.Checked = GridItems.Rows[e.NewEditIndex].Cells[4].Text == "Yes" ? true : false;
            //////txtOfferPrice.Text = GridItems.Rows[e.NewEditIndex].Cells[5].Text;


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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillSchools();
            Clearfields();
        }

        protected void ddlRegisterMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillRegisterDays();
        }

        protected void ddlRegisterYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillRegisterDays();
        }

        protected void ddlExpireMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillExpireDays();
        }

        protected void ddlExpireYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillExpireDays();
        }

        protected void GridItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Change")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridItems.Rows[index];

                //===========================================================
                //DataKeyNames = "ID,Description,Overview,OfferDescription,CategoryID,SubCategoryID,IsActive,ReleaseDate,Specefications" >
                hdItemID.Value = GridItems.DataKeys[index].Values["id"].ToString();// GridItems.Rows[index].Cells[0].Text;
                txtcode.Text = GridItems.DataKeys[index].Values["code"].ToString();
                txtName.Text = GridItems.DataKeys[index].Values["name"].ToString();
                txtEmail.Text = GridItems.DataKeys[index].Values["email"].ToString();
                fillPackage();
                ddlPackage.SelectedValue = GridItems.DataKeys[index].Values["package_id"].ToString();
                chkActive.Checked = GridItems.DataKeys[index].Values["status"].ToString() == "1" ? true : false;
                txtNationality.Text = GridItems.DataKeys[index].Values["nationality"].ToString();
                txtSchoolAddress.Text = GridItems.DataKeys[index].Values["address"].ToString();
                txtContactAddres.Text = GridItems.DataKeys[index].Values["contact_address"].ToString();
                txtPhone.Text = GridItems.DataKeys[index].Values["phone"].ToString();
                txtMobile.Text = GridItems.DataKeys[index].Values["mobile"].ToString();
                imgItem.ImageUrl = "/school_logo/" + GridItems.DataKeys[index].Values["logo"].ToString();
                txtSiteURL.Text = GridItems.DataKeys[index].Values["site_url"].ToString();
                txtContactPerson.Text = GridItems.DataKeys[index].Values["contact_person"].ToString();
                txtRegisterDate.Text = GridItems.DataKeys[index].Values["register_date"].ToString();
                txtExpireDate.Text = GridItems.DataKeys[index].Values["expire_date"].ToString();
                lblItemImage.Text = GridItems.DataKeys[index].Values["logo"].ToString();
                txtNotes.Text = GridItems.DataKeys[index].Values["notes"].ToString();

                //ddlRegisterYear.SelectedValue = Convert.ToDateTime(GridItems.DataKeys[index].Values["register_date"].ToString()).Year.ToString();
                //ddlRegisterMonth.SelectedValue = Convert.ToDateTime(GridItems.DataKeys[index].Values["register_date"].ToString()).Month.ToString();
                //ddlRegisterDay.SelectedValue = Convert.ToDateTime(GridItems.DataKeys[index].Values["register_date"].ToString()).Day.ToString();

                //ddlExpireYear.SelectedValue = Convert.ToDateTime(GridItems.DataKeys[index].Values["expire_date"].ToString()).Year.ToString();
                //ddlExpireMonth.SelectedValue = Convert.ToDateTime(GridItems.DataKeys[index].Values["expire_date"].ToString()).Month.ToString();
                //ddlExpireDay.SelectedValue = Convert.ToDateTime(GridItems.DataKeys[index].Values["expire_date"].ToString()).Day.ToString();

                txtRegisterDate.Text = Convert.ToDateTime(GridItems.DataKeys[index].Values["register_date"].ToString()).ToString("dd/MM/yyy");
                txtExpireDate.Text = Convert.ToDateTime(GridItems.DataKeys[index].Values["expire_date"].ToString()).ToString("dd/MM/yyy");

                txtWilayath.Text = GridItems.DataKeys[index].Values["wilayath"].ToString();
                txtWayNymber.Text = GridItems.DataKeys[index].Values["waynumber"].ToString();

                btnSave.Text = "Update";
                btnAddNew.Enabled = true;
                //=========================================================
            }
        }

        #endregion

        #region Private Methods

        private void fillPackagesearch()
        {
            try
            {
                DataTable dtTbl = new DataTable();
                dtTbl = (new eMallBL()).getPackage(0);
                ddlSearchPackage.DataSource = dtTbl;
                ddlSearchPackage.DataTextField = "name";
                ddlSearchPackage.DataValueField = "id";
                ddlSearchPackage.DataBind();

                // Insert 'Select'
                ddlSearchPackage.Items.Insert(0, new ListItem("--All--", "0"));
                // Set selected index
                ddlSearchPackage.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void fillPackage()
        {
            try
            {
                DataTable dtTbl = new DataTable();
                dtTbl = (new eMallBL()).getPackage(0);
                ddlPackage.DataSource = dtTbl;
                ddlPackage.DataTextField = "name";
                ddlPackage.DataValueField = "id";
                ddlPackage.DataBind();

                // Insert 'Select'
                ddlPackage.Items.Insert(0, new ListItem("--Select--", "0"));
                // Set selected index
                ddlPackage.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void fillRegisterDates()
        {
            //try
            //{
            //    for (int i = 2013; i <= 2020; i++)
            //    {
            //        ddlRegisterYear.Items.Add(i.ToString());
            //    }
            //    ddlRegisterYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;  //set current year as selected


            //    for (int i = 1; i <= 12; i++)
            //    {
            //        ddlRegisterMonth.Items.Add(i.ToString());
            //    }
            //    ddlRegisterMonth.Items.FindByValue(System.DateTime.Now.Month.ToString()).Selected = true; // Set current month as selected

            //    FillRegisterDays();
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }

        public void FillRegisterDays()
        {
            //ddlRegisterDay.Items.Clear();
            ////getting numbner of days in selected month & year
            //int noofdays = DateTime.DaysInMonth(Convert.ToInt32(ddlRegisterYear.SelectedValue), Convert.ToInt32(ddlRegisterMonth.SelectedValue));

            ////Fill days
            //for (int i = 1; i <= noofdays; i++)
            //{
            //    ddlRegisterDay.Items.Add(i.ToString());
            //}
            //if(!IsPostBack)
            //    ddlRegisterDay.Items.FindByValue(System.DateTime.Now.Day.ToString()).Selected = true;// Set current date as selected
        }

        private void fillExpireDates()
        {
            //try
            //{
            //    for (int i = 2013; i <= 2020; i++)
            //    {
            //        ddlExpireYear.Items.Add(i.ToString());
            //    }
            //    ddlExpireYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;  //set current year as selected


            //    for (int i = 1; i <= 12; i++)
            //    {
            //        ddlExpireMonth .Items.Add(i.ToString());
            //    }
            //    ddlExpireMonth.Items.FindByValue(System.DateTime.Now.Month.ToString()).Selected = true; // Set current month as selected

            //    FillExpireDays();
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }

        public void FillExpireDays()
        {
            //ddlExpireDay.Items.Clear();
            ////getting numbner of days in selected month & year
            //int noofdays = DateTime.DaysInMonth(Convert.ToInt32(ddlExpireYear.SelectedValue), Convert.ToInt32(ddlExpireMonth.SelectedValue));

            ////Fill days
            //for (int i = 1; i <= noofdays; i++)
            //{
            //    ddlExpireDay.Items.Add(i.ToString());
            //}
            //ddlExpireDay.Items.FindByValue(System.DateTime.Now.Day.ToString()).Selected = true;// Set current date as selected
        }

        private void fillSchools()
        {
            eMallEntity.school objSchool = new eMallEntity.school();
            //objSearch.categoryID = Convert.ToInt32(ddlSearchCategory.SelectedValue == "" ? "0" : ddlSearchCategory.SelectedValue);
            //objSearch.subCategoryID = Convert.ToInt32(ddlSearchSubCategory.SelectedValue == "" ? "0" : ddlSearchSubCategory.SelectedValue);
            //objSearch.isActive = Convert.ToInt32(ddlActive.SelectedValue == "" ? "0" : ddlActive.SelectedValue);
            //objSearch.hasOffer = Convert.ToInt32(ddlHasOffer.SelectedValue == "" ? "0" : ddlHasOffer.SelectedValue);
            objSchool.package_id = Convert.ToInt32(ddlPackage.SelectedValue == "" ? "0" : ddlSearchPackage.SelectedValue);
            switch (ddlSearchBy.SelectedValue)
            {
                case "All":
                    objSchool.name = txtSearch.Text.Trim();
                    objSchool.school_address = txtSearch.Text.Trim();
                    objSchool.search_operator = "OR";
                    break;
                case "Name":
                    objSchool.name = txtSearch.Text.Trim();
                    objSchool.search_operator = "AND";
                    break;
                case "Address":
                    objSchool.school_address = txtSearch.Text.Trim();
                    objSchool.search_operator = "AND";
                    break;
            }
            //objSearch.userID = Convert.ToInt32(ddlUsers.SelectedValue == "" ? "0" : ddlUsers.SelectedValue);

            DataTable dtTbl = new DataTable();
            dtTbl = (new eMallBL()).searchSchool(objSchool);
            GridItems.PageSize = Convert.ToInt32(10);
            GridItems.DataSource = dtTbl;
            GridItems.DataBind();
        }

        private void Clearfields()
        {
            txtContactAddres.Text = "";
            txtSchoolAddress.Text = "";
            txtEmail.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
            txtcode.Text = "";
            txtMobile.Text = "";
            btnSave.Text = "Save";
            hdItemID.Value = "";
            //hdItemImage.Value = "";
            lblItemImage.Text = "";
            txtNationality.Text = "";
            imgItem.ImageUrl = "";
            ddlPackage.SelectedIndex = 0;
            txtSiteURL.Text = "";
            txtNotes.Text = "";
            txtContactPerson.Text = "";
            //////txtRegisterDate.Text = "";
            //////txtExpire.Text = "";
            chkActive.Checked = false;
            txtWilayath.Text = "";
            txtWayNymber.Text = "";
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

        #endregion

    }
}