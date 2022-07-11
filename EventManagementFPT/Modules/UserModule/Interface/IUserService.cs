using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagementFPT.Model;

namespace EventManagementFPT.Modules.UserModule.Interface
{
    public interface IUserService
    {
        public Task<User> Authenticate(string Email, string Password);
        public User GetUserByUserID(Guid? ID);
        public Task<User> GetUserByEmail(string Email);
        public Task AddNewUser(User newUser);
        public Task UpdateUser(User userUpdate);
        public Task DeleteUser(Guid? ID);
        public ICollection<User> GetAll();
        public bool isExist(string email);
        public void LikeEvent(Guid userId, Guid eventId);
        public void UnlikeEvent(Guid userId, Guid eventId);
    }
}
