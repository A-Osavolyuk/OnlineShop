using Microsoft.EntityFrameworkCore;
using OnlineShop.CouponApi.Data.Entities;

namespace OnlineShop.CouponApi.Data
{
    public class CouponDbContext(DbContextOptions<CouponDbContext> options) : DbContext(options)
    {
        public DbSet<CouponEntity> Coupons => Set<CouponEntity>();
    }
}
