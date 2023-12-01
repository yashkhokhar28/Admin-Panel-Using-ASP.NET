using AdminPanel.Areas.LOC_Country.Models;
using AdminPanel.Areas.LOC_State.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace AdminPanel.DAL.LOC_StateDAL
{
    public class LOC_StateDAL : LOC_StateDALBase
    {
        #region Method : dbo.PR_LOC_Country_Combobox
        public List<LOC_CountryDropDownModel> dbo_PR_LOC_Country_Combobox()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Country_ComboBox");
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                List<LOC_CountryDropDownModel> listOfCountry = new List<LOC_CountryDropDownModel>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    LOC_CountryDropDownModel lOC_CountryDropDownModel = new LOC_CountryDropDownModel();
                    lOC_CountryDropDownModel.CountryID = Convert.ToInt32(dataRow["CountryID"]);
                    lOC_CountryDropDownModel.CountryName = dataRow["CountryName"].ToString();
                    listOfCountry.Add(lOC_CountryDropDownModel);
                }
                return listOfCountry;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method : dbo.PR_LOC_StateFilter
        public DataTable dbo_PR_LOC_StateFilter(LOC_StateFilterModel lOC_StateFilterModel)
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_StateFilter");
                sqlDatabase.AddInParameter(dbCommand, "@CountryID", DbType.Int64, lOC_StateFilterModel.CountryID);
                sqlDatabase.AddInParameter(dbCommand, "@StateName", DbType.String, lOC_StateFilterModel.StateName);
                sqlDatabase.AddInParameter(dbCommand, "@StateCode", DbType.String, lOC_StateFilterModel.StateCode);
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
    }
}
