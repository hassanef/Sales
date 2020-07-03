using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;

namespace Sales.Web.Controllers
{
    public class ViewerController : Controller
    {
        static ViewerController()
        {
            // How to Activate
            //Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnO...";
            //Stimulsoft.Base.StiLicense.LoadFromFile("license.key");
            //Stimulsoft.Base.StiLicense.LoadFromStream(stream);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetReport(int id = 1)
        {
            // Create the report object
            var report = new StiReport();

            report.Load(StiNetCoreHelper.MapPath(this, "Reports/SimpleReport.mrt"));
            //// Load report
            //switch (id)
            //{
            //    // Load report snapshot
            //    case 1:
            //        report.LoadDocument(StiNetCoreHelper.MapPath(this, "Reports/SimpleList.mdc"));
            //        break;

            //    // Load report template
            //    case 2:
            //        report.Load(StiNetCoreHelper.MapPath(this, "Reports/TwoSimpleLists.mrt"));
            //        break;
            //}

            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public IActionResult ViewerEvent()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }
    }
}
