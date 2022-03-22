using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using XRClarkSignalR.Api.Hubs;
using XRClarkSignalR.Api.Hubs.Client;

namespace XRClarkSignalR.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServerController : Controller
{
    private readonly IHubContext<ControlHub> _controlHub;

    public ServerController(IHubContext<ControlHub> controlHub)
    {
        _controlHub = controlHub;
    }

    [HttpGet]
    public ActionResult<List<User>> Index()
    {
        //Console.WriteLine("requete");
        return ConnectedUser.users;
    }
}