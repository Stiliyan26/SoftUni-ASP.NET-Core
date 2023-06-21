using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Infrastructure.Migrations
{
    public partial class UserExtended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0f7fa852-4560-4e67-ab5b-241b8912029e", "AQAAAAEAACcQAAAAEKz9KwJGqL6VgiAnvFJJCwQ0YTouHbXtAAfYAdwooGHy0JhkdnoEKiLe+np0gmiN9Q==", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b9b00431-3aba-468f-8b64-9356166cf711", "AQAAAAEAACcQAAAAEGhAtZMusSukygyAycTpdRzWMtflCt2YiHzg5uIppsprFEJIsT33yc90iYbIshao8w==", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b17604c5-629b-46e3-aa33-b7ee1d6ffa7e", "AQAAAAEAACcQAAAAEAaQ4RVvtAgZL3UUHVS/FTkO9vot0TXB8DNAuk/tV0EmbL+dtS5M00LWVHDCjypcCA==", "185c0378-72fb-483e-9f4b-0ac4f3fcafb4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "60892e96-8495-4f0c-9ccf-8a135d071878", "AQAAAAEAACcQAAAAEBAZ3CQvr+uDTO+cl1qyajho5MYoFEs80O2v3hTJHFEQrtwAyi/+mdm1t6mzKtN1hw==", "b127daa8-5dae-4462-a571-49294e13f718" });
        }
    }
}
