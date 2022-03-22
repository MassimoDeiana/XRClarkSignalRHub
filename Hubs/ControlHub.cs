using System.Diagnostics;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.SignalR;
using XRClarkSignalR.Api.Hubs.Client;

namespace XRClarkSignalR.Api.Hubs;

public class ControlHub : Hub<IClient>
{
    public async Task SendMessage(MessageFromClient message)
    {
        Console.WriteLine("Send Message");
        await Clients.Others.ReceiveMessage(message);
    }

    public async Task StartSimulation(User user)
    {
        Console.WriteLine("start simulation");
        //Console.WriteLine(user.Name + ":" + user.Model + ":" + user.MACAdress + ":" + user.id);
        await Clients.Client(user.id).ReceiveMessage(new MessageFromClient
        {
            User = "Master",
            Message = "Start"
        });
    }

    public async Task StopSimulation(User user)
    {
        Console.WriteLine("stop simulation");
        //Console.WriteLine(user.Name + ":" + user.Model + ":" + user.MACAdress + ":" + user.id);
        await Clients.Client(user.id).ReceiveMessage(new MessageFromClient
        {
            User = "Master",
            Message = "Stop"
        });
    }

    public void RegisterWebClient()
    {
        WebClient.id = Context.ConnectionId;
        Console.WriteLine("Web id :" + WebClient.id);
    }

    public async Task SendListOfUser()
    {
        Console.WriteLine(ConnectedUser.users[0].ToString());
        await Clients.Client(WebClient.id).ReceiveListOfUser(ConnectedUser.users);
        Console.WriteLine("On send la liste");
    }
    
    public async Task RegisterUser(User user)
    {
        Console.WriteLine("Register user");
        user.id = Context.ConnectionId;
        ConnectedUser.users.Add(user);
        await Clients.Caller.UserRegistered(user);
        await SendListOfUser();
    }
    
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        Console.WriteLine("Deconnexion : " + Context.ConnectionId);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
        ConnectedUser.users.Remove(new User
        {
            id = Context.ConnectionId
        });
        await SendListOfUser();
        await base.OnDisconnectedAsync(exception);
    }

    public async Task SetActiveScene(string scene)
    {
        Console.WriteLine("ici");
        ConnectedUser.users.Find(u=> u.Equals(new User{id = Context.ConnectionId}))!.ActiveScene = scene ;
        Console.WriteLine("la");
        await SendListOfUser();
    }
}