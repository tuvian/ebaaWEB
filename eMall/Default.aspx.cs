using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Entity;
using BusinussLayer;

namespace eMall
{
    public partial class WebForm1234 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Master.HomeMenu = "menubuttonselect";
            //Master.HomeLink = "linkselect";
        }

        protected void btnSearch_Click1(object sender, EventArgs e)
        {
            //Context.Items.Add("SearchBy", "All");
            ////Context.Items.Add("SearchKey", txtSearch.Text.Trim().Replace("'", "''"));
            //Server.Transfer("Products.aspx");
        }

        protected void btnLogin_Click1(object sender, EventArgs e)
        {
            DataTable dtTblLogin = new DataTable();
            DataTable dtTblEmployee = new DataTable();

            dtTblLogin = (new eMallBL()).getLogin(txtUserName.Text, txtPassword.Text.Trim(), "");
            if (dtTblLogin.Rows.Count > 0 && (dtTblLogin.Rows[0]["Type"].ToString() == "1" || dtTblLogin.Rows[0]["Type"].ToString() == "2"))
            {
                int loginID = 0;
                int.TryParse(dtTblLogin.Rows[0]["ID"].ToString(),out loginID);
                if (dtTblLogin.Rows[0]["type"].ToString() == "4")
                {
                    dtTblEmployee = (new eMallBL()).searchStudentByLoginID(loginID);
                    if (dtTblEmployee.Rows.Count > 0)
                    {
                        Session["employee_name"] = dtTblEmployee.Rows[0]["first_name"].ToString();
                        Session["employee_id"] = dtTblEmployee.Rows[0]["ID"].ToString();                        
                    }
                }
                else if (dtTblLogin.Rows[0]["type"].ToString() == "3")
                {
                    dtTblEmployee = (new eMallBL()).searchTeacherByLoginID(loginID);
                    if (dtTblEmployee.Rows.Count > 0)
                    {
                        Session["employee_name"] = dtTblEmployee.Rows[0]["name"].ToString();
                        Session["employee_id"] = dtTblEmployee.Rows[0]["ID"].ToString();
                    }
                }
                else if (dtTblLogin.Rows[0]["type"].ToString() == "2")
                {
                    Session["employee_name"] = "School Admin";
                    Session["employee_id"] = dtTblLogin.Rows[0]["ID"].ToString();
                }
                else if (dtTblLogin.Rows[0]["type"].ToString() == "1")
                {
                    Session["employee_name"] = "Super Admin";
                    Session["employee_id"] = dtTblLogin.Rows[0]["ID"].ToString();
                }
                else
                    Session["employee_name"] = "";


                Session["username"] = dtTblLogin.Rows[0]["UserName"].ToString();
                Session["password"] = dtTblLogin.Rows[0]["Password"].ToString();
                Session["usertype"] = dtTblLogin.Rows[0]["Type"].ToString();
                Session["user_id"] = dtTblLogin.Rows[0]["ID"].ToString();
                Session["school_id"] = dtTblLogin.Rows[0]["school_id"].ToString();
                Response.Redirect("teachers.aspx");
            }
            else
            {
                errorSpan.InnerHtml = "The username/Password you entered is wrong. Please try again.";
            }
        }
    }
}
