using System.Collections.Generic;

namespace Sales.Orders
{
    public interface ICostCalculator
    {
        decimal CostForProducts(IEnumerable<string> productIds);
    }
}