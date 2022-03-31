namespace XRClarkSignalR.Api.Hubs;

public class User 
{
    public string Name { get; set; }

    public string Model { get; set; }

    public string MACAdress { get; set; }

    public string ActiveScene { get; set; }

    public List<string> Scenes { get; set; }

    public List<string> Machines { get; set; }

    public string Id { get; set; }

    protected bool Equals(User other)
    {
        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((User) obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return Id + ":" + Name + ":" + ActiveScene;
    }
}