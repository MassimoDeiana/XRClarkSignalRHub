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
    
    /**
     * Start a simulation with the given parameters for the given user
     */
    public async Task StartSimulation(User user, string scene, string machine, float mass)
    {
        try
        {
            await Clients.Client(user.Id).ReceiveMessage(new MessageFromClient
            {
                User = "Master",
                Scene = scene,
                Machine = machine,
                Mass = mass
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /**
     * Stop the simulation for the given user
     */
    public async Task StopSimulation(User user)
    {
        await Clients.Client(user.Id).ReceiveMessage(new MessageFromClient
        {
            User = "Master",
            Scene = "Menu"
        });
    }

    
    public void RegisterWebClient()
    {
        WebClient.Id = Context.ConnectionId;
    }

    /**
     * Send the list of users to the web client
     */
    public async Task SendListOfUser()
    {
        if (WebClient.Id != null)
        {
            await Clients.Client(WebClient.Id).ReceiveListOfUser(ConnectedUser.users);
        }
    }
    
    /**
     * Send the list of machines to the web client
     */
    public async Task RegisterUser(User user)
    {
        try
        {
            user.Id = Context.ConnectionId;
            ConnectedUser.users.Add(user);
            await Clients.Caller.UserRegistered(user);
            await SendListOfUser();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
    
    
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        ConnectedUser.users.Remove(new User
        {
            Id = Context.ConnectionId
        });
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
        if (Context.ConnectionId != WebClient.Id)
        {
            await SendListOfUser();
        }
        else
        {
            WebClient.Id = null;
        }
        await base.OnDisconnectedAsync(exception);
    }

    
    
    public async Task SetActiveScene(string sceneName)
    {
        ConnectedUser.users.Find(u=> u.Equals(new User{Id = Context.ConnectionId}))!.ActiveScene.Name = sceneName ;
        await SendListOfUser();
    }

    /**
     * Add log to the user
     */
    public async Task AddLog(ActionLog log)
    {
        ConnectedUser.users.Find(u=> u.Equals(new User{Id = Context.ConnectionId}))!.ActiveScene.Logs.Add(log) ;
        await SendListOfUser();
    }
}