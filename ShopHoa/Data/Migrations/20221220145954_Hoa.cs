using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopHoa.Data.Migrations
{
    public partial class Hoa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoaiHoa",
                columns: table => new
                {
                    LoaiId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiHoa", x => x.LoaiId);
                });

            migrationBuilder.CreateTable(
                name: "Hoa",
                columns: table => new
                {
                    HoaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenHoa = table.Column<string>(nullable: false),
                    Gia = table.Column<int>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false),
                    MoTa = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    LoaiRefId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hoa", x => x.HoaId);
                    table.ForeignKey(
                        name: "FK_Hoa_LoaiHoa_LoaiRefId",
                        column: x => x.LoaiRefId,
                        principalTable: "LoaiHoa",
                        principalColumn: "LoaiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hoa_LoaiRefId",
                table: "Hoa",
                column: "LoaiRefId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hoa");

            migrationBuilder.DropTable(
                name: "LoaiHoa");
        }
    }
}
