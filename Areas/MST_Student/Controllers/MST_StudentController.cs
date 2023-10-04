using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using AdminPanel.Areas.MST_Student.Models;
using AdminPanel.Areas.MST_Branch.Models;

namespace AdminPanel.Areas.MST_Student.Controllers
{
    [Area("MST_Student")]
    [Route("MST_Student/[controller]/[action]")]
    public class MST_StudentController : Controller
    {
        #region Configration
        private IConfiguration Configuration;

        public MST_StudentController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        #region StudentList
        public IActionResult MST_StudentList()
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");

            #region Branch DropDown
            SqlConnection connection1 = new SqlConnection(connectionString);
            connection1.Open();
            SqlCommand command1 = connection1.CreateCommand();
            command1.CommandType = CommandType.StoredProcedure;
            command1.CommandText = "PR_Branch_ComboBox";
            SqlDataReader reader1 = command1.ExecuteReader();
            DataTable table1 = new DataTable();
            table1.Load(reader1);
            connection1.Close();

            List<MST_BranchDropDownModel> list = new List<MST_BranchDropDownModel>();

            foreach (DataRow dr in table1.Rows)
            {
                MST_BranchDropDownModel mST_BranchDropDownModel = new MST_BranchDropDownModel();
                mST_BranchDropDownModel.BranchID = Convert.ToInt32(dr["BranchID"]);
                mST_BranchDropDownModel.BranchName = dr["BranchName"].ToString();
                list.Add(mST_BranchDropDownModel);
            }
            ViewBag.BranchList = list;
            #endregion

            #region City DropDown
            SqlConnection connection2 = new SqlConnection(connectionString);
            connection2.Open();
            SqlCommand command2 = connection2.CreateCommand();
            command2.CommandType = CommandType.StoredProcedure;
            command2.CommandText = "PR_City_ComboBox";
            SqlDataReader reader2 = command2.ExecuteReader();
            DataTable table2 = new DataTable();
            table2.Load(reader2);
            connection2.Close();

            List<LOC_CityDropDownModel> list2 = new List<LOC_CityDropDownModel>();

            foreach (DataRow dr in table2.Rows)
            {
                LOC_CityDropDownModel lOC_CityDropDownModel = new LOC_CityDropDownModel();
                lOC_CityDropDownModel.CityID = Convert.ToInt32(dr["CityID"]);
                lOC_CityDropDownModel.CityName = dr["CityName"].ToString();
                list2.Add(lOC_CityDropDownModel);
            }
            ViewBag.CityList = list2;
            #endregion


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
        public IActionResult MST_StudentSave(MST_StudentModel mST_StudentModel, int StudentID = 0)
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            if (StudentID == 0)
            {
                command.CommandText = "PR_Student_Insert";
                command.Parameters.AddWithValue("@Created", DateTime.Now);
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
            command.Parameters.AddWithValue("@Modified", DateTime.Now);
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

            #region City ComboBox
            SqlConnection connection1 = new SqlConnection(connectionString);
            connection1.Open();
            SqlCommand command1 = connection1.CreateCommand();
            command1.CommandType = CommandType.StoredProcedure;
            command1.CommandText = "PR_City_ComboBox";
            SqlDataReader reader1 = command1.ExecuteReader();
            DataTable table1 = new DataTable();
            table1.Load(reader1);
            connection1.Close();

            List<LOC_CityDropDownModel> list = new List<LOC_CityDropDownModel>();
            foreach (DataRow row in table1.Rows)
            {
                LOC_CityDropDownModel lOC_CityDropDownModel = new LOC_CityDropDownModel();
                lOC_CityDropDownModel.CityID = Convert.ToInt32(row["CityID"]);
                lOC_CityDropDownModel.CityName = row["CityName"].ToString();
                list.Add(lOC_CityDropDownModel);
            }
            ViewBag.CityList = list;
            #endregion

            #region Branch ComboBox
            SqlConnection connection2 = new SqlConnection(connectionString);
            connection2.Open();
            SqlCommand command2 = connection2.CreateCommand();
            command2.CommandType = CommandType.StoredProcedure;
            command2.CommandText = "PR_Branch_ComboBox";
            SqlDataReader reader2 = command2.ExecuteReader();
            DataTable table2 = new DataTable();
            table2.Load(reader2);
            connection2.Close();

            List<MST_BranchDropDownModel> list2 = new List<MST_BranchDropDownModel>();
            foreach (DataRow row in table2.Rows)
            {
                MST_BranchDropDownModel mST_BranchDropDownModel = new MST_BranchDropDownModel();
                mST_BranchDropDownModel.BranchID = Convert.ToInt32(row["BranchID"]);
                mST_BranchDropDownModel.BranchName = row["BranchName"].ToString();
                list2.Add(mST_BranchDropDownModel);
            }
            ViewBag.BranchList = list2;
            Console.WriteLine(list2);
            #endregion
            if (StudentID != 0)
            {
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
            return View("MST_StudentAddEdit");
        }
        #endregion

        #region FILTER
        public IActionResult MST_StudentFilter(MST_StudentFilterModel mST_StudentFilterModel)
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");

            #region Branch DropDown
            SqlConnection connection1 = new SqlConnection(connectionString);
            connection1.Open();
            SqlCommand command1 = connection1.CreateCommand();
            command1.CommandType = CommandType.StoredProcedure;
            command1.CommandText = "PR_Branch_ComboBox";
            SqlDataReader reader1 = command1.ExecuteReader();
            DataTable table1 = new DataTable();
            table1.Load(reader1);
            connection1.Close();

            List<MST_BranchDropDownModel> list = new List<MST_BranchDropDownModel>();

            foreach (DataRow dr in table1.Rows)
            {
                MST_BranchDropDownModel mST_BranchDropDownModel = new MST_BranchDropDownModel();
                mST_BranchDropDownModel.BranchID = Convert.ToInt32(dr["BranchID"]);
                mST_BranchDropDownModel.BranchName = dr["BranchName"].ToString();
                list.Add(mST_BranchDropDownModel);
            }
            ViewBag.BranchList = list;
            #endregion

            #region City DropDown
            SqlConnection connection2 = new SqlConnection(connectionString);
            connection2.Open();
            SqlCommand command2 = connection2.CreateCommand();
            command2.CommandType = CommandType.StoredProcedure;
            command2.CommandText = "PR_City_ComboBox";
            SqlDataReader reader2 = command2.ExecuteReader();
            DataTable table2 = new DataTable();
            table2.Load(reader2);
            connection2.Close();

            List<LOC_CityDropDownModel> list2 = new List<LOC_CityDropDownModel>();

            foreach (DataRow dr in table2.Rows)
            {
                LOC_CityDropDownModel lOC_CityDropDownModel = new LOC_CityDropDownModel();
                lOC_CityDropDownModel.CityID = Convert.ToInt32(dr["CityID"]);
                lOC_CityDropDownModel.CityName = dr["CityName"].ToString();
                list2.Add(lOC_CityDropDownModel);
            }
            ViewBag.CityList = list2;
            #endregion

            DataTable table = new DataTable();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_StudentFilter";
            command.Parameters.AddWithValue("@StudentName", mST_StudentFilterModel.StudentName);
            command.Parameters.AddWithValue("@CityID", mST_StudentFilterModel.CityID);
            command.Parameters.AddWithValue("@BranchID", mST_StudentFilterModel.BranchID);
            SqlDataReader reader = command.ExecuteReader();
            table.Load(reader);
            ModelState.Clear();
            return View("MST_StudentList", table);
        }
        #endregion
    }
}
