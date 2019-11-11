using System;
using System.Linq;
namespace Store.CORE.Interfaces
{
    using System.Collections.Generic;
    using Models;
    public interface IStorage:IDisposable
    {
         IQueryable<Product> Products{get;}
         Product Insert(Product value);
         ICollection<Product> Insert(IEnumerable<Product> value);
         bool Update(Product value);
         int Update(IEnumerable<Product> values);
         
    }
}