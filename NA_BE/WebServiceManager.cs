using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;

namespace NA_BE
{
    public class WebServiceManager
    {
        private IConfiguration _configuration { get; set; }
        public WebServiceManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Dictionary<string, object> FindById(string id)
        {
            string url = _configuration["appSettings:findByIdUrl"] + id;
            string res = RetrieveData("GET", url);
            var o = (Dictionary<string, object>)JsonConvert.DeserializeObject(res, typeof(Dictionary<string, object>));
            return o;
            
        }

        public object Search(string searchText)
        {
            string url = _configuration["appSettings:searchUrl"];
            url += "?sps.searchQuery=" + searchText;// we should probably sanitise this text in case user is entering anything dangerous here

            // have not been asked to do anything with this during the test so not really implementing, only is useful for getting data for the unit tests.
            var res = RetrieveData("GET", url);
            Dictionary<string, object> all = (Dictionary<string,object>)JsonConvert.DeserializeObject(res, typeof(Dictionary<string, object>));
            var records = JsonConvert.DeserializeObject(all["records"].ToString(), typeof(IEnumerable<Dictionary<string, object>>));
            return records;
        }

        private string RetrieveData(string method, string url)
        {

            string res = "";
            HttpClient client = new HttpClient();

            try
            {
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    // this will include the 204 no content but having tested it it seems it will just return a null dictionary which turns out to be perfect for my needs rather than go down the error handling route
                    res = response.Content.ReadAsStringAsync().Result; 
                }
                else
                {
                   throw new Exception(response.ReasonPhrase);// problem with the webservice, need to report back even though this is not in the spec
                }
            }
            catch(Exception ex) 
            {
                throw; // I dont actually care, I want it to go up a level, i just want the finally clause so...
            }
            finally
            {

                // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
                if (client != null)
                {
                    client.Dispose();
                }

            }
            return res;
        }


    }
}
