using Facade._Task.Entities;

namespace Facade._Task.Interfaces;

interface IProductCatalog
{
    Product GetProductDetails(string productId);
}