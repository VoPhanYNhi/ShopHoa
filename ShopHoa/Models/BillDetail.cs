using System.ComponentModel.DataAnnotations.Schema;

namespace ShopHoa.Models
{
    public class BillDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillDetailId { get; set; }

        public int BillId { get; set; }

        public int HoaId { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public int Amount { get; set; }

        public Bill Bill { get; set; }

        public Hoa Hoa { get; set; }
    }
}
