namespace BTL.Models.APIModels
{
    public class Product
    {
        public string MaDienThoai { get; set; } = null!;

        public string? TenDienThoai { get; set; }

        public string? Anh { get; set; }

        public decimal? GiaBan { get; set; }

        public string MaHsx { get; set; } = null!;
    }
}
