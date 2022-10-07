using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_post.Migrations
{
    public partial class AddCategoriesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Pizzas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Pizze classiche" },
                    { 2, "Pizze bianche" },
                    { 3, "Pizze vegetariane" },
                    { 4, "Pizze di mare" }
                });

            migrationBuilder.UpdateData(
                table: "Pizzas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Pizzas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Pizzas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Pizzas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CategoryId",
                value: 1);

            migrationBuilder.InsertData(
                table: "Pizzas",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Photo", "Price" },
                values: new object[,]
                {
                    { 5, 3, "La classica pizza vegetariana", "Ortolana", "ortolana.png", 6.50m },
                    { 6, 2, "La classica pizza bianca", "Marinara", "marinara.png", 5.00m },
                    { 7, 4, "La classica pizza di mare", "Salmone e Rucola", "salmone_e_rucola.png", 8.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pizzas_CategoryId",
                table: "Pizzas",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizzas_Categories_CategoryId",
                table: "Pizzas",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizzas_Categories_CategoryId",
                table: "Pizzas");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Pizzas_CategoryId",
                table: "Pizzas");

            migrationBuilder.DeleteData(
                table: "Pizzas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Pizzas",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Pizzas",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Pizzas");
        }
    }
}
