namespace PCShop.Services.CPU
{
    using PCShop.ViewModels.CPU;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    public interface IProcessorService
    {
        Task<string> Create(CreateInputModel model);
        ByIdModel GetById(string id);

        List<PCShop.ViewModels.Shared.BaseModel> GetAll();
        void Delete(string id);
        void Update(ByIdModel model);
        List<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
