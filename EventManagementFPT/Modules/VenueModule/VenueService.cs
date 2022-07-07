using EventManagementFPT.Model;
using EventManagementFPT.Modules.VenueModule.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementFPT.Modules.VenueModule
{
    public class VenueService : IVenueService
    {
        private readonly IVenueRepository _venueRepository;

        public VenueService(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }
        public ICollection<Venue> GetAll()
        {
            return _venueRepository.GetAll().ToList();
        }
        public async Task AddNewVenue(Venue newVenue)
        {
            newVenue.VenueId = Guid.NewGuid();
            await _venueRepository.AddAsync(newVenue);
        }
        public async Task UpdateVenue(Venue venueUpdate)
        {
            await _venueRepository.UpdateAsync(venueUpdate);
        }
        public async Task DeleteVenue(Guid? id)
        {
            Venue venueDelete = _venueRepository.GetFirstOrDefaultAsync(x => x.VenueId.Equals(id) && x.Status == true).Result;
            if (venueDelete == null) return;
            venueDelete.Status = false;
            await _venueRepository.UpdateAsync(venueDelete);
        }
        public Venue GetVenueByID(Guid? venueID)
        {
            return _venueRepository.GetFirstOrDefaultAsync(x => x.VenueId.Equals(venueID)).Result;
        }
    }
}
