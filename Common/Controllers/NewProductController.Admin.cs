using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Widgets.Employees.Constants;
using Nop.Plugin.Widgets.NewProduct.Models;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.NewProduct.Controllers
{
    public partial class NewProductController
    {
        [AuthorizeAdmin]
        [Area(Areas.Admin)]
        public async Task<IActionResult> Configure()
        {
            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var settings = await _settingService.LoadSettingAsync<NewProductWidgetSettings>(storeScope);

            var widgetZonesData = GetWidgetZoneData();
            var lookup = widgetZonesData.ToDictionary(x => x.value, y => y.id);

            List<int> Zones(string zones)
            {
                return (from i in (zones ?? "").Split(';')
                        where lookup.ContainsKey(i)
                        select lookup[i]).ToList();
            }

            IList<SelectListItem> AvailableZones(List<int> zones)
            {
                return (from wzd in widgetZonesData
                        select new SelectListItem
                        {
                            Text = wzd.name,
                            Value = wzd.id.ToString(),
                            Selected = zones.Contains(wzd.id)
                        }
                       ).ToList();
            }

            var currentWidgetZones = Zones(settings.WidgetZones);

            var availableSpecCategories = await _specificationAttributeService.GetSpecificationAttributesAsync();

            var model = new ConfigurationModel
            {
                WidgetZones = currentWidgetZones,
                AvailableWidgetZones = AvailableZones(currentWidgetZones),
            };

            return View($"{Route}{nameof(Configure)}.cshtml", model);
        }

        [AuthorizeAdmin]
        [Area(Areas.Admin)]
        [HttpPost, ActionName("Configure")]
        [FormValueRequired("save")]
        public async Task<IActionResult> Configure(ConfigurationModel model)
        {
            if (!ModelState.IsValid)
            {
                return await Configure();
            }
            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var settings = await _settingService.LoadSettingAsync<NewProductWidgetSettings>(storeScope);

            var widgetZonesData = GetWidgetZoneData();
            var lookup = widgetZonesData.ToDictionary(x => x.id, y => y.value);

            string Join(IList<int> zones)
            {
                return model.WidgetZones != null && model.WidgetZones.Any()
                        ? string.Join(";",
                                from i in zones
                                where lookup.ContainsKey(i)
                                select lookup[i]
                                )
                        : "";
            }

            settings.WidgetZones = Join(model.WidgetZones);

            await _settingService.SaveSettingAsync(settings);
            await _settingService.ClearCacheAsync();
            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));
            return await Configure();
        }

        private List<(string name, string value, int id)> GetWidgetZoneData()
        {
            int id = 1000;
            return typeof(Nop.Web.Framework.Infrastructure.PublicWidgetZones)
                .GetProperties(BindingFlags.Static | BindingFlags.Public)
                .OrderBy(x => x.Name)
                .Select(x => (name: x.Name, value: x.GetValue(null, null).ToString(), id++))
                .ToList();
        }
    }
}
