using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace Parkway.Report.Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ReportTitleAttribute : System.Attribute
    {
        
        public string Title { get; }

        //public ReportTitleAttribute(string title)
        //{
        //    this.Title = title;
        //}}

        public string SessionFactoryKey { get; set; }
        public string SessionKey { get; set; }
        public ReportTitleAttribute(string title) /*, string sessionFactoryKey, string sessionKey="Default")*/
        {
            this.Title = title;
            //this.SessionFactoryKey = ConfigurationManager.AppSettings[sessionFactoryKey];
            //this.SessionKey = sessionKey;
            
        }

        
    }

}
