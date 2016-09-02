﻿using System;
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
using Entity;
using System.Data;
using BusinussLayer;
using System.Drawing;

namespace eMall
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //fillSchoolCodes();
            //fillClassForStudents();
            //DataTable dt = CreateDataTable();
            //BuildTree(dt, tvTest, true);
        }

        private void createListview()
        {
            //DataTable table = new DataTable();
            //table.Columns.Add("Dosage", typeof(int));
            //table.Columns.Add("Drug", typeof(string));
            //table.Columns.Add("Patient", typeof(string));
            //table.Columns.Add("Date", typeof(DateTime));

            //// Here we add five DataRows.
            //table.Rows.Add(25, "Indocin", "David", DateTime.Now);
            //table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
            //table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
            //table.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
            //table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);

            //tvTest.DataSource = table;
            //tvTest.DataBind();
        }


        private TreeNode Searchnode(string nodetext, TreeView trv)
        {
            foreach (TreeNode node in trv.Nodes)
            {
                if (node.Text == nodetext)
                {
                    return node;
                }
            }
            return null;
        }

        //=======================================================
        //Service provided by Telerik (www.telerik.com)
        //Conversion powered by NRefactory.
        //Twitter: @telerik
        //Facebook: facebook.com/telerik
        //=======================================================

        public DataTable CreateDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Country");
            // The value in this column will display on the TreeNode
            dataTable.Columns.Add("City");
            // The value in this column will identify its parentId 

            // Fill the DataTable
            dataTable.Rows.Add("India", "New Delhi");
            dataTable.Rows.Add("India", "Mumbai");
            dataTable.Rows.Add("India", "Kolkata");
            dataTable.Rows.Add("India", "Noida");
            dataTable.Rows.Add("USA", "New York");
            dataTable.Rows.Add("USA", "Washington");
            dataTable.Rows.Add("USA", "");
            dataTable.Rows.Add("USA", "India");
            return dataTable;
        }

        //=======================================================
        //Service provided by Telerik (www.telerik.com)
        //Conversion powered by NRefactory.
        //Twitter: @telerik
        //Facebook: facebook.com/telerik
        //=======================================================



        public void BuildTree(DataTable dt, TreeView trv, Boolean expandAll)
        {
            // Clear the TreeView if there are another datas in this TreeView
            trv.Nodes.Clear();
            TreeNode node = default(TreeNode);
            TreeNode subNode = default(TreeNode);
            foreach (DataRow row in dt.Rows)
            {
                //search in the treeview if any country is already present
                node = Searchnode(row.ItemArray[0].ToString(), trv);
                if (node != null)
                {
                    node.ShowCheckBox = true;
                    //Country is already present
                    subNode = new TreeNode(row.ItemArray[1].ToString().ToString());
                    subNode.Value = row.ItemArray[1].ToString().ToString();
                    subNode.ShowCheckBox = true;
                    //Add cities to country
                    node.ChildNodes.Add(subNode);

                }
                else
                {
                    node = new TreeNode(row.ItemArray[0].ToString().ToString());
                    node.ShowCheckBox = true;
                    subNode = new TreeNode(row.ItemArray[1].ToString());
                    subNode.Value = row.ItemArray[1].ToString().ToString();
                    subNode.ShowCheckBox = true;
                    //Add cities to country
                    node.ChildNodes.Add(subNode);
                    trv.Nodes.Add(node);                    
                }
            }
            if (expandAll)
            {
                // Expand the TreeView
                trv.ExpandAll();
            }
        }

        //=======================================================
        //Service provided by Telerik (www.telerik.com)
        //Conversion powered by NRefactory.
        //Twitter: @telerik
        //Facebook: facebook.com/telerik
        //=======================================================



        private void fillClassForStudents()
        {
            //try
            //{
            //    eMallEntity.Class objClass = new eMallEntity.Class();
            //    DataTable dtTbl = new DataTable();
            //    objClass.school_id = Convert.ToInt32(ddlSchoolCode.SelectedValue);
            //    if (objClass.school_id != 0)
            //    {
            //        dtTbl = (new eMallBL()).getClasses(objClass);
            //        chkClassForTeacher.DataSource = dtTbl;
            //        chkClassForTeacher.DataTextField = "classname";
            //        chkClassForTeacher.DataValueField = "id";
            //        chkClassForTeacher.DataBind();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }

        protected void ddlSchoolCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillClassForStudents();
        }

        //protected void btnSebd_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string result = "";
        //        result = SendNotification(txtDeviceID.Text.Replace("'", "''"), txtMsg.Text.Replace("'", "''"));
        //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
        //                result), true);
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
        //            "Error" + ex.Message), true);
        //    }
        //}

        //protected void btnSend_2_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        sendPushNotification2();
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
        //            "Error" + ex.Message), true);
        //    }
        //}

        //public string SendNotification(string deviceId, string message)
        //{
        //    try
        //    {
        //        string GoogleAppID = txtAppID.Text.Replace("'", "''");
        //        var SENDER_ID = txtSenderID.Text.Replace("'", "''");
        //        var value = message;
        //        WebRequest tRequest;
        //        tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
        //        tRequest.Method = "post";
        //        tRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
        //        tRequest.Headers.Add(string.Format("Authorization: key={0}", GoogleAppID));

        //        tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

        //        string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + value + "&data.time=" +
        //        System.DateTime.Now.ToString() + "&registration_id=" + deviceId + "";
        //        Console.WriteLine(postData);
        //        Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        //        tRequest.ContentLength = byteArray.Length;

        //        Stream dataStream = tRequest.GetRequestStream();
        //        dataStream.Write(byteArray, 0, byteArray.Length);
        //        dataStream.Close();

        //        WebResponse tResponse = tRequest.GetResponse();

        //        dataStream = tResponse.GetResponseStream();

        //        StreamReader tReader = new StreamReader(dataStream);

        //        String sResponseFromServer = tReader.ReadToEnd();

        //        tReader.Close();
        //        dataStream.Close();
        //        tResponse.Close();
        //        return sResponseFromServer;
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
        //                "Error :" + ex.Message), true);
        //        return "Eror";
        //    }
        //}

        //protected void sendPushNotification2()
        //{
        //    try
        //    {
        //        //Registration Id created by Android App i.e. DeviceId.  
        //        string regId;
        //        regId = txtRegID_2.Text.Replace("'", "''");
        //        //API Key created in Google project  
        //        var applicationID = txtAppID_2.Text.Replace("'", "''");

        //        //Project ID created in Google project.  
        //        var SENDER_ID = txtSender_2.Text.Replace("'", "''");
        //        var varMessage = txtMsg_2.Text.Replace("'", "''");
        //        WebRequest tRequest;
        //        tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
        //        tRequest.Method = "post";
        //        tRequest.ContentType = "application/json";
        //        tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));

        //        tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

        //        string postDataToServer = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message="
        //                           + varMessage + "®istration_id=" + regId + "";
        //        //"&data.time=" + System.DateTime.Now.ToString() + 

        //        Console.WriteLine(postDataToServer);
        //        Byte[] byteArray = Encoding.UTF8.GetBytes(postDataToServer);
        //        tRequest.ContentLength = byteArray.Length;

        //        Stream dataStream = tRequest.GetRequestStream();
        //        dataStream.Write(byteArray, 0, byteArray.Length);
        //        dataStream.Close();

        //        WebResponse tResponse = tRequest.GetResponse();

        //        dataStream = tResponse.GetResponseStream();

        //        StreamReader tReader = new StreamReader(dataStream);

        //        String sResponseFromServer = tReader.ReadToEnd();

        //        tReader.Close();
        //        dataStream.Close();
        //        tResponse.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
        //                "Error :" + ex.Message), true);
        //    }
        //}

        private void fillSchoolCodes()
        {
            //try
            //{
            //    eMallEntity.school objSchool = new eMallEntity.school();
            //    DataTable dtTbl = new DataTable();
            //    dtTbl = (new eMallBL()).searchSchool(objSchool);
            //    ddlSchoolCode.DataSource = dtTbl;
            //    ddlSchoolCode.DataTextField = "code";
            //    ddlSchoolCode.DataValueField = "id";
            //    ddlSchoolCode.DataBind();

            //    // Insert 'Select'
            //    ddlSchoolCode.Items.Insert(0, new ListItem("--Select--", "0"));
            //    // Set selected index
            //    ddlSchoolCode.SelectedIndex = 0;
            //    if (Session["usertype"] != null && Session["usertype"].ToString() == "2")
            //    {
            //        ddlSchoolCode.SelectedValue = Session["school_id"].ToString();
            //        ddlSchoolCode.Enabled = false;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //string ids = "";
            //TreeNodeCollection nodes = tvTest.Nodes;
            //foreach (TreeNode n in nodes)
            //{
            //    if (n.Selected)
            //        ids += n.Value;
            //    if (n.Checked)
            //        ids += n.Value;
            //    //n.Checked = true;
            //    //n.Selected = true;
            //}
        }

        protected void tvTest_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {

        }

        

        //private void CallRecursive(TreeView treeView)
        //{
        //    // Print each node recursively.
        //    TreeNodeCollection nodes = treeView.Nodes;
        //    foreach (TreeNode n in nodes)
        //    {
        //        PrintRecursive(n);
        //    }
        //}
    }
}