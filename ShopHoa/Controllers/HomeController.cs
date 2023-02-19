using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShopHoa.Data;
using ShopHoa.Models;

namespace ShopHoa.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Home
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Hoa.Include(h => h.LoaiHoa);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoa = await _context.Hoa
                .Include(h => h.LoaiHoa)
                .FirstOrDefaultAsync(m => m.HoaId == id);
            if (hoa == null)
            {
                return NotFound();
            }

            return View(hoa);
        }

        public async Task<IActionResult> TimHoa(string SearchString)
        {
            var hoa = await _context.Hoa
                .Include(h => h.LoaiHoa).ToListAsync();
            IQueryable<Hoa> hoaIQ = from s in _context.Hoa
                                             select s;
            if (!String.IsNullOrEmpty(SearchString))
            {
                hoaIQ = hoaIQ.Where(s => s.TenHoa.Contains(SearchString));
            }
            hoa = await hoaIQ.AsNoTracking().ToListAsync();
            return View(hoa);
        }

        public async Task<IActionResult> TimHoaChucMung(string SearchString)
        {
            var hoa = await _context.Hoa
                .Include(h => h.LoaiHoa).ToListAsync();
            IQueryable<Hoa> hoaIQ = from s in _context.Hoa
                                    select s;
            hoaIQ = hoaIQ.Where(s => s.LoaiRefId.Equals(2));
            if (!String.IsNullOrEmpty(SearchString))
            {
                hoaIQ = hoaIQ.Where(s => s.TenHoa.Contains(SearchString) && s.LoaiRefId.Equals(2));
            }
            hoa = await hoaIQ.AsNoTracking().ToListAsync();
            return View(hoa);
        }
        public async Task<IActionResult> TimHoaCuoi(string SearchString)
        {
            var hoa = await _context.Hoa
                .Include(h => h.LoaiHoa).ToListAsync();
            IQueryable<Hoa> hoaIQ = from s in _context.Hoa
                                    select s;
            hoaIQ = hoaIQ.Where(s => s.LoaiRefId.Equals(3));
            if (!String.IsNullOrEmpty(SearchString))
            {
                hoaIQ = hoaIQ.Where(s => s.TenHoa.Contains(SearchString) && s.LoaiRefId.Equals(3));
            }
            hoa = await hoaIQ.AsNoTracking().ToListAsync();
            return View(hoa);
        }
        public async Task<IActionResult> TimHoaSinhNhat(string SearchString)
        {
            var hoa = await _context.Hoa
                .Include(h => h.LoaiHoa).ToListAsync();
            IQueryable<Hoa> hoaIQ = from s in _context.Hoa
                                    select s;
            hoaIQ = hoaIQ.Where(s => s.LoaiRefId.Equals(1));
            if (!String.IsNullOrEmpty(SearchString))
            {
                hoaIQ = hoaIQ.Where(s => s.TenHoa.Contains(SearchString) &&  s.LoaiRefId.Equals(1));
            }
            hoa = await hoaIQ.AsNoTracking().ToListAsync();
            return View(hoa);

        }
        public async Task<IActionResult> TimHoaTinhYeu(string SearchString)
        {
            var hoa = await _context.Hoa
                .Include(h => h.LoaiHoa).ToListAsync();
            IQueryable<Hoa> hoaIQ = from s in _context.Hoa
                                    select s;
            hoaIQ = hoaIQ.Where(s => s.LoaiRefId.Equals(5));
            if (!String.IsNullOrEmpty(SearchString))
            {
                hoaIQ = hoaIQ.Where(s => s.TenHoa.Contains(SearchString) && s.LoaiRefId.Equals(5));
            }
            hoa = await hoaIQ.AsNoTracking().ToListAsync();
            return View(hoa);
        }

        public async Task<IActionResult> TimHoaTotNghiep(string SearchString)
        {
            var hoa = await _context.Hoa
                .Include(h => h.LoaiHoa).ToListAsync();
            IQueryable<Hoa> hoaIQ = from s in _context.Hoa
                                    select s;
            hoaIQ = hoaIQ.Where(s => s.LoaiRefId.Equals(4));
            if (!String.IsNullOrEmpty(SearchString))
            {
                hoaIQ = hoaIQ.Where(s => s.TenHoa.Contains(SearchString) && s.LoaiRefId.Equals(4));
            }
            hoa = await hoaIQ.AsNoTracking().ToListAsync();
            return View(hoa);
        }


        // Các phương thức liên quan đến GIỎ HÀNG

        // Đọc danh sách CartItem từ session
        List<CartItem> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString("shopcart");
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }

        // Lưu danh sách CartItem trong giỏ hàng vào session
        void SaveCartSession(List<CartItem> list)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(list);
            session.SetString("shopcart", jsoncart);
        }

        // Xóa session giỏ hàng
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove("shopcart");
        }


        // Cho hàng vào giỏ
        public async Task<IActionResult> AddToCart(int id)
        {
            var product = await _context.Hoa
                .FirstOrDefaultAsync(m => m.HoaId == id);
            if (product == null)
            {
                return NotFound("Sản phẩm không tồn tại");
            }
            var cart = GetCartItems();
            var item = cart.Find(p => p.Hoa.HoaId == id);
            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                cart.Add(new CartItem() { Hoa = product, Quantity = 1 });
            }
            SaveCartSession(cart);
            return RedirectToAction(nameof(ViewCart));
        }

        // Chuyển đến view xem giỏ hàng
        public IActionResult ViewCart()
        {
            return View(GetCartItems());
        }

        //Xóa 1 mặt hàng
        public IActionResult RemoveItem(int id)
        {
            var cart = GetCartItems();
            var item = cart.Find(p => p.Hoa.HoaId == id);
            if (item != null)
            {
                cart.Remove(item);
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(ViewCart));
        }

        //Cập nhật số lượng 1 mặt hàng trong giỏ
        public IActionResult UpdateItem(int id, int quantity)
        {
            var cart = GetCartItems();
            var item = cart.Find(p => p.Hoa.HoaId == id);
            if (item != null)
            {
                item.Quantity = quantity;
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(ViewCart));
        }

        //Thanh toán
        public IActionResult CheckOut()
        {
            return View(GetCartItems()); //trang web yêu cầu nhập thông tin giao hàng (tt khách hàng & tt mặt hàng)
        }

        //Lưu đơn hàng, chi tiết đơn hàng
        [HttpPost, ActionName("CreateBill")]
        public async Task<IActionResult> CreateBill(string cusName, string cusPhone, string cusAddress, int billTotal)
        {
            //1. Lưu hóa đơn
            var bill = new Bill();
            bill.Date = DateTime.Now;
            bill.CustomerName = cusName;
            bill.CustomerPhone = cusPhone;
            bill.CustomerAddress = cusAddress;
            bill.BillTotal = billTotal;
            // cập nhật tổng tiền hóa đơn ?
            _context.Add(bill);
            await _context.SaveChangesAsync();

            //2. thêm chi tiết hóa đơn
            var cart = GetCartItems();

            int amount = 0;
            int total = 0;
            foreach (var i in cart)
            {
                var b = new BillDetail();
                b.BillId = bill.BillId;
                b.HoaId = i.Hoa.HoaId;
                amount = i.Hoa.Gia * i.Quantity;
                total += amount;
                b.Price = i.Hoa.Gia;
                b.Quantity = i.Quantity;
                b.Amount = amount;
                _context.Add(b);

            }

            await _context.SaveChangesAsync();

            //3. Xóa giỏ hàng
            ClearCart();

            return RedirectToAction(nameof(Thank));
        }

        public IActionResult Thank()
        {
            return View();
        }
    }
}
