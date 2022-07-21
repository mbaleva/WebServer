namespace PCShop.Data.Models
{
    public class Laptop : BaseModel<string>
    {
        public CPU Processor { get; set; }
        public GPU GraphicalProcessor { get; set; }
        public RAM RamMemory { get; set; }
        public HardDrive Memory { get; set; }
        public string MonitorInfo { get; set; }
        public string OS { get; set; }
    }
}
