namespace PCShop.Data.Models
{
    public class RAM : BaseModel<string>
    {
        public int Capacity { get; set; }
        public string MemoryType { get; set; }
        public int Frequency { get; set; }
    }
}
