namespace PCShop.ViewModels.CPU
{
    using PCShop.ViewModels.Shared;
    public class ByIdModel : BaseModel
    {
        public double Frequency { get; set; }
        public double TurboFrequency { get; set; }
        public int PhysicalCores { get; set; }
        public int LogicalCores { get; set; }
        public string Cache { get; set; }
    }
}
