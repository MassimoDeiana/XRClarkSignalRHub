using Microsoft.EntityFrameworkCore;

namespace XRClarkSignalR.Api.Hubs.Model;

public class XrclarkContext : DbContext
{
    
    public DbSet<Machine> Machines { get; set; }
    public string DbPath { get; }
    
    public XrclarkContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "xrclark.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class Machine
{
    public int MachineId { get; set; }
    public string MachineName { get; set; }
    public double MachineMass { get; set; }
    public double MachinePower { get; set; }
    public double MachineSpeed { get; set; }
    public double MachineTension { get; set; }
    public double MachineMaxHeight { get; set; }
    public double MachineWheels { get; set; }
    public double MachineMaxWeight { get; set; }
}

