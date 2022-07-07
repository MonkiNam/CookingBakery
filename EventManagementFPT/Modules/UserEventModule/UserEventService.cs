using EventManagementFPT.Model;
using EventManagementFPT.Modules.UserEventModule.Interface;
using EventManagementFPT.Utils.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventManagementFPT.Modules.UserEventModule
{
    public class UserEventService : IUserEventService
    {
        private readonly IUserEventRepository _userEventRepository;
        public UserEventService(IUserEventRepository userEventRepository)
        {
            _userEventRepository = userEventRepository;
        }
        public int GetCountNumberUserEvent(Guid eventID)
        {            
            return _userEventRepository.GetUserEventsBy(x => x.EventId.Equals(eventID)).Count();
        }
    }
}
