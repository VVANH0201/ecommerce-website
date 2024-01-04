using BTL.Models;

namespace BTL.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly Web6ContextContext _context;

        public BrandRepository(Web6ContextContext context) 
        {
            _context = context;
        }

        public HangSanXuat Add(HangSanXuat hsx)
        {
            throw new NotImplementedException();
        }

        public HangSanXuat Delete(string maHsx)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HangSanXuat> GetAllHangSX()
        {
            return _context.HangSanXuats;
        }

        public HangSanXuat GetHangSanXuat(string maHsx)
        {
            return _context.HangSanXuats.Find(maHsx);
        }

        public HangSanXuat Update(HangSanXuat hsx)
        {
            throw new NotImplementedException();
        }
    }
}
