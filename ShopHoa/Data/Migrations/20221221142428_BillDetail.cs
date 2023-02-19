using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopHoa.Data.Migrations
{
    public partial class BillDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillDetail_Hoa_HoaId",
                table: "BillDetail");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "BillDetail");

            migrationBuilder.AlterColumn<int>(
                name: "HoaId",
                table: "BillDetail",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetail_Hoa_HoaId",
                table: "BillDetail",
                column: "HoaId",
                principalTable: "Hoa",
                principalColumn: "HoaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillDetail_Hoa_HoaId",
                table: "BillDetail");

            migrationBuilder.AlterColumn<int>(
                name: "HoaId",
                table: "BillDetail",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "BillDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetail_Hoa_HoaId",
                table: "BillDetail",
                column: "HoaId",
                principalTable: "Hoa",
                principalColumn: "HoaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
