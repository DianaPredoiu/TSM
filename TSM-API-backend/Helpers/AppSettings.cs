/*********************************************************************************
 * \file 
 * 
 * AppSettings.cs file contains the AppSettings class, which is included in Helpers
 * namespace.
 * 
 ********************************************************************************/

namespace WebApi.Helpers
{
    /*******************************************************
     * 
     * \class
     * 
     * AppSettings class contains the jwt token string, go 
     * see appSettings.json.
     * 
     ******************************************************/
    public class AppSettings
    {
        public string Secret { get; set; }

    }//CLASS AppStettings

}//NAMESPACE WebApi.Helpers