namespace XRClarkSignalR.Api.Hubs;

public static class ConnectedUser  
{  
    public static List<User> users = new List<User>();

    public static string ToString()
    {
        string res = "";
        foreach (var user in users)
        {
            res += user.ToString() + "\n";
        }

        return res;
    }
}  