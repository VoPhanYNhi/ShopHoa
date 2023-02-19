using Microsoft.EntityFrameworkCore.Internal;
using ShopHoa.Data;

namespace ShopHoa.Models
{
    public class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any data
            if (context.Hoa.Any())
            {
                return;   // DB has been seeded
            }

            var loaihoas = new LoaiHoa[]
            {
                new LoaiHoa{TenLoai="Hoa Sinh Nhật"},
                new LoaiHoa{TenLoai="Hoa Khai Trương"},
                new LoaiHoa{TenLoai="Hoa Cưới"},
                new LoaiHoa{TenLoai="Hoa Tốt Nghiệp"},
                new LoaiHoa{TenLoai="Hoa Tình Yêu"}
            };

            context.LoaiHoa.AddRange(loaihoas);
            context.SaveChanges();

            var hoas = new Hoa[]
            {
                new Hoa{TenHoa="Bó hoa hồng sáp 5 bông quà tặng sinh nhật, sự kiện",Gia=90000,SoLuong=10,
                    MoTa="Bó hoa hồng sáp thơm 5 bông tặng bạn gái, mẹ, cô giáo, người thân yêu\r\n\r\nBó to đẹp phối màu nhẹ nhàng, ngọt ngào phong cách Hàn Quốc mới nhất, không cần phân biệt độ tuổi ai nhận cũng sẽ thích mê\r\n\r\nBó hoa sáp kèm túi cũng là món quà tặng ý nghĩa cho đồng nghiệp, khách hàng, bạn bè, lưu giữ được lâu dài làm kỉ niệm, trưng trong phòng, làm đồ trang trí",
                    Image="sn1.jpg",LoaiRefId=1},
                new Hoa{TenHoa="Combo Thiệp bó hoa khô mini set quà tặng sinh nhật",Gia=85000,SoLuong=10,
                    MoTa="Combo Thiệp bó hoa khô mini set quà tặng sinh nhật người yêu bạn bè đồng nghiệp thấy cô ngày lễ kỷ niệm",
                    Image="sn2.jpg",LoaiRefId=1},
                new Hoa{TenHoa="Set hộp quà bó hoa khô và thiệp phối màu vintage",Gia=300000,SoLuong=10,
                    MoTa="Full set bao gồm:\r\n\r\nBó hoa khô như ảnh\r\n\r\nHộp quà giấy cứng cao cấp\r\n\r\nThiệp hoa khô cao cấp\r\n\r\nGiấy lụa lót hộp quà (tặng kèm)",
                    Image="sn3.jpg",LoaiRefId=1},
                new Hoa{TenHoa="Lẵng hoa 2 tầng chúc mừng khai trương",Gia=1500000,SoLuong=10,
                    MoTa="Sản phẩm gồm có:\r\n\r\n- Hoa hồng nhập khẩu Ohara hồng\r\n\r\n- Hoa hồng nhập khẩu Lâu Lan\r\n\r\n- Hoa Hồng sen nhí Đà Lạt\r\n\r\n-Hoa Hồng kem dâu Đà Lạt\r\n\r\n- Hoa cúc trắng\r\n\r\n- Hoa Baby\r\n\r\nCùng các phụ kiện khác.",
                    Image="kt1.jpg",LoaiRefId=2},
                new Hoa{TenHoa="Hoa cưới Rose Mix Baby",Gia=900000,SoLuong=10,
                    MoTa="Bó hoa cưới cổ điển và lãng mạn, được kết từ hoa hồng kem Đà Lạt mix cùng hoa baby nhập khẩu.",
                    Image="hc1.jpg",LoaiRefId=3},
                new Hoa{TenHoa="Hoa cưới cầm tay",Gia=550000,SoLuong=10,
                    MoTa="Gửi gắm thật nhiều những yêu thương trong từng nhành hoa mang gam sắc nắng trong veo và tươi mới, những bó hoa cầm tay tuy nhỏ xinh nhưng lại mang trong mình sứ mệnh cao cả để góp phần tô điểm cho ngày trọng đại lứa đôi.\r\nNguyên liệu: Hoa hồng, cúc tana, thiên ngân, hoa tuyết lá đô la và một số hoa lá phụ khác.",
                    Image="hc2.jpg",LoaiRefId=3},
                new Hoa{TenHoa="Bó hoa hướng dương tốt nghiệp",Gia=300000,SoLuong=10,
                    MoTa="Những bó hoa hướng dương rực rỡ sắc vàng trao tay như gửi gắm đến người nhận một khung trời rộng lớn với muôn vàn điều tốt đẹp, hy vọng trong tương lai. ",
                    Image="tn1.jpg",LoaiRefId=4},
                new Hoa{TenHoa="Bó hoa hồng đỏ trái tim",Gia=1100000,SoLuong=10,
                    MoTa="Mẫu gồm có các loại hoa:\r\n+50 hoa hồng đỏ\r\n+ hoa baby trắng\r\n+ và các lá phụ khác",
                    Image="ty1.jpg",LoaiRefId=5},
                new Hoa{TenHoa="Bó hồng đỏ Valentine",Gia=500000,SoLuong=10,
                    MoTa="Mang ý nghĩa tình yêu vĩnh viễn",
                    Image="ty2.jpg",LoaiRefId=5},
                new Hoa{TenHoa="Giỏ hoa hồng trái tim",Gia=1000000,SoLuong=10,
                    MoTa="Hoa hồng đỏ là biểu tượng của tình yêu nồng nàn và bất diệt. Không chỉ mang thông điệp yêu thương mà hoa hồng đỏ còn mang ý nghĩa về niềm hạnh phúc vô biên, may mắn và giàu có... Vì thế, giỏ hoa thích hợp chọn làm món quà ý nghĩa, thay cho lời chúc hạnh phúc, sung túc viên mãn.\r\n\r\nVới ý nghĩa này, giỏ hoa thích hợp chọn làm hoa sinh nhật, hoa tình yêu, hoa chúc mừng, hoa khai trương... Gửi đến những người thân yêu trong cuộc sống.",
                    Image="ty3.jpg",LoaiRefId=5}

            };

            context.Hoa.AddRange(hoas);
            context.SaveChanges();
        }
    }
}
