using AcFunDMJ_WASM.Shared;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace AcFunDMJ_WASM.Server.Hubs
{
    public class DanmakuHub : Hub<IDanmaku>
    {
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.Items["userId"] as string);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task Connect(string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            Context.Items.Add("userId", userId);
        }
    }

    public interface IDanmaku
    {
        public Task SendComment(Comment comment);
        public Task SendLike(Like like);
        public Task SendFollow(Follow follow);
        public Task SendEnter(Enter enter);
        public Task SendGift(Gift gift);
    }
}
