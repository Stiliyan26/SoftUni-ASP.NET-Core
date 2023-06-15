using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Infrastructure.Migrations
{
    public partial class HouseIsActiveAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Houses",
                type: "bit",
                nullable: false,
                defaultValue: false);

           /* migrationBuilder.UpdateData(
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
*/
            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsActive",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Houses");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0745ddcc-cc5a-4056-9a79-9abefb9fdadf", "AQAAAAEAACcQAAAAEFd+zflrPx9Kwwaf1qtGuzl87Y4mSjai4IXfev6rqsyah4TZxDuWMdPAkVa29xvJMQ==", "a2443c58-d600-4ce2-a8e1-ffcdcd3d25ca" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1858265-e605-48fc-b56d-1d228d46fdbb", "AQAAAAEAACcQAAAAEPLiBYVxNOl3jxFA3/BM2QP+E+dCkeHM68WkZ6wvATSvNCk3Rk/+57zBfl76rE4+1A==", "c397ec55-b1b6-48be-8f92-f112deecb7a8" });
        }
    }
}
