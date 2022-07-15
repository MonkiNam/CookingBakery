using EventManagementFPT.Model;
using EventManagementFPT.Modules.EventLikeModule.Interface;
using EventManagementFPT.Modules.UserModule.Interface;
using Microsoft.EntityFrameworkCore;
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
        private readonly EventManagementContext _context;

        public UserService(IUserRepository userRepository, IEventLikeRepository eventLikeRepository, EventManagementContext context)
        {
            _userRepository = userRepository;
            _eventLikeRepository = eventLikeRepository;
            _context = context;
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
            if (await _userRepository.GetFirstOrDefaultAsync(x => x.Email.Equals(newUser.Email)) != null) return;
            await _userRepository.AddAsync(newUser);
        }

        public async Task UpdateUser(User userUpdate)
        {
            if (userUpdate.IsBlocked == true) return;
            await _userRepository.UpdateAsync(userUpdate);
        }

        public async Task ChangePassword(string newPassword, string uid)
        {
            User user = GetUserByUserID(Guid.Parse(uid));
            if(user != null && user.IsBlocked == false)
            {
                user.Password = newPassword;
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public async Task DeleteUser(Guid? ID)
        {
            User userDelete = await _userRepository.GetFirstOrDefaultAsync(x => x.UserId.Equals(ID) && x.IsBlocked != true);
            if (userDelete == null) return;
            userDelete.IsBlocked = true;
            await _userRepository.UpdateAsync(userDelete);
        }

        public void LikeEvent(Guid userId, Guid eventId)
        {
            var likeInfo = new EventLike
            {
                EventId = eventId,
                UserId = userId,
                CreateDate = DateTime.Now,
                Status = true
            };
            _userRepository.LikeEvent(likeInfo);
        }

        public void UnlikeEvent(Guid userId, Guid eventId)
        {
            var likeInfo = _eventLikeRepository.GetFirstOrDefaultAsync(
                item => item.EventId.Equals(eventId) && item.UserId.Equals(userId)
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