using AdminPanel.Areas.LOC_City.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using adminpanel.dal.loc_citydal;
using AdminPanel.DAL.LOC_StateDAL;

namespace AdminPanel.Areas.LOC_City.Controllers
{
    [Area("LOC_City")]
    [Route("LOC_City/[controller]/[action]")]
    public class LOC_CityController : Controller
    {
        LOC_CityDAL lOC_CityDAL = new LOC_CityDAL();
        LOC_StateDAL lOC_StateDAL = new LOC_StateDAL();

        #region City List
        public IActionResult LOC_CityList()
        {

            DataTable dataTable = lOC_CityDAL.dbo_PR_LOC_City_SelectAll();
            #region Country ComboBox
            ViewBag.CountryList = lOC_StateDAL.dbo_PR_LOC_Country_Combobox();
            #endregion

            #region StateComboBox
            ViewBag.StateList = lOC_CityDAL.dbo_PR_LOC_State_Combobox();
            #endregion

            return View(dataTable);

        }
        #endregion

        #region City Add
        public IActionResult LOC_CityAdd(int CityID = 0)
        {
            LOC_CityModel lOC_CityModel = lOC_CityDAL.dbo_PR_LOC_City_SelectByPK(CityID);
            if (lOC_CityModel != null)
            {
                ViewBag.CountryList = lOC_StateDAL.dbo_PR_LOC_Country_Combobox();
                ViewBag.StateList = lOC_CityDAL.dbo_PR_LOC_State_Combobox();
                return View("LOC_CityAddEdit", lOC_CityModel);

            }
            else
            {
                ViewBag.CountryList = lOC_StateDAL.dbo_PR_LOC_Country_Combobox();
                ViewBag.StateList = lOC_CityDAL.dbo_PR_LOC_State_Combobox();
                return View("LOC_CityAddEdit");
            }
        }
        #endregion

        #region Satet Insert & State Update 
        public IActionResult LOC_CitySave(LOC_CityModel lOC_CityModel)
        {
            if (ModelState.IsValid)
            {
                if (lOC_CityDAL.dbo_PR_LOC_City_Save(lOC_CityModel))

                    return RedirectToAction("LOC_CityList");
            }
            return View("LOC_CityAddEdit");
        }
        #endregion

        #region Delete
        public IActionResult LOC_CityDelete(int CityID)
        {
            lOC_CityDAL.dbo_PR_LOC_City_Delete(CityID);
            return RedirectToAction("LOC_CityList");
        }
        #endregion

        #region DropDownByCountry
        public IActionResult DropDownByCountry(int CountryID)
        {
            var vModel = lOC_CityDAL.dbo_PR_LOC_State_SelectComboBoxByCountryName(CountryID);
            return Json(vModel);
        }
        #endregion

        #region Filter
        public IActionResult LOC_CityFilter(LOC_CityFilterModel lOC_CityFilterModel)
        {
            DataTable dataTable = lOC_CityDAL.dbo_PR_LOC_CityFilter(lOC_CityFilterModel);
            ViewBag.CountryList = lOC_StateDAL.dbo_PR_LOC_Country_Combobox();
            ViewBag.StateList = lOC_CityDAL.dbo_PR_LOC_State_Combobox();
            ModelState.Clear();
            return View("LOC_CityList", dataTable);
        }
        #endregion
    }
}
