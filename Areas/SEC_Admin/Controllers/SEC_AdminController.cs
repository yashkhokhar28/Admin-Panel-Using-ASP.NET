using AdminPanel.Areas.SEC_User.Models;
using AdminPanel.DAL.SEC_User;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AdminPanel.Areas.SEC_Admin.Controllers
{
    [Area("SEC_Admin")]
    [Route("SEC_Admin/[controller]/[action]")]
    public class SEC_AdminController : Controller
    {
        public IActionResult SEC_AdminDashboard()
        {
            return View();
        }
    }
}
