namespace PCShop.Services.GPU
{
    using PCShop.ViewModels.GPU;
    using PCShop.ViewModels.Shared;
    using System.Collections.Generic;
    using PCShop.Data;
    using PCShop.Data.Models;
    using System;
    using System.Linq;
    public class GPUService : IGPUService
    {
        private ApplicationDbContext dbContext;
        public GPUService(ApplicationDbContext db)
        {
            this.dbContext = db;
        }
        public string Create(GPUViewModel model)
        {
            var gpu = new GPU 
            {
                Id = Guid.NewGuid().ToString(),
                Capacity = model.Capacity,
                DirectXSupport = model.DirectXSupport,
                GraphicalProcessor = model.GraphicalProcessor,
                ImageUrl = model.ImageUrl,
                Interfaces = model.Interfaces,
                MemoryType = model.MemoryType,
                Name = model.Name,
                Price = model.Price,
            };
            this.dbContext.GraphicalProcessor.Add(gpu);
            this.dbContext.SaveChanges();
            return gpu.Id;
        }

        public void Delete(string id)
        {
            var entity = this.dbContext.GraphicalProcessor.Where(x =>
            x.Id == id).FirstOrDefault();
            this.dbContext.GraphicalProcessor.Remove(entity);
            this.dbContext.SaveChanges();
        }

        public List<BaseModel> GetAll()
        {
            return this.dbContext.GraphicalProcessor
                .Select(x => new BaseModel 
                {
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    Name = x.Name,
                    Price = x.Price
                }).ToList();
        }

        public GPUViewModel GetById(string id)
        {
            return this.dbContext.GraphicalProcessor.Where(x =>
            x.Id == id).Select(x => new GPUViewModel 
            {
                Capacity = x.Capacity,
                DirectXSupport = x.DirectXSupport,
                Id = x.Id,
                GraphicalProcessor = x.GraphicalProcessor,
                ImageUrl = x.ImageUrl,
                Interfaces = x.Interfaces,
                MemoryType = x.MemoryType,
                Name = x.Name,
                Price = x.Price
            }).FirstOrDefault();
        }

        public void Update(GPUViewModel model)
        {
            var entity = this.dbContext.GraphicalProcessor.Where(x => x.Id == model.Id)
                .FirstOrDefault();

            if (entity != null)
            {
                entity.Name = model.Name;
                entity.Price = model.Price;
                entity.ImageUrl = model.ImageUrl;
                entity.Interfaces = model.Interfaces;
                entity.MemoryType = model.MemoryType;
                entity.GraphicalProcessor = model.GraphicalProcessor;
                entity.DirectXSupport = model.DirectXSupport;
                entity.Capacity = model.Capacity;
            }
            this.dbContext.SaveChanges();
        }
        public List<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.dbContext.GraphicalProcessor
                .Select(x => new KeyValuePair<string, string>
                (x.Id, x.Name)).ToList();
        }
    }
}
