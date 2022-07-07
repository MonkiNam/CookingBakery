using System;
using System.Threading.Tasks;

namespace EventManagementFPT.Modules.UserEventModule.Interface
{
    public interface IUserEventService
    {
        public int GetCountNumberUserEvent(Guid eventID);
        public Task GoingAnEvent(Guid userID, Guid eventID);
    }
}
