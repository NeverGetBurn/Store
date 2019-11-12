using System.Collections.Generic;
using Store.CORE.Models;
using System;
using System.Linq;

namespace Store.DAL.Memory.Infrastructure
{
    internal class ProductTable
    {
        private static int _id =0;
        private Dictionary<int,Product> _store = new Dictionary<int, Product>();
        
        private object _lockItem = new object();
        private object _lockId = new object();
        public Product Insert(Product value){
            lock(_lockId){
                value.Id = _id;
                lock(_lockItem){
                    _store.Add(_id,value);
                }
                _id++;
            }
            return value;
        }

        public List<Product> Insert(IEnumerable<Product> value) 
            => value.Where(w=>w != null).Select(s=> Insert(s)).ToList();

        public void Update(Product value){
            if(value == null){
                throw new ArgumentNullException(nameof(value));
            }
            if(value.Id == 0){
                throw new InvalidOperationException($"{nameof(value)} Id not set. cant update.");
            }
            lock(_lockItem){
                if(!_store.ContainsKey(value.Id)){
                    throw new InvalidOperationException($"database not contains item with Id = {value.Id} not set. cant update.");
                }
                _store[value.Id] = value;
            }
        }

        public bool Delete(Product value)
        {
            lock(_lockItem){
                if(!_store.ContainsKey(value.Id)){
                    throw new InvalidOperationException($"database not contains item with Id = {value.Id} not set. cant update.");
                }
                return _store.Remove(value.Id);
            }
        }

        public void Update(IEnumerable<Product> value){
            if(value == null){
                throw new ArgumentNullException(nameof(value));
            }
            var items = value.Where(w=>w != null);
            foreach(var item in items){
                Update(items);
            }
        }
        public IReadOnlyCollection<Product> Items {
            get{
                IReadOnlyList<Product> items = null; 
                lock(_lockItem){
                    items = _store.Values.ToList().AsReadOnly();
                }
                return items;
            }
        }
        public Product this[int index] {
            get{
                Product item = null;
                lock(_lockItem){
                    var t =_store[index];
                    item = new Product{
                        Id = t.Id,
                        Name = t.Name,
                        Description = t.Description,
                        Price =t.Price,
                        Status =t.Status
                    };
                }
                return item;
            }
        }
    }
}