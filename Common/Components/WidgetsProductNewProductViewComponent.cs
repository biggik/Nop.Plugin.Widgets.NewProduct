using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.Widgets.NewProduct.Models;
using Nop.Services.Catalog;
using Nop.Web.Framework.Components;
using Nop.Web.Models.Catalog;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.NewProduct.Components
{
    [ViewComponent(Name = "WidgetsProductNewProduct")]
    public class WidgetsProductNewProductViewComponent : NopViewComponent
    {
        private readonly IProductService _productService;

        public WidgetsProductNewProductViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(RouteValueDictionary values)
        {
            object data = values["additionalData"];

            NewProductModel model = null;
            if (data is ProductDetailsModel pdm)
            {
                var product = await _productService.GetProductByIdAsync(pdm.Id);
                if (product.MarkAsNew)
                {
                    model = new NewProductModel();
                }
            }
            else if (data is ProductOverviewModel pom)
            {
                if (pom.MarkAsNew)
                {
                    model = new NewProductModel { IsInOverview = true };
                }
            }

            if (model != null)
            {
                return View("~/Plugins/Widgets.NewProduct/Views/Shared/Components/WidgetProductNewProduct/Default.cshtml", model);
            }
            return Content("");
        }
    }
}
