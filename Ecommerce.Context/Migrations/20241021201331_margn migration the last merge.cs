using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Context.Migrations
{
    /// <inheritdoc />
    public partial class margnmigrationthelastmerge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0d6afe28-4923-4782-bb6a-1db4e5a08979", "AQAAAAIAAYagAAAAEN+ZFkWaRqN8cBFviz09TPf1Vz6B1cChP2mPY8vFaqidkWzexLEJ4qucTKY6WG8Sbg==", "6fdd4c10-ed81-4e1d-ab77-9da39c9b98da" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "22f9aff1-005d-4e1f-9289-441ddf61d48b", "AQAAAAIAAYagAAAAEH61oxUsAWIPq4aNQNTITtjHTgdcxMmQH1hLF5v2Sh0U6AkF2pGq2+eQpPcD87a0jA==", "d4af1f87-bede-432e-9d7e-4372e9322fad" });
        }
    }
}
