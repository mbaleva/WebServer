namespace PCShop.Data.Models
{
    public class CPU : BaseModel<string>
    {
        public double NormalFrequency { get; set; }
        public double TurboFrequency { get; set; }
        public int PhysicalCores { get; set; }
        public int LogicalCores { get; set; }
        public string Cache { get; set; }
    }
}
