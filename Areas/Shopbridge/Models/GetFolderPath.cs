using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace ThinkBridgeSoft.Areas.Shopbridge.Models
{
    public class GetFolderPath
    {
        public static string loRootPath;
       
        public GetFolderPath(IConfiguration config)
        {
            loRootPath = config.GetValue<string>("GetFolderPath:RootFolder");
        }
    }
}
