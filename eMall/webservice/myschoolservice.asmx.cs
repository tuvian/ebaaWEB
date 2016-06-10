using BusinussLayer;
using Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace eMall.webservice
{
    /// <summary>
    /// Summary description for myschoolservice
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class myschoolservice : System.Web.Services.WebService
    {

        #region Login 
        [WebMethod]
        public string login(string objlogin)
        {
            try
            {            
                eMallEntity.login objloginOutput = new eMallEntity.login();    
                eMallEntity.login objloginInput = (eMallEntity.login)JsonConvert.DeserializeObject(objlogin, typeof(eMallEntity.login));
                DataTable dtTbl = new DataTable();
                dtTbl = (new eMallBL()).getLogin(objloginInput.username, objloginInput.password, "");

                if (dtTbl.Rows.Count > 0)
                {
                    objloginOutput.type = Convert.ToInt32(dtTbl.Rows[0]["type"].ToString());
                    objloginOutput.username = dtTbl.Rows[0]["username"].ToString();
                    objloginOutput.password = dtTbl.Rows[0]["password"].ToString();
                    objloginOutput.school_id = Convert.ToInt32(dtTbl.Rows[0]["school_id"].ToString());
                    objloginOutput.active = Convert.ToInt32(dtTbl.Rows[0]["status"].ToString());
                }
                else
                {
                    objloginOutput.username = "";
                    objloginOutput.password = "";
                    objloginOutput.type = 0;
                    objloginOutput.api_message = "Invalid username/password";
                }
                JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
                string jsonOutput = JsonConvert.SerializeObject(objloginOutput);

                return jsonOutput;
            }
            catch (Exception ex)
            {
                eMallEntity.login objloginOutput = new eMallEntity.login();
                objloginOutput.api_status = "Error";
                objloginOutput.api_message = ex.Message;
                string jsonOutput = JsonConvert.SerializeObject(objloginOutput);
                return jsonOutput;
            }
        }
        #endregion
        
    }
}
