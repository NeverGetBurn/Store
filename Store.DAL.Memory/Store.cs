using System;
using System.Linq;
using Store.CORE.Interfaces;
using Store.CORE.Models;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;

namespace Store.DAL.Memory
{
    public class Store:IStorage
    {
        private readonly Dictionary<int,Product> _products = new Dictionary<int, Product>();
        private readonly object _lock = new object();
        private int _maxId = 0;
        public Product Insert(Product value)
        {
            lock(_lock){
                value.Id = _maxId;
                _maxId++;
                _products.Add(value.Id,value);
            }
            return value;
        }

        public ICollection<Product> Insert(IEnumerable<Product> value)
        {
            var result = new List<Product>();
            return value.Select(s=>Insert(s)).ToArray();
        }

        public bool Update(Product value)
        {
            lock(_lock){
                if(!_products.ContainsKey(value.Id)){
                    return false; 
                }
                _products[value.Id]=value;
            }
            return true;
        }

        public int Update(IEnumerable<Product> values) => values.Select(s=>Update(s)).Where(w=>w == true).Count();
        public IQueryable<Product> Products => _products.Values.AsQueryable();

    }
}
