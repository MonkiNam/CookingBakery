using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementFPT.Modules.EventLikeModule.Interface
{
    public interface IEventLikeService
    {
        public int CountLikeOfEvent(Guid? eventID);
    }
}
