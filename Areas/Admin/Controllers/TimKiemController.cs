using BTL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("Admin/TimKiem")]


    public class TimKiemController : Controller
    {
        Web6ContextContext db = new Web6ContextContext();
        [Route("FindDT")]               
        
        //get
        [HttpPost]
        public IActionResult FindDT(string keyword)
        {
            List<DienThoai> ls = new List<DienThoai>();
            List<DienThoai> ls1 = new List<DienThoai>();
            ls1 = db.DienThoais.AsNoTracking().Include(x => x.MaHsxNavigation).OrderBy(x => x.TenDienThoai).Take(8).ToList();
            
            if (string.IsNullOrEmpty(keyword) || keyword.Length<1)
            {
                return PartialView("ListDTSearch", ls1);
            }
            ls = db.DienThoais.AsNoTracking().Include(x => x.MaHsxNavigation).Where(x => x.TenDienThoai.Contains(keyword)).OrderBy(x => x.TenDienThoai).Take(10).ToList();
            if (ls == null)
            {
                return PartialView("ListDTSearch", null);
            }
            else
            {
                return PartialView("ListDTSearch", ls);
            }
        }
        [Route("FindDTTheoHSX")]

        //get
        [HttpPost]
        public IActionResult FindDTTheoHSX(string keyword)
        {
            List<DienThoai> ls = new List<DienThoai>();
            List<DienThoai> ls1 = new List<DienThoai>();
            ls1 = db.DienThoais.AsNoTracking().Include(x => x.MaHsxNavigation).OrderBy(x => x.TenDienThoai).Take(8).ToList();

            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListDTSearch", ls1);
            }
            ls = db.DienThoais.AsNoTracking().Include(x => x.MaHsxNavigation).Where(x => x.MaHsxNavigation.TenHsx.Contains(keyword)).OrderBy(x => x.TenDienThoai).Take(10).ToList();
            if (ls == null)
            {
                return PartialView("ListDTSearch", null);
            }
            else
            {
                return PartialView("ListDTSearch", ls);
            }
        }
        [Route("FindDTTheoHDH")]

        //get
        [HttpPost]
        public IActionResult FindDTTheoHDH(string keyword)
        {
            List<DienThoai> ls = new List<DienThoai>();
            List<DienThoai> ls1 = new List<DienThoai>();
            ls1 = db.DienThoais.AsNoTracking().Include(x => x.MaHsxNavigation).OrderBy(x => x.TenDienThoai).Take(8).ToList();

            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListDTSearch", ls1);
            }
            ls = db.DienThoais.AsNoTracking().Include(x => x.MaHsxNavigation).Where(x => x.HeDieuHanh.Contains(keyword)).OrderBy(x => x.TenDienThoai).Take(10).ToList();
            if (ls == null)
            {
                return PartialView("ListDTSearch", null);
            }
            else
            {
                return PartialView("ListDTSearch", ls);
            }
        }

        //Tim kiem bang HSX
        [Route("FindHSX")]

        //get
        [HttpPost]
        public IActionResult FindHSX(string keyword)
        {
            List<HangSanXuat> ls = new List<HangSanXuat>();
            List<HangSanXuat> ls1 = new List<HangSanXuat>();
            ls1 = db.HangSanXuats.AsNoTracking().OrderByDescending(x => x.TenHsx).Take(8).ToList();

            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListHSXSearch", ls1);
            }
            ls = db.HangSanXuats.AsNoTracking().Where(x => x.TenHsx.Contains(keyword)).OrderByDescending(x => x.TenHsx).Take(10).ToList();
            if (ls == null)
            {
                return PartialView("ListHSXSearch", null);
            }
            else
            {
                return PartialView("ListHSXSearch", ls);
            }
        }



        [Route("FindNV")]

        //get
        [HttpPost]
        public IActionResult FindNV(string keyword)
        {
            List<NhanVien> ls = new List<NhanVien>();
            List<NhanVien> ls1 = new List<NhanVien>();
            ls1 = db.NhanViens.Include(x=>x.MaChucVuNavigation).AsNoTracking().OrderByDescending(x => x.TenNhanVien).Take(8).ToList();

            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListNVSearch", ls1);
            }
            ls = db.NhanViens.Include(x=>x.MaChucVuNavigation).AsNoTracking().Where(x => x.TenNhanVien.Contains(keyword)).OrderByDescending(x => x.TenNhanVien).Take(10).ToList();
            if (ls == null)
            {
                return PartialView("ListNVSearch", null);
            }
            else
            {
                return PartialView("ListNVSearch", ls);
            }
        }
        [Route("FindNVCV")]

        //get
        [HttpPost]
        public IActionResult FindNVCV(string keyword)
        {
            List<NhanVien> ls = new List<NhanVien>();
            List<NhanVien> ls1 = new List<NhanVien>();
            ls1 = db.NhanViens.Include(x => x.MaChucVuNavigation).AsNoTracking().OrderByDescending(x => x.TenNhanVien).Take(8).ToList();

            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListNVSearch", ls1);
            }
            ls = db.NhanViens.Include(x => x.MaChucVuNavigation).AsNoTracking().Where(x => x.MaChucVuNavigation.TenChucVu.Contains(keyword)).OrderByDescending(x => x.TenNhanVien).Take(10).ToList();
            if (ls == null)
            {
                return PartialView("ListNVSearch", null);
            }
            else
            {
                return PartialView("ListNVSearch", ls);
            }
        }
        [Route("FindNVSDT")]

        //get
        [HttpPost]
        public IActionResult FindNVSDT(string keyword)
        {
            List<NhanVien> ls = new List<NhanVien>();
            List<NhanVien> ls1 = new List<NhanVien>();
            ls1 = db.NhanViens.Include(x => x.MaChucVuNavigation).AsNoTracking().OrderByDescending(x => x.TenNhanVien).Take(8).ToList();

            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListNVSearch", ls1);
            }
            ls = db.NhanViens.Include(x => x.MaChucVuNavigation).AsNoTracking().Where(x => x.SoDienThoai.Contains(keyword)).OrderByDescending(x => x.TenNhanVien).Take(10).ToList();
            if (ls == null)
            {
                return PartialView("ListNVSearch", null);
            }
            else
            {
                return PartialView("ListNVSearch", ls);
            }
        }

        [Route("FindKH")]

        //get
        [HttpPost]
        public IActionResult FindKH(string keyword)
        {
            List<KhachHang> ls = new List<KhachHang>();
            List<KhachHang> ls1 = new List<KhachHang>();
            ls1 = db.KhachHangs.Include(x => x.UserNameNavigation).AsNoTracking().OrderByDescending(x => x.TenKhachHang).Take(8).ToList();

            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListKHSearch", ls1);
            }
            ls = db.KhachHangs.Include(x => x.UserNameNavigation).AsNoTracking().Where(x => x.TenKhachHang.Contains(keyword)).OrderBy(x => x.TenKhachHang).Take(10).ToList();
            if (ls == null)
            {
                return PartialView("ListKHSearch", null);
            }
            else
            {
                return PartialView("ListKHSearch", ls);
            }
        }

        [Route("FindKHSDT")]

        //get
        [HttpPost]
        public IActionResult FindKHSDT(string keyword)
        {
            List<KhachHang> ls = new List<KhachHang>();
            List<KhachHang> ls1 = new List<KhachHang>();
            ls1 = db.KhachHangs.Include(x => x.UserNameNavigation).AsNoTracking().OrderByDescending(x => x.TenKhachHang).Take(8).ToList();

            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListKHSearch", ls1);
            }
            ls = db.KhachHangs.Include(x => x.UserNameNavigation).AsNoTracking().Where(x => x.SoDienThoai.Contains(keyword)).OrderBy(x => x.TenKhachHang).Take(10).ToList();
            if (ls == null)
            {
                return PartialView("ListKHSearch", null);
            }
            else
            {
                return PartialView("ListKHSearch", ls);
            }
        }

        // tim kiem theo mahdb
        [Route("FindHDX")]
        [HttpPost]
        public IActionResult FindHDX(string keyword)
        {
            List<HoaDonBan> ls = new List<HoaDonBan>();
            List<HoaDonBan> ls1 = new List<HoaDonBan>();
            ls1 = db.HoaDonBans.Include(x => x.MaNhanVienNavigation).Include(x=>x.MaKhachHangNavigation).AsNoTracking().OrderBy(x => x.MaHdb).Take(8).ToList();

            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListHDXSearch", ls1);
            }
            ls = db.HoaDonBans.Include(x => x.MaNhanVienNavigation).Include(x => x.MaKhachHangNavigation).AsNoTracking().Where(x => x.MaHdb.Contains(keyword)).OrderBy(x => x.MaHdb).Take(10).ToList();
            if (ls == null)
            {
                return PartialView("ListHDXSearch", null);
            }
            else
            {
                return PartialView("ListHDXSearch", ls);
            }
        }

        [Route("FindHDXKH")]
        [HttpPost]
        public IActionResult FindHDXKH(string keyword)
        {
            List<HoaDonBan> ls = new List<HoaDonBan>();
            List<HoaDonBan> ls1 = new List<HoaDonBan>();
            ls1 = db.HoaDonBans.Include(x => x.MaNhanVienNavigation).Include(x => x.MaKhachHangNavigation).AsNoTracking().OrderBy(x => x.MaHdb).Take(8).ToList();

            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListHDXSearch", ls1);
            }
            ls = db.HoaDonBans.Include(x => x.MaNhanVienNavigation).Include(x => x.MaKhachHangNavigation).AsNoTracking().Where(x => x.MaKhachHangNavigation.TenKhachHang.Contains(keyword)).OrderBy(x => x.MaHdb).Take(10).ToList();
            if (ls == null)
            {
                return PartialView("ListHDXSearch", null);
            }
            else
            {
                return PartialView("ListHDXSearch", ls);
            }
        }

        [Route("FindHDXNV")]
        [HttpPost]
        public IActionResult FindHDXNV(string keyword)
        {
            List<HoaDonBan> ls = new List<HoaDonBan>();
            List<HoaDonBan> ls1 = new List<HoaDonBan>();
            ls1 = db.HoaDonBans.Include(x => x.MaNhanVienNavigation).Include(x => x.MaKhachHangNavigation).AsNoTracking().OrderBy(x => x.MaHdb).Take(8).ToList();

            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListHDXSearch", ls1);
            }
            ls = db.HoaDonBans.Include(x => x.MaNhanVienNavigation).Include(x => x.MaKhachHangNavigation).AsNoTracking().Where(x => x.MaNhanVienNavigation.TenNhanVien.Contains(keyword)).OrderBy(x => x.MaHdb).Take(10).ToList();
            if (ls == null)
            {
                return PartialView("ListHDXSearch", null);
            }
            else
            {
                return PartialView("ListHDXSearch", ls);
            }
        }

        //find Bang userLogin NV
        [Route("FindUserNV")]
        [HttpPost]
        public IActionResult FindUserNV(string keyword)
        {
            List<UserLogin> ls = new List<UserLogin>();
            List<UserLogin> ls1 = new List<UserLogin>();
            ls1 = db.UserLogins.Where(x => x.UserRole == 0).AsNoTracking().OrderBy(x => x.UserRole).Take(8).ToList();

            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListUserNVSearch", ls1);
            }
            ls = db.UserLogins.Where(x => x.UserRole == 0).AsNoTracking().Where(x => x.UserName.Contains(keyword)).OrderBy(x => x.UserName).Take(10).ToList();
            if (ls == null)
            {
                return PartialView("ListUserNVSearch", null);
            }
            else
            {
                return PartialView("ListUserNVSearch", ls);
            }
        }
        //
        [Route("FindUserNVEmail")]
        [HttpPost]
        public IActionResult FindUserNVEmail(string keyword)
        {
            List<UserLogin> ls = new List<UserLogin>();
            List<UserLogin> ls1 = new List<UserLogin>();
            ls1 = db.UserLogins.Where(x => x.UserRole == 0).AsNoTracking().OrderBy(x => x.UserRole).Take(8).ToList();

            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListUserNVSearch", ls1);
            }
            ls = db.UserLogins.Where(x => x.UserRole == 0).AsNoTracking().Where(x => x.Email.Contains(keyword)).OrderBy(x => x.UserName).Take(10).ToList();
            if (ls == null)
            {
                return PartialView("ListUserNVSearch", null);
            }
            else
            {
                return PartialView("ListUserNVSearch", ls);
            }
        }
        //find trên bảng DS User KH

        [Route("FindUserKH")]
        [HttpPost]
        public IActionResult FindUserKH(string keyword)
        {
            List<UserLogin> ls = new List<UserLogin>();
            List<UserLogin> ls1 = new List<UserLogin>();
            ls1 = db.UserLogins.Where(x => x.UserRole == 1).AsNoTracking().OrderBy(x => x.UserRole).Take(8).ToList();

            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListUserKHSearch", ls1);
            }
            ls = db.UserLogins.Where(x => x.UserRole == 1).AsNoTracking().Where(x => x.UserName.Contains(keyword)).OrderBy(x => x.UserName).Take(10).ToList();
            if (ls == null)
            {
                return PartialView("ListUserKHSearch", null);
            }
            else
            {
                return PartialView("ListUserKHSearch", ls);
            }
        }

        //
        [Route("FindUserKHEmail")]
        [HttpPost]
        public IActionResult FindUserKHEmail(string keyword)
        {
            List<UserLogin> ls = new List<UserLogin>();
            List<UserLogin> ls1 = new List<UserLogin>();
            ls1 = db.UserLogins.Where(x => x.UserRole == 0).AsNoTracking().OrderBy(x => x.UserRole).Take(8).ToList();

            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListUserKHSearch", ls1);
            }
            ls = db.UserLogins.Where(x => x.UserRole == 0).AsNoTracking().Where(x => x.Email.Contains(keyword)).OrderBy(x => x.UserName).Take(10).ToList();
            if (ls == null)
            {
                return PartialView("ListUserKHSearch", null);
            }
            else
            {
                return PartialView("ListUserKHSearch", ls);
            }
        }
    }
}
