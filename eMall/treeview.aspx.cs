using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eMall
{
    public partial class treeview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TreeView1.Nodes.Add(new TreeNode("Fruits", "Fruits"));
                TreeView1.Nodes[0].ChildNodes.Add(new TreeNode("Mango", "Mango"));
                TreeView1.Nodes[0].ChildNodes.Add(new TreeNode("Apple", "Apple"));
                TreeView1.Nodes[0].ChildNodes.Add(new TreeNode("Pineapple", "Pineapple"));
                TreeView1.Nodes[0].ChildNodes.Add(new TreeNode("Orange", "Orange"));
                TreeView1.Nodes[0].ChildNodes.Add(new TreeNode("Grapes", "Grapes"));

                TreeView1.Nodes.Add(new TreeNode("Vegetables", "Vegetables"));
                TreeView1.Nodes[1].ChildNodes.Add(new TreeNode("Carrot", "Carrot"));
                TreeView1.Nodes[1].ChildNodes.Add(new TreeNode("Cauliflower", "Cauliflower"));
                TreeView1.Nodes[1].ChildNodes.Add(new TreeNode("Potato", "Potato"));
                TreeView1.Nodes[1].ChildNodes.Add(new TreeNode("Tomato", "Tomato"));
                TreeView1.Nodes[1].ChildNodes.Add(new TreeNode("Onion", "Onion"));
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string ids = "";
            TreeNodeCollection nodes = TreeView1.Nodes;
            foreach (TreeNode n in nodes)
            {
                if (n.Selected)
                    ids += n.Value;
                if (n.Checked)
                    ids += n.Value;
                //n.Checked = true;
                //n.Selected = true;
            }
        }
    }
}