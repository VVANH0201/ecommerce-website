namespace BTL.Models.APIModels
{
    public class CartItems
    {
        public string MaGioHang { get; set; } = null!;
        public string MaDienThoai { get; set; } = null!;
        public string TenDienThoai { get; set; }
        public int? sl { get; set; }
        public double? dongia { get; set; }
        public string anh { get; set; }
        public double total { get; set; }
    }
}
