namespace AIMobile.Models.ViewModels
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public string TypeId { get; set; }
        public string? TypeName { get; set; }

        public string BrandId { get; set; }
        public string? BrandName { get; set; }
    }
}
