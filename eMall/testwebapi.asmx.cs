using BusinussLayer;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using Newtonsoft.Json;


namespace eMall
{
    /// <summary>
    /// Summary description for testwebapi
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class testwebapi : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string TestWorld(int a, int b)
        {
            eMallEntity.school objSchool = new eMallEntity.school();
            eMallEntity.school objSchoolNew = new eMallEntity.school();
            DataTable dtTbl = new DataTable();
            dtTbl = (new eMallBL()).searchSchool(objSchool);

            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();

            //optional: you can create your own custom converter
            //TheSerializer.RegisterConverters(new JavaScriptConverter[] {new MyCustomJson()});

            //var products =  dtTbl;
            string jsonEvents = JsonConvert.SerializeObject(dtTbl);

            List<eMallEntity.school> myDeserializedObjList = (List<eMallEntity.school>)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonEvents, typeof(List<eMallEntity.school>));

            //object objSchoolNew1 = new JavaScriptSerializer().DeserializeObject(jsonEvents);
            //objSchoolNew = (eMallEntity.school)objSchoolNew1;

            //var TheJson = TheSerializer.Serialize(products);

            string jsonOutput = TheSerializer.Serialize(myDeserializedObjList);

            return jsonEvents;
        }

        [WebMethod]
        public string login(string objlogin)
        {
            eMallEntity.login objloginOutput = new eMallEntity.login();
            eMallEntity.login objloginInput = (eMallEntity.login)JsonConvert.DeserializeObject(objlogin, typeof(eMallEntity.login));
            DataTable dtTbl = new DataTable();
            dtTbl = (new eMallBL()).getLogin(objloginInput.username, objloginInput.password,"");

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

            //eMallEntity.school objSchool = new eMallEntity.school();
            //eMallEntity.school objSchoolNew = new eMallEntity.school();
            //DataTable dtTbl = new DataTable();
            //dtTbl = (new eMallBL()).searchSchool(objSchool);

            //JavaScriptSerializer TheSerializer = new JavaScriptSerializer();

            ////optional: you can create your own custom converter
            ////TheSerializer.RegisterConverters(new JavaScriptConverter[] {new MyCustomJson()});

            ////var products =  dtTbl;
            //string jsonEvents = JsonConvert.SerializeObject(dtTbl);

            //List<eMallEntity.school> myDeserializedObjList = (List<eMallEntity.school>)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonEvents, typeof(List<eMallEntity.school>));

            //object objSchoolNew1 = new JavaScriptSerializer().DeserializeObject(jsonEvents);
            //objSchoolNew = (eMallEntity.school)objSchoolNew1;

            //var TheJson = TheSerializer.Serialize(products);

            //string jsonOutput = TheSerializer.Serialize(myDeserializedObjList);

            return jsonOutput;
        }


        [WebMethod]
        public string getLoginParameter()
        {
            eMallEntity.login objloginOutput = new eMallEntity.login();
            //eMallEntity.login objloginInput = (eMallEntity.login)JsonConvert.DeserializeObject(objlogin, typeof(eMallEntity.login));
            //DataTable dtTbl = new DataTable();
            //dtTbl = (new eMallBL()).getLogin(objloginInput.username, objloginInput.password, "");

            //if (dtTbl.Rows.Count > 0)
            //{
                //objloginOutput.type = Convert.ToInt32(dtTbl.Rows[0]["type"].ToString());
                objloginOutput.username = "b";
                objloginOutput.password = "b";
                //objloginOutput.school_id = Convert.ToInt32(dtTbl.Rows[0]["school_id"].ToString());
                //objloginOutput.active = Convert.ToInt32(dtTbl.Rows[0]["status"].ToString());
            //}
            //else
            //{
            //    objloginOutput.username = "";
            //    objloginOutput.password = "";
            //    objloginOutput.type = 0;
            //}
            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
            string jsonOutput = JsonConvert.SerializeObject(objloginOutput);

            //eMallEntity.school objSchool = new eMallEntity.school();
            //eMallEntity.school objSchoolNew = new eMallEntity.school();
            //DataTable dtTbl = new DataTable();
            //dtTbl = (new eMallBL()).searchSchool(objSchool);

            //JavaScriptSerializer TheSerializer = new JavaScriptSerializer();

            ////optional: you can create your own custom converter
            ////TheSerializer.RegisterConverters(new JavaScriptConverter[] {new MyCustomJson()});

            ////var products =  dtTbl;
            //string jsonEvents = JsonConvert.SerializeObject(dtTbl);

            //List<eMallEntity.school> myDeserializedObjList = (List<eMallEntity.school>)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonEvents, typeof(List<eMallEntity.school>));

            //object objSchoolNew1 = new JavaScriptSerializer().DeserializeObject(jsonEvents);
            //objSchoolNew = (eMallEntity.school)objSchoolNew1;

            //var TheJson = TheSerializer.Serialize(products);

            //string jsonOutput = TheSerializer.Serialize(myDeserializedObjList);

            return jsonOutput;
        }


        [WebMethod]
        public string Userlogin(string username, string password)
        {
            eMallEntity.login objloginOutput = new eMallEntity.login();
            //eMallEntity.login objloginInput = (eMallEntity.login)JsonConvert.DeserializeObject(objlogin, typeof(eMallEntity.login));
            DataTable dtTbl = new DataTable();
            dtTbl = (new eMallBL()).getLogin(username, password, "");

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

            //eMallEntity.school objSchool = new eMallEntity.school();
            //eMallEntity.school objSchoolNew = new eMallEntity.school();
            //DataTable dtTbl = new DataTable();
            //dtTbl = (new eMallBL()).searchSchool(objSchool);

            //JavaScriptSerializer TheSerializer = new JavaScriptSerializer();

            ////optional: you can create your own custom converter
            ////TheSerializer.RegisterConverters(new JavaScriptConverter[] {new MyCustomJson()});

            ////var products =  dtTbl;
            //string jsonEvents = JsonConvert.SerializeObject(dtTbl);

            //List<eMallEntity.school> myDeserializedObjList = (List<eMallEntity.school>)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonEvents, typeof(List<eMallEntity.school>));

            //object objSchoolNew1 = new JavaScriptSerializer().DeserializeObject(jsonEvents);
            //objSchoolNew = (eMallEntity.school)objSchoolNew1;

            //var TheJson = TheSerializer.Serialize(products);

            //string jsonOutput = TheSerializer.Serialize(myDeserializedObjList);

            return jsonOutput;
        }


    }
}
