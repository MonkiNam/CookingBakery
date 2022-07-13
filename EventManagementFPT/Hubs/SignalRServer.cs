using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.CommentModule.Interface;
using EventManagementFPT.Modules.EventModule.Interface;
using EventManagementFPT.Modules.UserModule.Interface;
using Microsoft.AspNetCore.SignalR;

namespace EventManagementFPT.Hubs
{
    public class SignalRServer : Hub
    {
        private readonly IEventService _eventService;
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;

        public SignalRServer(IEventService eventService, ICommentService commentService, IUserService userService)
        {
            _eventService = eventService;
            _commentService = commentService;
            _userService = userService;
        }

        public async Task SendComment(string eventId, string content)
        {
            var _event = await _eventService.GetEventByID(Guid.Parse(eventId));

            if (_event == null)
            {
                throw new Exception("Event not found");
            }

            var uid = Guid.Parse(GetUsername());

            var comment = await _commentService.AddNewComment(new Comment
            {
                CommentId = Guid.NewGuid(),
                EventId = Guid.Parse(eventId),
                Content = content,
                UserId = uid,
                CreateDate = DateTime.Now,
                IsParent = true,
                Status = true,
            });
            
            var user = _userService.GetUserByUserID(uid);

            comment.Event = null;
            comment.User.Comments = null;
            comment.User.Reports = null;
            comment.User.UserEvents = null;
            comment.User.EventLikes = null;
            comment.User.Avatar = user.Avatar;
            comment.User.Name = user.Name;
            
            await Clients.Group(eventId).SendAsync("ReceiveComment", comment);
        }

        private string GetUsername()
        {
            return Context.User?.Claims.FirstOrDefault(
                x => x.Type == ClaimTypes.NameIdentifier
            )?.Value;
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            var username = GetUsername();

            await Clients.Group(groupName).SendAsync("Send", $"{username} has joined the group.");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            var username = GetUsername();

            await Clients.Group(groupName).SendAsync("Send", $"{username} has left the group.");
        }
    }
}