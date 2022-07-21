namespace PCShop.Services.Memory
{
    using PCShop.Data.Models;
    using PCShop.Data;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class MemoryService : IMemoryService
    {
        private readonly ApplicationDbContext dbContext;

        public MemoryService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public string Create(RAM model)
        {
            model.Id = Guid.NewGuid().ToString();
            this.dbContext.Memory.Add(model);
            this.dbContext.SaveChanges();
            return model.Id;
        }

        public void Delete(string id)
        {
            var entity = this.dbContext.Memory.Where(x => x.Id == id)
                .FirstOrDefault();
            this.dbContext.Memory.Remove(entity);
            this.dbContext.SaveChanges();
        }

        public RAM GetById(string id)
        {
            return this.dbContext.Memory.Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public void Update(RAM model)
        {
            var entity = this.dbContext.Memory.Where(x => x.Id == model.Id)
                .FirstOrDefault();
            entity.Name = model.Name;
            entity.Price = model.Price;
            entity.ImageUrl = model.ImageUrl;
            entity.MemoryType = model.MemoryType;
            entity.Frequency = model.Frequency;
            entity.Capacity = model.Capacity;
            this.dbContext.SaveChanges();
        }
        public List<PCShop.ViewModels.Shared.BaseModel> All()
        {
            return this.dbContext.Memory.Select(x => new ViewModels.Shared.BaseModel
            {
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Price = x.Price,
                Name = x.Name
            }).ToList();
        }
        public List<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.dbContext.Memory.Select(x => new
            KeyValuePair<string, string>(x.Id, x.Name)).ToList();
        }
    }
}
