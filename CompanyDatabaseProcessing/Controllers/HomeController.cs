using System.Collections.Generic;
using System.Configuration;
using System.Web.Management;
using System.Web.Mvc;
using CompanyDatabaseProcessing.Models;

namespace CompanyDatabaseProcessing.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Строка соединения с базой данных, значение которой указано в Web.config
        /// </summary>
        public string ConnString
        {
            get { return ConfigurationManager.ConnectionStrings["ConnectonString"].ConnectionString; }
        }

        // GET: Home
        /// <summary>
        /// Главная страница веб-приложения
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Форма запроса на добавления элемента в БД (Запросы типа Get и Post)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddItemForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddItemForm(PersonView added)
        {
            if (ModelState.IsValid)
            {
                SqlQuery.ChangeData("AddValue", added, ConnString);
                return View("OperationSuccessful", null);
            }
            else
            {
                return View();
            }         
        }

        /// <summary>
        /// Форма запроса на удаление элемента из БД (Запросы типа Get и Post)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteItemForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteItemForm(PersonView deleted)
        {
            if (ModelState.IsValid)
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
            else
            {
                return View();
            }    
        }


        /// <summary>
        /// Форма запроса на нахождение фиксированного элемента в БД (Запросы типа Get и Post)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FindItemForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FindItemForm(PersonView find)
        {
            if (ModelState.IsValid)
            {
                List<PersonView> findedResult;
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
            else
            {
                return View();
            }    
        }

        /// <summary>
        /// Форма запроса на замену фиксированного элемента в БД (Запросы типа Get и Post)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ChangeItemForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangeItemForm(ChangedData data)
        {
            if (ModelState.IsValid)
            {
                var items = data.CreateListOfPersonView();
                try
                {
                    SqlQuery.ChangeData("ReplaceValue", items[0], items[1], ConnString);
                }
                catch (SqlExecutionException e)
                {
                    return View("OperationFailed", e);
                }
                return View("OperationSuccessful");
            }
            else
            {
                return View();
            }
        }
    }
}