using System;
using System.Linq;
using Store.CORE.Interfaces;
using Store.CORE.Models;
using System.Collections.Generic;

namespace Store.DAL.Memory
{
    using Infrastructure;
    public class Store:IStorage
    {
        public Product Insert(Product value) => FakeDb.Products.Insert(value);
        public ICollection<Product> Insert(IEnumerable<Product> value)=>FakeDb.Products.Insert(value);

        public bool Update(Product value)
        {
            FakeDb.Products.Update(value);
            return true;
        }

        public int Update(IEnumerable<Product> values){
            FakeDb.Products.Update(values);
            return values.Count();
        }
        public void Dispose() { }

        public bool Delete(Product value) => FakeDb.Products.Delete(value);
        public IQueryable<Product> Products => FakeDb.Products.Items.AsQueryable();
    }
}
