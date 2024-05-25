using AliceMarket.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AliceMarket.Contexts
{
    public class AliceMarketDbContext : DbContext
    {
        public AliceMarketDbContext() : base("name=AliceMarketDbContext")
        {
        }
        public DbSet<ProductsModel> Products { get; set; }
        public DbSet<CustomersModel> Customers { get; set; }    

    }
}
