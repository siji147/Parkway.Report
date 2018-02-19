using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Collections.Specialized;

namespace Parkway.Report.Mvc
{
    public class SessionFactoryEngine
    {

        public string GetSessionFactory()
        {
            NameValueCollection sessionFactorySession = ConfigurationManager.GetSection("parkway.tools.nhibernate") as NameValueCollection;

            sessionFactorySession.
        }

    }
}