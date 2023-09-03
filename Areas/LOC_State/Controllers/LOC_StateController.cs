﻿using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using AdminPanel.Areas.LOC_State.Models;
using AdminPanel.Areas.LOC_Country.Models;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace AdminPanel.Areas.LOC_State.Controllers
{
    [Area("LOC_State")]
    [Route("LOC_State/[controller]/[action]")]
    public class LOC_StateController : Controller
    {
        #region Configration
        private IConfiguration Configuration;

        public LOC_StateController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        #region State List
        public IActionResult LOC_StateList()
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            #region  Country ComboBox
            SqlConnection connection1 = new SqlConnection(connectionString);
            connection1.Open();
            SqlCommand command1 = connection1.CreateCommand();
            command1.CommandType = CommandType.StoredProcedure;
            command1.CommandText = "PR_Country_ComboBox";
            SqlDataReader reader1 = command1.ExecuteReader();
            DataTable table1 = new DataTable();
            table1.Load(reader1);
            connection1.Close();

            List<LOC_CountryDropDownModel> list = new List<LOC_CountryDropDownModel>();
            foreach (DataRow row in table1.Rows)
            {
                LOC_CountryDropDownModel lOC_CountryDropDownModel = new LOC_CountryDropDownModel();
                lOC_CountryDropDownModel.CountryID = Convert.ToInt32(row["CountryID"]);
                lOC_CountryDropDownModel.CountryName = row["CountryName"].ToString();
                list.Add(lOC_CountryDropDownModel);
            }
            ViewBag.CountryList = list;
            #endregion


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

        #region Add
        public IActionResult LOC_StateAdd(int StateID = 0)
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            #region  Country ComboBox
            SqlConnection connection1 = new SqlConnection(connectionString);
            connection1.Open();
            SqlCommand command1 = connection1.CreateCommand();
            command1.CommandType = CommandType.StoredProcedure;
            command1.CommandText = "PR_Country_ComboBox";
            SqlDataReader reader1 = command1.ExecuteReader();
            DataTable table1 = new DataTable();
            table1.Load(reader1);
            connection1.Close();

            List<LOC_CountryDropDownModel> list = new List<LOC_CountryDropDownModel>();
            foreach (DataRow row in table1.Rows)
            {
                LOC_CountryDropDownModel lOC_CountryDropDownModel = new LOC_CountryDropDownModel();
                lOC_CountryDropDownModel.CountryID = Convert.ToInt32(row["CountryID"]);
                lOC_CountryDropDownModel.CountryName = row["CountryName"].ToString();
                list.Add(lOC_CountryDropDownModel);
            }
            ViewBag.CountryList = list;
            #endregion

            #region Add
            if (StateID != 0)
            {
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
                    lOC_StateModel.StateID = Convert.ToInt32(dataRow["StateID"]);
                    lOC_StateModel.StateName = dataRow["StateName"].ToString();
                    lOC_StateModel.StateCode = dataRow["StateCode"].ToString();
                    lOC_StateModel.CountryID = Convert.ToInt32(dataRow["CountryID"]);
                }
                return View("LOC_StateAddEdit", lOC_StateModel);
            }
            return View("LOC_StateAddEdit");
            #endregion
        }
        #endregion

        #region Insert
        [HttpPost]
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
                command.Parameters.AddWithValue("@Created", DateTime.Now);
            }
            else
            {
                command.CommandText = "PR_State_UpdateByPK";
                command.Parameters.AddWithValue("@StateID", StateID);
            }
            command.Parameters.AddWithValue("@StateName", lOC_StateModel.StateName);
            command.Parameters.AddWithValue("@StateCode", lOC_StateModel.StateCode);
            command.Parameters.AddWithValue("@CountryID", lOC_StateModel.CountryID);
            command.Parameters.AddWithValue("@Modified", DateTime.Now);
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("LOC_StateList");
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

        #region Filter
        public IActionResult LOC_StateFilter(LOC_StateFilterModel lOC_StateFilterModel)
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            DataTable table = new DataTable();
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_StateFilter";
            command.Parameters.AddWithValue("@CountryID", lOC_StateFilterModel.CountryID);
            command.Parameters.AddWithValue("@StateName", lOC_StateFilterModel.StateName);
            command.Parameters.AddWithValue("@StateCode", lOC_StateFilterModel.StateCode);
            SqlDataReader reader = command.ExecuteReader();
            table.Load(reader);
            ModelState.Clear();

            #region  Country ComboBox
            SqlConnection connection1 = new SqlConnection(connectionString);
            connection1.Open();
            SqlCommand command1 = connection1.CreateCommand();
            command1.CommandType = CommandType.StoredProcedure;
            command1.CommandText = "PR_Country_ComboBox";
            SqlDataReader reader1 = command1.ExecuteReader();
            DataTable table1 = new DataTable();
            table1.Load(reader1);
            connection1.Close();

            List<LOC_CountryDropDownModel> list = new List<LOC_CountryDropDownModel>();
            foreach (DataRow row in table1.Rows)
            {
                LOC_CountryDropDownModel lOC_CountryDropDownModel = new LOC_CountryDropDownModel();
                lOC_CountryDropDownModel.CountryID = Convert.ToInt32(row["CountryID"]);
                lOC_CountryDropDownModel.CountryName = row["CountryName"].ToString();
                list.Add(lOC_CountryDropDownModel);
            }
            ViewBag.CountryList = list;
            #endregion

            
            return View("LOC_StateList", table);
        }
        #endregion
    }
}
