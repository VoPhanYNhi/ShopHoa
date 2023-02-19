using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace ShopHoa.Models
{
    public class Hoa
    {
        [Key]
        public int HoaId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên")]
        [Display(Name = "Tên hoa")]
        public string TenHoa { get; set; }
        

        [DisplayFormat(DataFormatString = "{0:#,##0} đ")]
        [Display(Name = "Giá")]
        public int Gia { get; set; }

        [Display(Name = "Số lượng")]
        public int SoLuong { get; set; }

        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        [Display(Name = "Loại Hoa")]
        public int LoaiRefId { get; set; }

        [ForeignKey("LoaiRefId")]
        public LoaiHoa LoaiHoa { get; set; }
    }
}
