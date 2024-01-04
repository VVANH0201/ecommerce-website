using BTL.Models;
using BTL.Models.APIModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchAPIController : ControllerBase
    {
        Web6ContextContext db = new Web6ContextContext();

        [HttpGet("{tensp}")]
        public IEnumerable<Product> GetProduct(string tensp)
        {
            var sp = (from p in db.DienThoais
                          where p.TenDienThoai.Contains(tensp.ToLower())
                          select new Product
                          {
                              MaDienThoai = p.MaDienThoai,
                              TenDienThoai = p.TenDienThoai,
                              Anh = p.Anh,
                              GiaBan = p.GiaBan,
                              MaHsx = p.MaHsx,
                          }).ToList();
            return sp;
        }
    }
}
