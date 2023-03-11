using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaloonNP.Data.Migrations
{
    /// <inheritdoc />
    public partial class CorrectStaffHairstyleRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staff_HairStyle_HairStyle_StaffId",
                table: "Staff_HairStyle");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_HairStyle_Staff_HairStyleId",
                table: "Staff_HairStyle");

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_HairStyle_HairStyle_HairStyleId",
                table: "Staff_HairStyle",
                column: "HairStyleId",
                principalTable: "HairStyle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_HairStyle_Staff_StaffId",
                table: "Staff_HairStyle",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staff_HairStyle_HairStyle_HairStyleId",
                table: "Staff_HairStyle");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_HairStyle_Staff_StaffId",
                table: "Staff_HairStyle");

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_HairStyle_HairStyle_StaffId",
                table: "Staff_HairStyle",
                column: "StaffId",
                principalTable: "HairStyle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_HairStyle_Staff_HairStyleId",
                table: "Staff_HairStyle",
                column: "HairStyleId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
