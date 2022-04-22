using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XRClarkSignalR.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    MachineId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MachineName = table.Column<string>(type: "TEXT", nullable: false),
                    MachineMass = table.Column<double>(type: "REAL", nullable: false),
                    MachinePower = table.Column<double>(type: "REAL", nullable: false),
                    MachineSpeed = table.Column<double>(type: "REAL", nullable: false),
                    MachineTension = table.Column<double>(type: "REAL", nullable: false),
                    MachineMaxHeight = table.Column<double>(type: "REAL", nullable: false),
                    MachineWheels = table.Column<double>(type: "REAL", nullable: false),
                    MachineMaxWeight = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.MachineId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Machines");
        }
    }
}
