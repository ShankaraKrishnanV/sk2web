using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Brain_IQ.Settings
{
    public class AppKey
    {
        /// <summary>
        /// Get WEB API URL
        /// </summary>
        /// <returns></returns>
        public string GetapiURL()
        {
            string GetURL = ConfigurationManager.AppSettings["apiURL"].ToString().Trim();
            return GetURL;
        }
    }
}