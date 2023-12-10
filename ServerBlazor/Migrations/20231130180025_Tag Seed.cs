using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerBlazor.Migrations
{
    public partial class TagSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "TagId", "TagText" },
                values: new object[] { 1, "Fiske" });

            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "TagId", "TagText" },
                values: new object[] { 2, "Klatring" });

            migrationBuilder.InsertData(
                table: "PostTag",
                columns: new[] { "PostId", "TagId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "PostTag",
                columns: new[] { "PostId", "TagId" },
                values: new object[] { 2, 1 });

            migrationBuilder.InsertData(
                table: "PostTag",
                columns: new[] { "PostId", "TagId" },
                values: new object[] { 3, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PostTag",
                keyColumns: new[] { "PostId", "TagId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "PostTag",
                keyColumns: new[] { "PostId", "TagId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "PostTag",
                keyColumns: new[] { "PostId", "TagId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "TagId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "TagId",
                keyValue: 2);
        }
    }
}
