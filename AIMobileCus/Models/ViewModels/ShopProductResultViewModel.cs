namespace AIMobileCus.Models.ViewModels
{
    public class ShopProductResultViewModel
    {
        public string Id { get; set; }
        public string Shop_Id { get; set; }
        public string Product_Id { get; set; }
        public string Image_Id { get; set; }
        public string ShopName { get; set; }
        public string ProductName { get; set; }
        public string ImageName { get; set; }
        public int StockCount { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
