using System;
using Microsoft.EntityFrameworkCore;

namespace Store.DB
{
    using Models;
    public interface IContext: IDisposable
    {
        DbSet<Product> Products { get; set; }
    }
}