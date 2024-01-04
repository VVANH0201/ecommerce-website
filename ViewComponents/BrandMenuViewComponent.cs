using BTL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BTL.ViewComponents
{
    public class BrandMenuViewComponent: ViewComponent
    {
        private readonly IBrandRepository _brandRepository;

        public BrandMenuViewComponent(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public IViewComponentResult Invoke()
        {
            var brands = _brandRepository.GetAllHangSX().OrderBy(x => x.TenHsx);
            return View(brands);
        }
    }
}
