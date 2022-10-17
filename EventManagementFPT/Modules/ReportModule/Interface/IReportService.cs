using CookingBakery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingBakery.Modules.ReportModule.Interface
{
    public interface IReportService
    {
        public Task AddNewReport(Report newReport);
        public Task DeleteReport(Guid? ID);
        public ICollection<Report> GetAll();
        public Task<Report> GetReportByID(Guid? reportID);
        public Task ChangeStatus(Guid? reportId, int newStatus);
    }
}
