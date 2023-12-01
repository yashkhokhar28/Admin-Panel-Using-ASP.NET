using AdminPanel.Areas.LOC_Country.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace AdminPanel.DAL.LOC_CountryDAL
{
    public class LOC_CountryDALBase : DALHelper
    {
        #region Method : dbo.PR_LOC_Country_SelectAll
        public DataTable dbo_PR_LOC_Country_SelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Country_SelectAll");
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

        #region Method : dbo.PR_LOC_Country_Insert & dbo.PR_LOC_Country_Update
        public bool dbo_PR_LOC_Country_Save(LOC_CountryModel lOC_CountryModel)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                if (lOC_CountryModel.CountryID == 0)
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Country_Insert");
                    sqlDatabase.AddInParameter(dbCommand, "@CountryName", DbType.String, lOC_CountryModel.CountryName);
                    sqlDatabase.AddInParameter(dbCommand, "@CountryCode", DbType.String, lOC_CountryModel.CountryCode);
                    sqlDatabase.AddInParameter(dbCommand, "@Created", DbType.DateTime, DBNull.Value);
                    sqlDatabase.AddInParameter(dbCommand, "@Modified", DbType.Int64, DBNull.Value);
                    sqlDatabase.ExecuteNonQuery(dbCommand);
                    return true;
                }
                else
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Country_UpdateByPK");
                    sqlDatabase.AddInParameter(dbCommand, "@CountryID", DbType.Int64, lOC_CountryModel.CountryID);
                    sqlDatabase.AddInParameter(dbCommand, "@CountryName", DbType.String, lOC_CountryModel.CountryName);
                    sqlDatabase.AddInParameter(dbCommand, "@CountryCode", DbType.String, lOC_CountryModel.CountryCode);
                    sqlDatabase.AddInParameter(dbCommand, "@Modified", DbType.Int64, DBNull.Value);
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

        #region Method : dbo.PR_LOC_Country_SelectByPK
        public LOC_CountryModel dbo_PR_LOC_Country_SelectByPK(int? CountryID)
        {
            LOC_CountryModel lOC_CountryModel = new LOC_CountryModel();
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Country_SelectByPK");
                sqlDatabase.AddInParameter(dbCommand, "@CountryID", DbType.Int64, CountryID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    lOC_CountryModel.CountryID = Convert.ToInt32(dataRow["CountryID"]);
                    lOC_CountryModel.CountryName = dataRow["CountryName"].ToString();
                    lOC_CountryModel.CountryCode = dataRow["CountryCode"].ToString();
                    lOC_CountryModel.Created = Convert.ToDateTime(dataRow["Created"].ToString());
                    lOC_CountryModel.Modified = Convert.ToDateTime(dataRow["Modified"].ToString());
                }
                return lOC_CountryModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method : dbo.PR_LOC_Country_Delete
        public void dbo_PR_LOC_Country_Delete(int? CountryID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Country_DeleteByPK");
                sqlDatabase.AddInParameter(dbCommand, "CountryID", DbType.Int64, CountryID);
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
