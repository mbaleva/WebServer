namespace PCShop.ViewModels.GPU
{
    using PCShop.ViewModels.Shared;
    public class GPUViewModel : BaseModel
    {
        public string GraphicalProcessor { get; set; }
        public int Capacity { get; set; }
        public string MemoryType { get; set; }
        public string Interfaces { get; set; }
        public string DirectXSupport { get; set; }
    }
}
