﻿namespace PCShop.Data.Models
{
    public class GPU : BaseModel<string>
    {
        public string GraphicalProcessor { get; set; }
        public int Capacity { get; set; }
        public string MemoryType { get; set; }
        public string Interfaces { get; set; }
        public string DirectXSupport { get; set; }
    }
}
