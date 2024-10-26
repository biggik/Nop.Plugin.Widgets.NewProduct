using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.NewProduct.Models
{
    public partial record NewProductModel : BaseNopModel
    {
        public NewProductModel()
        {
        }

        public string DisplayText { get; set; }

        public bool IsInOverview { get; set; }
    }
}