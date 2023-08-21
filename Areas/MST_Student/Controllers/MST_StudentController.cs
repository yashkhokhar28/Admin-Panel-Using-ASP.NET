using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using AdminPanel.Areas.MST_Student.Models;

namespace AdminPanel.Areas.MST_Student.Controllers
{
    [Area("MST_Student")]
    [Route("MST_Student/[controller]/[action]")]
    public class MST_StudentController : Controller
    {
        private IConfiguration Configuration;

        public MST_StudentController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #region SelectAll
        public IActionResult MST_StudentList()
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Student_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            connection.Close();
            return View(table);
        }
        #endregion

        #region Save
        public IActionResult MST_StudentSave(MST_StudentModel mST_StudentModel)
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            if (mST_StudentModel.StudentID == 0)
            {
                command.CommandText = "PR_Student_Insert";
            }
            else
            {
                command.CommandText = "PR_Student_UpdateByPK";
                command.Parameters.AddWithValue("@StudentID", mST_StudentModel.StudentID);
            }
            command.Parameters.AddWithValue("@BranchID", mST_StudentModel.BranchID);
            command.Parameters.AddWithValue("@CityID", mST_StudentModel.CityID);
            command.Parameters.AddWithValue("@StudentName", mST_StudentModel.StudentName);
            command.Parameters.AddWithValue("@MobileNoStudent", mST_StudentModel.MobileNoStudent);
            command.Parameters.AddWithValue("@Email", mST_StudentModel.Email);
            command.Parameters.AddWithValue("@MobileNoFather", mST_StudentModel.MobileNoFather);
            command.Parameters.AddWithValue("@Address", mST_StudentModel.Address);
            command.Parameters.AddWithValue("@BirthDate", mST_StudentModel.BirthDate);
            command.Parameters.AddWithValue("@Age", mST_StudentModel.Age);
            command.Parameters.AddWithValue("@IsActive", mST_StudentModel.IsActive);
            command.Parameters.AddWithValue("@Gender", mST_StudentModel.Gender);
            command.Parameters.AddWithValue("@Password", mST_StudentModel.Password);
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("MST_StudentList");
        }
        #endregion

        #region Delete
        public IActionResult MST_StudentDelete(int StudentID)
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Student_DeleteByPK";
            command.Parameters.AddWithValue("@StudentID", StudentID);
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("MST_StudentList");
        }
        #endregion

        #region Add
        public IActionResult MST_StudentAdd(int StudentID = 0)
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Student_SelectByPK";
            command.Parameters.AddWithValue("@StudentID", StudentID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            MST_StudentModel mST_StudentModel = new MST_StudentModel();
            foreach (DataRow dataRow in table.Rows)
            {
                mST_StudentModel.StudentID = Convert.ToInt32(dataRow["StudentID"]);
                mST_StudentModel.StudentName = dataRow["StudentName"].ToString();
                mST_StudentModel.MobileNoStudent = dataRow["MobileNoStudent"].ToString();
                mST_StudentModel.Email = dataRow["Email"].ToString();
                mST_StudentModel.MobileNoFather = dataRow["MobileNoFather"].ToString();
                mST_StudentModel.Address = dataRow["Address"].ToString();
                mST_StudentModel.BirthDate = Convert.ToDateTime(dataRow["BirthDate"]);
                mST_StudentModel.Age = dataRow["Age"].ToString();
                mST_StudentModel.IsActive = dataRow["IsActive"].ToString();
                mST_StudentModel.Gender = dataRow["Gender"].ToString();
                mST_StudentModel.Password = dataRow["Password"].ToString();
                mST_StudentModel.BranchID = Convert.ToInt32(dataRow["BranchID"]);
                mST_StudentModel.CityID = Convert.ToInt32(dataRow["CityID"]);

            }
            return View("MST_StudentAddEdit", mST_StudentModel);
        }
        #endregion
    }
}
