using System;
using System.Security.Claims;
using System.Threading.Tasks;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.CommentModule.Interface;
using EventManagementFPT.Modules.EventModule.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventManagementFPT.Pages.Home
{
    public class AddComment : PageModel
    {
        private readonly ICommentService _commentService;
        private readonly IEventService _eventService;

        public AddComment(ICommentService commentService, IEventService eventService)
        {
            _commentService = commentService;
            _eventService = eventService;
        }

        public async Task<IActionResult> OnPostAsync(Guid eventId, string txtContent)
        {
            var _event = await _eventService.GetEventByID(eventId);
            
            if (_event == null) return RedirectToPage("/Home/Index");
            
            var uid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (uid == null) return RedirectToPage("/Home/Details", new {id = eventId});
            
            await _commentService.AddNewComment(new Comment
            {
                CommentId = Guid.NewGuid(),
                UserId = Guid.Parse(uid),
                Content = txtContent,
                EventId = eventId,
                CreateDate = DateTime.Now,
                IsParent = true,
                Status = true,
            });
            
            return RedirectToPage("/Home/Details", new {id = eventId});
        }
    }
}