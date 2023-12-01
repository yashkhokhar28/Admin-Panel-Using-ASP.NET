using AdminPanel.Areas.LOC_Country.Models;
using AdminPanel.DAL.LOC_CountryDAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AdminPanel.Areas.LOC_Country.Controllers
{

    [Area("LOC_Country")]
    [Route("LOC_Country/[controller]/[action]")]
    public class LOC_CountryController : Controller
    {
        #region Country List
        LOC_CountryDAL lOC_CountryDAL = new LOC_CountryDAL();
        public IActionResult LOC_CountryList()
        {
            DataTable dataTable = lOC_CountryDAL.dbo_PR_LOC_Country_SelectAll();
            return View(dataTable);
        }
        #endregion

        #region Country Add
        public IActionResult LOC_CountryAdd(int CountryID = 0)
        {
            LOC_CountryModel lOC_CountryModel = lOC_CountryDAL.dbo_PR_LOC_Country_SelectByPK(CountryID);
            if (lOC_CountryModel != null)
            {
                return View("LOC_CountryAddEdit", lOC_CountryModel);
            }
            else
            {
                return View("LOC_CountryAddEdit");
            }
        }
        #endregion

        #region Country Insert & Country Update
        public IActionResult LOC_CountrySave(LOC_CountryModel lOC_CountryModel)
        {
            if (ModelState.IsValid)
            {
                if (lOC_CountryDAL.dbo_PR_LOC_Country_Save(lOC_CountryModel))
                {
                    if (lOC_CountryModel.CountryID == 0)
                    {
                        //TempData["CountryInsertMsg"] = "Record Inserted Successfully";
                        return RedirectToAction("LOC_CountryList");
                    }
                    else
                        return RedirectToAction("LOC_CountryList");
                }
            }
            return View("LOC_CountryAddEdit");
        }
        #endregion

        #region Country Delete
        public IActionResult LOC_CountryDelete(int CountryID)
        {
            lOC_CountryDAL.dbo_PR_LOC_Country_Delete(CountryID);
            return RedirectToAction("LOC_CountryList");
        }
        #endregion

        #region Country Filter
        public IActionResult LOC_CountryFilter(LOC_CountryFilterModel lOC_CountryFilterModel)
        {
            DataTable dataTable = lOC_CountryDAL.dbo_PR_LOC_Country_Filter(lOC_CountryFilterModel);
            return View("LOC_CountryList", dataTable);
        }
        #endregion

    }
}
