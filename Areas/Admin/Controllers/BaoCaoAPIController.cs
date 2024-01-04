using BTL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTL.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaoCaoAPIController : ControllerBase
    {
        Web6ContextContext db = new Web6ContextContext();
        [Route("GetSLDT")]
        [HttpGet]
        public IEnumerable<int> GetSLDT()
        {
            var sl = 0;
            IList<DienThoai> dienthoai = new List<DienThoai>();
            var dt = db.DienThoais.Include(x => x.MaHsxNavigation).ToList();
            foreach (var s in dt)
            {
                dienthoai.Add(new DienThoai
                {
                    MaDienThoai = s.MaDienThoai,
                    TenDienThoai = s.TenDienThoai,
                    Ram = s.Ram,
                    Rom = s.Rom,
                    Pin = s.Pin,
                    HeDieuHanh = s.HeDieuHanh,
                    ManHinh = s.ManHinh,
                    KichThuoc = s.KichThuoc,
                    Anh = s.Anh,
                    GiaBan = s.GiaBan,
                    GiaNhap = s.GiaNhap,
                    SoLuong = s.SoLuong,

                    TgbaoHanh = s.TgbaoHanh,
                    MaHsx = s.MaHsx
                });
            }
            for (int i = 0; i < dienthoai.Count; i++)
            {
                sl += (int)dienthoai[i].SoLuong;
            }
            int SL = dienthoai.Count;
            yield return sl;
        }

        [Route("GetSLNhanVien")]
        [HttpGet]
        public IEnumerable<int> GetSLNhanVien()
        {
            IList<NhanVien> Nhanvien = new List<NhanVien>();
            var nv = db.NhanViens.Include(x => x.MaChucVuNavigation).ToList();
            foreach (var s in nv)
            {
                Nhanvien.Add(new NhanVien
                {
                    MaNhanVien = s.MaNhanVien,
                    TenNhanVien = s.TenNhanVien,
                    UserName = s.UserName,
                    NgaySinh = s.NgaySinh,
                    GioiTinh = s.GioiTinh,
                    MaChucVu = s.MaChucVu,
                    SoDienThoai = s.SoDienThoai,
                    DiaChi = s.DiaChi,
                    AnhDaiDien = s.AnhDaiDien
                });
            }

            yield return Nhanvien.Count;
        }

        [Route("GetSLHoaDon")]
        [HttpGet]
        public IEnumerable<int> GetSLHoaDon()
        {
            IList<HoaDonBan> hoadonban = new List<HoaDonBan>();
            var hdb = db.HoaDonBans.Include(x => x.MaNhanVienNavigation).Include(x => x.MaKhachHangNavigation).ToList();
            yield return hdb.Count;
        }

        [Route("TkeTheoLoai")]
        [HttpGet]
        public List<Object> TkeTheoLoai()
        {
            List<Object> list = new List<object>();

            List<String> name = new List<string>();
            List<int> sl = new List<int>();

            var hsx = db.DienThoais.Include(x => x.MaHsxNavigation).GroupBy(x => x.MaHsxNavigation.TenHsx).Select(x => new { TenHsx = x.Key, Total = x.Sum(y => y.SoLuong) }).ToList();
            foreach (var h in hsx)
            {
                name.Add(h.TenHsx);
                sl.Add((int)h.Total);
            }
            list.Add(name);
            list.Add(sl);
            return list;
        }

        [Route("TkeTheoThang")]
        [HttpGet]
        public List<Object> TkeTheoThang(String Nam)
        {
            int y = int.Parse(Nam);
            List<Object> list = new List<object>();

            List<int> name = new List<int>();
            List<int> sl = new List<int>();

            var orders = db.HoaDonBans.Where(x => x.NgayBan.Year == y)
            .GroupBy(o => o.NgayBan.Month)
            .Select(g => new { Month = g.Key, TotalOrders = g.Sum(o => o.TongTien) })
            .ToList();

            //var hsx = db.DienThoais.Include(x => x.MaHsxNavigation).GroupBy(x => x.MaHsxNavigation.TenHsx).Select(x => new { TenHsx = x.Key, Total = x.Sum(y => y.SoLuong) }).ToList();
            foreach (var h in orders)
            {
                name.Add(h.Month);
                sl.Add((int)h.TotalOrders);
            }
            list.Add(name);
            list.Add(sl);
            return list;
        }
    }
}
