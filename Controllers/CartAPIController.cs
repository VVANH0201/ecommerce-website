using BTL.Models;
using BTL.Models.APIModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartAPIController : ControllerBase
    {

        Web6ContextContext db = new Web6ContextContext();

        [HttpGet]
        public IEnumerable<CartItems> getGioHang()
        {
            var carts = (from p in db.ChiTietGioHangs
                         select new CartItems
                         {
                             MaGioHang = p.MaGioHang,
                             MaDienThoai = p.MaDienThoai,
                             TenDienThoai = db.DienThoais.Single(h => h.MaDienThoai == p.MaDienThoai).TenDienThoai,
                             sl = (int)p.SoLuong,
                             dongia = (double?)db.DienThoais.Single(h => h.MaDienThoai == p.MaDienThoai).GiaBan,
                             anh = db.DienThoais.Single(h => h.MaDienThoai == p.MaDienThoai).Anh,
                             total = (double)(db.DienThoais.Single(h => h.MaDienThoai == p.MaDienThoai).GiaBan * p.SoLuong),
                         }).ToList();
            return carts;
        }
        [HttpDelete]
        public bool DeleteFromCart(string? mahang)
        {
            string username = HttpContext.Session.GetString("UserName");

            if (mahang == null)
            {
                return false;
            }
            
            var cartd = db.GioHangs.SingleOrDefault(x => x.UserName == username);

            if (cartd == null)
            {
                return false;
            }

            var sps = db.ChiTietGioHangs.SingleOrDefault(x => db.GioHangs == db.GioHangs && x.MaDienThoai == mahang);
            if (sps != null)
            {
                if (sps.MaDienThoai == mahang)
                {
                    db.ChiTietGioHangs.Remove(sps);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        [HttpPost]
        public bool AddProductToCart(string? mahang, int? quatity)
        {
            string username = HttpContext.Session.GetString("UserName");

            if (mahang == null || quatity == null)
            {
                return false;
            }

            //var cart = db.GioHangs.SingleOrDefault(x => x.UserName == username);
            var cart = db.GioHangs.SingleOrDefault(x => x.UserName == username);
            if (cart == null)
            {
                return false;
            }

            var sp = db.ChiTietGioHangs.SingleOrDefault(x => db.GioHangs == db.GioHangs && x.MaDienThoai == mahang);
            if (sp == null)
            {
                ChiTietGioHang ctgh = new ChiTietGioHang()
                {
                    MaGioHang = cart.MaGioHang,
                    MaDienThoai = mahang,
                    SoLuong = quatity
                };

                try
                {

                    db.ChiTietGioHangs.Add(ctgh);
                    db.SaveChanges();
                    return true;

                }
                catch
                {
                    return false;
                }

            }
            else
            {
                sp.SoLuong += quatity;

                try
                {
                    db.ChiTietGioHangs.Update(sp);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        
    }
}
