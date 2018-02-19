using Parkway.Report.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Parkway.Report.Mvc.Controllers
{
    public class MvcReportController : Controller
    {
        public List<Assembly> assemblyList = AssemblyRegister.AssemblyList;

        // GET: MvcReport
        public ActionResult Index()
        {

            ReportGenerator reportGenerator = new ReportGenerator(assemblyList);
            //ReportGeneratorModel reportGeneratorModel = reportGenerator.Generate();
            return View(reportGenerator);
        }

        // [HttpGet]
        //// [ActionName("Index")]
        // public ActionResult IndexWithQueryString(string queryString)
        // {
        //     return Index();
        // }

        [HttpPost]
        //[Route("MvcReport/Index/{selectedValue}")]
        public ActionResult Index(string id, string ReportsDropDownList, FormCollection formCollection /*ReportsDropDownList*/)
        {
            Dictionary<string, string> oldAndNewHeaders = new Dictionary<string, string>();
            var reportGenerator = new ReportGenerator(assemblyList);

            //try
            //{
            if (Request.IsAjaxRequest())
            {
                if (id == "" || id == null)
                {
                    return null;
                }

                else
                {
                    reportGenerator.GetHeadersOfSelectedReport(id);
                    // List<string> headers = reportGenerator.GetHeadersOfSelectedReport(id);
                    //ViewBag.ReportHeaders = headers;
                    // return View(reportGenerator);
                    return PartialView("_PartialIndex", reportGenerator);

                }
            }
            else
            {
                var checkboxNames = formCollection.AllKeys.Where(x => x.StartsWith("checkboxName")).ToList();
                var textfieldNames = formCollection.AllKeys.Where(x => x.StartsWith("textfieldName")).ToList();
                List<string> checkboxList = new List<string>();
                for(int i=0; i<textfieldNames.Count;i++)
                {
                    var checkboxValue = formCollection["checkboxName" + i];
                    var textfieldValue = formCollection["textfieldName" + i];
                    if (formCollection["checkboxName" + i] != null && formCollection["textfieldName" + i] != "")
                    {




                        oldAndNewHeaders.Add(checkboxValue, textfieldValue);
                        //checkboxList.Add(checkboxValue);
                    }
                    else if (formCollection["checkboxName" + i] != null && formCollection["textfieldName" + i] == "")
                    {
                        oldAndNewHeaders.Add(checkboxValue, checkboxValue);

                    }




                    //bool checkvalue = bool.Parse(Request.Form.GetValues("checkboxName[" + i + "]"));
                    //if (Convert.ToBoolean(formCollection["checkboxName"+ i]) == true)
                    //{
                    //    var checkboxValue = formCollection["checkboxName" + i];
                    //    var textfieldValue = formCollection["textfieldName" + i];

                    //    oldAndNewHeaders.Add(checkboxValue, textfieldValue);
                    //}

                }
                reportGenerator.Generate(oldAndNewHeaders, ReportsDropDownList);
                return View("ReportPage", reportGenerator);

                //     }
            }

            //catch (Exception ex)
            //{
            //    if (id == "" || id == null)
            //    {
            //        //return Index();
            //    }
            //    else
            //    {

            //    }
            //    //throw;
            //}
            //return null;


            //if (ReportsDropDownList == "")
            //{
            //    return Index();
            //}

            //else
            //{
            //    //ViewBag.newReportTitle = ReportsDropDownList;
            //    var reportGenerator = new ReportGenerator(assemblyList);
            //    //UpdateModel<ReportGenerator>(reportGenerator);
            //    reportGenerator.Generate(ReportsDropDownList);
            //    //var strReports = form["ReportsDropDownList"];
            //    //reportGenerator.Generate(strReports);
            // return View("ReportPage", reportGenerator);
            //}
        }

        
    }
}