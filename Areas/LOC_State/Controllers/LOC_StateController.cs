using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using AdminPanel.Areas.LOC_State.Models;

namespace AdminPanel.Areas.LOC_State.Controllers
{
    [Area("LOC_State")]
    [Route("LOC_State/[controller]/[action]")]
    public class LOC_StateController : Controller
    {
        private IConfiguration Configuration;

        public LOC_StateController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #region SelectAll
        public IActionResult LOC_StateList()
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_State_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            connection.Close();
            return View(table);
        }
        #endregion
        #region Delete
        public IActionResult LOC_StateDelete(int StateID)
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_State_DeleteByPK";
            command.Parameters.AddWithValue("@StateID", StateID);
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("LOC_StateList");
        }
        #endregion
        #region SelectByID
        public IActionResult LOC_StateListByID(int StateID)
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_State_SelectByPK";
            command.Parameters.AddWithValue("StateID", StateID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            connection.Close();
            return View(table);
        }
        #endregion

        #region Save
        public IActionResult LOC_StateSave(LOC_StateModel lOC_StateModel, int StateID = 0)
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            if (StateID == 0)
            {
                command.CommandText = "PR_State_Insert";
            }
            else
            {
                command.CommandText = "PR_State_UpdateByPK";
                command.Parameters.AddWithValue("@CountryID", StateID);
            }
            command.Parameters.AddWithValue("@StateName", lOC_StateModel.StateName);
            command.Parameters.AddWithValue("@StateCode", lOC_StateModel.StateCode);
            command.Parameters.AddWithValue("@CountryID", lOC_StateModel.CountryID);
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("LOC_StateList");
        }
        #endregion

        #region Add
        public IActionResult LOC_StateAdd(int StateID = 0)
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_State_SelectByPK";
            command.Parameters.AddWithValue("@StateID", StateID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            LOC_StateModel lOC_StateModel = new LOC_StateModel();
            foreach (DataRow dataRow in table.Rows)
            {
                lOC_StateModel.CountryID = Convert.ToInt32(dataRow["CountryID"]);
                lOC_StateModel.StateName = dataRow["StateName"].ToString();
                lOC_StateModel.StateCode = dataRow["StateCode"].ToString();
            }
            return View("LOC_StateAddEdit", lOC_StateModel);
        }
        #endregion




    }
}
