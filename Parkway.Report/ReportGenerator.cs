using NHibernate;
using NHibernate.Transform;
using Parkway.Report.Attribute;
using Parkway.Tools.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Parkway.Report
{
    public class ReportGenerator : IReportGenerator
    {
        private List<string> ReportTitles;

        public List<string> Titles
        {
            get { return this.ReportTitles; }
        }

        public Dictionary<int, string> Headers
        {
            get { return this.ReportHeaders; }
        }
        private Dictionary<int, string> ReportHeaders;
        private List<Assembly> assemblyList;
        private Dictionary<ReportTitleAttribute, Type> reportTitleTypes = new Dictionary<ReportTitleAttribute, Type>();
        public Dictionary<ReportTitleAttribute, Type> TypeList
        {
            get
            {
                return this.reportTitleTypes;
            }
        }
        //private List<string> sessionFactoryKey = new List<string>();
        //private List<string> sessionKey = new List<string>();

        //private string sessionFactoryKey, sessionKey;
        //public ReportGenerator(string sessionFactoryKey, string sessionKey)
        //{
        //    GetReportTitle();
        //    GetReportHeaders();
        //this.sessionFactoryKey = sessionFactoryKey;
        //this.sessionKey = sessionKey;

        //}
        public ReportGenerator(List<Assembly> assemblyList)
        {
            this.assemblyList = assemblyList;
            reportTitleTypes = GetTypesWithReportTitleAttribute();
            //GetReportTitles();
            //GetReportHeaders();
        }


        //private void GetReportTitles()
        //{
        //    try
        //    {
        //        foreach (var type in reportTitleTypes)
        //        {


        //            ReportTitleAttribute reportTitle = (ReportTitleAttribute)Attribute.GetCustomAttribute(type, typeof(ReportTitleAttribute));
        //            if (reportTitle == null)
        //            {
        //                throw new Exception($"");
        //            }
        //            this.ReportTitles.Add(reportTitle.Title);
        //            this.sessionFactoryKey.Add(reportTitle.SessionFactoryKey);
        //            this.sessionKey.Add(reportTitle.SessionKey);



        //        }
        //    }
        //    catch (Exception e)
        //    {

        //        throw;
        //    }

        //}


        private Dictionary<ReportTitleAttribute, Type> GetTypesWithReportTitleAttribute()
        {
            foreach (var assembly in assemblyList)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    ReportTitleAttribute reportTitle = (ReportTitleAttribute)System.Attribute.GetCustomAttribute(type, typeof(ReportTitleAttribute));
                    if (type.GetCustomAttributes(typeof(ReportTitleAttribute), true).Length > 0 && reportTitle != null)
                    {
                        // yield return type;
                        reportTitleTypes.Add(reportTitle, type);
                        //sessionFactoryKey.Add(reportTitle.SessionFactoryKey);
                        //sessionKey.Add(reportTitle.SessionKey);
                    }
                }
            }
            return reportTitleTypes;
        }



        private List<int> sortNumber = new List<int>();
        //private void GetReportHeaders()
        //{
        //    try
        //    {
        //        var headers = new Dictionary<int, string>();
        //        foreach (var kvp in reportTitleTypes)
        //        {
        //            var members = kvp.Value.GetProperties();

        //            foreach (var member in members)
        //            {
        //                var reportHeader = (ReportHeaderAttribute)Attribute.GetCustomAttribute(member, typeof(ReportHeaderAttribute));
        //                if (reportHeader != null)
        //                {
        //                    headers.Add(reportHeader.HeaderPosition, reportHeader.Name);
        //                    sortNumber.Add(reportHeader.HeaderPosition);
        //                }
        //            }
        //        }
        //        this.ReportHeaders = headers.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);




        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}


        public List<string> GetHeadersOfSelectedReport(string selectedValue)
        {
            KeyValuePair<ReportTitleAttribute, Type> typeKvp = reportTitleTypes.Select(x => x).Where(x => x.Key.Title == selectedValue).Single();
            //this.title = typeKvp.Key.Title;
            Dictionary<int, System.Reflection.PropertyInfo> reportPropertiesDictionary = new Dictionary<int, System.Reflection.PropertyInfo>();
            Type reportType = typeKvp.Value;
            var propertyArray = reportType.GetProperties().Where(prop => System.Attribute.IsDefined(prop, typeof(ReportHeaderAttribute)));


            var headers = new Dictionary<int, string>();

            var members = typeKvp.Value.GetProperties();

            foreach (var member in members)
            {
                var reportHeader = (ReportHeaderAttribute)System.Attribute.GetCustomAttribute(member, typeof(ReportHeaderAttribute));
                if (reportHeader != null)
                {
                    headers.Add(reportHeader.HeaderPosition, reportHeader.Name);
                    sortNumber.Add(reportHeader.HeaderPosition);
                }
            }
            this.ReportHeaders = headers.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            return this.ReportHeaders.Values.ToList();
        }









        private List<string> recordsString = new List<string>();


        public List<string> Generate(Dictionary<string, string> oldAndNewHeaders, string ReportsDropDownList)
        {
            this.selectedValue = ReportsDropDownList;
            if (this.selectedValue != null)
            {
                KeyValuePair<ReportTitleAttribute, Type> typeKvp = reportTitleTypes.Select(x => x).Where(x => x.Key.Title == this.selectedValue).Single();

                //var typeKvp = reportTitleTypes.Select(x => x).Where(x => x.Key.Title == (oldAndNewHeaders.Select(k => k.Key)).FirstOrDefault()).Single();
                // reportTitleTypes.Select(x => x.Value.GetProperties()).Where(x => x.Where(k => k.Name ==))
                this.title = typeKvp.Key.Title;

                Dictionary<int, System.Reflection.PropertyInfo> reportPropertiesDictionary = new Dictionary<int, System.Reflection.PropertyInfo>();
                Type reportType = typeKvp.Value;

                //foreach (var oldheader in oldAndNewHeaders.Keys)
                //{
                
                    var propertyList = reportType.GetProperties().Where(prop => System.Attribute.IsDefined(prop, typeof(ReportHeaderAttribute)));
                //}
                // propertyArray.Where(prop=>prop.GetCustomAttribute)
                var propertyArray = new List<PropertyInfo>();
                var newheaders = new Dictionary<int, string>();
                foreach (var property in propertyList)
                {
                    var reportHeader = (ReportHeaderAttribute)System.Attribute.GetCustomAttribute(property, typeof(ReportHeaderAttribute));
                    foreach (var kvp in oldAndNewHeaders)
                    {
                       
                        if (kvp.Key == reportHeader.Name)
                        {
                            
                            propertyArray.Add(property);
                            newheaders.Add(reportHeader.HeaderPosition, kvp.Value);
                            sortNumber.Add(reportHeader.HeaderPosition);
                            //newReportHeaders.Add(kvp.Value);
                        }
                    }
                }
                this.newReportHeaders = newheaders.OrderBy(x => x.Key).Select(x => x.Value).ToList();

                //var members = typeKvp.Value.GetProperties();

                //foreach (var member in members)
                //{
                //    var reportHeader = (ReportHeaderAttribute)Attribute.GetCustomAttribute(member, typeof(ReportHeaderAttribute));
                //    if (reportHeader != null)
                //    {
                //        headers.Add(reportHeader.HeaderPosition, reportHeader.Name);
                //        sortNumber.Add(reportHeader.HeaderPosition);
                //    }
                //}
                //this.newReportHeaders = headers.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);


                var propertyAndSort = propertyArray.Zip(sortNumber, (p, s) => new { propertyArray = p, sortNumber = s });
                //iterate two lists with one foreach
                foreach (var ps in propertyAndSort)
                {
                    reportPropertiesDictionary.Add(ps.sortNumber, ps.propertyArray);
                }

                var sortedReportPropertiesDictionary = reportPropertiesDictionary.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
                var sortedReportPropertiesList = sortedReportPropertiesDictionary.Values.ToList();


                var session = SessionManager.GetInstance(typeKvp.Value.Name + "_SessionFactory", typeKvp.Key.SessionKey).GetSession();
                System.Collections.IList recordsInTable;
                using (var tx = session.BeginTransaction())
                {
                    StringBuilder queryStringBuilder = new StringBuilder("SELECT ");
                    foreach (var property in sortedReportPropertiesList)
                    {
                        queryStringBuilder.Append(property.Name + ",");

                    }
                    string queryToString = queryStringBuilder.ToString().TrimEnd(',');
                    StringBuilder newQuery = new StringBuilder(queryToString);
                    newQuery.Append(" FROM " + typeKvp.Value.Name);
                    var columnsList = new List<string>();

                    IQuery recordsInTableQuery = session.CreateSQLQuery(newQuery.ToString()).SetResultTransformer(Transformers.AliasToBean(reportType));
                    /*.AddEntity(typeof(T));*/
                    recordsInTable = recordsInTableQuery.List();




                    tx.Commit();
                }

                //recordsString = new List<string>();

                foreach (var item in recordsInTable)
                {
                    //var itemProperties = item.GetType().GetProperties();
                    string line = "";
                    foreach (var property in sortedReportPropertiesList)
                    {
                        line += property.GetValue(item).ToString() + ",";
                    }
                    recordsString.Add(line.TrimEnd(','));
                }
                //} 
                //}
                return recordsString;
            }

            return null;

        }
            //return new ReportGeneratorModel()
            //{

            //    Headers = this.Headers.Values.ToList(),
            //    RecordsInTable = this.recordsString,
            //    Titles = this.Titles
            //};
        


        public List<string> RecordsInTable
        {
            get { return this.recordsString; }
        }

        private string title;
        public string Title
        {
            get { return this.title; }

        }

        public List<string> newReportHeaders;
        public List<string> NewReportHeaders
        {
            get { return this.newReportHeaders; }
        }

        private string selectedValue;
        public string SelectedValue
        {
            get { return selectedValue; }
        }
    }
}
