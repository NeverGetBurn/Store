using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Store.Models
{
    public class Context : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}