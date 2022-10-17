using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using CookingBakery.Models;

namespace CookingBakery.BakeryModules.UserModule.Interface
{
    public interface IUserService
    {
        public Task<User> Authenticate(string Email, string Password);
        public User GetUserByUserID(Guid? ID);
        public Task<User> GetUserByEmail(string Email);
        public Task AddNewUser(User newUser);
        public Task UpdateUser(User userUpdate);
        public Task ChangePassword(string newPassword, string uid);
        public Task DeleteUser(Guid? ID);
        public ICollection<User> GetAll();
        public bool isExist(string email);
        public void LikePost(Guid userId, Guid postId);
        public void UnlikePost(Guid userId, Guid postId);
    }
}
