using EventManagementFPT.Model;
using EventManagementFPT.Modules.EventLikeModule.Interface;
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
        private readonly IEventLikeRepository _eventLikeRepository;

        public UserService(IUserRepository userRepository, IEventLikeRepository eventLikeRepository)
        {
            _userRepository = userRepository;
            _eventLikeRepository = eventLikeRepository;
        }
        public ICollection<User> GetAll()
        {
            return _userRepository.GetAll().ToList();
        }

        public User GetUserByUserID(Guid? ID)
        {
            return _userRepository.GetFirstOrDefaultAsync(x => x.UserId.Equals(ID)).Result;
        }

        public async Task AddNewUser(User newUser)
        {
            newUser.UserId = Guid.NewGuid();
            await _userRepository.AddAsync(newUser);
        }

        public async Task UpdateUser(User userUpdate)
        {
            if (userUpdate.IsBlocked == true) return;
            await _userRepository.UpdateAsync(userUpdate);
        }

        public async Task DeleteUser(Guid? ID)
        {
            User userDelete = _userRepository.GetFirstOrDefaultAsync(x => x.UserId.Equals(ID) && x.IsBlocked == false).Result;
            if (userDelete == null) return;
            userDelete.IsBlocked = true;
            await _userRepository.UpdateAsync(userDelete);
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

        public Task<User> GetUserByEmail(string Email)
        {
            return _userRepository.GetFirstOrDefaultAsync(u => u.Email.Equals(Email));
        }

        public Task<User> Authenticate(string Email, string Password)
        {
            return _userRepository.GetFirstOrDefaultAsync(u => (u.Email == Email && u.Password == Password));
        }
    }
}