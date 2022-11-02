using CookingBakery.Model;
using CookingBakery.Modules.ReportModule.Interface;
using CookingBakery.Utils.Repository;

namespace CookingBakery.Modules.ReportModule
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
