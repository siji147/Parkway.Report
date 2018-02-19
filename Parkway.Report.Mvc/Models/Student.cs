using Parkway.Report.Attribute;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Parkway.Report.Mvc.Models
{
    
   [ReportTitle("Students Report")]
    public class Student
    {
        [ReportHeader("Id", 0)]
        public virtual int ID { get; set; }
        [ReportHeader("First Name", 1)]
        public virtual string FirstName { get; set; }
        [ReportHeader("Last Name", 2)]
        public virtual string LastName { get; set; }


    }
}