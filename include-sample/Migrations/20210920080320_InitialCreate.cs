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
                name: "second_layers_a",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstLayerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_second_layers_a", x => x.Id);
                    table.ForeignKey(
                        name: "FK_second_layers_a_first_layers_FirstLayerId",
                        column: x => x.FirstLayerId,
                        principalTable: "first_layers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "second_layers_b",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstLayerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_second_layers_b", x => x.Id);
                    table.ForeignKey(
                        name: "FK_second_layers_b_first_layers_FirstLayerId",
                        column: x => x.FirstLayerId,
                        principalTable: "first_layers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "third_layers_a",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SecondLayerAId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_third_layers_a", x => x.Id);
                    table.ForeignKey(
                        name: "FK_third_layers_a_second_layers_a_SecondLayerAId",
                        column: x => x.SecondLayerAId,
                        principalTable: "second_layers_a",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "third_layers_b",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SecondLayerBId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_third_layers_b", x => x.Id);
                    table.ForeignKey(
                        name: "FK_third_layers_b_second_layers_b_SecondLayerBId",
                        column: x => x.SecondLayerBId,
                        principalTable: "second_layers_b",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_second_layers_a_FirstLayerId",
                table: "second_layers_a",
                column: "FirstLayerId");

            migrationBuilder.CreateIndex(
                name: "IX_second_layers_b_FirstLayerId",
                table: "second_layers_b",
                column: "FirstLayerId");

            migrationBuilder.CreateIndex(
                name: "IX_third_layers_a_SecondLayerAId",
                table: "third_layers_a",
                column: "SecondLayerAId");

            migrationBuilder.CreateIndex(
                name: "IX_third_layers_b_SecondLayerBId",
                table: "third_layers_b",
                column: "SecondLayerBId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "third_layers_a");

            migrationBuilder.DropTable(
                name: "third_layers_b");

            migrationBuilder.DropTable(
                name: "second_layers_a");

            migrationBuilder.DropTable(
                name: "second_layers_b");

            migrationBuilder.DropTable(
                name: "first_layers");
        }
    }
}
