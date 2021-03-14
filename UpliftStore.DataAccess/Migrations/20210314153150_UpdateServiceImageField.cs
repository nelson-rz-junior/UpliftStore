using Microsoft.EntityFrameworkCore.Migrations;

namespace UpliftStore.DataAccess.Migrations
{
    public partial class UpdateServiceImageField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Services",
                newName: "ImageFile");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageFile",
                table: "Services",
                newName: "ImageUrl");
        }
    }
}
