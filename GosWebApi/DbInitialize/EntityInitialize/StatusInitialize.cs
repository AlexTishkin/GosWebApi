using GosWebApi.Models;
using System;
using System.Threading.Tasks;
using GosWebApi.Models.Entities;

namespace GosWebApi.DbInitialize.EntityInitialize
{
    public class StatusInitialize
    {
        public static async Task InitializeAsync(ApplicationContext db)
        {
            await db.Statuses.AddRangeAsync(
                new Status(Guid.NewGuid(), "Принят в обработку", 0),
                new Status(Guid.NewGuid(), "Ожидание оплаты", 1),
                new Status(Guid.NewGuid(), "Исполнение", 2),
                new Status(Guid.NewGuid(), "Проверка", 3),
                new Status(Guid.NewGuid(), "Завершен", 4),
                new Status(Guid.NewGuid(), "Отклонен", 5));

            await db.SaveChangesAsync();
        }
    }
}