namespace XRClarkSignalR.Api.Hubs;

public class Scene
{
    public string Name { get; set; }

    public List<ActionLog> Logs { get; set; } = new List<ActionLog>();
}