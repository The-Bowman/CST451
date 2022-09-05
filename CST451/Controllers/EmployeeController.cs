using CST451.Models.Users;
using CST451.Models.ViewModels;
using CST451.Models.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CST451.Controllers
{
    public class EmployeeController : Controller
    {

        // allow access to factory
        private Factory _factory;
        internal Factory oFactory
        {
            get
            {
                if (_factory == null)
                    _factory = new Factory();
                return _factory;
            }
        }

        /// <summary>
        /// View to login customer
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EmployeePortal()
        {
            return View();
        }

        /// <summary>
        /// Executes login authentication and adds to session
        /// </summary>
        /// <param name="employee">CustomerViewModel</param>
        /// <returns>CustomerViewModel</returns>
        [HttpPost]
        public IActionResult Login(EmployeeViewModel employee)
        {
            employee = oFactory.EmployeeHelper.Login(employee);
            HttpContext.Session.SetString("username", employee.Name);
            HttpContext.Session.SetString("userID", employee.ID.ToString());
            HttpContext.Session.SetString("isAdmin", employee.IsAdmin.ToString());
            ViewBag.emplid = employee.ID;
            ViewBag.isEmployee = true;
            ViewBag.isAdmin = HttpContext.Session.GetString("isAdmin").ToString();
            return View("LoginResult", employee);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("userID");
            HttpContext.Session.Remove("isAdmin");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// View to add and Employee
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }

        /// <summary>
        /// Reaches out to Helper to add employee to the db
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddEmployee(EmployeeViewModel employee)
        {
            bool isAdmin = Convert.ToBoolean(HttpContext.Session.GetString("isAdmin"));
            employee = oFactory.EmployeeHelper.AddEmployee(employee, isAdmin);
            return View("DisplayEmployee", employee);
        }

        [HttpGet]
        public IActionResult FindEmployeeByEmplID()
        {
            return View();  
        }

        /// <summary>
        /// View to add a product
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditEmployee(EmployeeViewModel employee)
        {
            employee = oFactory.EmployeeHelper.GetOne(employee);
            employee.Success = null;
            return View(employee);
        }

        /// <summary>
        /// Reaches out to helper to add a product to db
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ProcessEmployeeEdit(EmployeeViewModel employee)
        {
            employee = oFactory.EmployeeHelper.UpdateEmployee(employee);
            return View("EditEmployee", employee);
        }
    }
}
