using EventManagementFPT.Model;
using EventManagementFPT.Utils.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventManagementFPT.Modules.UserEventModule.Interface
{
    public interface IUserEventRepository : IRepository<UserEvent>
    {
        public ICollection<UserEvent> GetUserEventsBy(
            Expression<Func<UserEvent, bool>> filter = null,
            Func<IQueryable<UserEvent>, ICollection<UserEvent>> options = null,
            string includeProperties = null
        );
        public Task RemoveUserEvent(UserEvent userEvent);
    }
}
