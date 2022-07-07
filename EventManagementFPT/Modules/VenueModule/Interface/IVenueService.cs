using EventManagementFPT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementFPT.Modules.VenueModule.Interface
{
    public interface IVenueService
    {
        public Task AddNewVenue(Venue newVenue);
        public Task UpdateVenue(Venue venueUpdate);
        public Task DeleteVenue(Guid? ID);
        public ICollection<Venue> GetAll();
        public Venue GetVenueByID(Guid? venueID);
    }
}
