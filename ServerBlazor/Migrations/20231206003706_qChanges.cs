using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerBlazor.Migrations
{
    public partial class qChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "06433c15-8e4a-4b65-aeaf-8a396c4d199c", "AQAAAAEAACcQAAAAEAkdxCQitH0IkDam/6ACiFje7yE2wO3cmgXIwojW+kgiAqWVa5fRmlzlcgCGR9aDvA==", "d0e9a609-5278-4760-acc1-e5ce18d47c15" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "818867a5-7b7a-4d1d-bf2c-c5e1417fbefb", "AQAAAAEAACcQAAAAELtZSmMUFQ5C6rPWMcsFy/RVVrXMZRkr7ax4i2H7gZ8zcfpWX+zG7eagTK158UpiIA==", "ab6f5064-45a3-417c-b894-e7c329a98867" });
        }
    }
}
