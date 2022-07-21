namespace PCShop.Services.HardDrives
{
    using PCShop.Data.Models;
    using PCShop.ViewModels.Shared;
    using System.Collections.Generic;
    using PCShop.Data;
    using System.Linq;
    using System;
    public class HardDriveService : IHardDriveService
    {
        private ApplicationDbContext dbContext;
        public HardDriveService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.dbContext.HardDrives.Select(x => 
            new KeyValuePair<string, string>(x.Id, x.Name)).ToList();
        }
        public string Create(HardDrive model)
        {
            model.Id = Guid.NewGuid().ToString();
            this.dbContext.HardDrives.Add(model);
            this.dbContext.SaveChanges();
            return model.Id;
        }

        public void Delete(string id)
        {
            var entity = this.dbContext.HardDrives
                .Where(x => x.Id == id).FirstOrDefault();
            this.dbContext.HardDrives.Remove(entity);
            this.dbContext.SaveChanges();
        }

        public List<BaseModel> GetAll()
        {
            return this.dbContext.HardDrives
                .Select(x => new BaseModel 
                {
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    Name = x.Name,
                    Price = x.Price
                }).ToList();
        }

        public HardDrive GetById(string id)
        {
            return this.dbContext.HardDrives.Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public void Update(HardDrive model)
        {
            var entity = this.dbContext.HardDrives.FirstOrDefault(x => x.Id == model.Id);
            entity.Name = model.Name;
            entity.Price = model.Price;
            entity.ImageUrl = model.ImageUrl;
            entity.RPM = model.RPM;
            entity.Capacity = model.Capacity;
            entity.Interfaces = model.Interfaces;
            this.dbContext.SaveChanges();
        }
    }
}
