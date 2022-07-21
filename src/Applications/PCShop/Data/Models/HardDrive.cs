namespace PCShop.Data.Models
{
    public class HardDrive : BaseModel<string>
    {
        public string Capacity { get; set; }
        public int RPM { get; set; }
        public string Interfaces { get; set; }
    }
}
