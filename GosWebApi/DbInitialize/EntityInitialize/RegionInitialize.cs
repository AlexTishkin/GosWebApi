using GosWebApi.Models;
using System;
using System.Threading.Tasks;

namespace GosWebApi.DbInitialize.EntityInitialize
{
    public class RegionInitialize
    {
        public static async Task InitializeAsync(ApplicationContext db)
        {
            await db.Regions.AddRangeAsync(
                new Region(Guid.NewGuid(), "Центральный район"),
                new Region(Guid.NewGuid(), "Сеймский район")
            );
          
            db.SaveChanges();
        }
    }
}