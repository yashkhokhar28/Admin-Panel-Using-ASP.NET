using AdminPanel.Areas.LOC_State.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace AdminPanel.DAL.LOC_StateDAL
{
    public class LOC_StateDALBase : DALHelper
    {
        #region Method : dbo.PR_LOC_State_SelectAll
        public DataTable dbo_PR_LOC_State_SelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_State_SelectAll");
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

        #region Method : dbo.PR_LOC_State_Insert & dbo.PR_LOC_State_Update
        public bool dbo_PR_LOC_State_Save(LOC_StateModel lOC_StateModel)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                if (lOC_StateModel.StateID == 0)
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_State_Insert");
                    sqlDatabase.AddInParameter(dbCommand, "@StateName", DbType.String, lOC_StateModel.StateName);
                    sqlDatabase.AddInParameter(dbCommand, "@StateCode", DbType.String, lOC_StateModel.StateCode);
                    sqlDatabase.AddInParameter(dbCommand, "@CountryID", DbType.Int64, lOC_StateModel.CountryID);
                    sqlDatabase.AddInParameter(dbCommand, "@Created", DbType.DateTime, DBNull.Value);
                    sqlDatabase.AddInParameter(dbCommand, "@Modified", DbType.DateTime, DBNull.Value);
                    sqlDatabase.ExecuteNonQuery(dbCommand);
                    return true;
                }
                else
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_State_UpdateByPK");
                    sqlDatabase.AddInParameter(dbCommand, "@StateID", DbType.Int64, lOC_StateModel.StateID);
                    sqlDatabase.AddInParameter(dbCommand, "@StateName", DbType.String, lOC_StateModel.StateName);
                    sqlDatabase.AddInParameter(dbCommand, "@StateCode", DbType.String, lOC_StateModel.StateCode);
                    sqlDatabase.AddInParameter(dbCommand, "@CountryID", DbType.Int64, lOC_StateModel.CountryID);
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

        #region Method : dbo.PR_LOC_State_SelectByPK
        public LOC_StateModel dbo_PR_LOC_State_SelectByPK(int? StateID)
        {
            LOC_StateModel lOC_StateModel = new LOC_StateModel();
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_State_SelectByPK");
                sqlDatabase.AddInParameter(dbCommand, "@StateID", DbType.Int64, StateID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    lOC_StateModel.StateID = Convert.ToInt32(dataRow["StateID"]);
                    lOC_StateModel.StateName = dataRow["StateName"].ToString();
                    lOC_StateModel.StateCode = dataRow["StateCode"].ToString();
                    lOC_StateModel.Created = Convert.ToDateTime(dataRow["Created"].ToString());
                    lOC_StateModel.Modified = Convert.ToDateTime(dataRow["Modified"].ToString());
                    lOC_StateModel.CountryID = Convert.ToInt32(dataRow["CountryID"]);
                }
                return lOC_StateModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method : dbo.PR_LOC_State_Delete
        public void dbo_PR_LOC_State_Delete(int? StateID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_State_DeleteByPK");
                sqlDatabase.AddInParameter(dbCommand, "@StateID", DbType.Int64, StateID);
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
