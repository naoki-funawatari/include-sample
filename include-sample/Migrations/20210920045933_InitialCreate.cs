using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace include_sample.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "first_layers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_first_layers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "second_layers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstLayerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_second_layers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_second_layers_first_layers_FirstLayerId",
                        column: x => x.FirstLayerId,
                        principalTable: "first_layers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "third_layers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SecondLayerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_third_layers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_third_layers_second_layers_SecondLayerId",
                        column: x => x.SecondLayerId,
                        principalTable: "second_layers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fourth_layers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ThirdLayerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fourth_layers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fourth_layers_third_layers_ThirdLayerId",
                        column: x => x.ThirdLayerId,
                        principalTable: "third_layers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_fourth_layers_ThirdLayerId",
                table: "fourth_layers",
                column: "ThirdLayerId");

            migrationBuilder.CreateIndex(
                name: "IX_second_layers_FirstLayerId",
                table: "second_layers",
                column: "FirstLayerId");

            migrationBuilder.CreateIndex(
                name: "IX_third_layers_SecondLayerId",
                table: "third_layers",
                column: "SecondLayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fourth_layers");

            migrationBuilder.DropTable(
                name: "third_layers");

            migrationBuilder.DropTable(
                name: "second_layers");

            migrationBuilder.DropTable(
                name: "first_layers");
        }
    }
}
