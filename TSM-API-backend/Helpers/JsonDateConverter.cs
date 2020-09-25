using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Helpers
{
    public class JsonDateConverter : IsoDateTimeConverter
    {
        
            public JsonDateConverter()
            {
                DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            }
        
    }
}
