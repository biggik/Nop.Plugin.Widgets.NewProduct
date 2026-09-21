using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Plugin.Widgets.NewProduct.Models;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;
using Nop.Web.Models.Catalog;

namespace Nop.Plugin.Widgets.NewProduct.Components
{
    [ViewComponent(Name = "WidgetsProductNewProduct")]
    public class WidgetsProductNewProductViewComponent : NopViewComponent
    {
        private const string DefaultValue = "NÝ";

        private readonly IProductService _productService;
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;
        private NewProductWidgetSettings _settings;

        public WidgetsProductNewProductViewComponent(
            IProductService productService,
            IStoreContext storeContext,
            ISettingService settingService)
        {
            _productService = productService;
            _storeContext = storeContext;
            _settingService = settingService;
        }

        public async Task<IViewComponentResult> InvokeAsync(RouteValueDictionary values)
        {
            if (_settings == null)
            {
                var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
                _settings = await _settingService.LoadSettingAsync<NewProductWidgetSettings>(storeScope);
            }
            object data = null;
            values?.TryGetValue("additionalData", out data);

            NewProductModel model = null;
            if (data is ProductDetailsModel pdm)
            {
                var product = await _productService.GetProductByIdAsync(pdm.Id);
                if (product != null
                    && product.MarkAsNew
                    && (!product.MarkAsNewStartDateTimeUtc.HasValue || product.MarkAsNewStartDateTimeUtc.Value < DateTime.UtcNow) 
                    && (!product.MarkAsNewEndDateTimeUtc.HasValue || product.MarkAsNewEndDateTimeUtc.Value > DateTime.UtcNow))
                {
                    model = new NewProductModel { DisplayText = DisplayText };
                }
            }
            else if (data is ProductOverviewModel pom)
            {
                if (pom.MarkAsNew) // pom already checks the start and end date into MarkAsNew
                {
                    model = new NewProductModel 
                    { 
                        DisplayText = DisplayText,
                        IsInOverview = true 
                    };
                }
            }

            if (model != null)
            {
                return View("~/Plugins/Widgets.NewProduct/Views/Shared/Components/WidgetProductNewProduct/Default.cshtml", model);
            }
            return Content("");
        }

        private string DisplayText =>
            string.IsNullOrWhiteSpace(_settings.DisplayText) ? DefaultValue : _settings.DisplayText;
    }
}
