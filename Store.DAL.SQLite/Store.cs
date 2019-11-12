using System;
using System.Collections.Generic;
using System.Linq;
using Store.CORE.Interfaces;
using Store.CORE.Models;

namespace Store.DAL.SQLite
{
    using Context;
    public class Store : IStorage
    {
        private readonly SqliteContext _context; 
        public Store(string connection)
        {
            _context = new SqliteContext(connection);
        }
        public IQueryable<Product> Products {
            get{
                return _context.Products.AsQueryable();
            }
        }

        public void Dispose()
        {
            if(_context != null){
                _context.Dispose();
            }
        }

        public Product Insert(Product value)
        {
            return Insert(value,true);
        }

        private Product Insert(Product value, bool save)
        {
            var result = _context.Products.Add(value);
            if(save){
                _context.SaveChanges();
            }
            return result.Entity;
        }

        public ICollection<Product> Insert(IEnumerable<Product> value)
        {
            var results = value.Select(s=>Insert(s,false)).ToList();
            _context.SaveChanges();
            return results;
        }

        public bool Update(Product value)
        {
            return Update(value,true);
        }

        private bool Update(Product value, bool save)
        {
            var updatedEntity = _context.Update(value);
            if(save){
                _context.SaveChanges();
            }
            return updatedEntity.Entity == value;
        }

        public int Update(IEnumerable<Product> values)
        {
            var result = values.Select(s=>Update(s,false)).Where(w=>w).Count();
            _context.SaveChanges();
            return result;
        }

        public bool Delete(Product value)
        {
            _context.Remove(value);
            _context.SaveChanges();
            return true;
        }
    }
}
