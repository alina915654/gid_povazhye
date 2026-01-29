using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddPlaceIdToReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlaceId",
                table: "SiteReviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SiteReviews_PlaceId",
                table: "SiteReviews",
                column: "PlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_SiteReviews_Places_PlaceId",
                table: "SiteReviews",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiteReviews_Places_PlaceId",
                table: "SiteReviews");

            migrationBuilder.DropIndex(
                name: "IX_SiteReviews_PlaceId",
                table: "SiteReviews");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "SiteReviews");
        }
    }
}
