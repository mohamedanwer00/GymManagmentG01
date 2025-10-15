using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagmentDAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Members_MemberId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_MemberId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Sessions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "Sessions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_MemberId",
                table: "Sessions",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Members_MemberId",
                table: "Sessions",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }
    }
}
