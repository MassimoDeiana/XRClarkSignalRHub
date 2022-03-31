namespace XRClarkSignalR.Api.Hubs;

public interface IClient
{
    Task ReceiveMessage(MessageFromClient message);

    Task ReceiveListOfUser(List<User> clients);

    Task UserRegistered(User user);
}