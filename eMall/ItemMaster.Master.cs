using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace eMall
{
    public partial class ItemMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
      
        #endregion
    }
}
