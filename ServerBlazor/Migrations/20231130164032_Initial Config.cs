using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerBlazor.Migrations
{
    public partial class InitialConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    BlogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.BlogId);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Post_Blog_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blog",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comment_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Blog",
                columns: new[] { "BlogId", "Name" },
                values: new object[] { 1, "Fisking" });

            migrationBuilder.InsertData(
                table: "Blog",
                columns: new[] { "BlogId", "Name" },
                values: new object[] { 2, "Klatring" });

            migrationBuilder.InsertData(
                table: "Post",
                columns: new[] { "PostId", "BlogId", "Content", "Title" },
                values: new object[] { 1, 1, "Kort historie om fisking i Straumen", "Fisking i Straumen" });

            migrationBuilder.InsertData(
                table: "Post",
                columns: new[] { "PostId", "BlogId", "Content", "Title" },
                values: new object[] { 2, 1, "Kontakt meg for mer info", "Fiskekort til salgs" });

            migrationBuilder.InsertData(
                table: "Post",
                columns: new[] { "PostId", "BlogId", "Content", "Title" },
                values: new object[] { 3, 2, "Arrangeres Sommeren 2048", "Klatre Konkurranse i Narvik" });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "CommentId", "CommentText", "PostId" },
                values: new object[,]
                {
                    { 1, "Så kult, tenker jeg også må ta turen dit!", 1 },
                    { 2, "Hvordan var parkeringen ved elva?", 1 },
                    { 3, "Har sendt direktemelding", 2 },
                    { 4, "Klarer ikke vente!", 3 },
                    { 5, "Det gledes!", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PostId",
                table: "Comment",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_BlogId",
                table: "Post",
                column: "BlogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Blog");
        }
    }
}
