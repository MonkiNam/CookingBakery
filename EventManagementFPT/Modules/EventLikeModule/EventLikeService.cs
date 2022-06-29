using EventManagementFPT.Modules.EventLikeModule.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementFPT.Modules.EventLikeModule
{
    public class EventLikeService : IEventLikeService
    {
        private IEventLikeRepository _eventLikeRepository;
        public EventLikeService(IEventLikeRepository eventLikeRepository)
        {
            _eventLikeRepository = eventLikeRepository;
        }
        public int CountLikeOfEvent(Guid? eventID)
        {
            var _event = _eventLikeRepository.GetFirstOrDefaultAsync(x => x.EventId.Equals(eventID));
            if (_event == null)
            {
                return 0;
            }
            return _eventLikeRepository.CountLikeOfEvent(eventID);
        }
    }
}
