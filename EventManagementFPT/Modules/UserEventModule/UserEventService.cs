using EventManagementFPT.Model;
using EventManagementFPT.Modules.EventModule.Interface;
using EventManagementFPT.Modules.UserEventModule.Interface;
using EventManagementFPT.Modules.UserModule.Interface;
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
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        public UserEventService(IUserEventRepository userEventRepository, IUserRepository userRepository, IEventRepository eventRepository)
        {
            _userEventRepository = userEventRepository;
            _userRepository = userRepository;
            _eventRepository = eventRepository;
        }
        public UserEvent GetUserEvent(Guid userID, Guid eventID)
        {
            return _userEventRepository.GetFirstOrDefaultAsync(x => x.EventId.Equals(eventID) && x.UserId.Equals(userID)).Result;
        }
        public int GetCountNumberUserEvent(Guid eventID)
        {
            if (_eventRepository.GetFirstOrDefaultAsync(x => x.EventId.Equals(eventID) && x.Status == true).Result == null) return 0;
            return _userEventRepository.GetUserEventsBy(x => x.EventId.Equals(eventID)).Count();
        }
        public async Task GoingAnEvent(Guid userID, Guid eventID, bool isHost)
        {
            if (await _eventRepository.GetFirstOrDefaultAsync(x => x.EventId.Equals(eventID) && x.Status == true) == null) return;
            if (await _userRepository.GetFirstOrDefaultAsync(x => x.UserId.Equals(userID) && x.IsBlocked == false) == null) return;
            await _userEventRepository.AddAsync(new UserEvent
            {
                UserId = userID,
                EventId = eventID,
                IsHost = isHost,
            });
        }
        public async Task NotGoingAnEvent(Guid userID, Guid eventID)
        {
            if (await _eventRepository.GetFirstOrDefaultAsync(x => x.EventId.Equals(eventID) && x.Status == true) == null) return;
            if (await _userRepository.GetFirstOrDefaultAsync(x => x.UserId.Equals(userID) && x.IsBlocked == false) == null) return;
            var userEvent = await _userEventRepository.GetFirstOrDefaultAsync(x => x.EventId.Equals(eventID) && x.UserId.Equals(userID));
            if (userEvent == null) return;
            await _userEventRepository.RemoveUserEvent(userEvent);
        }
        public ICollection<User> GetUserGoingOfEvent(Guid eventID)
        {
            if (_eventRepository.GetFirstOrDefaultAsync(x => x.EventId.Equals(eventID) && x.Status == true).Result == null) return null;
            return _eventRepository.GetAll().Join(_userEventRepository.GetAll(), x => x.EventId, y => y.EventId, (x, y) => new
            {
                _userID = y.UserId
            }).Join(_userRepository.GetAll(), x => x._userID, y => y.UserId, (x, y) => new
            {
                _user = y
            }).Select(x => x._user).ToList();
        }
    }
}
