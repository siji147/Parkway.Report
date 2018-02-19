using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parkway.Report
{
    public class ReportGeneratorModel
    {
        public List<string> Titles { get; set; }

        public List<string> Headers { get; set; }

        public List<string> RecordsInTable { get; set; }

       // public List<string> ReportList = new List<string>() { "fisrtReport", "secondReport", "thirdReport" };
    }
}