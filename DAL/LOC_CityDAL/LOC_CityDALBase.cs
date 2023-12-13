using AdminPanel.Areas.LOC_City.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace AdminPanel.DAL.LOC_CityDAL
{
    public class LOC_CityDALBase : DALHelper
    {
        #region Method : dbo.PR_LOC_City_SelectAll
        public DataTable dbo_PR_LOC_City_SelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_City_SelectAll");
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method : dbo.PR_LOC_City_Insert & dbo.PR_LOC_City_Update

        public bool dbo_PR_LOC_City_Save(LOC_CityModel lOC_CityModel)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                if (lOC_CityModel.CityID == 0)
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_City_Insert");
                    sqlDatabase.AddInParameter(dbCommand, "@CityName", DbType.String, lOC_CityModel.CityName);
                    sqlDatabase.AddInParameter(dbCommand, "@CityCode", DbType.String, lOC_CityModel.CityName);
                    sqlDatabase.AddInParameter(dbCommand, "@StateID", DbType.Int32, lOC_CityModel.StateID);
                    sqlDatabase.AddInParameter(dbCommand, "@CountryID", DbType.Int32, lOC_CityModel.CountryID);
                    sqlDatabase.AddInParameter(dbCommand, "@Created", DbType.DateTime, DBNull.Value);
                    sqlDatabase.AddInParameter(dbCommand, "@Modified", DbType.DateTime, DBNull.Value);
                    sqlDatabase.ExecuteNonQuery(dbCommand);
                    return true;

                }
                else
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_City_UpdateByPK");
                    sqlDatabase.AddInParameter(dbCommand, "@CityID", DbType.Int32, lOC_CityModel.CityID);
                    sqlDatabase.AddInParameter(dbCommand, "@CityName", DbType.String, lOC_CityModel.CityName);
                    sqlDatabase.AddInParameter(dbCommand, "@CityCode", DbType.String, lOC_CityModel.CityName);
                    sqlDatabase.AddInParameter(dbCommand, "@StateID", DbType.Int32, lOC_CityModel.StateID);
                    sqlDatabase.AddInParameter(dbCommand, "@CountryID", DbType.Int32, lOC_CityModel.CountryID);
                    sqlDatabase.AddInParameter(dbCommand, "@Modified", DbType.DateTime, DBNull.Value);
                    sqlDatabase.ExecuteNonQuery(dbCommand);
                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Method : dbo.PR_LOC_City_SelectByPK
        public LOC_CityModel dbo_PR_LOC_City_SelectByPK(int? CityID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_City_SelectByPK");
                sqlDatabase.AddInParameter(dbCommand, "@CityID", DbType.Int64, CityID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                LOC_CityModel lOC_CityModel = new LOC_CityModel();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    lOC_CityModel.StateID = Convert.ToInt32(dataRow["StateID"]);
                    lOC_CityModel.Created = Convert.ToDateTime(dataRow["CreationDate"].ToString());
                    lOC_CityModel.Modified = Convert.ToDateTime(dataRow["Modified"].ToString());
                    lOC_CityModel.CountryID = Convert.ToInt32(dataRow["CountryID"]);
                    lOC_CityModel.CityID = Convert.ToInt32(dataRow["CityID"]);
                    lOC_CityModel.CityCode = dataRow["CityCode"].ToString();
                    lOC_CityModel.CityName = dataRow["CityName"].ToString();
                }
                return lOC_CityModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method : dbo.PR_LOC_City_Delete
        public void dbo_PR_LOC_City_Delete(int? CityID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_City_DeleteByPK");
                sqlDatabase.AddInParameter(dbCommand, "@CityID", DbType.Int64, CityID);
                sqlDatabase.ExecuteNonQuery(dbCommand);
                return;
            }
            catch (Exception ex)
            {
                return;
            }

        }
        #endregion



    }
}
