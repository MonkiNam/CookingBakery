using EventManagementFPT.Model;
using EventManagementFPT.Utils.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementFPT.Modules.EventLikeModule.Interface
{
    public interface IEventLikeRepository : IRepository<EventLike>
    {
        public int CountLikeOfEvent(Guid? eventID);
    }
}
