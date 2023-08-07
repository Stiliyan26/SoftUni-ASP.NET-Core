using ChatSignalR.Models;

namespace ChatSignalR.Clients
{
    public interface IChatClient
    {
        Task ReceiveMessage(ChatMessage message);
    }
}
