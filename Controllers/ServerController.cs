using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using XRClarkSignalR.Api.Hubs;
using XRClarkSignalR.Api.Hubs.Model;

namespace XRClarkSignalR.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServerController : ControllerBase
{
    private readonly IHubContext<ControlHub> _controlHub;

    public ServerController(IHubContext<ControlHub> controlHub)
    {
        _controlHub = controlHub;
    }


    [HttpGet("users")]
    public ActionResult<List<User>> GetAllUsers()
    {
        //Console.WriteLine("requete");
        return ConnectedUser.users;
    }
    

    

    /*[HttpGet]
    [Route("/scenes")]
    public ActionResult<List<string>> GetAllScenes()
    {
        return Scenes.scenes;
    }*/


}