using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkway.Report.Attribute
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ReportHeaderAttribute : System.Attribute
    {
        private string name;
        private int headerPosition;

        public virtual string Name
        {
            get { return name; }
        }
        public virtual int HeaderPosition
        {
            get { return headerPosition; }
        }

        public ReportHeaderAttribute(string name, int headerPosition)
        {
            this.name = name;
            this.headerPosition = headerPosition;
        }
    }
}
