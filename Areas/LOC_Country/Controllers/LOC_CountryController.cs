using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AdminPanel.Areas.LOC_Country.Controllers
{
    
    [Area("LOC_Country")]
    [Route("LOC_Country/[controller]/[action]")]
    public class LOC_CountryController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private IConfiguration Configuration;

        public LOC_CountryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
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
            return View(table);
        }

        public IActionResult LOC_CountryAddEdit()
        {
            
            return View();
        }
    }
}
