using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenFoodFacts.Persistence.Migrations
{
    public partial class createDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileHistorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    LinesRead = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalLines = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileHistorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<long>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    Imported_t = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", maxLength: 600, nullable: true),
                    Creator = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Created_t = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Last_modified_t = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Product_name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Quantity = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Brands = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Categories = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Labels = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Cities = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Purchase_places = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Stores = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Ingredients_Text = table.Column<string>(type: "TEXT", maxLength: 800, nullable: true),
                    Traces = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Serving_Size = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Serving_Quantity = table.Column<double>(type: "double(14,2)", nullable: true),
                    Nutriscore_Score = table.Column<double>(type: "double(14,2)", nullable: true),
                    Nutriscore_Grade = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Main_Category = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Image_Url = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileHistorys");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
