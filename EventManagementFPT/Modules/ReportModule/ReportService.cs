using CookingBakery.Model;
using CookingBakery.Modules.ReportModule.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingBakery.Modules.ReportModule
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public ICollection<Report> GetAll()
        {
            return _reportRepository.GetAll().ToList();
        }

        public async Task AddNewReport(Report newReport)
        {
            newReport.ReportId = Guid.NewGuid();
            newReport.CreateDate = DateTime.Now;
            newReport.Status = ReportEnum.Unprocess;
            await _reportRepository.AddAsync(newReport);
        }

        public async Task ChangeStatus(Guid? reportId, int newStatus)
        {
            var report = await _reportRepository.GetFirstOrDefaultAsync(
                x => x.ReportId.Equals(reportId) && x.Status != ReportEnum.Processed
            );
            if (report == null) return;
            report.Status = (ReportEnum) newStatus;
            await _reportRepository.UpdateAsync(report);
        }

        public async Task DeleteReport(Guid? id)
        {
            Report reportDelete = await _reportRepository.GetFirstOrDefaultAsync(x => x.ReportId.Equals(id));
            if (reportDelete == null) return;
            await _reportRepository.RemoveAsync(reportDelete);
        }

        public async Task<Report> GetReportByID(Guid? reportId)
        {
            return await _reportRepository.GetFirstOrDefaultAsync(
                x => x.ReportId.Equals(reportId),
                includeProperties: "AuthorNavigation"
            );
        }
    }
}