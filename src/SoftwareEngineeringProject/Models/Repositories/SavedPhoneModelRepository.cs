using Microsoft.EntityFrameworkCore;
using SoftwareEngineeringProject.Data;
using SoftwareEngineeringProject.Models;
using SoftwareEngineeringProject.Models.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SoftwareEngineeringProject.Models.Repositories
{
    public class SavedPhoneModelRepository : IJoinTableRepository<SavedPhoneModel, string, string>
    {
        ApplicationDbContext context;

        public SavedPhoneModelRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<SavedPhoneModel> GetAll()
        {
            return context.SavedPhoneModels.Include(spm => spm.PhoneModel).ThenInclude(pm => pm.Phones).ThenInclude(pm => pm.VendorPhones).AsEnumerable();
        }

        public IEnumerable<SavedPhoneModel> GetAllForUserID(string userID)
        {
            return GetAll().Where(spm => spm.UserID == userID);
        }

        public void Add(SavedPhoneModel savedPhoneModelIn)
        {
            context.SavedPhoneModels.Add(savedPhoneModelIn);
            context.SaveChanges();
        }

        public SavedPhoneModel Find(string userID, string phoneModelID)
        {
            return context.SavedPhoneModels.Include(spm => spm.PhoneModel).ThenInclude(pm => pm.Phones).ThenInclude(pm => pm.VendorPhones).SingleOrDefault(t => t.UserID == userID && t.PhoneModelID == phoneModelID);
        }

        public bool Remove(SavedPhoneModel savedPhoneModelIn)
        {
            bool successful = context.SavedPhoneModels.Remove(savedPhoneModelIn) != null;

            if (successful)
            {
                context.SaveChanges();
            }

            return successful;
        }

        public bool Remove(string userID, string phoneModelID)
        {
            return Remove(Find(userID, phoneModelID));
        }

        public void Update(SavedPhoneModel savedPhoneModelIn)
        {
            context.SavedPhoneModels.Update(savedPhoneModelIn);
            context.SaveChanges();
        }
    }
}