using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.NewProduct.Models
{
    public partial record NewProductModel : BaseNopModel
    {
        public NewProductModel()
        {
        }

        public bool IsInOverview { get; set; }
    }
}