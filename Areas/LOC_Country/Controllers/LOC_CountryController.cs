using AdminPanel.Areas.LOC_Country.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AdminPanel.Areas.LOC_Country.Controllers
{

    [Area("LOC_Country")]
    [Route("LOC_Country/[controller]/[action]")]
    public class LOC_CountryController : Controller
    {

        private IConfiguration Configuration;

        public LOC_CountryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #region SelectAll
        public IActionResult LOC_CountryList()
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Country_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            connection.Close();
            return View(table);
        }
        #endregion

        #region Save
        public IActionResult LOC_CountrySave(LOC_CountryModel modelLOC_Country)
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            //if (modelLOC_Country.CountryID == 0)
            //{
                
            //}
            command.CommandText = "PR_Country_Insert";
            //else
            //{
            //    command.CommandText = "PR_Country_UpdateByPK";
            //    command.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelLOC_Country.CountryID;
            //}
            command.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = modelLOC_Country.CountryName;
            command.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = modelLOC_Country.CountryCode;
            command.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DBNull.Value;
            command.Parameters.Add("@Created", SqlDbType.DateTime).Value = DBNull.Value;
            command.ExecuteNonQuery();
            connection.Close(); 
            return View("LOC_CountryList");
        }
        #endregion

        #region Delete
        public IActionResult LOC_CountryDelete(int CountryID)
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Country_DeleteByPK";
            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("LOC_CountryList");
        }
        #endregion

        #region Add
        public IActionResult LOC_CountryAdd(int? CountryID)
        {
            if(CountryID!=null)
            {
                string connectionString = this.Configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Country_SelectByPK";
                command.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                connection.Close();
                table.Load(reader);

            }
            return View("LOC_CountryAddEdit");
        }
        #endregion
    }
}
