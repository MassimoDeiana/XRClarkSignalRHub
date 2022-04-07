namespace XRClarkSignalR.Api.Hubs;

public class MessageFromClient
{
    public string User { get; set; }

    public string Scene { get; set; }

    public string Machine { get; set; }

    public float Mass { get; set; }
}