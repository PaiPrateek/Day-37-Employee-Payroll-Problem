using NUnit.Framework;
using EmployeePayroll;
using RestSharp;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RestAPI
{
    public class Tests
    {
        RestClient client;

        [SetUp]
        public void Setup()
        {
            client = new RestClient("http://localhost:3000");
        }

        //Retrieve all emplyee details
        [Test]
        public void onCallGetEmployeeList()
        {
            RestRequest request = new RestRequest("/employees ", Method.Get);
            RestResponse response = client.Execute(request);

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(5, dataResponse.Count);

            foreach (Employee emp in dataResponse)
            {
                System.Console.WriteLine("ID : " + emp.id + " Name : " + emp.name + " Salary : " + emp.salary);
            }
        }
    }
}