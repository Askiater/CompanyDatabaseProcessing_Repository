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
            var dataContext = SqlQuery.ViewData("GetAllValues", ConnString);
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
            return View("OperationSuccessful");
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
            return View("OperationSuccessful");            
        }
    }
}