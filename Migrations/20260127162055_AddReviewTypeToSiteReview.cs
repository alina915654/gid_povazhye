using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewTypeToSiteReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiteReviews_Places_PlaceId",
                table: "SiteReviews");

            migrationBuilder.DropIndex(
                name: "IX_SiteReviews_PlaceId",
                table: "SiteReviews");

            migrationBuilder.AlterColumn<int>(
                name: "PlaceId",
                table: "SiteReviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ReviewType",
                table: "SiteReviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewType",
                table: "SiteReviews");

            migrationBuilder.AlterColumn<int>(
                name: "PlaceId",
                table: "SiteReviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
