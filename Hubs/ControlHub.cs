using System.Diagnostics;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.SignalR;

namespace XRClarkSignalR.Api.Hubs;

public class ControlHub : Hub<IClient>
{
    public async Task SendMessage(MessageFromClient message)
    {
        Console.WriteLine("Send Message");
        await Clients.Others.ReceiveMessage(message);
    }

    public async Task StartSimulation(User user, string scene)
    {
        Console.WriteLine("start simulation " + scene );
        //Console.WriteLine(user.Name + ":" + user.Model + ":" + user.MACAdress + ":" + user.id);
        await Clients.Client(user.Id).ReceiveMessage(new MessageFromClient
        {
            User = "Master",
            Message = scene
        });
    }

    public async Task StopSimulation(User user)
    {
        Console.WriteLine("stop simulation");
        //Console.WriteLine(user.Name + ":" + user.Model + ":" + user.MACAdress + ":" + user.id);
        await Clients.Client(user.Id).ReceiveMessage(new MessageFromClient
        {
            User = "Master",
            Message = "Menu"
        });
    }

    public void RegisterWebClient()
    {
        WebClient.Id = Context.ConnectionId;
        Console.WriteLine("Web id :" + WebClient.Id);
    }

    public async Task SendListOfUser()
    {
    //    Console.WriteLine("users : " + ConnectedUser.ToString());
    Console.WriteLine("client web : " + WebClient.Id);
    if (WebClient.Id != null)
    {
        Console.WriteLine("web client id not null");
        await Clients.Client(WebClient.Id).ReceiveListOfUser(ConnectedUser.users);
        Console.WriteLine(ConnectedUser.ToString());
        
    }
    //    Console.WriteLine("On send la liste");
    }
    
    public async Task RegisterUser(User user)
    {
        Console.WriteLine("Register user");
        user.Id = Context.ConnectionId;
        ConnectedUser.users.Add(user);
        await Clients.Caller.UserRegistered(user);
        await SendListOfUser();
    }

    /*public async Task RegisterAllScenes(List<string> scenes)
    {
        Console.WriteLine("registering scenes");
        Scenes.scenes = scenes;
        Console.WriteLine(" scenes registered");

    }*/
    
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        Console.WriteLine("Deconnexion : " + Context.ConnectionId);
        ConnectedUser.users.Remove(new User
        {
            Id = Context.ConnectionId
        });
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
        if (Context.ConnectionId != WebClient.Id)
        {
            //Console.WriteLine(Context.ConnectionId + " / " + WebClient.Id);
            await SendListOfUser();
        }
        else
        {
            WebClient.Id = null;
        }
        await base.OnDisconnectedAsync(exception);
    }

    
    
    public async Task SetActiveScene(string scene)
    {
        //Console.WriteLine("ici");
        ConnectedUser.users.Find(u=> u.Equals(new User{Id = Context.ConnectionId}))!.ActiveScene = scene ;
        //Console.WriteLine("la");
        await SendListOfUser();
    }
}