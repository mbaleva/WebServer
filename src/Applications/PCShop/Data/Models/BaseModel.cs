namespace PCShop.Data.Models
{
    public class BaseModel<TId>
    {
        public TId Id { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
    }
}
