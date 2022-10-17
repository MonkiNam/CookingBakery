using CookingBakery.Model;
using CookingBakery.Utils.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingBakery.Modules.EventLikeModule.Interface
{
    public interface IEventLikeRepository : IRepository<EventLike>
    {
        public int CountLikeOfEvent(Guid? eventID);
    }
}
