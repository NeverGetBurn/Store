using System;
using System.Linq;
using Store.CORE.Interfaces;
using Store.CORE.Models;

using TinyCsvParser;

namespace Store.DAL.CSV
{
    public class Store:IStorage
    {
        private readonly string _csvFilePath;
        public Store(string filepath)
        {
            _csvFilePath = filepath ?? throw new ArgumentNullException(nameof(filepath));
        }
        public IQueryable<Product> Products => throw new NotImplementedException();
    }
}
