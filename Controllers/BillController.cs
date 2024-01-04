using BTL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        Web6ContextContext db = new Web6ContextContext();

        public string getcode(DateTime dateTime)
        {
            string values = "";
            values = "" + dateTime.Year + "" + (dateTime.Month + 1) + "" + dateTime.Day + "" + dateTime.Hour + "" + dateTime.Minute + "" + dateTime.Second;
            return "GT" + values;
        }
        [HttpPost]
        public bool addBill()
        {
            //if (id == null)
            //{
            //    return false;
            //}
            string username  = HttpContext.Session.GetString("UserName");
            var name = db.KhachHangs.SingleOrDefault(x => x.UserName == username);

            var cart1 = db.GioHangs.SingleOrDefault(x => x.UserName == username);
            
            var cart = db.GioHangs.SingleOrDefault(x => x.MaGioHang == cart1.MaGioHang);
            var cartitem = db.ChiTietGioHangs.Where(x => x.MaGioHang == cart.MaGioHang).ToList();
            int a = int.Parse(name.MaKhachHang.ToString());
            var hdb = db.HoaDonBans.Where(x=>x.MaHdb == cart.MaGioHang).FirstOrDefault();
            if (hdb == null)
            {
                HoaDonBan hbbb = new HoaDonBan()
                {
                    MaHdb = cart.MaGioHang,
                    MaKhachHang = name.MaKhachHang,
                    MaNhanVien = "NV01",
                    NgayBan = DateTime.Now,
                    TongTien = cart1.TongTien,
                    
                };
                db.HoaDonBans.Add(hbbb);
                db.SaveChanges();
                try
                {
                    for (int i = 0; i < cartitem.Count; i++)
                    {


                        ChiTietHdb cthd = new ChiTietHdb()
                        {
                            MaHdb = hbbb.MaHdb,
                            MaDienThoai = cartitem[i].MaDienThoai,
                            SoLuong = cartitem[i].SoLuong,
                            GiamGia = 0,


                        };
                        //hbbb.TongTien += cthd.ThanhTien;
                        
                        db.ChiTietHdbs.Add(cthd);
                        db.SaveChanges();
                    }
                    //db.Entry(hbbb).State = EntityState.Modified;
                    return true;

                }
                catch
                {
                    return false;
                }
                
                
            }

            //var billitem = db.ChiTietHdbs.SingleOrDefault(x => x.MaHdb == hdb.MaHdb);
            //if (billitem == null)
            //{
            else
            {
                return false;
            }
            

            //}
            //return false;
        }

        [HttpDelete]
        public bool DeleteCart()
        {
            string username = HttpContext.Session.GetString("UserName");

            var cartd = db.GioHangs.SingleOrDefault(x => x.UserName == username);

            

            if (cartd == null)
            {
                return false;
            }

            var listHD = db.ChiTietGioHangs.Where(x => x.MaGioHang.Trim() == cartd.MaGioHang.Trim()).ToList();
            if (listHD != null)
            {
                db.RemoveRange(listHD);
                db.Remove(db.GioHangs.Find(cartd.MaGioHang));
                var gh = new GioHang
                {
                    MaGioHang = getcode(DateTime.Now),
                    UserName = username,
                    TongTien = 0
                };
                db.GioHangs.Add(gh);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
