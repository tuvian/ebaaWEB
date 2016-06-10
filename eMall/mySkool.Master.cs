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
    public partial class mySkool : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            divSchool.Attributes.Add("Class", SchoolMenu);
            if (Session["usertype"] != null && Session["usertype"].ToString() == "2")
            {
                divSchool.Visible = false;
                int school_id = 0;
                int.TryParse(Session["school_id"].ToString(), out school_id);
                eMallEntity.school objSchool = new eMallEntity.school();
                objSchool.ID = school_id;
                DataTable dtTbl = new DataTable();
                dtTbl = (new eMallBL()).searchSchool(objSchool);
                imgSchooLogo.Src = "/school_logo/" + dtTbl.Rows[0]["logo"].ToString();
                imgSchooLogo.Width = 178;
                imgSchooLogo.Height = 95;
            }
        }

        #region Prperties

        private string _homeMenu = "menubutton";
        public string HomeMenu
        {
            get { return _homeMenu; }
            set { _homeMenu = value; }
        }
        private string _homeLink = "link";
        public string HomeLink
        {
            get { return _homeLink; }
            set { _homeLink = value; }
        }

        private string _shopeMenu = "menubutton";
        public string ShopeMenu
        {
            get { return _shopeMenu; }
            set { _shopeMenu = value; }
        }
        private string _shopeLink = "link";
        public string ShopeLink
        {
            get { return _shopeLink; }
            set { _shopeLink = value; }
        }

        private string _contactUsMenu = "menubutton";
        public string ContactUsMenu
        {
            get { return _contactUsMenu; }
            set { _contactUsMenu = value; }
        }
        private string _contactUsLink = "link";
        public string ContactUsLink
        {
            get { return _contactUsLink; }
            set { _contactUsLink = value; }
        }

        private string _aboutUsMenu = "menubutton";
        public string AboutUsMenu
        {
            get { return _aboutUsMenu; }
            set { _aboutUsMenu = value; }
        }
        private string _aboutUsLink = "link";
        public string AboutUsLink
        {
            get { return _aboutUsLink; }
            set { _aboutUsLink = value; }
        }

        private string _schoolMenu = "menubutton";
        public string SchoolMenu
        {
            get { return _schoolMenu; }
            set { _schoolMenu = value; }
        }
        private string _schoolLink = "link";
        public string SchoolLink
        {
            get { return _schoolLink; }
            set { _schoolLink = value; }
        }

        private string _userMenu = "menubutton";
        public string UserMenu
        {
            get { return _userMenu; }
            set { _userMenu = value; }
        }
        private string _userLink = "link";
        public string UserLink
        {
            get { return _userLink; }
            set { _userLink = value; }
        }


        private string _holidayMenu = "menubutton";
        public string HolidayMenu
        {
            get { return _holidayMenu; }
            set { _holidayMenu = value; }
        }
        private string _holidayLink = "link";
        public string HolidaLink
        {
            get { return _holidayLink; }
            set { _holidayLink = value; }
        }


        private string _studentsMenu = "menubutton";
        public string StudentsMenu
        {
            get { return _studentsMenu; }
            set { _studentsMenu = value; }
        }
        private string _studentsLink = "link";
        public string StudentsLink
        {
            get { return _studentsLink; }
            set { _studentsLink = value; }
        }

        private string _busMenu = "menubutton";
        public string BusMenu
        {
            get { return _busMenu; }
            set { _busMenu = value; }
        }
        private string _busLink = "link";
        public string BusLink
        {
            get { return _busLink; }
            set { _busLink = value; }
        }

        private string _driverMenu = "menubutton";
        public string DriverMenu
        {
            get { return _driverMenu; }
            set { _driverMenu = value; }
        }
        private string _driverLink = "link";
        public string DriverLink
        {
            get { return _driverLink; }
            set { _driverLink = value; }
        }

        private string _studentloginmenu = "menubutton";
        public string StudentLoginMenu
        {
            get { return _studentloginmenu; }
            set { _studentloginmenu = value; }
        }
        private string _studentloginLink = "link";
        public string StudentLoginLink
        {
            get { return _studentloginLink; }
            set { _studentloginLink = value; }
        }

        #endregion

        protected void lbLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("default.aspx");
        }
    }
}