using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopDemo.Core.Migrations
{
    public partial class ProductTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Primary key"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Product name"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Product price"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Product in stock")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                },
                comment: "Product to sell");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
