using Microsoft.EntityFrameworkCore;
using SoftwareEngineeringProject.Data;
using SoftwareEngineeringProject.Models;
using SoftwareEngineeringProject.Models.Repositories;
using System.Collections.Generic;
using System.Linq;

public class PhoneModelRepository : IRepository<PhoneModel, string>
{
    protected ApplicationDbContext context;

    public PhoneModelRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public IEnumerable<PhoneModel> GetAll()
    {
        return context.PhoneModels.Include(pm => pm.Manufacturer).Include(pm => pm.Phones).ThenInclude(p => p.VendorPhones).ThenInclude(vp => vp.Vendor).Include(pm => pm.Phones).ThenInclude(p => p.VendorPhones).ThenInclude(vp => vp.Carrier).AsEnumerable();
    }

    public void Add(PhoneModel PhoneModelIn)
    {
        context.PhoneModels.Add(PhoneModelIn);
        context.SaveChanges();
    }

    public PhoneModel Find(string id)
    {
        return context.PhoneModels.Include(pm => pm.Phones).ThenInclude(p => p.VendorPhones).ThenInclude(vp => vp.Vendor).Include(pm => pm.Phones).ThenInclude(p => p.VendorPhones).ThenInclude(vp => vp.Carrier).SingleOrDefault(q => q.PhoneModelID == id);
    }

    public bool Remove(PhoneModel PhoneModelIn)
    {
        bool successful = context.PhoneModels.Remove(PhoneModelIn) != null;

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

    public void Update(PhoneModel PhoneModelIn)
    {
        context.PhoneModels.Update(PhoneModelIn);
        context.SaveChanges();
    }
}