using AliceMarket.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliceMarket.Contexts
{
    internal class AliceMarketDbContext
    {
        public DbSet<CustomersModel> Customers { get; set; }
        public DbSet<ProductsModel> Products { get; set; }
        public DbSet<OrdersModel> Orders { get; set; }
        public DbSet<CategoriesModel> Categories { get; set; }

    }
}
