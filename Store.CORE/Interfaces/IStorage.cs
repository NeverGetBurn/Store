using System.Linq;
namespace Store.CORE.Interfaces
{
    using Models;
    public interface IStorage
    {
         IQueryable<Product> Products{get;}
    }
}