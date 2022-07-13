using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.ReportModule.Interface;
using Microsoft.AspNetCore.Authorization;

namespace EventManagementFPT.Pages.ReportPage
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IReportService _reportService;

        public IndexModel(IReportService reportService)
        {
            _reportService = reportService;
        }

        public IList<Report> Report { get; set; }

        public void OnGet()
        {
            Report = _reportService.GetAll().ToList();
        }
    }
}