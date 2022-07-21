namespace PCShop.Services.GPU
{
    using PCShop.ViewModels.GPU;
    using System.Collections.Generic;
    public interface IGPUService
    {
        List<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
        string Create(GPUViewModel model);
        GPUViewModel GetById(string id);

        List<PCShop.ViewModels.Shared.BaseModel> GetAll();
        void Delete(string id);
        void Update(GPUViewModel model);
    }
}
