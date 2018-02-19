using Parkway.Report.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parkway.Report.Mvc.Models
{
    [ReportTitle("Employee Report")]
    public class Employee
    {
        [ReportHeader("Id", 0)]
        public virtual int ID { get; set; }
        [ReportHeader("Name", 1)]
        public virtual string Name { get; set; }
        [ReportHeader("Age", 2)]
        public virtual int Age { get; set; }
        [ReportHeader("Gender", 3)]
        public virtual string Sex { get; set; }
        [ReportHeader("State Of Origin", 4)]
        public virtual string State { get; set; }

    }
}