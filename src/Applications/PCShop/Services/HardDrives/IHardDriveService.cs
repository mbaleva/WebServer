namespace PCShop.Services.HardDrives
{
    using PCShop.Data.Models;
    using System.Collections.Generic;
    public interface IHardDriveService
    {
        List<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
        string Create(HardDrive model);
        HardDrive GetById(string id);

        List<PCShop.ViewModels.Shared.BaseModel> GetAll();
        void Delete(string id);
        void Update(HardDrive model);
    }
}
