using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementFPT.Modules.UserEventModule.Interface
{
    public interface IUserEventService
    {
        public int GetCountNumberUserEvent(Guid eventID);
    }
}
