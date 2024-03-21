using CollectionManager.Models.Socials;
using CollectionManager.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace CollectionManager.SignalR
{
    public class SocialsHub : Hub
    {
        public const string HubUrl = "/socialHub";
        ISocialsService _socialsService;
        public SocialsHub(ISocialsService socialsService)
        {
            _socialsService = socialsService;
        }

        public async Task Broadcast(CommentModel comment)
        {
            if (comment != null)
            {
                await _socialsService.CreateComment(comment).ConfigureAwait(false);
                await Clients.Group(comment.ItemId).SendAsync("BroadcastComment", comment);
            }
        }
        public async Task BroadcastDelete(CommentModel comment)
        {
            if (comment != null)
            {
                await _socialsService.RemoveComment(comment.CommentModelId).ConfigureAwait(false);
                await Clients.Group(comment.ItemId).SendAsync("BroadcastCommentDelete", comment);
            }
        }

        public async Task SetGroup(string hubId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, hubId);
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId} connected");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            await base.OnDisconnectedAsync(e);
        }
    }
}