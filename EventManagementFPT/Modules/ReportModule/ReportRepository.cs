using EventManagementFPT.Model;
using EventManagementFPT.Modules.ReportModule.Interface;
using EventManagementFPT.Utils.Repository;

namespace EventManagementFPT.Modules.ReportModule
{
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        private readonly EventManagementContext _db;

        public ReportRepository(EventManagementContext db) : base(db)
        {
            _db = db;
        }
    }
}
