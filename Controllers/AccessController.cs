using BTL.Models;
using BTL.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace BTL.Controllers
{
    public class AccessController : Controller
    {
        Web6ContextContext db = new Web6ContextContext();

        public string getcode(DateTime dateTime)
        {
            string values = "";
            values = "" + dateTime.Year + "" + (dateTime.Month + 1) + "" + dateTime.Day + "" + dateTime.Hour + "" + dateTime.Minute + "" + dateTime.Second;
            return "GT"+values;
        }
        [HttpGet]
        // Login
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Login(UserLogin user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                UserLogin u;
                try
                {
                    u = db.UserLogins.Where(x => x.UserName == user.UserName &&
                            x.Password == user.Password).First();
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = false;
                    return View();
                }
                if (u != null && u.UserRole == 0)
                {
                    HttpContext.Session.SetString("UserName", u.UserName.ToString());
                    return RedirectToAction("Index", "Admin");
                }
                else if (u != null && u.UserRole == 1)
                {
                    HttpContext.Session.SetString("UserName", u.UserName.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(user);
        }

        // Register
        [HttpGet]
        public IActionResult Register()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Register(UserLogin user)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            bool isValid = Regex.IsMatch(user.Email, pattern);
            if (isValid)
            {
                user.UserRole = 1;
                db.Add(user);
                db.SaveChanges();
            }
            else
            {
                return RedirectToAction("Login", "Access");
            }
            if (HttpContext.Session.GetString("UserName") == null)
            {
                if (user != null && user.UserRole == 0)
                {
                    HttpContext.Session.SetString("UserName", user.UserName.ToString());
                    return RedirectToAction("Index", "Admin");
                }
                else if (user != null && user.UserRole == 1)
                {
                    HttpContext.Session.SetString("UserName", user.UserName.ToString());
                    return RedirectToAction("NhapThongTinKhachHang", "Access");
                }
            }
            return RedirectToAction("Login", "Access");
        }
        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Access");
        }

        [Route("NhapThongTinKhachHang")]
        [HttpGet]
        public IActionResult NhapThongTinKhachHang()
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.Username = username;
            return View();
        }

        [Route("NhapThongTinKhachHang")]
        [HttpPost]
        public IActionResult NhapThongTinKhachHang(KhachHang kh)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.Username = username;
            try
            {
                var gh = new GioHang {
                    MaGioHang = getcode(DateTime.Now),
                    UserName = username,
                    TongTien = 0
                };
                db.KhachHangs.Add(kh);
                db.GioHangs.Add(gh);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return View(kh);
            }
        }

        [Route("SuaThongTinKhachHang")]
        [HttpGet]
        public IActionResult SuaThongTinKhachHang(string uname)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.Username = username;
            KhachHang? kh = db.KhachHangs.Find(uname);
            return View(kh);
        }


        [Route("SuaThongTinKhachHang")]
        [HttpPost]
        public IActionResult SuaThongTinKhachHang(KhachHang kh)
        {
            String username = HttpContext.Session.GetString("UserName");
            ViewBag.Username = username;
            if(ModelState.IsValid)
            {
                db.KhachHangs.Update(kh);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(kh);
        }
    }
}
