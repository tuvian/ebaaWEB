using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinussLayer
{
    /// <summary>
    /// This class do all Holiday operations. Insert, delete, Get..etc.
    /// </summary>
    public class HolidayBL
    {
        private static string sqlQuery = string.Empty;
        private eMallDA objDA;

        /// <summary>
        /// Get the Holiday based given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetHoliday(int id)
        {
            DataTable holiday = new DataTable();
            objDA = new eMallDA();
            sqlQuery = "SELECT * FROM holiday where status = 1 and id =" + id;
            try
            {
                holiday = objDA.ExecuteDataTable(sqlQuery);
            }
            catch (Exception ex)
            {
                eMallBL.logErrors(this.GetType().FullName, ex.GetType().ToString(), ex.ToString());
                return holiday;
            }

            return holiday;
        }

        /// <summary>
        /// Returns the list of all holidays
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetHolidays()
        {
            DataTable holidays = new DataTable();
            objDA = new eMallDA();
            sqlQuery = "SELECT * FROM holiday where status = 1";
            try
            {
                holidays = objDA.ExecuteDataTable(sqlQuery);
            }
            catch (Exception ex)
            {
                eMallBL.logErrors(this.GetType().FullName, ex.GetType().ToString(), ex.ToString());
                return holidays;
            }
            return holidays;
        }

        /// <summary>
        /// Returns list of holidas based on date filter
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public DataTable GetHolidaysByDate(DateTime fromDate, DateTime toDate)
        {
            DataTable holidays = null;
            objDA = new eMallDA();
            sqlQuery = "SELECT * FROM holiday where fromdate>'" +
                fromDate + "' and todate<'" + toDate + "'";

            try
            {
                holidays = objDA.ExecuteDataTable(sqlQuery);
            }
            catch (Exception ex)
            {
                eMallBL.logErrors(this.GetType().FullName, ex.GetType().ToString(), ex.ToString());
                return holidays;
            }

            return holidays;
        }

        /// <summary>
        /// Returns list of holidas based on date filter
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public DataTable GetHolidaysByFilter(int status, int schoolId, string searchText)
        {
            DataTable holidays = null;
            objDA = new eMallDA();
            try
            {
                if (status == 0 && schoolId ==0)
                    sqlQuery = "SELECT * FROM holiday where name like '%" + searchText + "%' OR description like '%" + searchText + "%'";
                else if (status == 0)
                    sqlQuery = "SELECT * FROM holiday where schoolId=" + schoolId
                        + " AND (name like '%" + searchText + "%' OR description like '%" + searchText + "%')" ;
                else if (schoolId == 1)
                    sqlQuery = "SELECT * FROM holiday where status=" + status
                        + " AND (name like '%" + searchText + "%' OR description like '%" + searchText + "%')";
                else
                    sqlQuery = "SELECT * FROM holiday where status=" + status + " and schoolId=" + schoolId 
                    + " AND (name like '%" + searchText + "%' OR description like '%" + searchText + "%'";

                holidays = objDA.ExecuteDataTable(sqlQuery);
            }
            catch (Exception ex)
            {
                eMallBL.logErrors(this.GetType().FullName, ex.GetType().ToString(), ex.ToString());
                return holidays;
            }

            return holidays;
        }

        /// <summary>
        /// Insert data into Holiday Datable
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(Entity.eMallEntity.Holiday entity)
        {
            objDA = new eMallDA();
            int result = 0;

            try
            {
                sqlQuery = "INSERT into holiday (name, description, fromdate, type, status, schoolId, todate, createdBy, createdDate, updatedBy, updatedDate) values ('"
                + entity.Name + "', '" + entity.Desccription + "', '" + entity.FromDate.ToString("yyyy-MM-dd") + "'," + entity.Type + "," + entity.Status + "," + entity.SchoolId +
                ", '" + entity.ToDate.ToString("yyyy-MM-dd") + "', '" + entity.CreatedBy + "', NOW(), '" + entity.UpdatedBy + "', NOW())";
                result = objDA.ExecuteQuery(sqlQuery);
            }
            catch (Exception ex)
            {
                eMallBL.logErrors(this.GetType().FullName, ex.GetType().ToString(), ex.ToString());
                return result;
            }
            return result;
        }

        /// <summary>
        /// Update Holidate Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(Entity.eMallEntity.Holiday entity)
        {
            objDA = new eMallDA();
            int result = 0;

            try
            {
                sqlQuery = "UPDATE holiday SET name ='" + entity.Name + "', description='" + entity.Desccription +
                    "',fromdate='" + entity.FromDate.ToString("yyyy-MM-dd") + "',todate ='" + entity.ToDate.ToString("yyyy-MM-dd") + "', schoolId =" + entity.SchoolId
               + ", type =" + entity.Type + ", updatedBy='" + entity.UpdatedBy + "', updatedDate =NOW() WHERE ID =" + entity.ID;
                result = objDA.ExecuteQuery(sqlQuery);
            }
            catch (Exception ex)
            {
                eMallBL.logErrors(this.GetType().FullName, ex.GetType().ToString(), ex.ToString());
                return result;
            }

            return result;
        }

        /// <summary>
        /// Delete Holiday, based selected Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            objDA = new eMallDA();
            int result = 0;
            try
            {
                sqlQuery = "DELETE FROM holiday WHERE id=" + Id;
                result = objDA.ExecuteQuery(sqlQuery);
            }
            catch (Exception ex)
            {
                eMallBL.logErrors(this.GetType().FullName, ex.GetType().ToString(), ex.ToString());
                return result;
            }

            return result;
        }

        /// <summary>
        /// Checks whether holiday is already exists or not.
        /// </summary>
        /// <returns></returns>
        public int IsExist(Entity.eMallEntity.Holiday holiday)
        {
            objDA = new eMallDA();
            int result = 0;
            sqlQuery = "";

            try
            {

            }
            catch (Exception ex)
            {
                eMallBL.logErrors(this.GetType().FullName, ex.GetType().ToString(), ex.ToString());
                return result;
            }
            return result;
        }
    }
}
