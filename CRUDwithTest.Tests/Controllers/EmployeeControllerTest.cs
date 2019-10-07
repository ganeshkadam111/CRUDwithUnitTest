using CRUDwithTest.Controllers;
using CRUDwithTest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace CRUDwithTest.Tests.Controllers
{
    [TestClass]
    public class EmployeeControllerTest
    {
        List<Employee> expectedEmployees;
        private Mock<IEmployeeRepository> mockEmployeeRepository;
        private EmployeeController employeeController;

        [TestInitialize]
        public void InitializeTestData()
        {
            //Setup test data
            expectedEmployees = GetExpectedEmployees();

            //Arrange
            mockEmployeeRepository = new Mock<IEmployeeRepository>();
            employeeController = new EmployeeController(mockEmployeeRepository.Object);

            //Setup
            mockEmployeeRepository.Setup(m => m.GetAllEmployees()).Returns(expectedEmployees);

            //add
            mockEmployeeRepository.Setup(m => m.Add(It.IsAny<Employee>())).Returns(
                (Employee target) =>
                {
                    expectedEmployees.Add(target);

                    return target;
                });

            mockEmployeeRepository.Setup(m => m.Update(It.IsAny<Employee>())).Returns(
                (Employee target) =>
                {
                    var employee = expectedEmployees.Where(p => p.id == target.id).FirstOrDefault();

                    employee.id = target.id;
                    employee.name = target.name;
                    employee.gender = target.gender;
                    employee.age = target.age;
                    employee.salary = target.salary;

                    return employee;
                });


            //delete

            mockEmployeeRepository.Setup(m => m.Delete(It.IsAny<int>())).Returns(
                (int employeeId) =>
                {
                    var product = expectedEmployees.Where(p => p.id == employeeId).FirstOrDefault();

                    expectedEmployees.Remove(product);

                    return expectedEmployees;
                });
        }

        [TestMethod]
        public void Get_All_Emlpoyee_returnsAllEmployee()
        {
            //Act
            // var actualEmployees = mockEmployeeRepository.Object.GetAllEmployees();
            var actualEmployee = employeeController.GetAllEmployees();

            //Assert
            Assert.AreSame(expectedEmployees, actualEmployee);
        }

        [TestMethod]
        public void Add_employee()
        {
            //int employeeCount = mockEmployeeRepository.Object.GetAllEmployees().Count;
            int employeeCount = employeeController.GetAllEmployees().Count();
            Assert.AreEqual(2, employeeCount);

            //Prepare
            Employee newEmployee = GetNewEmployee(100, "sachin","Male", 32,2000);

            //Act
            // mockEmployeeRepository.Object.Add(newEmployee);
            employeeController.AddEmployee(newEmployee);

            //employeeCount = mockEmployeeRepository.Object.GetProducts().Count;
            employeeCount = employeeController.GetAllEmployees().Count();
            //Assert
            Assert.AreEqual(3, employeeCount);
        }

        [TestMethod]
        public void Update_Employee()
        {
            Employee product = new Employee()
            {
                id = 15,
                name = "N22",//Changed Name
                gender = "Male",
                age = 35,
                salary = 22000
            };

            // mockEmployeeRepository.Object.Update(product);
            employeeController.UpdateEmployee(product.id, product);

            // Verify the change
            // Assert.AreEqual("N22", mockEmployeeRepository.Object.GetAllEmployees().SingleOrDefault(x=>x.id== product.id).Name);
            Assert.AreEqual("N22", employeeController.GetAllEmployees().SingleOrDefault(x=>x.id== product.id).name);
        }
        [TestMethod]
        public void Delete_Product()
        {
            //Assert.AreEqual(2, mockEmployeeRepository.Object.GetAllEmployees().Count());
            Assert.AreEqual(2, employeeController.GetAllEmployees().Count());

            //mockEmployeeRepository.Object.Delete(1);
            employeeController.DeleteEmployee(15);

            // Verify the change
            //Assert.AreEqual(1, mockEmployeeRepository.Object.GetAllEmployees().Count());
            Assert.AreEqual(1, employeeController.GetAllEmployees().Count());
        }

        [TestCleanup]
        public void CleanUpTestData()
        {
            expectedEmployees = null;
            mockEmployeeRepository = null;
        }

        #region HelperMethods
        private static List<Employee> GetExpectedEmployees()
        {
            return new List<Employee>()
            {
                new Employee {id = 15, name = "ganesh kadam", gender = "Male", age = 25, salary = 10000},
                new Employee {id = 16, name = "Manohar kadam", gender = "Male", age = 20, salary = 50000},
            };
        }
        private static Employee GetNewEmployee(int id, string name, string gender, int age, int salary)
        {
            return new Employee()
            {
                id = id,
                name = name,
                gender = gender,
                age = age,
                salary = salary
            };
        }
        #endregion
    }
}
