using BTL.Models;
using BTL.Models.Authentication;
using BTL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BTL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        Web6ContextContext db = new Web6ContextContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authentication]
        public IActionResult Index()
        {
            var lstSanPham = db.DienThoais.Include(x => x.MaHsxNavigation);
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.Username = username;
            return View(lstSanPham.ToList());
        }

        [Authentication]
        public IActionResult SanPhamTheoHSX(string maHsx)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.Username = username;
            var check = db.DienThoais.Count(x => x.MaHsx == maHsx);
            if(check != 0)
            {
                var lstsanPham = db.DienThoais.Include(x => x.MaHsxNavigation).Where(x => x.MaHsx == maHsx).OrderBy(x => x.TenDienThoai);
                DienThoai dt = db.DienThoais.Where(x => x.MaHsx == maHsx).FirstOrDefault();
                ViewBag.TenHSX = db.HangSanXuats.Find(dt.MaHsx).TenHsx.ToString();
                ViewBag.MaHsx = maHsx;
                return View(lstsanPham.ToList());
            }
            return RedirectToAction("error404", "Home");
        }

        [Authentication]
        public IActionResult ChiTietSanPham(string maDienThoai)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.Username = username;
            DienThoai sp = db.DienThoais.Include(x => x.MaHsxNavigation).SingleOrDefault(x => x.MaDienThoai == maDienThoai);
			var Comment = db.Comments.Where(x => x.MaDienThoai == maDienThoai).ToList();
			var viewmodel = new ChiTietSanPhamViewModel(sp, Comment);
			ViewBag.Images = db.AnhSps.Where(x => x.MaDienThoai == maDienThoai);
			ViewBag.MaDT = maDienThoai;
			ViewBag.Count = db.Comments.Count(x => x.MaDienThoai == maDienThoai);
			return View(viewmodel);
		}

        [Authentication]
        [HttpPost]
		public IActionResult AddComment(string mdt, string username, string content)
		{
            String uname = HttpContext.Session.GetString("UserName");
            ViewBag.Username = uname;
            ViewBag.name = uname;
            var comments = db.DienThoais.FirstOrDefault(x => x.MaDienThoai == mdt);
			if (comments == null)   
			{
				return NotFound();
			}
			var cmt = new Comment
			{
				NdbinhLuan = content,
				CreatedTime = DateTime.Now,
				MaDienThoai = comments.MaDienThoai,
				UserName = uname,
			};
			db.Comments.Add(cmt);
			db.SaveChanges();
			return RedirectToAction("ChiTietSanPham", new { maDienThoai = mdt });
		}

        [Authentication]
        public IActionResult SanPhamTheoRAM(string ram)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.Username = username;
            var check = db.DienThoais.Count(x => x.Ram == ram);
            if(check == 0)
            {
                return RedirectToAction("error404","Home");
            }
            var lstsanPham = db.DienThoais.Include(x => x.MaHsxNavigation).Where(x => x.Ram == ram).OrderBy(x => x.TenDienThoai);
            ViewBag.Ram = ram;
            return View(lstsanPham.ToList());
        }

        [Authentication]
        public IActionResult SanPhamTheoROM(string rom)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.Username = username;
            var lstsanPham = db.DienThoais.Include(x => x.MaHsxNavigation).Where(x => x.Rom == rom).OrderBy(x => x.TenDienThoai);
            ViewBag.Rom = rom;
            return View(lstsanPham.ToList());
        }

        [Authentication]
        public IActionResult SanPhamTheoOS(string os)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.Username = username;
            var lstSanPham = db.DienThoais.Include(x => x.MaHsxNavigation).Where(x => x.HeDieuHanh.Contains(os)).OrderBy(x => x.TenDienThoai);
            ViewBag.OS = os;
            return View(lstSanPham.ToList());
        }

        [Authentication]
        public IActionResult Contact()
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.Username = username;
            return View();
        }

        [Authentication]
        public IActionResult About()
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.Username = username;
            return View();
        }

        [Authentication]
        public IActionResult DienThoaiBanner(string tenSp)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.Username = username;
            var lstsanPham = db.DienThoais.Include(x => x.MaHsxNavigation).Where(x => x.TenDienThoai.Contains(tenSp)).OrderBy(x => x.TenDienThoai);
            //ViewBag.Hsx = new SelectList(db.HangSanXuats.Where(x => x.MaHsx == maHsx), "MaHsx", "TenHsx");
            return View(lstsanPham.ToList());
        }

        [Authentication]
        public IActionResult SanPhamTheoPrice(int tien1, int tien2)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.Username = username;
            var lstsanPham = db.DienThoais.Include(x => x.MaHsxNavigation).Where(x => x.GiaBan >= tien1 && x.GiaBan <= tien2).OrderBy(x => x.TenDienThoai);
            ViewBag.Tien1 = tien1;
            ViewBag.Tien2 = tien2;
            return View(lstsanPham.ToList());
        }

        [Authentication]
        public IActionResult Cart()
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.Username = username;
            return View();
        }

        [Authentication]
        public IActionResult TrendingProduct()
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.Username = username;
            var lstSanPham = db.DienThoais.Include(x => x.MaHsxNavigation).OrderBy(x => x.GiaBan).Take(6);
			return View(lstSanPham.ToList());
		}

        [Authentication]
        public IActionResult SearchProduct(string strSearch)
        {
            String uname = HttpContext.Session.GetString("UserName");
            ViewBag.Username = uname;
            var lstSanPham = db.DienThoais.Include(x => x.MaHsxNavigation).Where(x => x.TenDienThoai.Contains(strSearch.ToLower())).OrderBy(x => x.GiaBan);
            return View(lstSanPham.ToList());
        }

        [Authentication]
        public IActionResult error404()
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.Username = username;
            return View();
        }
    }
}