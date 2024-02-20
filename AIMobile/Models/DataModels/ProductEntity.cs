namespace AIMobile.Models.DataModels
{
    public class ProductEntity:BaseEntity
    {
        public string Name { get; set; }
        public string UnitPrice { get; set; }
        public string TypeId { get; set; }
        public string BrandId { get; set; }
    }
}
