namespace PCShop.ViewModels.Laptops
{
    using PCShop.ViewModels.Shared;
    using System.Collections.Generic;

    public class CreateInputModel : BaseModel
    {
        public string SelectedCpuId { get; set; }
        public string SelectedHddId { get; set; }
        public string SelectedGpuId { get; set; }
        public string SelectedRamId { get; set; }
        public List<KeyValuePair<string,string>> AvaliableCPUs { get; set; }
        public List<KeyValuePair<string, string>> AvaliableHDDs { get; set; }
        public List<KeyValuePair<string, string>> AvaliableRAMs { get; set; }
        public List<KeyValuePair<string, string>> AvaliableGPUs { get; set; }
        public string MonitorInfo { get; set; }
        public string OS { get; set; }
    }
}
