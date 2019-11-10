using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;

namespace Store.Core.Models
{
    public class Context : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}