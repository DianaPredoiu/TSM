/*********************************************************************************
 * \file 
 * 
 * AppExeception.cs file contains the AppExceptioN class, which is included in 
 * Helpers namespace.
 * 
 ********************************************************************************/

//included namespaces
using System;
using System.Globalization;
//

namespace WebApi.Helpers
{

    /***********************************************************
     * 
     * \class
     * 
     * Custom exception class for throwing application specific 
     * exceptions (e.g. for validation) that can be caught and 
     * handled within the application.
     * 
     ***********************************************************/
    public class AppException : Exception
    {
        public AppException() : base() {}

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args) 
               : base(String.Format(CultureInfo.CurrentCulture, message, args)){}

    }//CLASS AppException

}//NAMESPACE WebApi.Helpers