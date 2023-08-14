using AdminPanel.Areas.LOC_City.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace AdminPanel.Areas.LOC_City.Controllers
{
    [Area("LOC_City")]
    [Route("LOC_City/[controller]/[action]")]
    public class LOC_CityController : Controller
    {
        private IConfiguration Configuration;

        public LOC_CityController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #region SelectAll
        public IActionResult LOC_CityList()
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_City_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            connection.Close();
            return View(table);
        }
        #endregion

        #region SelectByID
        public IActionResult LOC_CityListByID(int CityID)
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_City_SelectByPK";
            command.Parameters.AddWithValue("CityID", CityID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            connection.Close();
            return View(table);
        }
        #endregion

        #region Save
        public IActionResult LOC_CitySave(LOC_CityModel lOC_CityModel, int CityID = 0)
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            if (CityID == 0)
            {
                command.CommandText = "PR_City_Insert";
            }
            else
            {
                command.CommandText = "PR_City_UpdateByPK";
                command.Parameters.AddWithValue("@CityID", CityID);
            }
            command.Parameters.AddWithValue("@CityName", lOC_CityModel.CityName);
            command.Parameters.AddWithValue("@CityCode", lOC_CityModel.CityCode);
            command.Parameters.AddWithValue("@StateID", lOC_CityModel.StateID);
            command.Parameters.AddWithValue("@CountryID", lOC_CityModel.CountryID);
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("LOC_CityList");
        }
        #endregion

        #region Delete
        public IActionResult LOC_CityDelete(int CityID)
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_City_DeleteByPK";
            command.Parameters.AddWithValue("@CityID", CityID);
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("LOC_CityList");
        }
        #endregion

        #region Add
        public IActionResult LOC_CityAdd(int CityID = 0)
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_City_SelectByPK";
            command.Parameters.AddWithValue("@CityID", CityID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            LOC_CityModel lOC_CityModel = new LOC_CityModel();
            foreach (DataRow dataRow in table.Rows)
            {
                lOC_CityModel.CityName = dataRow["CityName"].ToString();
                lOC_CityModel.CityCode = dataRow["CityCode"].ToString();
                lOC_CityModel.StateID = Convert.ToInt32(dataRow["StateID"]);
                lOC_CityModel.CountryID = Convert.ToInt32(dataRow["CountryID"]);
            }
            return View("LOC_CityAddEdit", lOC_CityModel);
        }
        #endregion
    }
}
