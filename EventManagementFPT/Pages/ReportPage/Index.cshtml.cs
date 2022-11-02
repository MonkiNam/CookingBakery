using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BussinessObject.Models;
using CookingBakery.Modules.ReportModule.Interface;
using Microsoft.AspNetCore.Authorization;

namespace CookingBakery.Pages.ReportPage
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