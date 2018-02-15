using DGD.Hub.DB;
using System.Windows.Forms;


namespace DGD.Hub.SpotView
{
    sealed class SpotViewItem: ListViewItem
    {
        public SpotViewItem(SpotValue spotValue):
            base(GetItemContent(spotValue))
        {
            SpotValue = spotValue;
        }


        public SpotValue SpotValue { get; }


        //private:
        static string[] GetItemContent(SpotValue spotValue)
        {
            return new string[]
            {
                spotValue.Product.Name,
                spotValue.SpotTime.ToShortDateString(),
                $"{spotValue.Price.ToString()} {spotValue.ValueContext.Currency.Name} / {spotValue.ValueContext.Unit.Name}",                
                spotValue.ValueContext.Origin?.Name ?? "",
                spotValue.ValueContext.Incoterm?.Name ?? "",
                spotValue.ValueContext.Place?.Name ?? "",
                spotValue.Description.Text,
                spotValue.DataSupplier.Name
            };

        }
    }
}
