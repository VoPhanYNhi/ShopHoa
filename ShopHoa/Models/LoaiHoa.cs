using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShopHoa.Models
{
    public class LoaiHoa
    {
        [Key]
        public int LoaiId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên loại")]
        [Display(Name = "Loại hoa")]
        public string TenLoai { get; set; }

        public ICollection<Hoa> Hoas { get; set; }
    }
}
