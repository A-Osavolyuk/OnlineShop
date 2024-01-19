namespace OnlineShop.CouponApi.Models.DTOs
{
    public class CouponDto
    {
        public string CouponName { get; set; } = "";
        public double CouponBonus { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime ExpiredAt { get; set; } = DateTime.Now;
    }
}
