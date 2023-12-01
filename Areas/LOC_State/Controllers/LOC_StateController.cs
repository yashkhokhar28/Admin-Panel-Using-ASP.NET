using Microsoft.AspNetCore.Mvc;
using System.Data;
using AdminPanel.Areas.LOC_State.Models;
using AdminPanel.DAL.LOC_StateDAL;

namespace AdminPanel.Areas.LOC_State.Controllers
{
    [Area("LOC_State")]
    [Route("LOC_State/[controller]/[action]")]
    public class LOC_StateController : Controller
    {
        #region State List
        LOC_StateDAL lOC_StateDAL = new LOC_StateDAL();
        public IActionResult LOC_StateList()
        {
            #region  Country ComboBox
            ViewBag.CountryList = lOC_StateDAL.dbo_PR_LOC_Country_Combobox();
            #endregion

            DataTable dataTable = lOC_StateDAL.dbo_PR_LOC_State_SelectAll();
            return View(dataTable);
        }
        #endregion

        #region State Add
        public IActionResult LOC_StateAdd(int StateID = 0)
        {
            LOC_StateModel lOC_StateModel = lOC_StateDAL.dbo_PR_LOC_State_SelectByPK(StateID);
            if (lOC_StateModel != null)
            {

                ViewBag.CountryList = lOC_StateDAL.dbo_PR_LOC_Country_Combobox();
                return View("LOC_StateAddEdit", lOC_StateModel);

            }
            else
            {
                ViewBag.CountryList = lOC_StateDAL.dbo_PR_LOC_Country_Combobox();
                return View("LOC_StateAddEdit");
            }
        }
        #endregion

        #region Satet Insert & State Update 
        public IActionResult LOC_StateSave(LOC_StateModel lOC_StateModel, int StateID = 0)
        {
            if (ModelState.IsValid)
            {
                if (lOC_StateDAL.dbo_PR_LOC_State_Save(lOC_StateModel))
                {
                    if (lOC_StateModel.StateID == 0)
                    {
                        return RedirectToAction("LOC_StateList");
                    }
                    else
                        return RedirectToAction("LOC_StateList");
                }
            }
            return View("LOC_StateAddEdit");
        }
        #endregion

        #region State Delete
        public IActionResult LOC_StateDelete(int StateID)
        {
            lOC_StateDAL.dbo_PR_LOC_State_Delete(StateID);
            return RedirectToAction("LOC_StateList");
        }
        #endregion

        #region State Filter
        public IActionResult LOC_StateFilter(LOC_StateFilterModel lOC_StateFilterModel)
        {
            DataTable dataTable = lOC_StateDAL.dbo_PR_LOC_StateFilter(lOC_StateFilterModel);
            ViewBag.CountryList = lOC_StateDAL.dbo_PR_LOC_Country_Combobox();
            ModelState.Clear();
            return View("LOC_StateList", dataTable);
        }
        #endregion
    }
}