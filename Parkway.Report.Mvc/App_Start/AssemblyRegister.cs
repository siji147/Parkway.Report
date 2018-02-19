using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Collections.Specialized;
using System.Reflection;

namespace Parkway.Report.Mvc
{
    public class AssemblyRegister
    {
        private static List<Assembly> assemblyList = new List<Assembly>();

        public static void RegisterAssemblies()
        {
            var assemblySection = ConfigurationManager.GetSection("ReportGeneratorAssemblies") as NameValueCollection;
                     /* Assembly.GetAssembly(assemblyKey.GetType());*/

            if (assemblySection != null)
            {
                foreach(var assemblyKey in assemblySection.AllKeys)
                {
                    Assembly assembly = Assembly.Load(assemblySection[assemblyKey]);
                        assemblyList.Add(assembly); 
                    
                }
            }
        }

        public static List<Assembly> AssemblyList
        {
            get { return assemblyList; }
        }
    }
}