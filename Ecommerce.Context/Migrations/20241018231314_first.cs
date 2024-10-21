using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Context.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c0a5102-3e4a-45ff-a186-de8f12cbda53", "AQAAAAIAAYagAAAAEDOFj9No13ABZ3i+RrGtYxMAy5AG3y78dgnkK6y4ErnPBEjfEPEMEv8DL+Da9lOXUw==", "790ea1da-8f51-42bc-8d13-8ca6a830a5ac" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8de8310c-6a99-42a6-a193-78d9e0c5beed", "AQAAAAIAAYagAAAAEOouF+9k1rOWLB9d/PCkHsrbsE0/l92k7TlVmnRsMUZU/OgcbfoWG16iGMrBdkHF9A==", "353d122b-6444-402c-a62c-d87f0be58192" });
        }
    }
}
