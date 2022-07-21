namespace PCShop.ViewModels.CPU
{
    using WebServer.MVC.Attributes.Validation;
    public class CreateInputModel
    {
        [Required("Name is required")]
        public string Name { get; set; }
        [Required("Image is required")]
        public string ImageUrl { get; set; }
        [Required("Price is required")]
        public double Price { get; set; }
        [Required("Frequency is required")]
        public double Frequency { get; set; }
        public double TurboFrequency { get; set; }
        [Required("Physical cores is required")]
        public int PhysicalCores { get; set; }
        public int LogicalCores { get; set; }
        [Required("Cache is required")]
        public string Cache { get; set; }
    }
}
