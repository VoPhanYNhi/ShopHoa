using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ShopHoa.Models;

namespace ShopHoa.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ShopHoa.Models.Hoa> Hoa { get; set; }
        public DbSet<ShopHoa.Models.LoaiHoa> LoaiHoa { get; set; }
        public DbSet<ShopHoa.Models.Bill> Bill { get; set; }
        public DbSet<ShopHoa.Models.BillDetail> BillDetail { get; set; }
    }
}
