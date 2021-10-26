using MobileWarehouse.Entity.Models;
using System.Linq;

namespace MobileWarehouse.Entity.PreDeploimentDate
{
    public static class SampleData
    {
        public static void Initialize(ApplicationContext context)
        {
            if (!context.Phones.Any())
            {
                context.Phones.AddRange(
                    new Phone
                    {
                        Name = "iPhone X",
                        Company = "Apple",
                        Price = 600
                    },
                    new Phone
                    {
                        Name = "Samsung Galaxy Edge",
                        Company = "Samsung",
                        Price = 550
                    },
                    new Phone
                    {
                        Name = "Pixel 3",
                        Company = "Google",
                        Price = 500
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
