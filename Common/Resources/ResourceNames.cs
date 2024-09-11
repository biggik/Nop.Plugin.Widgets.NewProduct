using nopLocalizationHelper;

namespace Nop.Plugin.Widgets.NewProduct.Resources
{
    internal static class Cultures
    {
        public const string EN = "en-US";
        public const string IS = "is-IS";
    }

    [LocaleStringProvider]
    public static class NewProductResources
    {
        [LocaleString(Cultures.EN, "Configure")]
        [LocaleString(Cultures.IS, "Stillingar")]
        public const string Configure = "Status.NewProductWidget.NewProduct.Configure";
    }

    [LocaleStringProvider]
    public static class ConfigurationResources
    {
        [LocaleString(Cultures.EN, "Widget zones", "In which zones should the widget be displayed")]
        [LocaleString(Cultures.IS, "Birta í", "Hvar á síðunni á að birta íhlutinn")]
        public const string WidgetZones = "Status.NewProductWidget.Configuration.WidgetZones";
    }
}
