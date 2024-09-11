using Nop.Core;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.NewProduct.Controllers
{
    public partial class NewProductController : BasePluginController
    {
        public static string ControllerName = nameof(NewProductController).Replace("Controller", "");
        const string Route = "~/Plugins/Widgets.NewProduct/Views/NewProduct/";

        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;
        private readonly ISpecificationAttributeService _specificationAttributeService;
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IWorkContext _workContext;

        public NewProductController(
            IStoreContext storeContext,
            ISettingService settingService,
            ISpecificationAttributeService specificationAttributeService,
            ILocalizationService localizationService,
            INotificationService notificationService,
            IWorkContext workContext)
        {
            _storeContext = storeContext;
            _settingService = settingService;
            _specificationAttributeService = specificationAttributeService;
            _localizationService = localizationService;
            _notificationService = notificationService;
            _workContext = workContext;
        }
    }
}
