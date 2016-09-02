using BusinussLayer;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Google.GData.Extensions;
using Google.GData.YouTube;
using Google.GData.Extensions.MediaRss;
using Google.YouTube;
using Google.GData.Client;
using System.Threading;


namespace eMall
{
    public partial class library : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.LibMenu = "menubuttonselect";
            Master.LibLink = "linkselect";
            lblError.Text = "";
            if (!IsPostBack)
            {
                fillSearchScoolCodes();
                fillSearchStds();
                fillLibrary();
                fillSchoolCodes();
                fillStds();
                fillSubjects();
            }
        }

        private void fillLibrary()
        {
            eMallEntity.library objLib = new eMallEntity.library();
            objLib.school_id = Convert.ToInt32(ddlSearchSchoolCodes.SelectedValue == "" ? "0" : ddlSearchSchoolCodes.SelectedValue);
            objLib.std = ddlSeachClass.SelectedValue == "0" ? "All" : ddlSeachClass.SelectedValue;

            DataTable dtTbl = new DataTable();
            dtTbl = (new eMallBL()).searchLibrary(objLib);
            GridItems.PageSize = Convert.ToInt32(10);
            GridItems.DataSource = dtTbl;
            GridItems.DataBind();
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
                ddlSchoolCode.DataMember = "youtube_cert_file";
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
                ddlSearchSchoolCodes.Items.Insert(0, new ListItem("--All--", "0"));
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

        private void fillStds()
        {
            try
            {
                eMallEntity.Class objClass = new eMallEntity.Class();
                DataTable dtTbl = new DataTable();
                objClass.school_id = Convert.ToInt32(ddlSchoolCode.SelectedValue);
                if (objClass.school_id != 0)
                {
                    dtTbl = (new eMallBL()).getStds(objClass);
                    ddlClass.DataSource = dtTbl;
                    ddlClass.DataTextField = "std";
                    ddlClass.DataValueField = "std";
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

        private void fillSearchStds()
        {
            try
            {
                eMallEntity.Class objClass = new eMallEntity.Class();
                DataTable dtTbl = new DataTable();
                objClass.school_id = Convert.ToInt32(ddlSearchSchoolCodes.SelectedValue);
                if (objClass.school_id != 0)
                {
                    dtTbl = (new eMallBL()).getStds(objClass);
                    ddlSeachClass.DataSource = dtTbl;
                    ddlSeachClass.DataTextField = "std";
                    ddlSeachClass.DataValueField = "std";
                    ddlSeachClass.DataBind();
                }
                // Insert 'Select'
                ddlSeachClass.Items.Insert(0, new ListItem("--All--", "0"));
                // Set selected index
                ddlSeachClass.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillLibrary();
            Clearfields();
        }

        private void Clearfields()
        {
            tblfileUpload.Visible = false;
            btnSave.Text = "Save";
            hdItemID.Value = "";
            ddlClass.SelectedIndex = 0;
            ddlSubject.SelectedIndex = 0;
            txtFileTitle.Text = "";
            txUploadedFileName.Text = "";
            chkActive.Checked = false;
            if (Session["usertype"] != null && Session["usertype"].ToString() == "1")
                ddlSchoolCode.SelectedIndex = 0;
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Clearfields();
        }

        protected void GridItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridItems.PageIndex = e.NewPageIndex;
            fillLibrary();
        }

        protected void GridItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ItemID = Convert.ToInt32(GridItems.DataKeys[e.RowIndex].Values["id"].ToString());
            eMallBL objBL = new eMallBL();
            string status = objBL.DeleteLibrary(ItemID);
            if (status == "success")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert('Records Deleted Successfully');", true);
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "onclick", "alert('Error... Please try Again');", true);
            fillLibrary();
            btnSave.Text = "Save";
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
                        string libid = hdItemID.Value == null ? "" : hdItemID.Value;
                        string filename = Path.GetFileName(fuTeacher.FileName);
                        string FileExtension = filename.Substring(filename.LastIndexOf('.') + 1).ToLower();
                        string newFileName = Server.MapPath("~/lib_file/") + libid + filename;
                        if (FileExtension == "pdf")
                        {
                            if (fuTeacher.PostedFile.ContentLength < 2048000)
                            {
                                fuTeacher.SaveAs(newFileName);
                                txUploadedFileName.Text = filename;
                                //imgItem.ImageUrl = "teacher_image/" + filename;
                            }
                            else
                                lblError.Text = "Upload status: The file has to be less than 2 MB!";
                        }
                        else if (FileExtension == "mp4" || FileExtension == "flv" || FileExtension == "mpeg" || FileExtension == "avi" || FileExtension == "mov"
                            || FileExtension == "3gp" || FileExtension == "mkv" || FileExtension == "wmv" || FileExtension == "swf")
                        {
                            if (fuTeacher.PostedFile.ContentLength < 5096000)
                            {
                                fuTeacher.SaveAs(newFileName);
                                
                                List<string> tags = new List<string>();
                                tags.Add(ddlSubject.Text);
                                tags.Add(txtFileTitle.Text);
                                tags.Add(ddlClass.Text);
                                tags.Add("libid:" + libid);

                                string videoUniqueID = string.Format(@"{0}.{1}", System.DateTime.Now.Ticks, libid);
                                hdVideoUniqueID.Value = videoUniqueID;

                                string title = txtFileTitle.Text;
                                string description = "Class : " + ddlClass.Text + ", Subject = " + ddlSubject.Text + ", Title = " + txtFileTitle.Text;

                                eMallBL objBL = new eMallBL();
                                string youtubeCertFilePath = "";
                                int school_id = Convert.ToInt32(ddlSchoolCode.SelectedValue);
                                DataTable dtSchool = objBL.getSchoolYoutubeCertFile(school_id);
                                if (dtSchool.Rows.Count > 0)
                                    youtubeCertFilePath = dtSchool.Rows[0]["youtube_cert_file"] == null ? "" : dtSchool.Rows[0]["youtube_cert_file"].ToString();
                                if (youtubeCertFilePath == "")
                                {
                                    lblError.Text = "Youtube Certificate is not uploaded. Please contact Administrator";
                                    return;
                                }
                                youtubeCertFilePath = Server.MapPath("~/school_Youtube_Cert/") + youtubeCertFilePath;

                                YoutubeAPI yAPI = new YoutubeAPI();
                                string status = yAPI.uploadYoutubeVideo(newFileName, title, description, tags, videoUniqueID, youtubeCertFilePath);
                                if (status == "success")
                                    txUploadedFileName.Text = filename;
                                else
                                    lblError.Text = "Upload not successfull to Youtube :" + status ;
                            }
                            else
                                lblError.Text = "Upload status: The file has to be less than 5 MB!";
                        }
                        else
                            lblError.Text = "Upload status: Only pdf/mp4/flv/avi/mpeg/swf/mkv/mov/3gp files are accepted!";
                    }
                    catch (Exception ex)
                    {
                        lblError.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                eMallBL.logErrors("btnUpload_Click", ex.GetType().ToString(), ex.Message);
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
                txtFileTitle.Text = GridItems.DataKeys[index].Values["title"].ToString();
                txUploadedFileName.Text = GridItems.DataKeys[index].Values["file_path"].ToString();
                ddlSchoolCode.SelectedValue = GridItems.DataKeys[index].Values["school_id"].ToString();
                fillStds();
                ddlClass.SelectedValue = GridItems.DataKeys[index].Values["std"].ToString() == "" ? "0" : GridItems.DataKeys[index].Values["std"].ToString();
                fillSubjects();
                ddlSubject.SelectedValue = GridItems.DataKeys[index].Values["subject_id"].ToString() == "" ? "0" : GridItems.DataKeys[index].Values["subject_id"].ToString();
                chkActive.Checked = GridItems.DataKeys[index].Values["status"].ToString() == "1" ? true : false;

                btnSave.Text = "Update";
                btnAddNew.Enabled = true;
                tblfileUpload.Visible = true;
                //=========================================================
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
                    eMallEntity.library objLib = new eMallEntity.library();
                    objLib.std = ddlClass.SelectedValue.Trim().Replace("'", "''");
                    objLib.title = txtFileTitle.Text.Trim().Replace("'", "''");
                    objLib.filepath = txUploadedFileName.Text.Trim().Replace("'", "''");
                    objLib.status = chkActive.Checked;
                    objLib.ID = String.IsNullOrEmpty(hdItemID.Value) ? 0 : int.Parse(hdItemID.Value);
                    objLib.videoUniqueID = hdVideoUniqueID.Value == null ? "" : hdVideoUniqueID.Value;
                    objLib.school_id = Convert.ToInt32(ddlSchoolCode.SelectedValue);
                    objLib.subjectID = Convert.ToInt32(ddlSubject.SelectedValue);
                    objLib.filetype = txUploadedFileName.Text.Trim().Substring(txUploadedFileName.Text.Trim().LastIndexOf('.') + 1).ToLower();
                    objLib.createdBy = String.IsNullOrEmpty(Session["employee_id"].ToString()) ? 0 : int.Parse(Session["employee_id"].ToString());
                    objLib.createdByType = String.IsNullOrEmpty(Session["usertype"].ToString()) ? 0 : int.Parse(Session["usertype"].ToString());

                    string result = objBL.insertLibraryFile(objLib);
                    fillLibrary();
                    if (result == "success")
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                            "Libraray files Added/Updated Successfully"), true);
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

        protected void ddlSchoolCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillStds();
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillSubjects();
        }

        protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            //fillLessons();
        }

        private void fillSubjects()
        {
            try
            {
                string std = ddlClass.SelectedValue;
                int school_id = Convert.ToInt32(ddlSchoolCode.SelectedValue);
                if (std != "")
                {
                    DataTable dtTbl = (new eMallBL()).getLibrarySubjects(std, school_id);
                    ddlSubject.DataSource = dtTbl;
                    ddlSubject.DataTextField = "subject";
                    ddlSubject.DataValueField = "id";
                    ddlSubject.DataBind();
                }
                // Insert 'Select'
                ddlSubject.Items.Insert(0, new ListItem("--Select--", "0"));
                // Set selected index
                ddlSubject.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void ddlSearchSchoolCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillSearchStds();
        }

        public string UploadVideo(string FilePath, string Title, string Description)
        {
            try
            {
                YouTubeRequestSettings settings;
                YouTubeRequest request;
                //string devkey = "ebaa-141506";
                //string username = "ebaa4oman@gmail.com";
                //string password = "ebaa@1234";
                //settings = new YouTubeRequestSettings("ebaa", devkey, username, password) { Timeout = -1 };
                settings = new YouTubeRequestSettings("ebaa", "AIzaSyAeHQSjK5ozCVYXlRkvRtAyk11PuF9n_yY", "ebba4oman@gmail.com", "ebaa@1234") { Timeout = -1 };

                request = new YouTubeRequest(settings);

                Video newVideo = new Video();

                newVideo.Title = Title;
                newVideo.Description = Description;
                //newVideo.Private = true;
                newVideo.YouTubeEntry.Private = false;

                //newVideo.Tags.Add(new MediaCategory("Autos", YouTubeNameTable.CategorySchema));

                //newVideo.Tags.Add(new MediaCategory("mydevtag, anotherdevtag", YouTubeNameTable.DeveloperTagSchema));

                //newVideo.YouTubeEntry.setYouTubeExtension("location", "Paris, FR");
                // You can also specify just a descriptive string ==>
                // newVideo.YouTubeEntry.Location = new GeoRssWhere(71, -111);
                // newVideo.YouTubeEntry.setYouTubeExtension("location", "Paris, France.");

                newVideo.YouTubeEntry.MediaSource = new MediaFileSource(FilePath, "video/mp4");
                Video createdVideo = request.Upload(newVideo);

                return createdVideo.VideoId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void fillLessons()
        //{
        //    try
        //    {
        //        DataTable dtTbl = new DataTable();
        //        int subjectID  = Convert.ToInt32(ddlSubject.SelectedValue);
        //        if (subjectID != 0)
        //        {
        //            dtTbl = (new eMallBL()).getLibraryLessons(subjectID);
        //            ddlLesson.DataSource = dtTbl;
        //            ddlLesson.DataTextField = "Lesson";
        //            ddlLesson.DataValueField = "id";
        //            ddlLesson.DataBind();
        //        }
        //        // Insert 'Select'
        //        ddlLesson.Items.Insert(0, new ListItem("--Select--", "0"));
        //        // Set selected index
        //        ddlLesson.SelectedIndex = 0;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
    }
}