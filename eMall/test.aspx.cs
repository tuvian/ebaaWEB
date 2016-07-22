using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using PushSharp;
//using PushSharp.Android;
using PushSharp.Apple;
using PushSharp.Core;
using System.Net;
using System.Text;
using System.IO;

namespace eMall
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSebd_Click(object sender, EventArgs e)
        {
            try
            {
                string result = "";
                result = SendNotification(txtDeviceID.Text.Replace("'", "''"), txtMsg.Text.Replace("'", "''"));
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                        result), true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                    "Error" + ex.Message), true);
            }
        }
        
        protected void btnSend_2_Click(object sender, EventArgs e)
        {
            try
            {
                sendPushNotification2();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                    "Error" + ex.Message), true);
            }
        }
        
        public string SendNotification(string deviceId, string message)
        {
            try
            {
                string GoogleAppID = txtAppID.Text.Replace("'", "''");
                var SENDER_ID = txtSenderID.Text.Replace("'", "''");
                var value = message;
                WebRequest tRequest;
                tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", GoogleAppID));

                tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

                string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + value + "&data.time=" +
                System.DateTime.Now.ToString() + "&registration_id=" + deviceId + "";
                Console.WriteLine(postData);
                Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                tRequest.ContentLength = byteArray.Length;

                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse tResponse = tRequest.GetResponse();

                dataStream = tResponse.GetResponseStream();

                StreamReader tReader = new StreamReader(dataStream);

                String sResponseFromServer = tReader.ReadToEnd();

                tReader.Close();
                dataStream.Close();
                tResponse.Close();
                return sResponseFromServer;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                        "Error :" + ex.Message), true);
                return "Eror";
            }
        }

        protected void sendPushNotification2()
        {
            try
            {
                //Registration Id created by Android App i.e. DeviceId.  
                string regId;
                regId = txtRegID_2.Text.Replace("'", "''");
                //API Key created in Google project  
                var applicationID = txtAppID_2.Text.Replace("'", "''");

                //Project ID created in Google project.  
                var SENDER_ID = txtSender_2.Text.Replace("'", "''");
                var varMessage = txtMsg_2.Text.Replace("'", "''");
                WebRequest tRequest;
                tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));

                tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

                string postDataToServer = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message="
                                   + varMessage + "®istration_id=" + regId + "";
                //"&data.time=" + System.DateTime.Now.ToString() + 

                Console.WriteLine(postDataToServer);
                Byte[] byteArray = Encoding.UTF8.GetBytes(postDataToServer);
                tRequest.ContentLength = byteArray.Length;

                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse tResponse = tRequest.GetResponse();

                dataStream = tResponse.GetResponseStream();

                StreamReader tReader = new StreamReader(dataStream);

                String sResponseFromServer = tReader.ReadToEnd();

                tReader.Close();
                dataStream.Close();
                tResponse.Close();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                        "Error :" + ex.Message), true);
            }
        }
    }
}