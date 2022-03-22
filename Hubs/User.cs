namespace XRClarkSignalR.Api.Hubs;

public class User
{
    public string Name { get; set; }

    public string Model { get; set; }

    public string MACAdress { get; set; }

    public string ActiveScene { get; set; }

    public string id { get; set; }

    protected bool Equals(User other)
    {
        return id == other.id;
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
        return id.GetHashCode();
    }

    public override string ToString()
    {
        return id + ":" + Name + ":" + ActiveScene;
    }
}