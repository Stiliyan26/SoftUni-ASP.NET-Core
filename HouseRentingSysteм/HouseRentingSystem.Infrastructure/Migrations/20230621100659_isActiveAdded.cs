using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Infrastructure.Migrations
{
    public partial class isActiveAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "IsActive", "PasswordHash" },
                values: new object[] { "bb17086b-bcee-4a6c-ae00-14df9586c081", true, "AQAAAAEAACcQAAAAECrOMANNEXFslF9InTsN9P9bb4Lrz/ncfXjjZIPaUBmxpJtMMOdPuDhTEztrZ0ZVWA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "IsActive", "PasswordHash" },
                values: new object[] { "ada9231c-66e6-46da-9f70-7b19b3aa07a9", true, "AQAAAAEAACcQAAAAEMOCL4cRxQx4LiLiEsBZgBKufhch8w3kZxm/mf5jB7NasVAMre5q6oDmFCg2QIy91g==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0f7fa852-4560-4e67-ab5b-241b8912029e", "AQAAAAEAACcQAAAAEKz9KwJGqL6VgiAnvFJJCwQ0YTouHbXtAAfYAdwooGHy0JhkdnoEKiLe+np0gmiN9Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b9b00431-3aba-468f-8b64-9356166cf711", "AQAAAAEAACcQAAAAEGhAtZMusSukygyAycTpdRzWMtflCt2YiHzg5uIppsprFEJIsT33yc90iYbIshao8w==" });
        }
    }
}
