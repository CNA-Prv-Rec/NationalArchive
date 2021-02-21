using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using NA_BE;

namespace NationalArchiveDemo
{
    public class Program
    {
         static void Main(string[] args)
        {
            string id = "";// is really a guid but...
            string input = "";

            IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appSettings.json", false, true)
               .Build();

         

            try
            {
                if (args.Length >0)  // first argument should be an integer
                {
                    id = args[0];
                }
                else
                { //we didn't get any arguments shall we try and get one from the user?
                    
                    Console.WriteLine("Please enter an ID");
                    input = Console.ReadLine();
                    if (input != "")
                    {
                        id =input;
                    }
                }
                if (id!="")
                {
                    /* if I was in a big application with lots of different projects and probably a web front-end or something, 
                     * I might use a factory here, but for this simple project I am keeping everything in the same project,
                     * loose coupling rules don't really apply to something this simple.
                     * also I am not ever going to use this project again so will never extend to more than one option
                     * so for speed and simplicity reasons I am leaving out the factory that some developers might want me to create here */
                    OutputGenerator outputChecker = new OutputGenerator();

                    /* however other SOLID design principles like dependency injection are probably still ok to use 
                     * even in a tiny project, since they don't generate any extra code and dont have any time overhead
                     * so I am doing single object of responsibility and splitting it into an output checker and a webservice caller
                     * */
                    WebServiceManager webservice = new WebServiceManager(configuration);
                    Dictionary<string,object> o = webservice.FindById(id);
                    string output = outputChecker.ProcessInput(o);

                }
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message); //this will include the no content message
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message); //any fundmental errors with the code or the webservice like it is down. even though these not in the spec it can stil happen
            }

           
        }
    }
}
