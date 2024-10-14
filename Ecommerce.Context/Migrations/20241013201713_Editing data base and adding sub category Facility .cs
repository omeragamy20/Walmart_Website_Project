using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Context.Migrations
{
    /// <inheritdoc />
    public partial class EditingdatabaseandaddingsubcategoryFacility : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_Products_ProductId",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_SubCategories_SubCategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SubCategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Facilities_ProductId",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Facilities");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SubCategories",
                newName: "Name_en");

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                table: "SubCategories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "subCatFacility",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    facilityId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subCatFacility", x => x.Id);
                    table.ForeignKey(
                        name: "FK_subCatFacility_Facilities_facilityId",
                        column: x => x.facilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_subCatFacility_SubCategories_SubCategoryID",
                        column: x => x.SubCategoryID,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_subCatFacility_facilityId",
                table: "subCatFacility",
                column: "facilityId");

            migrationBuilder.CreateIndex(
                name: "IX_subCatFacility_SubCategoryID",
                table: "subCatFacility",
                column: "SubCategoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "subCatFacility");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                table: "SubCategories");

            migrationBuilder.RenameColumn(
                name: "Name_en",
                table: "SubCategories",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Facilities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubCategoryId",
                table: "Products",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_ProductId",
                table: "Facilities",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_Products_ProductId",
                table: "Facilities",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SubCategories_SubCategoryId",
                table: "Products",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id");
        }
    }
}
