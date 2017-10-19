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
        //readonly TableContext dataContext = new TableContext();

        // GET: Home
        public ActionResult Index()
        {
            var dataContext = SqlQuery.CreateQuery(ConfigurationManager.ConnectionStrings["ConnectonString"].ConnectionString);
            return View(dataContext);
        }
    }
}