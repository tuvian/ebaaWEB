using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Configuration;
using BusinussLayer;

namespace ASTCWeb.User_Controls
{
    public partial class CommonFileUploader : System.Web.UI.UserControl
    {
        #region Declarations
        public EventHandler uploadFileEvtArgs;
        public static string productImage = ConfigurationManager.AppSettings["ProductImages"].ToString();
        public string fileUploders = "";
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string pathOfFiles = "";
                string fileNames = "";
                if (Request.Files.Count > 0)
                {
                    if (isExceedFileSize())
                    {
                        //Alert.Show("File size exceed. Maximum allowed size is " + MaxSize);
                        return;
                    }
                    lblProgressLabel.Visible = true;
                    int totalFiles = 0;
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFile PostedFile = Request.Files[i];
                        if (PostedFile.ContentLength > 0 && isAllowedFileType(System.IO.Path.GetExtension(PostedFile.FileName)))
                        {
                            string folderPath = Server.MapPath(_filePath);
                            if (!Directory.Exists(folderPath))
                                Directory.CreateDirectory(folderPath);
                            PostedFile.SaveAs(Server.MapPath(_filePath + "\\") + PostedFile.FileName);
                            totalFiles += 1;
                            pathOfFiles += pathOfFiles == "" ? (Server.MapPath(_filePath + "\\") + PostedFile.FileName) :
                                "," + (Server.MapPath(_filePath + "\\") + PostedFile.FileName);
                            fileNames += fileNames == "" ? PostedFile.FileName : "," + PostedFile.FileName;
                        }
                    }
                    lblProgressLabel.Visible = false;
                    //Alert.Show((totalFiles == 0 ? "No" : totalFiles.ToString()) + " files uploaded");
                }

                _uploadedFilePath = pathOfFiles;
                _uploadedFileNames = fileNames;
                uploadFileEvtArgs(sender, e);
            }
            catch (Exception ex)
            {
                eMallBL.logErrors("UC_FileUploader", ex.GetType().ToString(), ex.Message);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// The no of maximukm files to upload.
        /// </summary>
        private int _UpperLimit = 0;
        public int UpperLimit
        {
            get { return _UpperLimit; }
            set { _UpperLimit = value; }
        }

        private string _filePath = productImage;
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        private int _MaxSize = 4194304;
        public int MaxSize
        {
            get { return _MaxSize; }
            set { _MaxSize = value; }
        }

        private string _fileTypes = "";
        public string FileTypes
        {
            get { return _fileTypes; }
            set { _fileTypes = value; }
        }

        private string _uploadedFilePath = "";
        public string UploadedFilePath
        {
            get { return _uploadedFilePath; }
            set { _uploadedFilePath = value; }
        }

        private string _uploadedFileNames = "";
        public string UploadedFileNames
        {
            get { return _uploadedFileNames; }
            set { _uploadedFileNames = value; }
        }

        #endregion

        #region Private Methods

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

        public static void PrintProgressBar()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id='updiv' style='Font-weight:bold;font-size:11pt;Left:320px;COLOR:black;font-family:verdana;Position:absolute;Top:140px;Text-Align:center;'>");
            sb.Append("&nbsp;<script> var up_div=document.getElementById('updiv');up_div.innerText='';</script>");
            sb.Append("<script language=javascript>");
            sb.Append("var dts=0; var dtmax=10;");
            sb.Append("function ShowWait(){var output;output='Please wait while uploading!';dts++;if(dts>=dtmax)dts=1;");
            sb.Append("for(var x=0;x < dts; x++){output+='';}up_div.innerText=output;up_div.style.color='red';}");
            sb.Append("function StartShowWait(){up_div.style.visibility='visible';ShowWait();window.setInterval('ShowWait()',100);}");
            sb.Append("StartShowWait();</script>");
            HttpContext.Current.Response.Write(sb.ToString());
            HttpContext.Current.Response.Flush();
        }

        //Javascript function to clear progressbar
        public static void ClearProgressBar()
        {
            StringBuilder sbc = new StringBuilder();
            sbc.Append("<script language='javascript'>");
            sbc.Append("alert('Upload process completed successfully!');");
            sbc.Append("up_div.style.visibility='hidden';");
            sbc.Append("history.go(-1)");
            sbc.Append("</script>");
            HttpContext.Current.Response.Write(sbc);
        }

        #endregion
    }
}