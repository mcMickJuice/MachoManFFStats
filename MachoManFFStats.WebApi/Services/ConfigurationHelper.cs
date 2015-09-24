using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.Data.Edm;

namespace MachoManFFStats.WebApi.Services
{
    public class ConfigurationHelper
    {
        public static string Environment
        {
            get { return get("environment"); }
        }

        private static string get(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}