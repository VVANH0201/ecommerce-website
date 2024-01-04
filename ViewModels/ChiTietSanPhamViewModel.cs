using BTL.Models;

namespace BTL.ViewModels
{
	public class ChiTietSanPhamViewModel
	{
		public DienThoai DienThoai { get; set; }

		public List<Comment> Cmt { get; set; }

		public ChiTietSanPhamViewModel(DienThoai dienThoai, List<Comment> cmt)
		{
			DienThoai = dienThoai;
			Cmt = cmt;
		}
	}
}
