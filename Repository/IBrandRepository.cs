using BTL.Models;

namespace BTL.Repository
{
    public interface IBrandRepository
    {
        HangSanXuat Add(HangSanXuat hsx);
        HangSanXuat Update(HangSanXuat hsx);
        HangSanXuat Delete(string maHsx);
        HangSanXuat GetHangSanXuat(string maHsx);
        IEnumerable<HangSanXuat> GetAllHangSX();
    }
}
