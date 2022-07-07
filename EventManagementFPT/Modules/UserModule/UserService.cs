using EventManagementFPT.Model;
using EventManagementFPT.Modules.EventLikeModule.Interface;
using EventManagementFPT.Modules.FollowEventModule.Interface;
using EventManagementFPT.Modules.UserModule.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementFPT.Modules.UserModule
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFollowEventRepository _followEventRepository;
        private readonly IEventLikeRepository _eventLikeRepository;

        public UserService(IUserRepository userRepository, IFollowEventRepository followEventRepository, IEventLikeRepository eventLikeRepository)
        {
            _userRepository = userRepository;
            _followEventRepository = followEventRepository;
            _eventLikeRepository = eventLikeRepository;
        }
        public User GetUserByUserID(Guid? ID) => _userRepository.GetFirstOrDefaultAsync(x => x.UserId.Equals(ID)).Result;
        public async Task AddNewUser(User newUser)
        {
            newUser.UserId = Guid.NewGuid();
            await _userRepository.AddAsync(newUser);
        }
        public async Task UpdateUser(User userUpdate)
        {
            await _userRepository.UpdateAsync(userUpdate);
        }
        public async Task DeleteUser(Guid? ID)
        {
            User userDelete = _userRepository.GetFirstOrDefaultAsync(x => x.UserId.Equals(ID)).Result;
            if (userDelete != null) await _userRepository.RemoveAsync(userDelete);
        }
        public void FollowEvent(User user, Event followingEvent)
        {
            var followInfo = new FollowEvent
            {
                EventId = followingEvent.EventId,
                UserId = user.UserId
            };
            _userRepository.FollowEvent(followInfo);
        }

        public void UnfollowEvent(User user, Event followingEvent)
        {
            var followInfo = _followEventRepository.GetFirstOrDefaultAsync(
                item => item.EventId.Equals(followingEvent.EventId) && item.UserId.Equals(user.UserId)
            ).Result;
            if (followInfo != null) _userRepository.UnfollowEvent(followInfo);
        }
        public ICollection<User> GetAll()
        {
            return _userRepository.GetAll().ToList();
        }

        public void LikeEvent(User user, Event _event)
        {
            var likeInfo = new EventLike
            {
                EventId = _event.EventId,
                UserId = user.UserId
            };
            _userRepository.LikeEvent(likeInfo);
        }

        public void UnlikeEvent(User user, Event _event)
        {
            var likeInfo = _eventLikeRepository.GetFirstOrDefaultAsync(
                item => item.EventId.Equals(_event.EventId) && item.UserId.Equals(user.UserId)
            ).Result;
            if (likeInfo != null) _userRepository.UnlikeEvent(likeInfo);
        }

        public bool isExist(string email)
        {
            return _userRepository.isExist(email);
        }
    }
}
