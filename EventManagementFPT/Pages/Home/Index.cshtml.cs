using System;
using System.Collections.Generic;
using System.Linq;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.EventModule.Interface;
using EventManagementFPT.Utils;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventManagementFPT.Pages.Home
{
    public class Index : PageModel
    {
        private readonly IEventService _eventService;
        private readonly EventManagementContext _context;

        public Index(IEventService eventService, EventManagementContext context)
        {
            _eventService = eventService;
            _context = context;
        }

        public PaginatedList<Event> Event { get; set; }
        public IEnumerable<Event> NewestEvent { get; set; }
        public string SearchName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public Guid? CategoryId { get; set; }

        public void OnGetAsync(
            string currentSearchName, string txtSearchName,
            DateTime? currentFilterDateFrom, DateTime? filterDateFrom,
            DateTime? currentFilterDateTo, DateTime? filterDateTo,
            Guid? currentFilterCategory, Guid? filterCategory,
            int? pageIndex
        )
        {
            NewestEvent = _eventService.GetNewestEvents(3);

            if (txtSearchName != null || filterDateFrom != null || filterDateTo != null || filterCategory != null)
            {
                pageIndex = 1;
            }
            else
            {
                txtSearchName = currentSearchName;
                filterDateFrom = currentFilterDateFrom;
                filterDateTo = currentFilterDateTo;
                filterCategory = currentFilterCategory;
            }

            var events = _eventService.GetAll().Where(o => o.Status).AsQueryable();

            if (!string.IsNullOrEmpty(txtSearchName))
            {
                SearchName = txtSearchName;
                events = events.Where(o => o.Name.ToLower().Contains(txtSearchName.ToLower()));
            }
            
            if (filterDateFrom != null && filterDateTo == null)
            {
                DateTime? now = DateTime.Now;

                if (filterDateFrom > now)
                {
                    (filterDateFrom, now) = (now, filterDateFrom);
                }
                DateFrom = filterDateFrom;
                DateTo = now;

                events = events.Where(o => o.StartDate >= filterDateFrom && o.StartDate <= DateTime.Now);
            }
            else if (filterDateFrom != null && filterDateTo != null)
            {
                DateTime? now = DateTime.Now;
                if (filterDateFrom > now)
                {
                    (filterDateFrom, filterDateTo) = (filterDateTo, filterDateFrom);
                }
                DateFrom = filterDateFrom;
                DateTo = filterDateTo;
                events = events.Where(o => o.StartDate >= filterDateFrom && o.StartDate <= filterDateTo);
            }

            if (filterCategory != null)
            {
                CategoryId = (Guid) filterCategory;
                events = events.Where(o => o.Category == filterCategory);
            }

            Event = PaginatedList<Event>.Create(events, pageIndex ?? 1, 5);

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
        }
    }
}