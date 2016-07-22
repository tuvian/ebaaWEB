using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using DataLayer;
using System.Data;
using eMall;
using System.Web.UI;

using System.Web.Security;
using System.Web.UI.WebControls;
//using System.Net.Mail;
//using System.Web.Mail;
using System.Configuration;
using System.Data.Odbc;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace BusinussLayer
{
    public class eMallBL
    {
        #region Static Functions

        public static DataTable CallSPSample()
        {
            DataSet dtSet = new DataSet();
            eMallDA objDA = new eMallDA();
            string sqlQuery = "{call usp_getErrors}";
            objDA.ClearParameters();
            objDA.AddParameter("id", OdbcType.Int, 30, ParameterDirection.Input, 1);
            dtSet = objDA.ExecuteDataSet(sqlQuery, CommandType.StoredProcedure);
            objDA.ClearParameters();

            return dtSet.Tables[0];
        }

        public static void logErrors(string Page, string Type, string description)
        {
            eMallDA objDA = new eMallDA();
            string sqlQuery = string.Empty;
            //ID, Name, Description
            sqlQuery = "INSERT INTO  error_log (page,description,date) VALUES('" + Page.Replace("'", "''").Trim() + "','" + description.Replace("'", "''").Trim() + "',now())";
            objDA.ExecuteNonQuery(sqlQuery);

            Alert.Show(description);

        }

        public static void logErrors(string page, string description)
        {
            eMallDA objDA = new eMallDA();
            string sqlQuery = string.Empty;
            //ID, Name, Description
            sqlQuery = "INSERT INTO  error_log (page,description,date) VALUES('" + page.Replace("'", "''").Trim() + "','" + description.Replace("'", "''").Trim() + "',now())";
            objDA.ExecuteNonQuery(sqlQuery);

            Alert.Show(description);
        }

        public static void logErrors(string description)
        {
            eMallDA objDA = new eMallDA();
            string sqlQuery = string.Empty;
            //ID, Name, Description
            sqlQuery = "INSERT INTO  error_log (description,date) VALUES('" + description.Replace("'", "''").Trim() + "',now())";
            objDA.ExecuteNonQuery(sqlQuery);

            //Alert.Show(description);

        }

        public static DataTable GetErrors()
        {
            eMallDA objDA = new eMallDA();
            string sqlQuery = string.Empty;
            DataTable dtTble = new DataTable();
            //ID, Name, Description
            sqlQuery = "SELECT * FROM  ErrorLog ";
            dtTble = objDA.ExecuteDataTable(sqlQuery);

            return dtTble;
        }

        public static DataTable makeDefaultCart()
        {
            DataTable dtTblCart = new DataTable();
            DataRow dtRow;
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "ItemID";
            myDataColumn.AutoIncrement = true;
            dtTblCart.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "ItemCode";
            dtTblCart.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "ItemName";
            dtTblCart.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Qnty";
            dtTblCart.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "Price";
            dtTblCart.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Photo";
            dtTblCart.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Status";
            dtTblCart.Columns.Add(myDataColumn);

            return dtTblCart;
        }

        public static IList<eMallEntity.OrderDetails> createDefaultCart()
        {
            IList<eMallEntity.OrderDetails> shoppingCart = new List<eMallEntity.OrderDetails>();
            return shoppingCart;
        }

        //public static void sendEmail(eMallEntity.EmailDetails emailDetails)
        //{
        //    try
        //    {
        //        SmtpClient client = new SmtpClient();
        //        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        client.EnableSsl = true;
        //        client.Host = "smtp.gmail.com";
        //        client.Port = 587;

        //        // setup Smtp authentication
        //        System.Net.NetworkCredential credentials =
        //            new System.Net.NetworkCredential("ansu.ansar@gmail.com", "allahuakbar");
        //        client.UseDefaultCredentials = false;
        //        client.Credentials = credentials;

        //        MailMessage msg = new MailMessage();
        //        msg.From = new MailAddress(emailDetails.From);
        //        msg.To.Add(new MailAddress(emailDetails.To));
        //        //msg.From = new MailAddress("info@tuvian.com");
        //        //msg.To.Add(new MailAddress("ansu.ansar@gmail.com"));

        //        msg.Subject = emailDetails.Subject;
        //        msg.IsBodyHtml = true;
        //        msg.Body = string.Format(emailDetails.Message);
        //        client.Send(msg);
        //    }
        //    catch (Exception ex)
        //    {                
        //        throw;
        //    }
        //}

        public static void sendEmail(eMallEntity.EmailDetails emailDetails)
        {
            try
            {
                //string SERVER = ConfigurationSettings.AppSettings["MailServer"].ToString();

                //MailMessage oMail = new System.Web.Mail.MailMessage();
                //oMail.From = emailDetails.From;
                //oMail.To = emailDetails.To;
                //oMail.Cc = ConfigurationSettings.AppSettings["AdminEmailID"].ToString();                
                //oMail.Subject = emailDetails.Subject;
                //oMail.BodyFormat = MailFormat.Html;	// enumeration
                //oMail.Priority = MailPriority.High;	// enumeration
                //oMail.Body = emailDetails.Message;
                //SmtpMail.SmtpServer = SERVER;
                //SmtpMail.Send(oMail);
                //oMail = null;	// free up resources
            }
            catch (Exception ex)
            {
                logErrors("On sendEmail " + ex.Message);
                throw;
            }
        }

        #endregion

        #region Category SubCategory

        public string InserItemCategory(eMallEntity.ItemCatagory objCategory)
        {
            eMallDA objDA = new eMallDA();
            string returnValue = string.Empty;
            string sqlQuery = string.Empty;
            try
            {
                if (objCategory.ID == 0)
                {
                    //ID, Name, Description
                    sqlQuery = "INSERT INTO  category (Name, Description) VALUES(" +
                            "'" + objCategory.Name + "', '" + objCategory.Description + "')";
                }
                else
                {
                    sqlQuery = "UPDATE category SET Name = '" + objCategory.Name + "', Description = '" + objCategory.Description + "'"
                        + " WHERE ID = " + objCategory.ID;

                }
                objDA.ExecuteNonQuery(sqlQuery);
                return "success";
            }
            catch (Exception ex)
            {
                return "error";
            }
        }

        public System.Data.DataTable getCategories(int categoryId)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT * FROM category WHERE 0 = " + categoryId + " OR ID = " + categoryId;
                resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                eMallBL.logErrors("getCategories", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        public string DeleteCategory(int categoryID)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                string sqlQuery = string.Empty;
                sqlQuery = "SELECT Count(*) FROM subcategory WHERE CategoryID = " + categoryID;
                int count = Convert.ToInt32(objDA.ExecuteScalar(sqlQuery));
                if (count > 0)
                    return "Category references exist";
                sqlQuery = "DELETE FROM category WHERE ID = " + categoryID;
                objDA.ExecuteNonQuery(sqlQuery);
                return "success";
            }
            catch (Exception ex)
            {
                return "error occured" + ex.Message;
            }
        }

        public DataTable getSubCategories(int categoryID, int subCategoryID)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT subcategory.ID as ID,subcategory.Name as Name,subcategory.Description as Description,category.ID as CategoryID,category.Name as CategoryName FROM subcategory INNER JOIN category ON subcategory.CategoryID = category.ID WHERE ((0 = " + subCategoryID + ") OR (subcategory.ID = " + subCategoryID + ")) AND " +
                        "((0 = " + categoryID + ") OR (categoryID = " + categoryID + " ))";
                resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                eMallBL.logErrors("getSubCategories", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        public string InsertItemSubCategory(eMallEntity.ItemSubCatagory objSubCategory)
        {
            eMallDA objDA = new eMallDA();
            string returnValue = string.Empty;
            string sqlQuery = string.Empty;
            try
            {
                if (objSubCategory.ID == 0)
                {
                    //ID, Name, Description
                    sqlQuery = "INSERT INTO  subcategory (Name, Description, CategoryID) VALUES(" +
                            "'" + objSubCategory.Name + "', '" + objSubCategory.Description + "'," + objSubCategory.CategoryID + ")";
                }
                else
                {
                    sqlQuery = "UPDATE subcategory SET Name = '" + objSubCategory.Name + "', Description = '" + objSubCategory.Description + "',"
                        + " CategoryID = " + objSubCategory.CategoryID + " WHERE ID = " + objSubCategory.ID;

                }
                objDA.ExecuteNonQuery(sqlQuery);
                return "success";
            }
            catch (Exception ex)
            {
                return "error";
            }
        }

        public string DeleteSubCategory(int SubCategoryID)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                string sqlQuery = string.Empty;

                sqlQuery = "SELECT Count(*) FROM items WHERE SubCategoryID = " + SubCategoryID;
                int count = Convert.ToInt32(objDA.ExecuteScalar(sqlQuery));
                if (count > 0)
                    return "SubCategory references exist";
                sqlQuery = "DELETE FROM subcategory WHERE ID = " + SubCategoryID;
                objDA.ExecuteNonQuery(sqlQuery);
                return "success";
            }
            catch (Exception ex)
            {
                return "error : " + ex.Message;
            }
        }

        public bool isCategoryAlreadyExist(eMallEntity.ItemCatagory objCategory)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT * FROM category WHERE Name = '" + objCategory.Name + "' AND ID != " + objCategory.ID;
                int count = Convert.ToInt32(objDA.ExecuteScalar(sqlQuery));
                if (count > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public bool isSubCategoryAlreadyExist(eMallEntity.ItemSubCatagory objSubCategory)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT * FROM subcategory WHERE Name = '" + objSubCategory.Name + "' AND ID != " + objSubCategory.ID;
                int count = Convert.ToInt32(objDA.ExecuteScalar(sqlQuery));
                if (count > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                return true;
            }
        }

        #endregion

        #region Items
        public DataTable getItems(int categoryID, int subCategoryID, int itemID)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT items.ID,items.Name as Name,items.Description,items.Overview,items.Price,items.Image,items.HasOffer," +
                    "items.OfferPrice,items.OfferDescription,items.ItemCode,category.ID as CategoryID," +
                    "category.Name as category,subcategory.ID as SubCategoryID," +
                    "subcategory.Name as subcategory, items.IsActive, items.ReleaseDate, items.Specefications " +
                    "FROM items INNER JOIN subcategory ON " +
                   "items.SubCategoryID = subcategory.ID INNER JOIN category ON subcategory.CategoryID = category.ID " +
                    "WHERE ((0 = " + categoryID + ") OR (items.CategoryID = " + categoryID + ")) AND" +
                    "((0 = " + subCategoryID + ") OR (items.SubCategoryID = " + subCategoryID + " )) AND" +
                    "((0 = " + itemID + ") OR (items.ID = " + itemID + " ))";
                resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                logErrors("getItems", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        public DataTable getOfferItems()
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT items.ID,items.Name,items.Description,items.Overview,items.Price,items.Image,items.HasOffer," +
                    "items.OfferPrice,items.OfferDescription,items.ItemCode,category.ID as CategoryID," +
                    "category.Name as category,subcategory.ID as SubCategoryID," +
                    "subcategory.Name as subcategory, items.IsActive, items.ReleaseDate, items.Specefications " +
                    "FROM items INNER JOIN subcategory ON " +
                   "items.SubCategoryID = subcategory.ID INNER JOIN category ON subcategory.CategoryID = category.ID " +
                    "WHERE hasOffer = 1";
                resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                logErrors("getItems", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        public DataTable searchItems(eMallEntity.SeacrhCriteria objSearch)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT items.ID,items.Name,items.Description,items.Overview,items.Price,items.Image,items.HasOffer," +
                    "items.OfferPrice,items.OfferDescription,items.ItemCode,category.ID as CategoryID," +
                    "category.Name as category,subcategory.ID as SubCategoryID," +
                    "subcategory.Name as subcategory, items.IsActive, items.ReleaseDate, items.Specefications " +
                    "FROM items INNER JOIN subcategory ON " +
                   "items.SubCategoryID = subcategory.ID INNER JOIN category ON subcategory.CategoryID = category.ID " +
                    "WHERE ((0 = " + objSearch.categoryID + ") OR (items.CategoryID = " + objSearch.categoryID + ")) AND" +
                    "((0 = " + objSearch.subCategoryID + ") OR (items.SubCategoryID = " + objSearch.subCategoryID + " )) AND" +
                    "((0 = " + objSearch.itemID + ") OR (items.ID = " + objSearch.itemID + " )) AND" +
                    "((('' = '" + objSearch.itemCode + "') OR (items.ItemCode like '%" + objSearch.itemCode + "%' )) " + objSearch.relativeOperator +
                    "(('' = '" + objSearch.itemName + "') OR (items.Name like '%" + objSearch.itemName + "%' )) " + objSearch.relativeOperator +
                    "(('' = '" + objSearch.itemDescription + "') OR (items.Description like '%" + objSearch.itemDescription + "%' )))";
                resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                logErrors("searchItems", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        public string InsertItems(eMallEntity.Items objItems)
        {
            eMallDA objDA = new eMallDA();
            string returnValue = string.Empty;
            string sqlQuery = string.Empty;
            //try
            //{
            if (objItems.ID == 0)
            {
                //ID, Name, Description
                sqlQuery = "INSERT INTO items (ItemCode, Name, Description, Overview, Price, HasOffer, OfferPrice, OfferDescription," +
                        "CategoryID,SubCategoryID,Image, IsActive, ReleaseDate, Specefications,userid) VALUES(" +
                        "'" + objItems.ItemCode + "','" + objItems.Name + "','" + objItems.Description + "','" + objItems.OverView + "'," +
                        "" + objItems.Price + "," + objItems.HasOffer + "," + objItems.OfferPrice + ",'" + objItems.OfferDescription + "'," +
                        "" + objItems.CategoryID + "," + objItems.SubCategoryID + ",'" + objItems.Image + "'," +
                        "" + objItems.IsActive + ", now() ,'" + objItems.Specefications + "'," + objItems.userID + ")";
            }
            else
            {
                sqlQuery = "UPDATE items SET ItemCode = '" + objItems.ItemCode + "', Name = '" + objItems.Name + "', "
                    + "Description = '" + objItems.Description + "', "
                    + "Overview = '" + objItems.OverView + "', Price = " + objItems.Price + ", HasOffer = " + objItems.HasOffer + ", "
                    + "OfferPrice = " + objItems.OfferPrice + ", OfferDescription = '" + objItems.OfferDescription + "', "
                    + "CategoryID = " + objItems.CategoryID + ", SubCategoryID = " + objItems.SubCategoryID + ", "
                    + "Image = '" + objItems.Image + "', IsActive = " + objItems.IsActive + ", ModifiedDate = now() , "
                    + "Specefications = '" + objItems.Specefications + "' "
                    + "WHERE ID = " + objItems.ID;

            }
            objDA.ExecuteNonQuery(sqlQuery);
            return "success";
            //}
            //catch (Exception ex)
            //{
            //return "error";
            //}
        }

        public string DeleteItem(int itemID)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                string sqlQuery = string.Empty;
                sqlQuery = "DELETE FROM items WHERE ID = " + itemID;
                objDA.ExecuteNonQuery(sqlQuery);
                return "success";
            }
            catch (Exception ex)
            {
                return "error : " + ex.Message;
            }
        }

        public bool isItemCodeAlreadyExist(eMallEntity.Items objItems)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT * FROM items WHERE ItemCode = '" + objItems.ItemCode + "' AND ID != " + objItems.ID;
                int count = Convert.ToInt32(objDA.ExecuteScalar(sqlQuery));
                if (count > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public bool isItemNameAlreadyExist(eMallEntity.Items objItems)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT * FROM items WHERE Name = '" + objItems.Name + "' AND ID != " + objItems.ID;
                int count = Convert.ToInt32(objDA.ExecuteScalar(sqlQuery));
                if (count > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                return true;
            }
        }

        //Search Produucts from Admin Section
        public DataTable searchProducts(eMallEntity.SeacrhCriteria objSearch)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT items.ID,items.Name,items.Description,items.Overview,items.Price,items.Image,items.HasOffer," +
                    "items.OfferPrice,items.OfferDescription,items.ItemCode,category.ID as CategoryID," +
                    "category.Name as category,subcategory.ID as SubCategoryID," +
                    "subcategory.Name as subcategory, items.IsActive, items.ReleaseDate, items.Specefications " +
                    "FROM items INNER JOIN subcategory ON " +
                   "items.SubCategoryID = subcategory.ID INNER JOIN category ON subcategory.CategoryID = category.ID " +
                    "WHERE ((0 = " + objSearch.categoryID + ") OR (items.CategoryID = " + objSearch.categoryID + ")) AND" +
                    "((0 = " + objSearch.userID + ") OR (items.userid = " + objSearch.userID + " )) AND" +
                    "((0 = " + objSearch.subCategoryID + ") OR (items.SubCategoryID = " + objSearch.subCategoryID + " )) AND" +
                    "((0 = " + objSearch.itemID + ") OR (items.ID = " + objSearch.itemID + " )) AND" +
                    "((-1 = " + objSearch.isActive + ") OR (items.IsActive = " + objSearch.isActive + " )) AND" +
                    "((-1 = " + objSearch.hasOffer + ") OR (items.HasOffer = " + objSearch.hasOffer + " )) AND" +
                    "((('' = '" + objSearch.itemCode + "') OR (items.ItemCode like '%" + objSearch.itemCode + "%' )) " + objSearch.relativeOperator +
                    "(('' = '" + objSearch.itemName + "') OR (items.Name like '%" + objSearch.itemName + "%' )) " + objSearch.relativeOperator +
                    "(('' = '" + objSearch.itemDescription + "') OR (items.Description like '%" + objSearch.itemDescription + "%' )))";
                resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                logErrors("searchItems", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        //List items With paging 
        public DataSet searchItemsByPaging(eMallEntity.SeacrhCriteria objSearch)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataSet resultDataSet = new DataSet();
                string sqlQuery = "{call usp_getItems}";
                objDA.ClearParameters();
                objDA.AddParameter("firstRow", OdbcType.Int, 30, ParameterDirection.Input, 1);
                objDA.AddParameter("lastRow", OdbcType.Int, 30, ParameterDirection.Input, 10);
                objDA.AddParameter("categoryid", OdbcType.Int, 30, ParameterDirection.Input, objSearch.categoryID);
                objDA.AddParameter("subcategoryid", OdbcType.Int, 30, ParameterDirection.Input, objSearch.subCategoryID);
                objDA.AddParameter("itemid", OdbcType.Int, 30, ParameterDirection.Input, objSearch.itemID);
                objDA.AddParameter("itemcode", OdbcType.VarChar, 500, ParameterDirection.Input, objSearch.itemCode);
                objDA.AddParameter("relativeoperator", OdbcType.VarChar, 10, ParameterDirection.Input, objSearch.relativeOperator);
                objDA.AddParameter("itemname", OdbcType.VarChar, 300, ParameterDirection.Input, objSearch.itemName);
                objDA.AddParameter("description", OdbcType.VarChar, 300, ParameterDirection.Input, objSearch.itemDescription);
                resultDataSet = objDA.ExecuteDataSet(sqlQuery, CommandType.StoredProcedure);
                objDA.ClearParameters();
                return resultDataSet;
            }
            catch (Exception ex)
            {
                logErrors("searchItems", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        #endregion

        #region Shops

        public DataTable GetShops(int cityID, int shopID)
        {
            eMallDA _dataObject = new eMallDA();

            DataTable _resultTable = new DataTable();
            string _sqlQuery = "SELECT shops.ID, shops.Name, shops.Address, shops.CityID, shops.Phone, shops.Email" +
                    ", shops.ContactPerson, shops.MapDiscription, city.Name as city FROM shops " +
                    "Inner Join city on shops.CityID = city.ID  WHERE (0 = " + cityID + " OR city.ID = " + cityID + ")" +
                    " AND (0 = " + shopID + " OR shops.ID = " + shopID + ")";
            _resultTable = _dataObject.ExecuteDataTable(_sqlQuery);
            return _resultTable;
        }

        public DataTable GetCities(int cityID)
        {
            eMallDA _dataObject = new eMallDA();

            DataTable _resultTable = new DataTable();
            string _sqlQuery = "SELECT ID, Name, Discription FROM city WHERE (0 = " + cityID + " OR city.ID = " + cityID + ") ORDER BY Name";
            _resultTable = _dataObject.ExecuteDataTable(_sqlQuery);
            return _resultTable;
        }

        public string DeleteShops(int shopID)
        {
            try
            {
                eMallDA _dataObject = new eMallDA();
                string _sqlQuery = string.Empty;
                _sqlQuery = "DELETE FROM shops WHERE ID = " + shopID;
                _dataObject.ExecuteNonQuery(_sqlQuery);
                return "success";
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string InserShopDetails(eMallEntity.ShopEntity shopsDetails)
        {
            eMallDA _dataObject = new eMallDA();
            string _returnValue = string.Empty;
            string _sqlQuery = string.Empty;
            try
            {
                if (shopsDetails.ID == 0)
                {
                    //ID, Name, Address, CityID, Phone, Email, ContactPerson, MapDiscription
                    _sqlQuery = "INSERT INTO  shops (Name, Address, CityID, Phone, Email, ContactPerson, MapDiscription) VALUES(" +
                            "'" + shopsDetails.ShopName + "', '" + shopsDetails.Address
                            + "', " + shopsDetails.CityID + ",'" + shopsDetails.Phone + "','" + shopsDetails.Email +
                            "','" + shopsDetails.ContactPerson + "','" + shopsDetails.MapDiscription + "')";
                }
                else
                {
                    _sqlQuery = "UPDATE shops SET Name = '" + shopsDetails.ShopName + "',Address = '" + shopsDetails.Address + "'" +
                            ",CityID = " + shopsDetails.CityID +
                            ",Phone = '" + shopsDetails.Phone + "', Email = '" + shopsDetails.Email + "'" +
                            ",ContactPerson = '" + shopsDetails.ContactPerson + "',MapDiscription = '" + shopsDetails.MapDiscription +
                            "' WHERE ID = " + shopsDetails.ID;

                }
                _dataObject.ExecuteNonQuery(_sqlQuery);
                return "success";
            }
            catch (Exception ex)
            {
                return "error";
            }
        }
        #endregion

        #region Login

        public DataTable getLogin(string userName, string password, string type)
        {
            eMallDA _dataObject = new eMallDA();

            DataTable _resultTable = new DataTable();
            string _sqlQuery = "SELECT * FROM login WHERE ('' = '" + userName + "' OR login.userName = '" + userName + "')" +
                    " AND ('' = '" + password + "' OR login.password = '" + password + "')" +
                    " AND ('' = '" + type + "' OR login.type = '" + type + "')";
            _resultTable = _dataObject.ExecuteDataTable(_sqlQuery);
            return _resultTable;
        }

        public DataTable getUsers(int userID)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT * FROM login WHERE 0 = " + userID + " OR ID = " + userID;
                resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                eMallBL.logErrors("getUsers", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        #endregion

        #region Orders
        public int SaveOrder(eMallEntity.Customer objCustomer, eMallEntity.ShoppingCart objShoppingCart)
        {
            eMallDA objDA = new eMallDA();
            int OrderID = 0;
            string sql = "INSERT INTO customer (FirstName, SecondName, Email, Address, Phone, Mobile ) values ('" + objCustomer.FirstName + "'," +
                "'" + objCustomer.LastName + "','" + objCustomer.Email + "','" + objCustomer.Address + "','" + objCustomer.Phone + "'," +
                "'" + objCustomer.Mobile + "');";
            objDA.ExecuteNonQuery(sql);
            int CustomerID = Convert.ToInt32(objDA.ExecuteScalar("SELECT LAST_INSERT_ID()"));

            for (int i = 0; i <= objShoppingCart.orderDetails.Count(); i++)
            {

            }
            return OrderID;
        }
        #endregion

        #region Teacher

        public DataTable getDepartment(int department_id)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT * FROM department WHERE 0 = " + department_id + " OR id = " + department_id;
                resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                eMallBL.logErrors("getDepartment", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        public DataTable getDesignation(int designation_id)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT * FROM designation WHERE 0 = " + designation_id + " OR id = " + designation_id;
                resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                eMallBL.logErrors("getDesignation", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        public DataTable searchTeachers(eMallEntity.teacher objTeacher)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                objTeacher.search_operator = objTeacher.search_operator == null ? " OR " : objTeacher.search_operator;
                string sqlQuery = ""; // "SET NAMES cp1256;  set character set cp1256; set character set cp1256; ";

                sqlQuery = sqlQuery + "SELECT t.id,t.school_id,t.code,t.name,t.mobile,t.email,t.present_address,t.permenant_address,t.status,t.qualification, t.image_path, " +
                    "dep.name as department, des.id as designation_id,des.name as designation, dep.id as department_id,t.nationality, " +
                    "cls.id as class_id, cls.std as class_std, cls.division as class_division, CONCAT(std,division) as classname " +
                    "FROM teacher t INNER JOIN department dep ON t.department_id = dep.id INNER JOIN designation des ON des.id = t.designation_id " +
                    "LEFT JOIN class cls ON t.class_id = cls.id  " +
                    //"WHERE ((('' = '" + objTeacher.code + "') OR (t.code like '%" + objTeacher.code + "%' )) " + objTeacher.search_operator +
                    "WHERE ((0 = " + objTeacher.department_id + ") OR (dep.id = " + objTeacher.department_id + ")) AND" +
                    "((0 = " + objTeacher.ID + ") OR (t.id = " + objTeacher.ID + ")) AND" +
                    "((0 = " + objTeacher.school_id + ") OR (t.school_id = " + objTeacher.school_id + ")) AND" +
                    "((('' = '" + objTeacher.code + "') OR (t.code like '%" + objTeacher.code + "%' )) " + objTeacher.search_operator +
                    "(('' = '" + objTeacher.name + "') OR (t.name like '%" + objTeacher.name + "%' )) " + objTeacher.search_operator +
                    "(('' = '" + objTeacher.email + "') OR (t.email like '%" + objTeacher.email + "%' )) " + objTeacher.search_operator +
                    "(('' = '" + objTeacher.mobile + "') OR (t.mobile like '%" + objTeacher.mobile + "%' ))) ORDER BY t.name;";
                //objDA.ExecuteNonQuery("SET NAMES cp1256");
                //objDA.ExecuteNonQuery("set character set cp1256");
                resultTable = objDA.ExecuteDataTable(sqlQuery);

                //"((('' = '" + objSearch.itemCode + "') OR (items.ItemCode like '%" + objSearch.itemCode + "%' )) " + objSearch.relativeOperator +
                //"(('' = '" + objSearch.itemName + "') OR (items.Name like '%" + objSearch.itemName + "%' )) " + objSearch.relativeOperator +
                //"(('' = '" + objSearch.itemDescription + "') OR (items.Description like '%" + objSearch.itemDescription + "%' )))";
                //resultTable = objDA.ExecuteDataTable(sqlQuery);

                //+
                // "WHERE ((0 = " + objSearch.categoryID + ") OR (items.CategoryID = " + objSearch.categoryID + ")) AND" +
                // "((0 = " + objSearch.userID + ") OR (items.userid = " + objSearch.userID + " )) AND" +
                // "((0 = " + objSearch.subCategoryID + ") OR (items.SubCategoryID = " + objSearch.subCategoryID + " )) AND" +
                // "((0 = " + objSearch.itemID + ") OR (items.ID = " + objSearch.itemID + " )) AND" +
                // "((-1 = " + objSearch.isActive + ") OR (items.IsActive = " + objSearch.isActive + " )) AND" +
                // "((-1 = " + objSearch.hasOffer + ") OR (items.HasOffer = " + objSearch.hasOffer + " )) AND" +
                // "((('' = '" + objSearch.itemCode + "') OR (items.ItemCode like '%" + objSearch.itemCode + "%' )) " + objSearch.relativeOperator +
                // "(('' = '" + objSearch.itemName + "') OR (items.Name like '%" + objSearch.itemName + "%' )) " + objSearch.relativeOperator +
                // "(('' = '" + objSearch.itemDescription + "') OR (items.Description like '%" + objSearch.itemDescription + "%' )))";
                //resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                logErrors("searchTeachers", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        public bool isTeacherCodeAlreadyExist(eMallEntity.teacher objTeachers)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT * FROM teacher WHERE code = '" + objTeachers.code + "' AND ID != " + objTeachers.ID;
                int count = Convert.ToInt32(objDA.ExecuteScalar(sqlQuery));
                if (count > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public string InsertTeacher(eMallEntity.teacher objTeacher)
        {
            eMallDA objDA = new eMallDA();
            string returnValue = string.Empty;
            string sqlQuery = string.Empty;
            //try
            //{
            if (objTeacher.ID == 0)
            {
                //ID, Name, Description
                sqlQuery = "INSERT INTO teacher (code, name, mobile, email, nationality, qualification, status, present_address, permenant_address, " +
                        "department_id,designation_id,image_path, school_id, class_id) VALUES(" +
                        "'" + objTeacher.code + "','" + objTeacher.name + "','" + objTeacher.mobile + "','" + objTeacher.email + "','" + objTeacher.nationality + "'," +
                        "'" + objTeacher.qualification + "'," + objTeacher.status + ",'" + objTeacher.present_address + "','" + objTeacher.permenant_address + "'," +
                        "" + objTeacher.department_id + "," + objTeacher.designation_id + ",'" + objTeacher.image_path + "'," + objTeacher.school_id + "," + objTeacher.class_id + ")";
            }
            else
            {
                sqlQuery = "UPDATE teacher SET code = '" + objTeacher.code + "', name = '" + objTeacher.name + "', "
                + "mobile = '" + objTeacher.mobile + "', school_id = " + objTeacher.school_id + ", class_id = " + objTeacher.class_id + ", "
                + "email = '" + objTeacher.email + "', nationality = '" + objTeacher.nationality + "', status = " + objTeacher.status + ", "
                + "qualification = '" + objTeacher.qualification + "', present_address = '" + objTeacher.present_address + "', "
                + "department_id = " + objTeacher.department_id + ", designation_id = " + objTeacher.designation_id + ", "
                    //+ "Image = '" + objItems.Image + "', IsActive = " + objItems.IsActive + ", ModifiedDate = now() , "
                + "permenant_address = '" + objTeacher.permenant_address + "', image_path = '" + objTeacher.image_path + "'"
                + "WHERE ID = " + objTeacher.ID;

                //sqlQuery = "UPDATE items SET ItemCode = '" + objItems.ItemCode + "', Name = '" + objItems.Name + "', "
                //    + "Description = '" + objItems.Description + "', "
                //    + "Overview = '" + objItems.OverView + "', Price = " + objItems.Price + ", HasOffer = " + objItems.HasOffer + ", "
                //    + "OfferPrice = " + objItems.OfferPrice + ", OfferDescription = '" + objItems.OfferDescription + "', "
                //    + "CategoryID = " + objItems.CategoryID + ", SubCategoryID = " + objItems.SubCategoryID + ", "
                //    + "Image = '" + objItems.Image + "', IsActive = " + objItems.IsActive + ", ModifiedDate = now() , "
                //    + "Specefications = '" + objItems.Specefications + "' "
                //    + "WHERE ID = " + objItems.ID;

            }
            objDA.ExecuteNonQuery(sqlQuery);
            return "success";
            //}
            //catch (Exception ex)
            //{
            //return "error";
            //}
        }

        public string DeleteTeacher(int teacherID)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                string sqlQuery = string.Empty;
                sqlQuery = "DELETE FROM teacher WHERE ID = " + teacherID;
                objDA.ExecuteNonQuery(sqlQuery);
                return "success";
            }
            catch (Exception ex)
            {
                return "error : " + ex.Message;
            }
        }

        #endregion

        #region School

        public string InsertSchool(eMallEntity.school objSchool)
        {
            eMallDA objDA = new eMallDA();
            string returnValue = string.Empty;
            string sqlQuery = string.Empty;
            //try
            //{
            if (objSchool.ID == 0)
            {
                //ID, Name, Description
                sqlQuery = "INSERT INTO school (code,name,site_url,address,contact_person,mobile,email,phone,contact_address,wilayath,waynumber,nationality,package_id,  " +
                        "register_date, expire_date, logo, status, notes) VALUES(" +
                        "'" + objSchool.code + "','" + objSchool.name + "','" + objSchool.siteurl + "','" + objSchool.school_address + "','" + objSchool.contact_person + "'," +
                        "'" + objSchool.mobile + "','" + objSchool.email + "','" + objSchool.phone + "','" + objSchool.contact_address + "'," +
                        "'" + objSchool.wilayath + "','" + objSchool.waynumber + "'," +
                        "'" + objSchool.nationality + "'," + objSchool.package_id + ",'" + objSchool.register_date.ToString("yyyy-MM-dd") + "','" + objSchool.expire_date.ToString("yyyy-MM-dd") + "'," +
                        "'" + objSchool.logo + "'," + objSchool.status + ",'" + objSchool.notes + "')";
            }
            else
            {
                sqlQuery = "UPDATE school SET code = '" + objSchool.code + "', name = '" + objSchool.name + "', "
                + "mobile = '" + objSchool.mobile + "', contact_address = '" + objSchool.contact_address + "', "
                + "email = '" + objSchool.email + "', nationality = '" + objSchool.nationality + "', status = " + objSchool.status + ", "
                + "site_url = '" + objSchool.siteurl + "', address = '" + objSchool.school_address + "', "
                + "wilayath = '" + objSchool.wilayath + "', waynumber = '" + objSchool.waynumber + "', "
                + "contact_person = '" + objSchool.contact_person + "', package_id = " + objSchool.package_id + ", "
                + "logo = '" + objSchool.logo + "', status = " + objSchool.status + ", notes = '" + objSchool.notes + "', "
                + "register_date = '" + objSchool.register_date.ToString("yyyy-MM-dd") + "', expire_date = '" + objSchool.expire_date.ToString("yyyy-MM-dd") + "'"
                + "WHERE ID = " + objSchool.ID;

                //sqlQuery = "UPDATE items SET ItemCode = '" + objItems.ItemCode + "', Name = '" + objItems.Name + "', "
                //    + "Description = '" + objItems.Description + "', "
                //    + "Overview = '" + objItems.OverView + "', Price = " + objItems.Price + ", HasOffer = " + objItems.HasOffer + ", "
                //    + "OfferPrice = " + objItems.OfferPrice + ", OfferDescription = '" + objItems.OfferDescription + "', "
                //    + "CategoryID = " + objItems.CategoryID + ", SubCategoryID = " + objItems.SubCategoryID + ", "
                //    + "Image = '" + objItems.Image + "', IsActive = " + objItems.IsActive + ", ModifiedDate = now() , "
                //    + "Specefications = '" + objItems.Specefications + "' "
                //    + "WHERE ID = " + objItems.ID;

            }
            objDA.ExecuteNonQuery(sqlQuery);
            return "success";
            //}
            //catch (Exception ex)
            //{
            //return "error";
            //}
        }

        public bool isSchoolCodeAlreadyExist(eMallEntity.school objSchool)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT * FROM school WHERE code = '" + objSchool.code + "' AND ID != " + objSchool.ID;
                int count = Convert.ToInt32(objDA.ExecuteScalar(sqlQuery));
                if (count > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public string DeleteSchool(int ID)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                string sqlQuery = string.Empty;
                sqlQuery = "DELETE FROM school WHERE ID = " + ID;
                objDA.ExecuteNonQuery(sqlQuery);
                return "success";
            }
            catch (Exception ex)
            {
                return "error : " + ex.Message;
            }
        }

        public DataTable searchSchool(eMallEntity.school objSchool)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                objSchool.search_operator = objSchool.search_operator == null ? " OR " : objSchool.search_operator;
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT s.id,s.code,s.name,s.site_url,s.address,s.contact_person,s.mobile,s.email,s.phone,s.contact_address, " +
                    "s.nationality, s.package_id, s.register_date, s.expire_date, s.logo, s.status, s.notes, p.name as package, s.wilayath, s.waynumber " +
                    "FROM school s INNER JOIN package p ON s.package_id = p.id " +
                    //"WHERE ((('' = '" + objTeacher.code + "') OR (t.code like '%" + objTeacher.code + "%' )) " + objTeacher.search_operator +
                    "WHERE ((0 = " + objSchool.package_id + ") OR (p.id = " + objSchool.package_id + ")) AND" +
                    "((0 = " + objSchool.ID + ") OR (s.id = " + objSchool.ID + ")) AND" +
                    "((('' = '" + objSchool.name + "') OR (s.name like '%" + objSchool.name + "%' )) " + objSchool.search_operator +
                    "(('' = '" + objSchool.school_address + "') OR (s.address like '%" + objSchool.school_address + "%' ))) ORDER BY s.name;";
                resultTable = objDA.ExecuteDataTable(sqlQuery);

                //"((('' = '" + objSearch.itemCode + "') OR (items.ItemCode like '%" + objSearch.itemCode + "%' )) " + objSearch.relativeOperator +
                //"(('' = '" + objSearch.itemName + "') OR (items.Name like '%" + objSearch.itemName + "%' )) " + objSearch.relativeOperator +
                //"(('' = '" + objSearch.itemDescription + "') OR (items.Description like '%" + objSearch.itemDescription + "%' )))";
                //resultTable = objDA.ExecuteDataTable(sqlQuery);

                //+
                // "WHERE ((0 = " + objSearch.categoryID + ") OR (items.CategoryID = " + objSearch.categoryID + ")) AND" +
                // "((0 = " + objSearch.userID + ") OR (items.userid = " + objSearch.userID + " )) AND" +
                // "((0 = " + objSearch.subCategoryID + ") OR (items.SubCategoryID = " + objSearch.subCategoryID + " )) AND" +
                // "((0 = " + objSearch.itemID + ") OR (items.ID = " + objSearch.itemID + " )) AND" +
                // "((-1 = " + objSearch.isActive + ") OR (items.IsActive = " + objSearch.isActive + " )) AND" +
                // "((-1 = " + objSearch.hasOffer + ") OR (items.HasOffer = " + objSearch.hasOffer + " )) AND" +
                // "((('' = '" + objSearch.itemCode + "') OR (items.ItemCode like '%" + objSearch.itemCode + "%' )) " + objSearch.relativeOperator +
                // "(('' = '" + objSearch.itemName + "') OR (items.Name like '%" + objSearch.itemName + "%' )) " + objSearch.relativeOperator +
                // "(('' = '" + objSearch.itemDescription + "') OR (items.Description like '%" + objSearch.itemDescription + "%' )))";
                //resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                logErrors("searchSchool", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        public DataTable getPackage(int package_id)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT * FROM package WHERE 0 = " + package_id + " OR id = " + package_id;
                resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                eMallBL.logErrors("getPackage", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        #endregion

        #region school login

        public bool isSchoolUserNameAlreadyExist(eMallEntity.schooluser objSchoolLogin)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT * FROM login WHERE username = '" + objSchoolLogin.username + "' AND ID != " + objSchoolLogin.ID;
                int count = Convert.ToInt32(objDA.ExecuteScalar(sqlQuery));
                if (count > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public string InsertSchoolLogin(eMallEntity.schooluser objSchoolLogin)
        {
            eMallDA objDA = new eMallDA();
            string returnValue = string.Empty;
            string sqlQuery = string.Empty;
            try
            {
                if (objSchoolLogin.ID == 0)
                {
                    //ID, Name, Description
                    sqlQuery = "INSERT INTO login (username,password,type,user_id,created_date,school_id) VALUES(" +
                            "'" + objSchoolLogin.username + "','" + objSchoolLogin.password + "','" + objSchoolLogin.type + "'," + objSchoolLogin.schoolID + ",now()," +
                            "" + objSchoolLogin.schoolID + ")";
                }
                else
                {
                    sqlQuery = "UPDATE login SET username = '" + objSchoolLogin.username + "', password = '" + objSchoolLogin.password + "', "
                    + "type = '" + objSchoolLogin.type + "', user_id = " + objSchoolLogin.schoolID + ", school_id = " + objSchoolLogin.schoolID
                    + " WHERE ID = " + objSchoolLogin.ID;

                    //sqlQuery = "UPDATE items SET ItemCode = '" + objItems.ItemCode + "', Name = '" + objItems.Name + "', "
                    //    + "Description = '" + objItems.Description + "', "
                    //    + "Overview = '" + objItems.OverView + "', Price = " + objItems.Price + ", HasOffer = " + objItems.HasOffer + ", "
                    //    + "OfferPrice = " + objItems.OfferPrice + ", OfferDescription = '" + objItems.OfferDescription + "', "
                    //    + "CategoryID = " + objItems.CategoryID + ", SubCategoryID = " + objItems.SubCategoryID + ", "
                    //    + "Image = '" + objItems.Image + "', IsActive = " + objItems.IsActive + ", ModifiedDate = now() , "
                    //    + "Specefications = '" + objItems.Specefications + "' "
                    //    + "WHERE ID = " + objItems.ID;

                }
                objDA.ExecuteNonQuery(sqlQuery);
                return "success";
            }
            catch (Exception ex)
            {
                return "error";
            }
        }

        public string DeleteSchoolLogin(int ID)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                string sqlQuery = string.Empty;
                sqlQuery = "DELETE FROM login WHERE ID = " + ID;
                objDA.ExecuteNonQuery(sqlQuery);
                return "success";
            }
            catch (Exception ex)
            {
                return "error : " + ex.Message;
            }
        }

        public DataTable searchSchoolLogin(eMallEntity.schooluser objSchooluser)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT lg.id,lg.username,lg.password,lg.type,s.id as school_id,s.code,s.name,s.mobile,s.email FROM login lg RIGHT JOIN school s ON lg.user_id = s.id " +
                    //"WHERE ((('' = '" + objTeacher.code + "') OR (t.code like '%" + objTeacher.code + "%' )) " + objTeacher.search_operator +
                    "WHERE ((0 = " + objSchooluser.schoolID + ") OR (s.id = " + objSchooluser.schoolID + ")) AND (lg.type = '2' OR lg.type is null) ";
                resultTable = objDA.ExecuteDataTable(sqlQuery);

                //"((('' = '" + objSearch.itemCode + "') OR (items.ItemCode like '%" + objSearch.itemCode + "%' )) " + objSearch.relativeOperator +
                //"(('' = '" + objSearch.itemName + "') OR (items.Name like '%" + objSearch.itemName + "%' )) " + objSearch.relativeOperator +
                //"(('' = '" + objSearch.itemDescription + "') OR (items.Description like '%" + objSearch.itemDescription + "%' )))";
                //resultTable = objDA.ExecuteDataTable(sqlQuery);

                //+
                // "WHERE ((0 = " + objSearch.categoryID + ") OR (items.CategoryID = " + objSearch.categoryID + ")) AND" +
                // "((0 = " + objSearch.userID + ") OR (items.userid = " + objSearch.userID + " )) AND" +
                // "((0 = " + objSearch.subCategoryID + ") OR (items.SubCategoryID = " + objSearch.subCategoryID + " )) AND" +
                // "((0 = " + objSearch.itemID + ") OR (items.ID = " + objSearch.itemID + " )) AND" +
                // "((-1 = " + objSearch.isActive + ") OR (items.IsActive = " + objSearch.isActive + " )) AND" +
                // "((-1 = " + objSearch.hasOffer + ") OR (items.HasOffer = " + objSearch.hasOffer + " )) AND" +
                // "((('' = '" + objSearch.itemCode + "') OR (items.ItemCode like '%" + objSearch.itemCode + "%' )) " + objSearch.relativeOperator +
                // "(('' = '" + objSearch.itemName + "') OR (items.Name like '%" + objSearch.itemName + "%' )) " + objSearch.relativeOperator +
                // "(('' = '" + objSearch.itemDescription + "') OR (items.Description like '%" + objSearch.itemDescription + "%' )))";
                //resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                logErrors("searchSchool", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        #endregion

        #region teacher login

        public bool isTeacherUserNameAlreadyExist(eMallEntity.teacherlogin objteacherLogin)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT * FROM login WHERE username = '" + objteacherLogin.username + "' AND ID != " + objteacherLogin.ID;
                int count = Convert.ToInt32(objDA.ExecuteScalar(sqlQuery));
                if (count > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public string InsertTeacherLogin(eMallEntity.teacherlogin objteacherLogin)
        {
            eMallDA objDA = new eMallDA();
            string returnValue = string.Empty;
            string sqlQuery = string.Empty;
            try
            {
                if (objteacherLogin.ID == 0)
                {
                    //ID, Name, Description
                    sqlQuery = "INSERT INTO login (username,password,type,user_id,created_date,school_id) VALUES(" +
                            "'" + objteacherLogin.username + "','" + objteacherLogin.password + "','" + objteacherLogin.type + "'," + objteacherLogin.teacherid + ",now()," +
                    "" + objteacherLogin.schoolID + ")";
                }
                else
                {
                    sqlQuery = "UPDATE login SET username = '" + objteacherLogin.username + "', password = '" + objteacherLogin.password + "', "
                    + "type = '" + objteacherLogin.type + "', user_id = " + objteacherLogin.teacherid
                    + " WHERE ID = " + objteacherLogin.ID;
                }
                objDA.ExecuteNonQuery(sqlQuery);
                return "success";
            }
            catch (Exception ex)
            {
                return "error";
            }
        }

        public DataTable searchTeacherLogin(eMallEntity.teacherlogin objteacherlogin)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT lg.id,lg.username,lg.password,lg.type,t.id as teacher_id,t.code,t.name,t.mobile,t.email,t.school_id FROM login lg RIGHT JOIN teacher t ON lg.user_id = t.id " +
                    //"WHERE ((('' = '" + objTeacher.code + "') OR (t.code like '%" + objTeacher.code + "%' )) " + objTeacher.search_operator +
                    "WHERE ((0 = " + objteacherlogin.schoolID + ") OR (t.school_id = " + objteacherlogin.schoolID + ")) AND" +
                    "((('' = '" + objteacherlogin.teachername + "') OR (t.name like '%" + objteacherlogin.teachername + "%' )) " + objteacherlogin.search_operator +
                    "(('' = '" + objteacherlogin.teachercode + "') OR (t.code like '%" + objteacherlogin.teachercode + "%' ))) AND (lg.type = '3' OR lg.type is null) ";
                resultTable = objDA.ExecuteDataTable(sqlQuery);

                //"((('' = '" + objSearch.itemCode + "') OR (items.ItemCode like '%" + objSearch.itemCode + "%' )) " + objSearch.relativeOperator +
                //"(('' = '" + objSearch.itemName + "') OR (items.Name like '%" + objSearch.itemName + "%' )) " + objSearch.relativeOperator +
                //"(('' = '" + objSearch.itemDescription + "') OR (items.Description like '%" + objSearch.itemDescription + "%' )))";
                //resultTable = objDA.ExecuteDataTable(sqlQuery);

                //+
                // "WHERE ((0 = " + objSearch.categoryID + ") OR (items.CategoryID = " + objSearch.categoryID + ")) AND" +
                // "((0 = " + objSearch.userID + ") OR (items.userid = " + objSearch.userID + " )) AND" +
                // "((0 = " + objSearch.subCategoryID + ") OR (items.SubCategoryID = " + objSearch.subCategoryID + " )) AND" +
                // "((0 = " + objSearch.itemID + ") OR (items.ID = " + objSearch.itemID + " )) AND" +
                // "((-1 = " + objSearch.isActive + ") OR (items.IsActive = " + objSearch.isActive + " )) AND" +
                // "((-1 = " + objSearch.hasOffer + ") OR (items.HasOffer = " + objSearch.hasOffer + " )) AND" +
                // "((('' = '" + objSearch.itemCode + "') OR (items.ItemCode like '%" + objSearch.itemCode + "%' )) " + objSearch.relativeOperator +
                // "(('' = '" + objSearch.itemName + "') OR (items.Name like '%" + objSearch.itemName + "%' )) " + objSearch.relativeOperator +
                // "(('' = '" + objSearch.itemDescription + "') OR (items.Description like '%" + objSearch.itemDescription + "%' )))";
                //resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                logErrors("searchSchool", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        public string DeleteUserLogin(int ID)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                string sqlQuery = string.Empty;
                sqlQuery = "DELETE FROM login WHERE ID = " + ID;
                objDA.ExecuteNonQuery(sqlQuery);
                return "success";
            }
            catch (Exception ex)
            {
                return "error : " + ex.Message;
            }
        }

        public DataTable searchTeacherByLoginID(int loginID)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT t.id,t.school_id,t.name " +
                    "FROM teacher t INNER JOIN login lg ON t.id = lg.user_id AND lg.type = 3 " +
                    "WHERE lg.id = " + loginID;
                return resultTable;
            }
            catch (Exception ex)
            {
                logErrors("searchSchool", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        #endregion

        #region Events

        public DataTable searchEvents(eMallEntity.events objEvents)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                objEvents.search_operator = objEvents.search_operator == null ? " OR " : objEvents.search_operator;
                string sqlQuery = "SELECT e.id,e.title,e.school_id,e.start_date,e.end_date,e.description,e.status,e.created_date, e.created_by, " +
                    "s.code as school_code " +
                    "FROM events e INNER JOIN school s ON e.school_id = s.id  " +
                    "WHERE ((0 = " + objEvents.ID + ") OR (e.id = " + objEvents.ID + ")) AND " +
                    "((0 = " + objEvents.status + ") OR (e.status = " + objEvents.status + ")) AND " +
                    "((0 = " + objEvents.school_id + ") OR (e.school_id = " + objEvents.school_id + ")) AND " +
                    "((('' = '" + objEvents.title + "') OR (e.title like '%" + objEvents.title + "%' )) " + objEvents.search_operator +
                    "(('' = '" + objEvents.description + "') OR (e.description like '%" + objEvents.description + "%' ))) ORDER BY e.start_date;";
                resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                logErrors("searchEvents", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        public string insertEvent(eMallEntity.events objEvents)
        {
            eMallDA objDA = new eMallDA();
            string returnValue = string.Empty;
            string sqlQuery = string.Empty;
            try
            {
                if (objEvents.ID == 0)
                {
                    sqlQuery = "INSERT INTO events (title, description, start_date, end_date, status, created_by, created_date, modified_date, school_id ) VALUES(" +
                            "'" + objEvents.title + "','" + objEvents.description + "','" + objEvents.start_date.ToString("yyyy-MM-dd") + "', " +
                            "'" + objEvents.end_date.ToString("yyyy-MM-dd") + "'," + objEvents.status + "," + objEvents.created_by + ", " +
                            "now(), now(), " + objEvents.school_id + " )";
                }
                else
                {
                    sqlQuery = "UPDATE events SET title = '" + objEvents.title + "', description = '" + objEvents.description + "', "
                    + "start_date = '" + objEvents.start_date.ToString("yyyy-MM-dd") + "', "
                    + "end_date = '" + objEvents.start_date.ToString("yyyy-MM-dd") + "', status = " + objEvents.status + ", "
                    + "created_by = " + objEvents.created_by + ", modified_date = now(),  "
                    + "school_id = " + objEvents.school_id
                    + " WHERE ID = " + objEvents.ID;

                }
                objDA.ExecuteNonQuery(sqlQuery);
                return "success";
            }
            catch (Exception ex)
            {
                return "error";
            }
        }

        public string deleteEvent(int id)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                string sqlQuery = string.Empty;
                sqlQuery = "DELETE FROM events WHERE ID = " + id;
                objDA.ExecuteNonQuery(sqlQuery);
                return "success";
            }
            catch (Exception ex)
            {
                return "error : " + ex.Message;
            }
        }

        #endregion

        #region Student

        public DataTable searchStudents(eMallEntity.student objStudent)
        {
            try
            {
                objStudent.search_operator = objStudent.search_operator == null ? "OR" : objStudent.search_operator;
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT t.id,t.student_id,t.school_id,t.first_name,t.mobile,t.email,t.present_address,t.permenant_address,t.class,t.father_name, t.mother_name, " +
                    "t.contact_mobile, t.contact_email,t.gender, t.wilayath,t.waynumber,t.nationality,t.middle_name,t.family_name,t.gender,t.image_path,t.status, " +
                    "cls.id as class_id, cls.std as class_std, cls.division as class_division, CONCAT(std,division) as classname, b.id as bus_id" +
                    " FROM  student t INNER JOIN class cls ON t.class_id = cls.ID " +
                    "LEFT JOIN bus b ON t.bus_id = b.id " +
                    "WHERE ((0 = " + objStudent.schoolId + ") OR (t.school_id = " + objStudent.schoolId + ")) AND" +
                    //" ((('' = '" + objStudent.studentclass + "') OR (t.class like '%" + objStudent.studentclass + "%' )) " + objStudent.search_operator +
                    "((('' = '" + objStudent.firstname + "') OR (t.first_name like '%" + objStudent.firstname + "%' )) " + objStudent.search_operator +
                    "(('' = '" + objStudent.email + "') OR (t.email like '%" + objStudent.email + "%' )) " + objStudent.search_operator +
                    "(('' = '" + objStudent.mobile + "') OR (t.mobile like '%" + objStudent.mobile + "%' )));";
                resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                logErrors("searchTeachers", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        public string InsertStudent(eMallEntity.student objStudent)
        {
            eMallDA objDA = new eMallDA();
            string returnValue = string.Empty;
            string sqlQuery = string.Empty;
            try
            {
                int status = objStudent.status ? 1 : 0;
                if (objStudent.ID == 0)
                {
                    //ID, Name, Description
                    sqlQuery = "INSERT INTO student (student_id,first_name, mobile, email,father_name,mother_name,contact_email,contact_mobile,nationality,present_address, permenant_address, " +
                            "class_id,wilayath,waynumber,middle_name,family_name,gender,status,image_path,school_id,bus_id) VALUES(" + "'" + objStudent.studentID + "','" + objStudent.firstname + "','" + objStudent.mobile + "','" + objStudent.email + "','" + objStudent.fathername + "','" + objStudent.mothername + "','" + objStudent.contactemail + "'," +
                            "'" + objStudent.contactmobile + "','" + objStudent.nationality + "','" + objStudent.present_address + "','" + objStudent.permenant_address + "'," +
                            "" + objStudent.class_id + ",'" + objStudent.wilayath + "','" + objStudent.waynumber + "','" + objStudent.middlename + "','" + objStudent.familyname + "','" +
                            "" + objStudent.gender + "','" + status + "','" + objStudent.image_path + "'," + objStudent.studentID + "," + objStudent.busID + ")";
                }
                else
                {

                    sqlQuery = "UPDATE student SET first_name = '" + objStudent.firstname + "',student_id = '" + objStudent.studentID + "', mobile = '" + objStudent.mobile + "', "
                + "email = '" + objStudent.email + "',school_id = " + objStudent.schoolId + ","
                + "father_name = '" + objStudent.fathername + "', " + "mother_name = '" + objStudent.mothername + "', " + "contact_email = '" + objStudent.contactemail + "', "
                + "contact_mobile = '" + objStudent.contactmobile + "', nationality = '" + objStudent.nationality + "', present_address = '" + objStudent.present_address + "',"
                + "permenant_address = '" + objStudent.permenant_address + "',"
                + "class_id = " + objStudent.class_id + ", wilayath = '" + objStudent.wilayath + "', bus_id =" + objStudent.busID + ","
                        //+ "Image = '" + objItems.Image + "', IsActive = " + objItems.IsActive + ", ModifiedDate = now() , "
                + "waynumber = '" + objStudent.waynumber + "'," + "middle_name = '" + objStudent.middlename + "'," + "family_name = '" + objStudent.familyname + "'," + "gender = '" + objStudent.gender + "'," + "status = '" + status + "', image_path = '" + objStudent.image_path + "'"
                + "WHERE ID = " + objStudent.ID;

                    //sqlQuery = "UPDATE items SET ItemCode = '" + objItems.ItemCode + "', Name = '" + objItems.Name + "', "
                    //    + "Description = '" + objItems.Description + "', "
                    //    + "Overview = '" + objItems.OverView + "', Price = " + objItems.Price + ", HasOffer = " + objItems.HasOffer + ", "
                    //    + "OfferPrice = " + objItems.OfferPrice + ", OfferDescription = '" + objItems.OfferDescription + "', "
                    //    + "CategoryID = " + objItems.CategoryID + ", SubCategoryID = " + objItems.SubCategoryID + ", "
                    //    + "Image = '" + objItems.Image + "', IsActive = " + objItems.IsActive + ", ModifiedDate = now() , "
                    //    + "Specefications = '" + objItems.Specefications + "' "
                    //    + "WHERE ID = " + objItems.ID;

                }
                objDA.ExecuteNonQuery(sqlQuery);
                return "success";
            }
            catch (Exception ex)
            {
                return "error";
            }
        }

        public string DeleteStudent(int studentID)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                string sqlQuery = string.Empty;
                sqlQuery = "DELETE FROM student WHERE ID = " + studentID;
                objDA.ExecuteNonQuery(sqlQuery);
                return "success";
            }
            catch (Exception ex)
            {
                return "error : " + ex.Message;
            }
        }

        public bool isStudentAlreadyExist(eMallEntity.student objstudent)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT * FROM student WHERE  student_id = '" + objstudent.studentID + "' AND id != " + objstudent.ID;
                int count = Convert.ToInt32(objDA.ExecuteScalar(sqlQuery));
                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public DataTable searchStudentByLoginID(int loginID)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT t.id,t.student_id,t.school_id,t.first_name " +
                    " FROM  student t INNER JOIN login lg ON t.ID = lg.user_id AND lg.type = 4 " +
                    " WHERE lg.id =  " + loginID;
                resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                logErrors("searchTeachers", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        #endregion

        #region class(STD)
        public DataTable getClasses(eMallEntity.Class objClass)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();

                string sqlQuery = "SELECT id, school_id, std, division, status, CONCAT(std,division) as classname FROM class " +
                    "WHERE ((0 = " + objClass.ID + ") OR (id = " + objClass.ID + ")) AND " +
                    "((0 = " + objClass.school_id + ") OR (school_id = " + objClass.school_id + ")) ; ";
                resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                logErrors("searchEvents", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }
        #endregion

        #region Bus
        public DataTable searchBus(eMallEntity.bus objBus)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                objBus.search_operator = objBus.search_operator == null ? " OR " : objBus.search_operator;
                string sqlQuery = "SELECT b.id, b.school_id, b.bus_number, b.rout, b.driver, b.driver_mobile, b.driver_id from bus b " +
                    "WHERE ((0 = " + objBus.ID + ") OR (b.id = " + objBus.ID + ")) AND " +
                    "((0 = " + objBus.school_id + ") OR (b.school_id = " + objBus.school_id + ")) AND " +
                    "((('' = '" + objBus.bus_number + "') OR (b.bus_number like '%" + objBus.bus_number + "%' )) " + objBus.search_operator +
                    "(('' = '" + objBus.rout + "') OR (b.rout like '%" + objBus.rout + "%' ))) ORDER BY b.bus_number;";
                resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                logErrors("searchBus", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        public string insertBus(eMallEntity.bus objBus)
        {
            eMallDA objDA = new eMallDA();
            string returnValue = string.Empty;
            string sqlQuery = string.Empty;
            try
            {
                if (objBus.ID == 0)
                {
                    sqlQuery = "INSERT INTO bus (bus_number, rout, driver, driver_mobile, school_id, driver_id ) VALUES(" +
                            "'" + objBus.bus_number + "','" + objBus.rout + "','" + objBus.driver + "', " +
                            "'" + objBus.mobile + "'," + objBus.school_id + "," + objBus.driver_id + " )";
                }
                else
                {
                    sqlQuery = "UPDATE bus SET bus_number = '" + objBus.bus_number + "', rout = '" + objBus.rout + "', "
                    + "driver = '" + objBus.driver + "', "
                    + "driver_mobile = '" + objBus.mobile + "',"
                    + "school_id = " + objBus.school_id + ", driver_id = " + objBus.driver_id
                    + " WHERE ID = " + objBus.ID;

                }
                objDA.ExecuteNonQuery(sqlQuery);
                return "success";
            }
            catch (Exception ex)
            {
                return "error";

            }
        }

        public string deleteBus(int id)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                string sqlQuery = string.Empty;
                sqlQuery = "DELETE FROM bus WHERE ID = " + id;
                objDA.ExecuteNonQuery(sqlQuery);
                return "success";
            }
            catch (Exception ex)
            {
                return "error : " + ex.Message;
            }
        }

        #endregion

        #region Driver

        public DataTable getDriver(int school_id)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT id,name,mobile,address FROM driver WHERE 0 = " + school_id + " OR school_id = " + school_id;
                resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                eMallBL.logErrors("getDriver", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        public DataTable searchDriver(eMallEntity.driver objDriver)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT d.id,d.name,d.school_id,d.mobile,d.address,l.id as login_id,l.username,l.password " +
                    //"s.code as school_code " +
                    "FROM driver d INNER JOIN school s ON d.school_id = s.id LEFT JOIN login l ON d.id = l.user_id AND l.type = 5 " +
                    "WHERE ((0 = " + objDriver.ID + ") OR (d.id = " + objDriver.ID + ")) AND " +
                    "((0 = " + objDriver.school_id + ") OR (d.school_id = " + objDriver.school_id + "))  ";
                resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                logErrors("seacrh Driver", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        public string insertDriver(eMallEntity.driver objDriver)
        {
            eMallDA objDA = new eMallDA();
            string returnValue = string.Empty;
            string sqlQuery = string.Empty;
            try
            {
                if (objDriver.ID == 0)
                {
                    sqlQuery = "INSERT INTO driver (name, mobile, address, school_id ) VALUES(" +
                            "'" + objDriver.name + "','" + objDriver.mobile + "','" + objDriver.address + "', " + objDriver.school_id + " )";
                    objDA.ExecuteNonQuery(sqlQuery);
                    string sqlQuery1 = " SELECT MAX(id) FROM driver WHERE school_id = " + objDriver.school_id;
                    int driver_id = Convert.ToInt32(objDA.ExecuteScalar(sqlQuery1));
                    string sqlQuery3 = "INSERT INTO login (username, password, type, school_id, user_id ) VALUES(" +
                            "'" + objDriver.username + "','" + objDriver.password + "',5, " + objDriver.school_id + "," + driver_id + " )";
                    objDA.ExecuteNonQuery(sqlQuery3);
                }
                else
                {
                    string sqlQueryUp1 = "UPDATE driver SET name = '" + objDriver.name + "', mobile = '" + objDriver.mobile + "', "
                    + "address = '" + objDriver.address + "', "
                    + "school_id = " + objDriver.school_id
                    + " WHERE ID = " + objDriver.ID;
                    objDA.ExecuteNonQuery(sqlQueryUp1);

                    string sqlQueryUp2 = "UPDATE login SET username = '" + objDriver.username + "', password = '" + objDriver.password + "' "
                        //+ "school_id = " + objDriver.school_id
                    + " WHERE user_id = " + objDriver.ID + " AND type = 5 AND school_id = " + objDriver.school_id;
                    objDA.ExecuteNonQuery(sqlQueryUp2);
                }
                return "success";
            }
            catch (Exception ex)
            {
                return "error";
            }
        }

        public string deleteDriver(int driver_id, int login_id)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                string sqlQuery = string.Empty;
                sqlQuery = "DELETE FROM driver WHERE ID = " + driver_id;
                objDA.ExecuteNonQuery(sqlQuery);

                string sqlQuery1 = string.Empty;
                sqlQuery1 = "DELETE FROM login WHERE ID = " + login_id;
                objDA.ExecuteNonQuery(sqlQuery1);

                return "success";
            }
            catch (Exception ex)
            {
                return "error : " + ex.Message;
            }
        }

        public bool isUserNameAlreadyExist(string username, int school_id, int user_id)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT * FROM login WHERE username = '" + username + "' AND school_id = " + school_id + " AND user_id != " + user_id;
                int count = Convert.ToInt32(objDA.ExecuteScalar(sqlQuery));
                if (count > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                return true;
            }
        }

        #endregion

        #region Student Login

        public DataTable searchStudentLogin(eMallEntity.studentlogin objStudentLogin)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT lg.id as login_id,lg.device_id,lg.username,lg.password,lg.type,s.id as student_id,s.student_id as student_code,s.first_name as name,s.mobile,s.email,s.school_id FROM login lg RIGHT JOIN student s ON lg.user_id = s.id " +
                    //"WHERE ((('' = '" + objTeacher.code + "') OR (t.code like '%" + objTeacher.code + "%' )) " + objTeacher.search_operator +
                    "WHERE ((0 = " + objStudentLogin.schoolID + ") OR (s.school_id = " + objStudentLogin.schoolID + ")) AND (lg.type = '4' OR lg.type is null) ";
                //"((('' = '" + objteacherlogin.teachername + "') OR (t.name like '%" + objteacherlogin.teachername + "%' )) " + objteacherlogin.search_operator +
                //"(('' = '" + objteacherlogin.teachercode + "') OR (t.code like '%" + objteacherlogin.teachercode + "%' ))) AND (lg.type = '3' OR lg.type is null) ";
                resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                logErrors("searchSchool", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        public string InsertStudentLogin(eMallEntity.studentlogin objStudentLogin)
        {
            eMallDA objDA = new eMallDA();
            string returnValue = string.Empty;
            string sqlQuery = string.Empty;
            try
            {
                if (objStudentLogin.loginID == 0)
                {
                    //ID, Name, Description
                    sqlQuery = "INSERT INTO login (username,password,type,user_id,created_date,school_id,device_id) VALUES(" +
                            "'" + objStudentLogin.username + "','" + objStudentLogin.password + "','" + objStudentLogin.type + "'," + objStudentLogin.student_id + ",now()," +
                    "" + objStudentLogin.schoolID + ",'" + objStudentLogin.device_id + "' )";
                }
                else
                {
                    sqlQuery = "UPDATE login SET username = '" + objStudentLogin.username + "', password = '" + objStudentLogin.password + "', "
                    + "type = '" + objStudentLogin.type + "', user_id = " + objStudentLogin.student_id + ", device_id = '" + objStudentLogin.device_id + "' "
                    + " WHERE ID = " + objStudentLogin.loginID;
                }
                objDA.ExecuteNonQuery(sqlQuery);
                return "success";
            }
            catch (Exception ex)
            {
                return "error";
            }
        }

        public bool isStudentUserNameAlreadyExist(eMallEntity.studentlogin objStudentLogin)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT * FROM login WHERE username = '" + objStudentLogin.username + "' AND ID != " + objStudentLogin.loginID;
                int count = Convert.ToInt32(objDA.ExecuteScalar(sqlQuery));
                if (count > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public DataTable getschoolCodes(int school_id)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT s.id as id, s.student_id as code FROM student s INNER JOIN school sc ON s.school_id = sc.id WHERE sc.id =" + school_id;

                resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                logErrors("searchTeachers", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        #endregion

        #region Common Services

        public string isReferenceExist(int ID, string column, string tables)
        {
            string msg = "NO";
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery;

                string[] tablelist = tables.Split(',');
                for (int i = 0; i < tablelist.Length; i++)
                {
                    sqlQuery = "SELECT * FROM " + tablelist[i].ToString() + " WHERE " + column + " = " + ID; ;
                    int count = Convert.ToInt32(objDA.ExecuteScalar(sqlQuery));
                    if (count > 0)
                    {
                        if (msg == "NO")
                            msg = "Reference exist in " + tablelist[i].ToString();
                        else
                            msg += ", " + tablelist[i].ToString();
                    }
                }
                return msg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region Notification
        public DataTable searchNotifications(int school_id)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT n.id,n.from_id,n.from_type,n.subject,n.message,n.date,n.school_id,n.tmembers,n.smembers " +
                    //"s.code as school_code " +
                    "FROM notification n INNER JOIN school s ON n.school_id = s.id  " +
                    "WHERE ((0 = " + school_id + ") OR (n.school_id = " + school_id + "))";
                resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                logErrors("searchNotifications", ex.GetType().ToString(), ex.Message);
                return null;
            }
        }

        public string sendEmailNotification(string sub, string msg, string from, string teacherClasses, string studentClasses)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT GROUP_CONCAT(em.email SEPARATOR ',') as emails FROM " +
                    "(SELECT s.email,s.class_id from student s INNER JOIN class c ON s.class_id = c.id WHERE s.class_id IN (" + studentClasses + ") UNION  " +
                    " SELECT t.email,t.class_id FROM teacher t INNER JOIN class c ON t.class_id = c.id WHERE t.class_id IN (" + teacherClasses + ") ) as em;  ";
                resultTable = objDA.ExecuteDataTable(sqlQuery);

                for (int i = 0; i < resultTable.Rows.Count; i++)
                {
                    string emails = resultTable.Rows[i]["emails"].ToString();
                    if (emails != "")
                        sendEmail(sub, msg, from, emails);
                }
                return "success";
            }
            catch (Exception ex)
            {
                logErrors("searchNotifications", ex.GetType().ToString(), ex.Message);
                return "error";
            }
        }

        public static void sendEmail(string sub, string msg, string from, string bcc)
        {
            try
            {
                using (MailMessage mm = new MailMessage("info@tuvian.com", "info@tuvian.com"))
                {
                    mm.Subject = sub;
                    mm.Body = msg;
                    //if (fuAttachment.HasFile)
                    //{
                    //    string FileName = Path.GetFileName(fuAttachment.PostedFile.FileName);
                    //    mm.Attachments.Add(new Attachment(fuAttachment.PostedFile.InputStream, FileName));
                    //}
                    mm.Bcc.Add(bcc);
                    mm.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "relay-hosting.secureserver.net";
                    smtp.EnableSsl = false;
                    NetworkCredential NetworkCred = new NetworkCredential("info@tuvian.com", "tuvian@123");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 25;
                    smtp.Send(mm);
                }
            }
            catch (Exception ex)
            {
                logErrors("On sendEmail " + ex.Message);
                throw;
            }
        }

        public void sendemailfortesting()
        {
            try
            {
                using (MailMessage mm = new MailMessage("info@tuvian.com", "ansu.ansar@gmail.com"))
                {
                    mm.Subject = "test";
                    mm.Body = "test msg";
                    //if (fuAttachment.HasFile)
                    //{
                    //    string FileName = Path.GetFileName(fuAttachment.PostedFile.FileName);
                    //    mm.Attachments.Add(new Attachment(fuAttachment.PostedFile.InputStream, FileName));
                    //}
                    mm.Bcc.Add("info@tuvian.com");
                    mm.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "relay-hosting.secureserver.net";
                    smtp.EnableSsl = false;
                    NetworkCredential NetworkCred = new NetworkCredential("info@tuvian.com", "tuvian@123");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 25;
                    smtp.Send(mm);
                }
            }
            catch (Exception ex)
            {
                //("SendEmailForTesting", ex.Message);
                throw ex;
            }
        }

        public string sendPushNotification(string fname, string fid, string ftype, string file, string sub, string msg, int school_id, string teacherClasses, string studentClasses)
        {
            eMallDA objDA = new eMallDA();
            DataTable resultTable = new DataTable();
            DataTable APItable = new DataTable();

            //string sqlQuery = "SELECT GROUP_CONCAT(em.google_regid SEPARATOR ',') as regids FROM " +
            //    "(SELECT s.email,s.class_id,l.google_regid from student s INNER JOIN class c ON s.class_id = c.id INNER JOIN login l ON l.user_id = s.id AND l.type = 4 WHERE s.class_id IN (" + studentClasses + ") UNION  " +
            //    " SELECT t.email,t.class_id,l.google_regid FROM teacher t INNER JOIN class c ON t.class_id = c.id INNER JOIN login l ON l.user_id = t.id AND l.type = 4 WHERE t.class_id IN (" + teacherClasses + ") ) as em;  ";

            string sqlQuery = "SELECT s.email,s.class_id,l.google_regid from student s INNER JOIN class c ON s.class_id = c.id INNER JOIN login l ON l.user_id = s.id AND l.type = 4 WHERE s.class_id IN (" + studentClasses + ") UNION  " +
                " SELECT t.email,t.class_id,l.google_regid FROM teacher t INNER JOIN class c ON t.class_id = c.id INNER JOIN login l ON l.user_id = t.id AND l.type = 3 WHERE t.class_id IN (" + teacherClasses + ") ";
            resultTable = objDA.ExecuteDataTable(sqlQuery);

            string googleSenderID = "";
            string googleAppID = "";

            string sqlQueryAPIKey = "SELECT google_sender_id, google_app_id FROM school WHERE id = " + school_id;
            APItable = objDA.ExecuteDataTable(sqlQueryAPIKey);
            if (APItable.Rows.Count > 0)
            {
                googleSenderID = APItable.Rows[0]["google_sender_id"].ToString();
                googleAppID = APItable.Rows[0]["google_app_id"].ToString();
            }

            string notStatus = "success";
            for (int i = 0; i < resultTable.Rows.Count; i++)
            {
                string regid = resultTable.Rows[i]["google_regid"].ToString();
                if (regid != null && regid != "" && googleSenderID != "" && googleAppID != "")
                    notStatus = sendPushNot(fname, fid, ftype, file, sub, msg, regid, googleSenderID, googleAppID);
            }
            return "success";
        }

        private string sendPushNot(string fname, string fid, string ftype, string file, string sub, string msg, string regid, string senderID, string googleAPPID)
        {
            try
            {
                string GoogleAppID = googleAPPID;
                var SENDER_ID = senderID;
                string devider = ":RBAIJSDUR:";
                var value = "NT" + devider + System.DateTime.Now + devider + fname + devider + fid + devider + ftype + devider + sub + devider + file + devider + msg;
                WebRequest tRequest;
                tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", GoogleAppID));

                tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

                string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + value + "&data.time=" +
                System.DateTime.Now.ToString() + "&registration_id=" + regid + "";
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
                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');",
                //        "Error :" + ex.Message), true);
                return "Error" + ex.Message;
            }
        }

        #endregion
        
        public DataTable searchPwd(int school_id)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                DataTable resultTable = new DataTable();
                string sqlQuery = "SELECT p.id, p.school_id, p.password, p.is_taken from password p Where p.school_id = " + school_id + " AND is_taken = 0";
                resultTable = objDA.ExecuteDataTable(sqlQuery);
                return resultTable;
            }
            catch (Exception ex)
            {
                logErrors("searchBus", ex.GetType().ToString(), ex.Message);
                return null;
            }

        }

        public string deletePassword(int ID)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                string sqlQuery = string.Empty;
                sqlQuery = "DELETE FROM password WHERE ID = " + ID;
                objDA.ExecuteNonQuery(sqlQuery);
                return "success";
            }
            catch (Exception ex)
            {
                return "error : " + ex.Message;
            }
        }

        public string generatePwd(string prefix, int count, int school_id)
        {
            try
            {
                eMallDA objDA = new eMallDA();
                string sqlQuery = string.Empty;
                for (int i = 0; i < count; i++)
                {
                    string password = prefix + CreateRandomPassword();
                    sqlQuery = "INSERT INTO password (school_id,password,is_taken) VALUES(" + school_id + ",'" + password + "',0);";
                    objDA.ExecuteNonQuery(sqlQuery);
                }
                
                return "success";
            }
            catch (Exception ex)
            {
                return "error : " + ex.Message;
            }
        }

        public static string CreateRandomPassword()
        {
            //string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            string _allowedChars = "0123456789";
            Random randNum = new Random();
            char[] chars = new char[6];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < 6; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
    }
}
