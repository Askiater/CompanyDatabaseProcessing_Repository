using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
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

        //readonly TableContext dataContext = new TableContext();

        // GET: Home
        public ActionResult Index()
        {
            var dataContext = SqlQuery.GetAllData(ConnString);
            return View(dataContext);
        }
    }
}