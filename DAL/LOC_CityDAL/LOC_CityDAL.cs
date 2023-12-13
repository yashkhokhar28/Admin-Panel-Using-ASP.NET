using AdminPanel.Areas.LOC_City.Models;
using AdminPanel.Areas.LOC_State.Models;
using AdminPanel.DAL.LOC_CityDAL;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace adminpanel.dal.loc_citydal
{
    public class LOC_CityDAL : LOC_CityDALBase
    {
        #region Method : dbo.PR_LOC_State_Combobox
        public List<LOC_StateDropDownModel> dbo_PR_LOC_State_Combobox()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_State_ComboBox");
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                List<LOC_StateDropDownModel> listOfState = new List<LOC_StateDropDownModel>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    LOC_StateDropDownModel lOC_StateDropDownModel = new LOC_StateDropDownModel();
                    lOC_StateDropDownModel.StateID = Convert.ToInt32(dataRow["StateID"]);
                    lOC_StateDropDownModel.StateName = dataRow["StateName"].ToString();
                    listOfState.Add(lOC_StateDropDownModel);
                }
                return listOfState;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method : dbo.PR_LOC_State_SelectComboBoxByCountryName
        public List<LOC_StateDropDownModel> dbo_PR_LOC_State_SelectComboBoxByCountryName(int? CountryID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_LOC_State_SelectComboBoxByCountryName");
                sqlDatabase.AddInParameter(dbCommand, "@CountryID", DbType.Int32, CountryID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                List<LOC_StateDropDownModel> listOfState = new List<LOC_StateDropDownModel>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    LOC_StateDropDownModel lOC_StateDropDownModel = new LOC_StateDropDownModel();
                    lOC_StateDropDownModel.StateID = Convert.ToInt32(dataRow["StateID"]);
                    lOC_StateDropDownModel.StateName = dataRow["StateName"].ToString();
                    listOfState.Add(lOC_StateDropDownModel);
                }
                return listOfState;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method : dbo.PR_LOC_CityFilter
        public DataTable dbo_PR_LOC_CityFilter(LOC_CityFilterModel lOC_CityFilterModel)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_CityFilter");
                sqlDatabase.AddInParameter(dbCommand, "@CountryID", DbType.Int32, lOC_CityFilterModel.CountryID);
                sqlDatabase.AddInParameter(dbCommand, "@StateID", DbType.Int32, lOC_CityFilterModel.StateID);
                sqlDatabase.AddInParameter(dbCommand, "@CityName", DbType.String, lOC_CityFilterModel.CityName);
                sqlDatabase.AddInParameter(dbCommand, "@CityCode", DbType.String, lOC_CityFilterModel.CityCode);
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

    }
}
