using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Management;
using System.Web.Mvc;
using CompanyDatabaseProcessing.Models;

namespace CompanyDatabaseProcessing.Controllers
{
    public class HomeController : Controller
    {
        public string ConnString
        {
            get { return ConfigurationManager.ConnectionStrings["ConnectonString"].ConnectionString; }
        }

        // GET: Home
        public ActionResult Index()
        {
            List<PersonView> dataContext;
            try
            {
                dataContext = SqlQuery.ViewData("GetAllValues", null, ConnString);
            }
            catch (SqlExecutionException e)
            {
                return View("OperationFailed", e);
            }       
            return View(dataContext);
        }

        [HttpGet]
        public ActionResult AddItemForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddItemForm(PersonView added)
        {
            SqlQuery.ChangeData("AddValue",added, ConnString);
            return View("OperationSuccessful", null);
        }

        [HttpGet]
        public ActionResult DeleteItemForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteItemForm(PersonView deleted)
        {
            try
            {
                SqlQuery.ChangeData("DeleteValue", deleted, ConnString);               
            }
            catch (SqlExecutionException e)
            {
                return View("OperationFailed", e);
            }
            return View("OperationSuccessful", null);            
        }

        [HttpGet]
        public ActionResult FindItemForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FindItemForm(PersonView find)
        {
            var findedResult = new List<PersonView>();
            try
            {
                findedResult = SqlQuery.ViewData("FindValue", find, ConnString);
            }
            catch (SqlExecutionException e)
            {
                return View("OperationFailed", e);
            }
            return View("OperationSuccessful", findedResult);
        }
    }
}