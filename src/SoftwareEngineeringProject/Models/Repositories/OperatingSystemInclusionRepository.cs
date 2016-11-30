using Microsoft.EntityFrameworkCore;
using SoftwareEngineeringProject.Data;
using SoftwareEngineeringProject.Models;
using SoftwareEngineeringProject.Models.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SoftwareEngineeringProject.Models.Repositories
{
    public class OperatingSystemInclusionRepository : IJoinTableRepository<OperatingSystemInclusion, string, string>
    {
        ApplicationDbContext context;

        public OperatingSystemInclusionRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<OperatingSystemInclusion> GetAll()
        {
            return context.OperatingSystemInclusions.AsEnumerable();
        }

        public IEnumerable<OperatingSystemInclusion> GetAllForIncludedOSID(string iosID)
        {
            return context.OperatingSystemInclusions.Where(osi => osi.IncludedOperatingSystemID == iosID).AsEnumerable();
        }

        public void Add(OperatingSystemInclusion OperatingSystemInclusionIn)
        {
            context.OperatingSystemInclusions.Add(OperatingSystemInclusionIn);
            context.SaveChanges();
        }

        public OperatingSystemInclusion Find(string osID, string iosID)
        {
            return context.OperatingSystemInclusions.SingleOrDefault(osi => osi.OperatingSystemID == osID && osi.IncludedOperatingSystemID == iosID);
        }

        public bool Remove(OperatingSystemInclusion OperatingSystemInclusionIn)
        {
            bool successful = context.OperatingSystemInclusions.Remove(OperatingSystemInclusionIn) != null;

            if (successful)
            {
                context.SaveChanges();
            }

            return successful;
        }

        public bool Remove(string osID, string iosID)
        {
            return Remove(Find(osID, iosID));
        }

        public void Update(OperatingSystemInclusion OperatingSystemInclusionIn)
        {
            context.OperatingSystemInclusions.Update(OperatingSystemInclusionIn);
            context.SaveChanges();
        }
    }
}