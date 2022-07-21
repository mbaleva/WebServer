namespace PCShop.Services.Memory
{
    using PCShop.Data.Models;
    using System.Collections.Generic;
    public interface IMemoryService
    {
        List<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
        string Create(RAM model);
        RAM GetById(string id);
        void Update(RAM model);
        void Delete(string id);
        List<PCShop.ViewModels.Shared.BaseModel> All();
    }
}
