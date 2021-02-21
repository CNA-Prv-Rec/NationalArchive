using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NA_BE
{
    public class OutputGenerator
    {
        /// <summary>
        /// normally in .net some developers would expect some kind of dto/proper class or something here derived from the webservice but I am short of time so using generics
        /// but where you see a generic object you will probably expect something hard-coded with many objects hand-written in c#. 
        /// I am not doing this here for speed reasons. It's also unnecessary if the result is convertible to json then I can convert automatically
        /// </summary>
        /// <param name="jsonOutput"></param>
        public string ProcessInput(Dictionary<string,object> input = null)
        {
            if (input==null)
            {
                return "No record found";
            }
            if (input["title"]!=null)
            {
                return input["title"].ToString();
            }
        
            if (input["scopeContent"]!=null)
            {
                var scopecontent = input["scopeContent"].ToString() ;
                var data = (Dictionary<string, object>)JsonConvert.DeserializeObject(scopecontent, typeof(Dictionary<string, object>));
                if (data["description"]!=null)
                 {
                    return data["description"].ToString();
                 }
                
                var x = 0;

            }
            if (input["reference"] != null)
            {
                return input["reference"].ToString();
            }

            return "not sufficient information";
        }
    }
}
