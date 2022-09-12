using CST451.Data.DataModel.UserDataModels;
using CST451.Models.Users;
using CST451.Models.ViewModels.Products;

namespace CST451.Helpers
{
    public class EmployeeHelper
    {
        /// <summary>
        /// Allows access to Factory
        /// </summary>
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
        /// Return a single Employee from employee ID
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        internal EmployeeViewModel GetOne(EmployeeViewModel vmEmployee)
        {
            EmployeeDataModel dbEmployee = ParseVMEmployeeToDMEmployee(vmEmployee);
            dbEmployee = oFactory.EmployeeBizLogic.GetOne(dbEmployee);
            return ParseDMEmployeeToVMEmployee(dbEmployee);
        }

        /// <summary>
        /// Gets all employees from DB
        /// </summary>
        /// <returns></returns>
        internal List<EmployeeViewModel> GetAll()
        {
            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();

            employees.AddRange(oFactory.EmployeeBizLogic.GetAll().Select(x => ParseDMEmployeeToVMEmployee(x)));

            return employees;
        }


        /// <summary>
        /// Reaches out to BizLogic layer to add customer to database
        /// </summary>
        /// <param name="employee">CustomerViewModel</param>
        /// <returns>CustomerViewModel</returns>
        public EmployeeViewModel AddEmployee(EmployeeViewModel employee, bool isAdmin)
        {
            EmployeeDataModel dmEmployee= ParseVMEmployeeToDMEmployee(employee);
            dmEmployee = oFactory.EmployeeBizLogic.Add(dmEmployee, isAdmin);
            employee = ParseDMEmployeeToVMEmployee(dmEmployee);
            return employee;
        }

        /// <summary>
        /// Update employee record in DB
        /// </summary>
        /// <param name="vmEmployee"></param>
        /// <param name="isAdmin"></param>
        /// <returns></returns>
        internal EmployeeViewModel UpdateEmployee(EmployeeViewModel vmEmployee, bool isAdmin)
        {
            EmployeeDataModel dbEmployee = ParseVMEmployeeToDMEmployee(vmEmployee);
            dbEmployee = oFactory.EmployeeBizLogic.Update(dbEmployee, isAdmin);
            return ParseDMEmployeeToVMEmployee(dbEmployee);
        }

        /// <summary>
        /// Reaches out to BizLogic layer to authenticate customer
        /// </summary>
        /// <param name="vmEmployee">CustomerViewModel</param>
        /// <returns>CustomerViewModel</returns>
        public EmployeeViewModel Login(EmployeeViewModel vmEmployee)
        {
            EmployeeDataModel dmEmployee = ParseVMEmployeeToDMEmployee(vmEmployee);
            dmEmployee = oFactory.EmployeeBizLogic.Login(dmEmployee);
            return ParseDMEmployeeToVMEmployee(dmEmployee);
        }

        #region "Parsing

        /// <summary>
        /// Parse from View to Data layers
        /// </summary>
        /// <param name="vmEmployee"></param>
        /// <returns></returns>
        private EmployeeDataModel ParseVMEmployeeToDMEmployee(EmployeeViewModel vmEmployee)
        {
            EmployeeDataModel dmEmployee = new EmployeeDataModel()
            {
                Name = vmEmployee.Name,
                Address = vmEmployee.Address,
                City = vmEmployee.City,
                State = vmEmployee.State,
                Zip = vmEmployee.Zip,
                Country = vmEmployee.Country,
                Email = vmEmployee.Email,
                Phone = vmEmployee.Phone,
                Username = vmEmployee.Username,
                Password = vmEmployee.Password,
                Success = vmEmployee.Success,
                IsAdmin = vmEmployee.IsAdmin,
            };
            if (vmEmployee.ID != null)
                dmEmployee.ID = vmEmployee.ID;

            return dmEmployee;
        }

        /// <summary>
        /// Parse from Data to View layers
        /// </summary>
        /// <param name="dmEmployee"></param>
        /// <returns></returns>
        private EmployeeViewModel ParseDMEmployeeToVMEmployee(EmployeeDataModel dmEmployee)
        {
            EmployeeViewModel vmEmployee = new EmployeeViewModel()
            {
                Name = dmEmployee.Name,
                Address = dmEmployee.Address,
                City = dmEmployee.City,
                State = dmEmployee.State,
                Zip = dmEmployee.Zip,
                Country = dmEmployee.Country,
                Email = dmEmployee.Email,
                Phone = dmEmployee.Phone,
                Password = dmEmployee.Password,
                Username = dmEmployee.Username,
                IsAdmin = dmEmployee.IsAdmin,
                Success = dmEmployee.Success
            };
            if (dmEmployee.ID != null)
                vmEmployee.ID = dmEmployee.ID;

            return vmEmployee;
        }

        #endregion
    }
}
