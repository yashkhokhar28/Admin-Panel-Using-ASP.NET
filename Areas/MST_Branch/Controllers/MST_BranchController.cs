using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using AdminPanel.Areas.MST_Branch.Models;

namespace AdminPanel.Areas.MST_Branch.Controllers
{
    [Area("MST_Branch")]
    [Route("MST_Branch/[controller]/[action]")]
    public class MST_BranchController : Controller
    {
        #region Configration
        private IConfiguration Configuration;

        public MST_BranchController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        #region Branch List
        public IActionResult MST_BranchList()
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Branch_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            connection.Close();
            return View(table);
        }
        #endregion

        #region Add
        public IActionResult MST_BranchAdd(int BranchID = 0)
        {
            if (BranchID != 0)
            {
                string connectionString = this.Configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Branch_SelectByPK";
                command.Parameters.AddWithValue("@BranchID", BranchID);
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                MST_BranchModel mST_BranchModel = new MST_BranchModel();
                foreach (DataRow dataRow in table.Rows)
                {
                    mST_BranchModel.BranchID = Convert.ToInt32(dataRow["BranchID"]);
                    mST_BranchModel.BranchName = dataRow["BranchName"].ToString();
                    mST_BranchModel.BranchCode = dataRow["BranchCode"].ToString();
                }
                return View("MST_BranchAddEdit", mST_BranchModel);
            }
            return View("MST_BranchAddEdit");
        }
        #endregion

        #region Insert
        public IActionResult MST_BranchSave(MST_BranchModel mST_BranchModel, int BranchID = 0)
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            if (BranchID == 0)
            {
                command.CommandText = "PR_Branch_Insert";
                command.Parameters.AddWithValue("@Created", DateTime.Now);
            }
            else
            {
                command.CommandText = "PR_Branch_UpdateByPK";
                command.Parameters.AddWithValue("@BranchID", mST_BranchModel.BranchID);
            }
            command.Parameters.AddWithValue("@BranchName", mST_BranchModel.BranchName);
            command.Parameters.AddWithValue("@BranchCode", mST_BranchModel.BranchCode);
            command.Parameters.AddWithValue("@Modified", DateTime.Now);
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("MST_BranchList");
        }
        #endregion

        #region Delete
        public IActionResult MST_BranchDelete(int BranchID)
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Branch_DeleteByPK";
            command.Parameters.AddWithValue("@BranchID", BranchID);
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("MST_BranchList");
        }
        #endregion

        #region Filter
        public IActionResult MST_BranchFilter(MST_BranchFilterModel mST_BranchFilterModel)
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_BranchFilter";
            command.Parameters.AddWithValue("@BranchName", mST_BranchFilterModel.BranchName);
            command.Parameters.AddWithValue("@BranchCode", mST_BranchFilterModel.BranchCode);
            DataTable table = new DataTable();
            SqlDataReader reader = command.ExecuteReader();
            table.Load(reader);
            ModelState.Clear();
            return View("MST_BranchList", table);
        }

        #endregion
    }
}
