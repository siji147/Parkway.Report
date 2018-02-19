using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkway.Report
{
    public interface IReportGenerator
    {
        List<string> Generate(Dictionary<string, string> oldAndNewHeaders, string dropDownListValue);
        
    }
}
