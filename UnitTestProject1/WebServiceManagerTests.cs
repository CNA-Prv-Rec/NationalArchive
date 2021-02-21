using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NA_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NA_BE.Tests
{
    [TestClass()]
    public class WebServiceManagerTests
    {
        IConfiguration _configuration = null;
        public WebServiceManagerTests()
        {

            IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appSettings.json", false, true)
               .Build();
            _configuration = configuration;

        }

        private object RunSearchTest(string searchText)
        {
            OutputGenerator outputChecker = new OutputGenerator();
            WebServiceManager webservice = new WebServiceManager(_configuration);
            var o = webservice.Search(searchText);
            return o;
          }

        [TestMethod()]
        public void FindByIdTest_Search()
        {
            string search = "titanic";
            var o= RunSearchTest(search);
            Assert.IsNotNull(o); //tbh this is only going to be run to get data to find test cases to match the other unit tests
            /* I have run this, but i don't seem to be able to find much data which doesn't have a title, in order to pass id's into the other test cases
             * so I am not going to be able to write as many test cases as I would like due to insufficient knowledge of teh data in order to write the test cases
             * */

        }


        [TestMethod()]
        public void FindByIdTest_NoContent()
        {
            string id = "blah-blah-blah-blah-blah";
            string output = RunFindByIDTest(id);
            Assert.IsTrue(output == "No record found");
        }
        [TestMethod()]
        public void FindByIdTest_FindTitle()
        {
            string id = "a147aa58-38c5-45fb-a340-4a348efa01e6";
            string output = RunFindByIDTest(id);
            Assert.IsTrue(output == "<p>Titan Tractor</p>");
        }

        [TestMethod()]
        public void FindByIdTest_FindDescription()
        {
            string id = "N13759454";
            string output = RunFindByIDTest(id);
            Assert.IsTrue(output == "minute books, cash book, accounts, registers of payments and beneficiaries");
        }


        private string RunFindByIDTest(string id)
        {
            OutputGenerator outputChecker = new OutputGenerator();
            WebServiceManager webservice = new WebServiceManager(_configuration);
            Dictionary<string, object> o = webservice.FindById(id);
            string output = outputChecker.ProcessInput(o);
            return output;
        }
  
    }
}