using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using XRClarkSignalR.Api.Hubs;

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

    [HttpGet]
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