namespace AIMobileCus.Models.ViewModels
{
    public class ShopProductResultViewModel
    {
        public string Id { get; set; }
        public string TypeId { get; set; }
        public string Shop_Id { get; set; }
        public string ShopName { get; set; }
        public string Product_Id { get; set; }
        public string ProductName { get; set; }
        public string Image_Id { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public int StockCount { get; set; }
        public decimal Unitprice { get; set;}
    }
}
