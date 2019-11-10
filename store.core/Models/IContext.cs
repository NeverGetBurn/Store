using System;
using Microsoft.EntityFrameworkCore;

namespace Store.Core.Models
{
    public interface IContext: IDisposable
    {
        DbSet<Product> Products { get; set; }
    }
}