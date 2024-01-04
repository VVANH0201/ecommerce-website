using Azure;
using BTL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BTL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("Admin/HomeAdmin")]
    public class HomeAdminController : Controller
    {
        Web6ContextContext db = new Web6ContextContext();
        [Route("Index")]
        [Route("")]
        
        public IActionResult Index()
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;
            return View();
        }

        [Route("DanhMucSanPham")]
        public IActionResult DanhMucSanPham(int? page)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;
            int pageSize = 8;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            var lstsanpham = db.DienThoais.Include(x=>x.MaHsxNavigation).AsNoTracking().OrderBy(x => x.TenDienThoai);
            PagedList<DienThoai> lst = new PagedList<DienThoai>(lstsanpham, pageNumber, pageSize);
            return View(lst);
        }

        //Them moi dien thoai
        [Route("ThemDienThoai")]
        [HttpGet]
        public IActionResult ThemDienThoai()
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;
            ViewBag.MaHsx = new SelectList(db.HangSanXuats.ToList(), "MaHsx", "TenHsx");
            return View();
        }

        [Route("ThemDienThoai")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemDienThoai(DienThoai Dt, IFormFile formFile)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            Guid guid = Guid.NewGuid();

            string newfileName = guid.ToString();

            string fileextention = Path.GetExtension(formFile.FileName);

            string fileName = newfileName + fileextention;

            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", fileName);

            var stream = new FileStream(uploadpath, FileMode.Create);

            formFile.CopyToAsync(stream);
            ViewBag.MaHsx = new SelectList(db.HangSanXuats.ToList(), "MaHsx", "TenHsx");
            try
            {
                
                    Dt.Anh = fileName.ToString();
                    db.DienThoais.Add(Dt);
                    db.SaveChanges();
                    return RedirectToAction("DanhMucSanPham");
                
            } catch( Exception e)
            {
                return View("Index");
            }
            //if (ModelState.IsValid)
            //{
            //    //sanPham.Anh = fileName.ToString();
            //    db.DienThoais.Add(Dt);
            //    db.SaveChanges();
            //    //return RedirectToAction("DanhMucSanPham");
            //    return RedirectToAction("DanhMucSanPham");
            //}
            //else
            //{
            //    return View("Index");
            //}
            
        }

        [Route("ChiTietSanPham")]
        [HttpGet]
        public IActionResult ChiTietSanPham(String MaDienThoai)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            DienThoai? sp = db.DienThoais.Find(MaDienThoai);
            ViewBag.HangSX = db.HangSanXuats.Find(sp.MaHsx).TenHsx.ToString();
            return View(sp);
        }

        // sua

        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(String MaDienThoai)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            DienThoai? sp = db.DienThoais.Find(MaDienThoai);
            
            ViewBag.MaHSX = new SelectList(db.HangSanXuats.OrderBy(x => x.MaHsx), "MaHsx", "TenHsx");
            return View(sp);
        }

        [Route("SuaSanPham")]
        [HttpPost]
        public IActionResult SuaSanPham(DienThoai sanPham, IFormFile formFile)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            Guid guid = Guid.NewGuid();

            string newfileName = guid.ToString();

            string fileextention = Path.GetExtension(formFile.FileName);

            string fileName = newfileName + fileextention;

            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", fileName);

            var stream = new FileStream(uploadpath, FileMode.Create);

            formFile.CopyToAsync(stream);
            try
            {
                sanPham.Anh = fileName.ToString();
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
            }
            catch(Exception e)
            {
                return View("Index");
            }

        }

        //xoa sp
        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(String MaDienThoai)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            DienThoai? sp = db.DienThoais.Find(MaDienThoai);

            return View(sp);
        }

        [Route("XoaSanPham")]
        [HttpPost]
        public IActionResult XoaSanPham(DienThoai sp)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;
            try
            {
                var listChiTietHDB = db.ChiTietHdbs.Where(x => x.MaDienThoai.Trim() == sp.MaDienThoai.Trim()).ToList();
                if(listChiTietHDB.Count ==0) {
                    var listAnh = db.AnhSps.Where(x => x.MaDienThoai == sp.MaDienThoai);
                    if (listAnh != null) db.RemoveRange(listAnh);

                    db.Remove(db.DienThoais.Find(sp.MaDienThoai));
                    db.SaveChanges();
                    return RedirectToAction("DanhMucSanPham");
                }

                return RedirectToAction("Index");

            }
            catch(Exception e)
            {
                return RedirectToAction("Index");
                //return View(sanPham);
            }


        }

        //HSX
        [Route("DanhMucHSX")]
        public IActionResult DanhMucHSX(int? page)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            int pageSize = 8;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            var lstsanpham = db.HangSanXuats.AsNoTracking().OrderBy(x => x.TenHsx);
            PagedList<HangSanXuat> lst = new PagedList<HangSanXuat>(lstsanpham, pageNumber, pageSize);
            return View(lst);
        }
        //Them HSX
        [Route("ThemHSX")]
        [HttpGet]
        public IActionResult ThemHSX()
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;
            return View();
        }

        [Route("ThemHSX")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemHSX(HangSanXuat hsx)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            if (ModelState.IsValid)
            {
                db.HangSanXuats.Add(hsx);
                db.SaveChanges();
                return RedirectToAction("DanhMucHSX");
            }
            return View(hsx);
        }

        [Route("ChiTietHSX")]
        [HttpGet]
        public IActionResult ChiTietHSX(String MaHsx)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            HangSanXuat? sp = db.HangSanXuats.Find(MaHsx);
            //ViewBag.MaLoai = new SelectList(db.TL.OrderBy(x => x.TenNuoc), "MaNuoc", "TenNuoc");

            return View(sp);
        }

        [Route("SuaHSX")]
        [HttpGet]
        public IActionResult SuaHSX(String MaHsx)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            HangSanXuat? sp = db.HangSanXuats.Find(MaHsx);
            return View(sp);
        }

        [Route("SuaHsx")]
        [HttpPost]
        public IActionResult SuaHSX(HangSanXuat sanPham)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("DanhMucHsx");

            }
            //return RedirectToAction("Index");
            return View("Index");

        }
        // Xoa
        [Route("XoaHSX")]
        [HttpGet]
        public IActionResult XoaHSX(String MaHsx)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            HangSanXuat? sp = db.HangSanXuats.Find(MaHsx);
            return View(sp);
        }

        [Route("XoaHSX")]
        [HttpPost]
        public IActionResult XoaHSX(HangSanXuat hsx)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            var listDt = db.DienThoais.Where(x => x.MaHsx.Trim() == hsx.MaHsx.Trim()).ToList();
            if(listDt.Count == 0)
            {
                db.Remove(db.HangSanXuats.Find(hsx.MaHsx));
                db.SaveChanges();
                return RedirectToAction("DanhMucHSX");
            }
            else
            {
                return RedirectToAction("Index");
            }
            //return View(sanPham);
        }


        //Nhan Vien
        [Route("DanhMucNhanVien")]
        public IActionResult DanhMucNhanVien(int? page)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            int pageSize = 8;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            var lstNv = db.NhanViens.Include(x=>x.MaChucVuNavigation).AsNoTracking().OrderBy(x => x.TenNhanVien);
            PagedList<NhanVien> lst = new PagedList<NhanVien>(lstNv, pageNumber, pageSize);
            return View(lst);
        }
        //Them nv
        [Route("ThemNhanVien")]
        [HttpGet]
        public IActionResult ThemNhanVien()
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            ViewBag.MaChucVu = new SelectList(db.ChucVus.ToList(), "MaChucVu", "TenChucVu");
            ViewBag.UserName1 = new SelectList(db.UserLogins.Where(x => x.UserRole == 0).ToList(), "UserName", "UserName");

            return View();
        }

        [Route("ThemNhanVien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNhanVien(NhanVien nv , IFormFile formFile)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            Guid guid = Guid.NewGuid();

            string newfileName = guid.ToString();

            string fileextention = Path.GetExtension(formFile.FileName);

            string fileName = newfileName + fileextention;

            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", fileName);

            var stream = new FileStream(uploadpath, FileMode.Create);

            formFile.CopyToAsync(stream);

            if (ModelState.IsValid)
            {
                nv.AnhDaiDien = fileName.ToString();
                //sanPham.Anh = fileName.ToString();
                db.NhanViens.Add(nv);
                db.SaveChanges();
                //return RedirectToAction("DanhMucSanPham");
                return RedirectToAction("DanhMucNhanVien");
            }
            return View(nv);
        }

        [Route("ChiTietNV")]
        [HttpGet]
        public IActionResult ChiTietNV(String MaNhanVien)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            NhanVien? sp = db.NhanViens.Find(MaNhanVien);
            ViewBag.ChucVu = db.ChucVus.Find(sp.MaChucVu).TenChucVu.ToString();
            return View(sp);
        }

        [Route("SuaNV")]
        [HttpGet]
        public IActionResult SuaNV(String MaNhanVien)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            ViewBag.MaChucVu = new SelectList(db.ChucVus.ToList(), "MaChucVu", "TenChucVu");
            NhanVien? sp = db.NhanViens.Find(MaNhanVien);
            return View(sp);
        }

        [Route("SuaNV")]
        [HttpPost]
        public IActionResult SuaNV(NhanVien nv, IFormFile formFile)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            Guid guid = Guid.NewGuid();

            string newfileName = guid.ToString();

            string fileextention = Path.GetExtension(formFile.FileName);

            string fileName = newfileName + fileextention;

            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", fileName);

            var stream = new FileStream(uploadpath, FileMode.Create);

            formFile.CopyToAsync(stream);
            if (ModelState.IsValid)
            {
                nv.AnhDaiDien = fileName.ToString();
                db.Entry(nv).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("DanhMucNhanVien");

            }
            //return RedirectToAction("Index");
            return View("Index");

        }

        // Xoa
        [Route("XoaNV")]
        [HttpGet]
        public IActionResult XoaNV(String MaNhanVien)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            NhanVien? sp = db.NhanViens.Find(MaNhanVien);
            //ViewBag.ChucVu = db.ChucVus.Find(sp.MaChucVu).TenChucVu.ToString();
            return View(sp);
        }

        [Route("XoaNV")]
        [HttpPost]
        public IActionResult XoaNV(NhanVien nv)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            var listHD = db.HoaDonBans.Where(x => x.MaNhanVien.Trim() == nv.MaNhanVien.Trim()).ToList();
            if(listHD.Count == 0)
            {
                db.Remove(db.NhanViens.Find(nv.MaNhanVien));
                db.SaveChanges();
                return RedirectToAction("DanhMucNhanVien");
            }
            else
            {
                return RedirectToAction("Index");
            }
            //var listDt = db.DienThoais.Where(x => x.MaHsx.Trim() == hsx.MaHsx.Trim()).ToList();
            //if (listDt.Count == 0)
            //{
            //    db.Remove(db.HangSanXuats.Find(hsx.MaHsx));
            //    db.SaveChanges();
            //    return RedirectToAction("DanhMucHSX");
            //}
            //else
            //{
            //    return RedirectToAction("Index");
            //}
            //return View(sanPham);
        }

        //KH
        [Route("DanhMucKH")]
        public IActionResult DanhMucKH(int? page)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            int pageSize = 8;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            var lstkh = db.KhachHangs.Include(x=>x.UserNameNavigation).AsNoTracking().OrderBy(x => x.TenKhachHang);
            PagedList<KhachHang> lst = new PagedList<KhachHang>(lstkh, pageNumber, pageSize);
            return View(lst);
        }

        [Route("ChiTietKH")]
        [HttpGet]
        public IActionResult ChiTietKH(int MaKhachHang)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            KhachHang? kh = db.KhachHangs.Find(MaKhachHang);
            
            return View(kh);
        }


        [Route("SuaKH")]
        [HttpGet]
        public IActionResult SuaKH(int MaKhachHang)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            KhachHang? kh = db.KhachHangs.Find(MaKhachHang);
            ViewBag.UserName = new SelectList(db.KhachHangs.Where(x => x.MaKhachHang == MaKhachHang), "UserName", "UserName");
            return View(kh);
        }

        [Route("SuaKH")]
        [HttpPost]
        public IActionResult SuaKH(KhachHang kh)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            //kh.UserName = 
            db.Entry(kh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucKH");
        }

        [Route("XoaKH")]
        [HttpGet]
        public IActionResult XoaKH(int MaKhachHang)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            KhachHang? kh = db.KhachHangs.Find(MaKhachHang);

            return View(kh);
        }

        [Route("XoaKH")]
        [HttpPost]
        public IActionResult XoaKH(KhachHang kh)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            try
            {
                var listHDB = db.HoaDonBans.Where(x => x.MaKhachHang == kh.MaKhachHang).ToList();
                if (listHDB.Count == 0)
                {
                    db.Remove(db.KhachHangs.Find(kh.MaKhachHang));
                    db.SaveChanges(); 
                    return RedirectToAction("DanhMucKH");
                }

                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
                //return View(sanPham);
            }


        }
        [Route("DanhMucHDX")]
        public IActionResult DanhMucHDX(int? page)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            int pageSize = 8;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            var lstsanpham = db.HoaDonBans.Include(x => x.MaNhanVienNavigation).Include(x=>x.MaKhachHangNavigation).AsNoTracking();
            PagedList<HoaDonBan> lst = new PagedList<HoaDonBan>(lstsanpham, pageNumber, pageSize);
            return View(lst);
        }


        //Them HSX
        [Route("ThemHDX")]
        [HttpGet]
        public IActionResult ThemHDX()
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            ViewBag.NV = new SelectList(db.NhanViens.ToList(), "MaNhanVien", "TenNhanVien");
            ViewBag.KH = new SelectList(db.KhachHangs.ToList(), "MaKhachHang", "TenKhachHang");
            return View();
        }

        [Route("ThemHDX")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemHDX(HoaDonBan hdb)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            if (ModelState.IsValid)
            {
                if(hdb.TongTien == null)
                {
                    hdb.TongTien = 0;
                }
                db.HoaDonBans.Add(hdb);
                db.SaveChanges();
                return RedirectToAction("DanhMucHDX");
            }
            return View(hdb);
        }

        [Route("SuaHDX")]
        [HttpGet]
        public IActionResult SuaHDX(String MaHdb)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            ViewBag.NV = new SelectList(db.NhanViens.ToList(), "MaNhanVien", "TenNhanVien");
            ViewBag.KH = new SelectList(db.KhachHangs.ToList(), "MaKhachHang", "TenKhachHang");
            HoaDonBan? kh = db.HoaDonBans.Find(MaHdb);
            return View(kh);
        }

        [Route("SuaHDX")]
        [HttpPost]
        public IActionResult SuaHDX(HoaDonBan hdb)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            try
            {
                db.Entry(hdb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucHDX");
            }
            catch (Exception e)
            {
                return View("Index");
            }
        }

        [Route("XoaHDX")]
        [HttpGet]
        public IActionResult XoaHDX(String MaHdb)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            HoaDonBan? sp = db.HoaDonBans.Find(MaHdb);
            ViewBag.KH = db.KhachHangs.Find(sp.MaKhachHang).TenKhachHang.ToString();
            ViewBag.NV = db.NhanViens.Find(sp.MaNhanVien).TenNhanVien.ToString();
            return View(sp);
        }

        [Route("XoaHDX")]
        [HttpPost]
        public IActionResult XoaHDX(HoaDonBan hdb)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            //try
            //{
            var listHD = db.ChiTietHdbs.Where(x => x.MaHdb.Trim() == hdb.MaHdb.Trim()).ToList();
                
                
                if (listHD != null) db.RemoveRange(listHD);
                db.Remove(db.HoaDonBans.Find(hdb.MaHdb));
                db.SaveChanges();
                return RedirectToAction("DanhMucHDX");
            //}
            //catch(Exception e)
            //{
            //    return View(hdb);
            //}
            
            //return View(sanPham);
        }


        [Route("ChiTietHDX")]
        [HttpGet]
        public IActionResult ChiTietHDX(String MaHdb, int? page)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            int pageSize = 8;
            ViewBag.MaHdb = MaHdb;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            var lstsanpham = db.ChiTietHdbs.Include(x => x.MaDienThoaiNavigation).Where(x=>x.MaHdb == MaHdb) .AsNoTracking();
            PagedList<ChiTietHdb> lst = new PagedList<ChiTietHdb>(lstsanpham, pageNumber, pageSize);
            return View(lst);
           
            //ChiTietHdb? sp = db.ChiTietHdbs.Find(MaHdb);
            //ViewBag.SP = db.DienThoais.Find(sp.MaDienThoai).TenDienThoai.ToString();
            //return View(sp);
        }

        [Route("SuaChiTietHDX")]
        [HttpGet]
        public IActionResult SuaChiTietHDX(String MaHdb, String MaDienThoai)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            ChiTietHdb chiTietHdb = db.ChiTietHdbs.Find(MaHdb, MaDienThoai);
            ViewBag.MaHdb = new SelectList(db.HoaDonBans.Where(x => x.MaHdb == MaHdb), "MaHdb", "MaHdb");
            ViewBag.DT = new SelectList(db.DienThoais.ToList(), "MaDienThoai", "TenDienThoai");
            return View(chiTietHdb);
        }

        [Route("SuaChiTietHDX")]
        [HttpPost]
        public IActionResult SuaChiTietHDX(ChiTietHdb hdb)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            //ChiTietHdb chiTietHdb = db.ChiTietHdbs.Where(x => x.MaDienThoai == hdb.MaDienThoai && x.MaHdb == hdb.MaHdb).FirstOrDefault();
            db.ChiTietHdbs.Update(hdb);
            db.SaveChanges();
            return RedirectToAction("DanhMucHDX");
        }

        [Route("ThemChiTietHDX")]
        [HttpGet]
        public IActionResult ThemChiTietHDX( String MaHdb)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            ViewBag.MaHdb = new SelectList(db.HoaDonBans.Where(x=>x.MaHdb == MaHdb),"MaHdb","MaHdb");
            ViewBag.DT = new SelectList(db.DienThoais.ToList(), "MaDienThoai", "TenDienThoai");
            return View();
        }

        [Route("ThemChiTietHDX")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemChiTietHDX(ChiTietHdb hdb)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            try
            {
                if(hdb.GiamGia == null)
                {
                    hdb.GiamGia = 0;
                }
                ChiTietHdb chiTietHdb = db.ChiTietHdbs.Where(x => x.MaDienThoai == hdb.MaDienThoai && x.MaHdb == hdb.MaHdb).FirstOrDefault();
                if(chiTietHdb != null)
                {
                    chiTietHdb.SoLuong += hdb.SoLuong;
                    db.ChiTietHdbs.Update(chiTietHdb);
                    db.SaveChanges();
                    return RedirectToAction("DanhMucHDX", new {MaHdb = hdb.MaHdb});
                }
                else
                {
                    db.ChiTietHdbs.Add(hdb);
                    db.SaveChanges();
                    return RedirectToAction("DanhMucHDX");
                }
                
            }
            catch(Exception e)
            {
                return View("Index");
            }
        }

        [Route("XoaChiTietHDX")]
        [HttpGet]
        public IActionResult XoaChiTietHDX(String MaHdb, String MaDienThoai)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;

            ChiTietHdb chiTietHdb = db.ChiTietHdbs.Find(MaHdb, MaDienThoai);
            
            ViewBag.DT = db.DienThoais.Find(chiTietHdb.MaDienThoai).TenDienThoai.ToString();
            
            return View(chiTietHdb);
        }

        [Route("XoaChiTietHDX")]
        [HttpPost]
        public IActionResult XoaChiTietHDX(ChiTietHdb hdb)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;
            //try
            //{
            db.Remove(db.ChiTietHdbs.Find(hdb.MaHdb, hdb.MaDienThoai));
            db.SaveChanges();
            return RedirectToAction("DanhMucHDX");
            //}
            //catch(Exception e)
            //{
            //    return View(hdb);
            //}

            //return View(sanPham);
        }

        [Route("DanhMucTK")]
        public IActionResult DanhMucTK(int? page)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;
            int pageSize = 8;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            var lstsanpham = db.UserLogins.AsNoTracking().Where(x => x.UserRole == 0).OrderBy(x => x.UserName);
            var user = db.UserLogins.AsNoTracking().Where(x => x.UserRole == 0).FirstOrDefault();
            ViewBag.Role = db.UserLogins.Find(user.UserName).UserRole.ToString();
            PagedList<UserLogin> lst = new PagedList<UserLogin>(lstsanpham, pageNumber, pageSize);
            return View(lst);
        }

        [Route("ThemTK")]
        [HttpGet]
        public IActionResult ThemTK()
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;
            return View();
        }

        [Route("ThemTK")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemTK(UserLogin user)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;
            if (ModelState.IsValid)
            {
                user.UserRole = 0;
                db.UserLogins.Add(user);
                db.SaveChanges();
                return RedirectToAction("DanhMucTK");
            }
            return View(user);
        }

        [Route("SuaTK")]
        [HttpGet]
        public IActionResult SuaTK(String UserName)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;
            UserLogin? user = db.UserLogins.Find(UserName);
            return View(user);
        }

        [Route("SuaTK")]
        [HttpPost]
        public IActionResult SuaTK(UserLogin userLogin)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;
            if (ModelState.IsValid)
            {
                userLogin.UserRole = 0;
                db.Entry(userLogin).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("DanhMucTK");

            }
            //return RedirectToAction("Index");
            return View("Index");

        }

        [Route("DanhMucTKKH")]
        public IActionResult DanhMucTKKH(int? page)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.username = username;
            int pageSize = 8;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            var lstKH = db.UserLogins.AsNoTracking().Where(x => x.UserRole == 1).OrderBy(x => x.UserName);
            var user = db.UserLogins.AsNoTracking().Where(x => x.UserRole == 1).FirstOrDefault();
            ViewBag.Role = db.UserLogins.Find(user.UserName).UserRole.ToString();
            PagedList<UserLogin> lst = new PagedList<UserLogin>(lstKH, pageNumber, pageSize);
            return View(lst);
        }
    }

        


    
}
