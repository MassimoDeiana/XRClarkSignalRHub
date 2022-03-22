namespace XRClarkSignalR.Api.Hubs.Client;

public interface IClient
{
    Task ReceiveMessage(MessageFromClient message);

    Task ReceiveListOfUser(List<User> clients);

    Task UserRegistered(User user);
}