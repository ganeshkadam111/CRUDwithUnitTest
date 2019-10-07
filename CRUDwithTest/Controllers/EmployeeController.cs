using CRUDwithTest.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace CRUDwithTest.Controllers
{
    public class EmployeeController : ApiController
    {
        IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository _employeeRepository)
        {
            employeeRepository = _employeeRepository;
        }
        //GET all employees

        //api/employee

        public IEnumerable<Employee> GetAllEmployees()
        {
            return employeeRepository.GetAllEmployees();
        }

        public Employee GetEmployee(int id)
        {
            return employeeRepository.GetEmployee(id);
        }

        //POST (insert) an employee

        //api/employee/
        public Employee AddEmployee(Employee employee)
        {
            return employeeRepository.Add(employee);
        }

        //PUT (update) an employee

        //api/employee/{id}
        public Employee UpdateEmployee(int id, Employee employee)
        {
            employee.id = id;
            return employeeRepository.Update(employee);

        }

        //DELETE an employee

        //api/employee/{id}
        public IEnumerable<Employee> DeleteEmployee(int id)
        {
            return employeeRepository.Delete(id);

        }
    }
}
