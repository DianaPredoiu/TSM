/**************************************************************************************************************
 * \file 
 * 
 * Program.cs file contains WebApi namespace and 
 * Program class.
 * 
 * For more details about ASP.NET Core see: https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1
 * 
 **************************************************************************************************************/

//namespaces used in Program class
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
//list of namespaces

//description in Startup.cs file
namespace WebApi
{
    /*******************************************************
     * 
     * Program class creates the host for the web app
     * 
     ******************************************************/
    public class Program
    {
        /*******************************************************
         * 
         * Main Method is the entry point, from which the app 
         * starts executing. 
         * 
         ******************************************************/
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();

        }//METHOD Main


        /***************************************************************************************************************************
         * 
         * BuildWebHost Method returns an IWebHost oject that 
         * uses Startup's configuration and the mentioned url.
         * 
         * For more details go to: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.1
         * 
         **************************************************************************************************************************/
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://localhost:4000")
                .Build();

        //METHOD BuildWebHost

    }//CLASS Program

}//NAMESPACE WebApi
