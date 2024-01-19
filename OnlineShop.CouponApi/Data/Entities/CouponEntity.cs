namespace OnlineShop.CouponApi.Data.Entities
{
    public class CouponEntity
    {
        public Guid CouponId { get; set; } = Guid.NewGuid();
        public string CouponName { get; set; } = "";
        public double CouponBonus { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime ExpiredAt { get; set; } = DateTime.Now;
    }
}
