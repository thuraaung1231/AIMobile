namespace AIMobileCus.Models.ViewModels
{
    public class AddToCartViewModel
    {
        public string Id { get; set; }
        public string ShopId { get; set; }
        public string ShopName { get; set; }
        public string ProductName { get; set; }
        public string ImageName { get; set; }
        public int numberOfItem { get; set; }
        public int StockCount { get; set; }
        public decimal Unitprice { get; set; }
    }
}
