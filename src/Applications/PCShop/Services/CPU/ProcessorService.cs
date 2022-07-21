namespace PCShop.Services.CPU
{
    using PCShop.ViewModels.CPU;
    using System.Threading.Tasks;
    using PCShop.Data;
    using PCShop.Data.Models;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class ProcessorService : IProcessorService
    {
        private ApplicationDbContext dbContext;
        public ProcessorService(ApplicationDbContext appDbContext)
        {
            this.dbContext = appDbContext;
        }
        public async Task<string> Create(CreateInputModel model)
        {
            var cpu = new CPU 
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                NormalFrequency = model.Frequency,
                TurboFrequency = model.TurboFrequency,
                Cache= model.Cache,
                LogicalCores = model.LogicalCores,
                PhysicalCores = model.PhysicalCores
            };
            await this.dbContext.Processors.AddAsync(cpu);
            await this.dbContext.SaveChangesAsync();

            return cpu.Id;
        }
        public ByIdModel GetById(string id) 
        {
            return this.dbContext.Processors.Where(x => x.Id == id)
                .Select(x => new ByIdModel
                {
                    Id = x.Id,
                    Cache = x.Cache,
                    Frequency = x.NormalFrequency,
                    TurboFrequency = x.TurboFrequency,
                    ImageUrl = x.ImageUrl,
                    LogicalCores = x.LogicalCores,
                    Name = x.Name,
                    PhysicalCores = x.PhysicalCores,
                    Price = x.Price
                }).FirstOrDefault();
        }
        public List<PCShop.ViewModels.Shared.BaseModel> GetAll() 
        {
            return this.dbContext.Processors.Select(x => new ViewModels.Shared.BaseModel
            {
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Name = x.Name,
                Price = x.Price
            }).ToList();
        }
        public void Delete(string id)
        {
            var entity = this.dbContext.Processors.Where(x => x.Id == id).FirstOrDefault();
            this.dbContext.Processors.Remove(entity);
            this.dbContext.SaveChanges();
        }
        public void Update(ByIdModel model) 
        {
            var entity = this.dbContext.Processors.Where(x => x.Id == model.Id).FirstOrDefault();
            if (entity != null)
            {
                entity.ImageUrl = model.ImageUrl;
                entity.Price = model.Price;
                entity.NormalFrequency = model.Frequency;
                entity.TurboFrequency = model.TurboFrequency;
                entity.Price = model.Price;
                entity.Name = model.Name;
                entity.Cache = model.Cache;
                entity.ImageUrl = model.ImageUrl;
                entity.PhysicalCores = model.PhysicalCores;
                entity.LogicalCores = model.LogicalCores;
            }
            this.dbContext.SaveChanges();
        }
        public List<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.dbContext.Processors
                .Select(x =>new KeyValuePair<string, string>
                (x.Id, x.Name)).ToList();
        }
    }
}
