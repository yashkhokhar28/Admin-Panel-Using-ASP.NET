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

        [HttpPost]
        public IActionResult AdminLogin(SEC_UserModel modelSEC_User)
        {
            string error = null;

            if (modelSEC_User.UserName == null)
            {
                error += "User Name is required";
            }
            if (modelSEC_User.Password == null)
            {
                error += "<br/>Password is required";
            }

            if (error != null)
            {
                TempData["Error"] = error;
                return RedirectToAction("SEC_AdminDashboard");
            }
            else
            {
                SEC_UserDAL sEC_UserDAL = new SEC_UserDAL();
                DataTable dt = sEC_UserDAL.dbo_PR_SEC_User_SelectByUserNamePassword(modelSEC_User.UserName, modelSEC_User.Password);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Console.WriteLine(dr);
                        HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                        HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                        HttpContext.Session.SetString("Password", dr["Password"].ToString());
                        HttpContext.Session.SetString("FirstName", dr["FirstName"].ToString());
                        HttpContext.Session.SetString("LastName", dr["LastName"].ToString());
                        HttpContext.Session.SetString("PhotoPath", dr["PhotoPath"].ToString());
                        break;
                    }
                }
                else
                {
                    TempData["Error"] = "User Name or Password is invalid!";
                    return RedirectToAction("SEC_AdminDashboard");
                }
                if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Password") != null && HttpContext.Session.GetString("UserName") == "Admin" && HttpContext.Session.GetString("Password") == "Admin")
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("SEC_AdminDashboard");
        }
    }
}
