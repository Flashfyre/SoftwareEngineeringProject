using Microsoft.EntityFrameworkCore;
using SoftwareEngineeringProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareEngineeringProject.Models.Repositories
{
    public class ManufacturerRepository : IRepository<Manufacturer, string>
    {
        ApplicationDbContext context;

        public ManufacturerRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Manufacturer> GetAll()
        {
            return context.Manufacturers.Include(m => m.PhoneModels).AsEnumerable();
        }

        public void Add(Manufacturer ManufacturerIn)
        {
            context.Manufacturers.Add(ManufacturerIn);
            context.SaveChanges();
        }

        public Manufacturer Find(string id)
        {
            return context.Manufacturers.Include(m => m.PhoneModels).SingleOrDefault(q => q.ManufacturerID == id);
        }

        public bool Remove(Manufacturer ManufacturerIn)
        {
            bool successful = context.Manufacturers.Remove(ManufacturerIn) != null;

            if (successful)
            {
                context.SaveChanges();
            }

            return successful;
        }

        public bool Remove(string id)
        {
            return Remove(Find(id));
        }

        public void Update(Manufacturer ManufacturerIn)
        {
            context.Manufacturers.Update(ManufacturerIn);
            context.SaveChanges();
        }
    }
}