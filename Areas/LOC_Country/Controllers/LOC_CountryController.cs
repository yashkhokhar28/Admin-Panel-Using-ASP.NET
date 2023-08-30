﻿using AdminPanel.Areas.LOC_Country.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AdminPanel.Areas.LOC_Country.Controllers
{

    [Area("LOC_Country")]
    [Route("LOC_Country/[controller]/[action]")]
    public class LOC_CountryController : Controller
    {
        #region Configration
        private IConfiguration Configuration;

        public LOC_CountryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        #region Country List
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

        #region Add
        public IActionResult LOC_CountryAdd(int CountryID = 0)
        {
            if (CountryID != 0)
            {
                string connectionString = this.Configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Country_SelectByPK";
                command.Parameters.AddWithValue("@CountryID", CountryID);
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                LOC_CountryModel lOC_CountryModel = new LOC_CountryModel();
                foreach (DataRow dataRow in table.Rows)
                {
                    lOC_CountryModel.CountryID = Convert.ToInt32(dataRow["CountryID"]);
                    lOC_CountryModel.CountryName = dataRow["CountryName"].ToString();
                    lOC_CountryModel.CountryCode = dataRow["CountryCode"].ToString();
                }
                return View("LOC_CountryAddEdit", lOC_CountryModel);
            }
            return View("LOC_CountryAddEdit");
        }
        #endregion

        #region Insert
        [HttpPost]
        public IActionResult LOC_CountrySave(LOC_CountryModel lOC_CountryModel, int CountryID = 0)
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            if (CountryID == 0)
            {
                command.CommandText = "PR_Country_Insert";
                command.Parameters.AddWithValue("@Created", DateTime.Now);
            }
            else
            {
                command.CommandText = "PR_Country_UpdateByPK";
                command.Parameters.AddWithValue("@CountryID", CountryID);
            }
            command.Parameters.AddWithValue("@CountryName", lOC_CountryModel.CountryName);
            command.Parameters.AddWithValue("@CountryCode", lOC_CountryModel.CountryCode);
            command.Parameters.AddWithValue("@Modified", DateTime.Now);
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("LOC_CountryList");
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

        #region Filter
        public IActionResult LOC_CountryFilter(string CountryData = "")
        {
            string connectionString = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_CountryFilter";
            if (CountryData != "")
                command.Parameters.AddWithValue("@CountryData", CountryData);
            SqlDataReader reader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            ModelState.Clear();
            return View("LOC_CountryList", dataTable);
        }
        #endregion

    }
}
