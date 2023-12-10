using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerBlazor.Migrations
{
    public partial class addIdentityToEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Post",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Comment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Blog",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "818867a5-7b7a-4d1d-bf2c-c5e1417fbefb", "AQAAAAEAACcQAAAAELtZSmMUFQ5C6rPWMcsFy/RVVrXMZRkr7ax4i2H7gZ8zcfpWX+zG7eagTK158UpiIA==", "ab6f5064-45a3-417c-b894-e7c329a98867" });

            migrationBuilder.CreateIndex(
                name: "IX_Post_OwnerId",
                table: "Post",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_OwnerId",
                table: "Comment",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_OwnerId",
                table: "Blog",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_AspNetUsers_OwnerId",
                table: "Blog",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_OwnerId",
                table: "Comment",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_AspNetUsers_OwnerId",
                table: "Post",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_AspNetUsers_OwnerId",
                table: "Blog");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_OwnerId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_AspNetUsers_OwnerId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_OwnerId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Comment_OwnerId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Blog_OwnerId",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Blog");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "16f06278-d202-4ae1-aad5-1ad8fc1d0544", "AQAAAAEAACcQAAAAEC+ipHUHsCQOPXwMvVxiSBGTM2pE/udCDYbQqBxvoqcBURu67KTd9zAxJp+NsAvRSQ==", "7b71e37a-63c0-49f3-bc73-9ce310ead922" });
        }
    }
}
