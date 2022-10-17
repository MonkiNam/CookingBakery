using CookingBakery.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using CookingBakery.BakeryModules.UserModule.Interface;
using CookingBakery.BakeryModules.PostReactionModule.Interface;
using System.Linq;

namespace CookingBakery.BakeryModules.UserModule
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostReactionRepository _postReactionRepository;
        private readonly CookingBakeryContext _context;

        public UserService(IUserRepository userRepository, IPostReactionRepository postReactionRepository, CookingBakeryContext context)
        {
            _userRepository = userRepository;
            _postReactionRepository = postReactionRepository;
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
            if (user is { IsBlocked: false })
            {
                user.Password = newPassword;
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteUser(Guid? ID)
        {
            User userDelete = await _userRepository.GetFirstOrDefaultAsync(x => x.UserId.Equals(ID) && x.IsBlocked != true);
            if (userDelete == null) return;
            userDelete.IsBlocked = true;
            await _userRepository.UpdateAsync(userDelete);
        }

        public void LikePost(Guid userId, Guid postId)
        {
            var likeInfo = new PostReaction
            {
                PostId = postId,
                UserId = userId,
                CreatedDate = DateTime.Now,
                Status = true
            };
            _userRepository.LikePost(likeInfo);
        }

        public void UnlikePost(Guid userId, Guid postId)
        {
            var likeInfo = _postReactionRepository.GetFirstOrDefaultAsync(
                item => item.PostId.Equals(postId) && item.UserId.Equals(userId)
            ).Result;
            if (likeInfo != null) _userRepository.UnlikePost(likeInfo);
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
