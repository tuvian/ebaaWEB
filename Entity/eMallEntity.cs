using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class eMallEntity
    {
        #region MySchool
        public class teacher
        {
            public int ID { get; set; }
            public int school_id { get; set; }
            public int department_id { get; set; }
            public int designation_id { get; set; }
            public string code { get; set; }
            public string name { get; set; }
            public string qualification { get; set; }
            public string mobile { get; set; }
            public string email { get; set; }
            public string permenant_address { get; set; }
            public bool status { get; set; }
            public string nationality { get; set; }
            public string present_address { get; set; }
            public string image_path { get; set; }
            public string notes { get; set; }
            public string search_operator { get; set; }
            public int class_id { get; set; }
            public string class_std { get; set; }
            public string class_division { get; set; }
            public string class_name { get; set; }
            public List<String> class_ids { get; set; }


        }

        public class school
        {
            public int ID { get; set; }
            public int package_id { get; set; }
            public string code { get; set; }
            public string name { get; set; }
            public string siteurl { get; set; }
            public string school_address { get; set; }
            public string contact_person { get; set; }
            public string mobile { get; set; }
            public bool status { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string contact_address { get; set; }
            public string nationality { get; set; }
            public DateTime register_date { get; set; }
            public DateTime expire_date { get; set; }
            public string logo { get; set; }
            public string notes { get; set; }
            public string search_operator { get; set; }
            public string wilayath { get; set; }
            public string waynumber { get; set; }
        }

        public class schooluser
        {
            public int ID { get; set; }
            public int schoolID { get; set; }
            public string code { get; set; }
            public string name { get; set; }
            public string mobile { get; set; }
            public string email { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public string type { get; set; }
            public string search_operator { get; set; }
        }

        public class teacherlogin
        {
            public int ID { get; set; }
            public int schoolID { get; set; }
            public string schoolcode { get; set; }
            public int teacherid { get; set; }
            public string teachername { get; set; }
            public string teachercode { get; set; }
            public string department { get; set; }
            public string mobile { get; set; }
            public string email { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public string type { get; set; }
            public string search_operator { get; set; }
        }

        public class studentlogin
        {
            public int loginID { get; set; }
            public int schoolID { get; set; }
            public string schoolcode { get; set; }
            public int student_id { get; set; }
            public string studentname { get; set; }
            public string student_Code { get; set; }
            public string mobile { get; set; }
            public string email { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public string type { get; set; }
            public string device_id { get; set; }
            public string search_operator { get; set; }
        }


        public class events
        {
            public int ID { get; set; }
            public int school_id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public DateTime start_date { get; set; }
            public DateTime end_date { get; set; }
            public int status { get; set; }
            public int created_by { get; set; }
            public DateTime created_date { get; set; }
            public DateTime modified_date { get; set; }
            public string search_operator { get; set; }
        }

        public class driver
        {
            public int ID { get; set; }
            public int school_id { get; set; }
            public int bus_id { get; set; }
            public string name { get; set; }
            public string mobile { get; set; }
            public string address { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public string search_operator { get; set; }
        }

        public class login
        {
            public int ID { get; set; }
            public int school_id { get; set; }
            public int user_id { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public int active { get; set; }
            public int type { get; set; }
            public int force_change { get; set; }
            public string language { get; set; }
            public string api_status { get; set; }
            public string api_message { get; set; }

        }

        public class login_service
        {
            public int ID { get; set; }
            public int school_id { get; set; }
            public int user_id { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public int active { get; set; }
            public int type { get; set; }
            public int force_change { get; set; }
            public string language { get; set; }
            public string api_status { get; set; }
            public string api_message { get; set; }

        }


        /// <summary>
        /// Base Entiry having ID and Logs
        /// </summary>
        public class BaseEntity
        {
            public int ID { get; set; }
            public string CreatedBy { get; set; }
            public DateTime CreatedDate { get; set; }
            public string UpdatedBy { get; set; }
            public string UpdatedDate { get; set; }
        }

        /// <summary>
        /// Stores Holiday Informations
        /// </summary>
        public class Holiday : BaseEntity
        {
            public string Name { get; set; }
            public string Desccription { get; set; }
            public int Status { get; set; }
            public int Type { get; set; }
            public int SchoolId { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
        }

        #endregion

        #region Student

        public class student
        {
            public int ID { get; set; }
            public string studentID { get; set; }
            public string firstname { get; set; }
            public string mobile { get; set; }
            public string email { get; set; }
            public string dob { get; set; }
            public string fathername { get; set; }
            public string mothername { get; set; }
            public string contactemail { get; set; }
            public string contactmobile { get; set; }
            public string nationality { get; set; }
            public string permenant_address { get; set; }
            public string present_address { get; set; }
            public string studentclass { get; set; }
            public string wilayath { get; set; }
            public string waynumber { get; set; }
            public string middlename { get; set; }
            public string familyname { get; set; }
            public string gender { get; set; }
            public int schoolId { get; set; }
            public string search_operator { get; set; }
            public bool status { get; set; }
            public string image_path { get; set; }
            public int class_id { get; set; }
            public string class_std { get; set; }
            public string class_division { get; set; }
            public string class_name { get; set; }
            public int busID { get; set; }

        }
        #endregion

        #region Student

        public class Class
        {
            public int ID { get; set; }
            public int school_id { get; set; }
            public string std { get; set; }
            public string division { get; set; }
            public bool status { get; set; }

        }
        #endregion

        #region bus

        public class bus
        {
            public int ID { get; set; }
            public int school_id { get; set; }
            public int driver_id { get; set; }
            public string bus_number { get; set; }
            public string rout { get; set; }
            public string driver { get; set; }
            public string mobile { get; set; }
            public string search_operator { get; set; }


        }

        #endregion

        public class password
        {
            public int ID { get; set; }
            public int school_id { get; set; }
            public string pwd { get; set; }
            public int is_taken { get; set; }
        }

        #region shope
        public class ItemCatagory
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public IList<ItemSubCatagory> subCategories { get; set; }
        }

        public class ItemSubCatagory
        {
            public int ID { get; set; }
            public int CategoryID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class Items
        {
            public int ID { get; set; }
            public int CategoryID { get; set; }
            public int SubCategoryID { get; set; }
            public string ItemCode { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string OverView { get; set; }
            public double Price { get; set; }
            public string Image { get; set; }
            public bool HasOffer { get; set; }
            public double OfferPrice { get; set; }
            public string OfferDescription { get; set; }
            public bool IsActive { get; set; }
            public DateTime ReleaseDate { get; set; }
            public string Specefications { get; set; }
            public int userID { get; set; }
        }

        //ID, Name, Address, CityID, Phone, Email, ContactPerson, MapDiscription
        public class ShopEntity
        {
            public int ID { get; set; }
            public string ShopName { get; set; }
            public string Address { get; set; }
            public string Image { get; set; }
            public int CityID { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string ContactPerson { get; set; }
            public string MapDiscription { get; set; }
        }

        public class Customer
        {
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Mobile { get; set; }
        }

        public class OrderDetails
        {
            public int ItemID { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public double Price { get; set; }
            public int Qnty { get; set; }
            public string Image { get; set; }
        }

        public class ShoppingCart
        {
            public int OrderID { get; set; }
            public Double TotalPrice { get; set; }
            public DateTime OrderDate { get; set; }
            public IList<OrderDetails> orderDetails { get; set; }
        }

        public class EmailDetails
        {
            public string Subject { get; set; }
            public string Message { get; set; }
            public string To { get; set; }
            public string From { get; set; }
            public List<string> CC { get; set; }
        }

        public class SeacrhCriteria
        {
            public int itemID { get; set; }
            public int categoryID { get; set; }
            public int subCategoryID { get; set; }
            public string itemCode { get; set; }
            public string itemName { get; set; }
            public string itemDescription { get; set; }
            public int hasOffer { get; set; }
            public int isActive { get; set; }
            public string relativeOperator { get; set; } // "OR" and "AND".. 
            //If we specify the search by Code/Name/Description need to use AND operator for search. should match all filters
            //If we wont specify search by(means ALL) need to use OR operator for search.
            public int userID { get; set; }
        }
        #endregion
    }
}
