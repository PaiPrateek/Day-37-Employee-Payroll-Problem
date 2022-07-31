using NUnit.Framework;
using EmployeePayroll;
using RestSharp;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

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

        //Add new employee details

        // Add multiple Employee detalis 
        [Test]
        public void onCallAddEmployeeList()
        {
            //Arrange
            RestRequest request = new RestRequest("/employees ", Method.Post);
            JObject jobject = new JObject();
            jobject.Add("name", "Ramanath");
            jobject.Add("salary", "30000");

            request.AddParameter("application/json", jobject, ParameterType.RequestBody);

            //Act
            RestResponse response = client.Execute(request);


            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);

            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Ramanath", dataResponse.name);
            Assert.AreEqual("30000", dataResponse.salary);
            System.Console.WriteLine(response.Content);
        }

        [Test]
        public void onCallEditEmployeeList()
        {
            //Arrange
            RestRequest request = new RestRequest("/employees/8 ", Method.Put);
            JObject jobject = new JObject();
            jobject.Add("name", "Prateeksha");
            jobject.Add("salary", "10000");

            request.AddParameter("application/json", jobject, ParameterType.RequestBody);

            //Act
            RestResponse response = client.Execute(request);


            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Prateeksha", dataResponse.name);
            Assert.AreEqual("10000", dataResponse.salary);
            System.Console.WriteLine(response.Content);
        }

    }
}