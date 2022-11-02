using CookingBakery.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookingBakery.Modules.UserEventModule.Interface
{
    public interface IUserEventService
    {
        public int GetCountNumberUserEvent(Guid eventID);
        public Task GoingAnEvent(Guid userID, Guid eventID, bool isHost);
        public UserEvent GetUserEvent(Guid userID, Guid eventID);
        public Task NotGoingAnEvent(Guid userID, Guid eventID);
        public ICollection<User> GetUserGoingOfEvent(Guid eventID);
    }
}
