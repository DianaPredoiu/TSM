/*********************************************************************************
 * \file 
 * 
 * JsonDateConverter.cs file contains the JsonDateConverter class, which is 
 * included in Helpers namespace.
 * 
 ********************************************************************************/

using Newtonsoft.Json.Converters;

namespace WebApi.Helpers
{
    /*******************************************************
     * 
     * \class
     * 
     * JsonDateConverter class inherits IsoDateTimeConverter
     * and formats all the date into yyyy-MM-dd HH:mm:ss
     * 
     ******************************************************/
    public class JsonDateConverter : IsoDateTimeConverter
    {
        /*******************************************************
         * 
         * The constructor formats the date
         * 
         ******************************************************/
        public JsonDateConverter()
        {
                DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        }//CONSTRUCTOR
        
    }//CLASS JsonDateConverter

}//NAMESPACE WebApi.Helpers
