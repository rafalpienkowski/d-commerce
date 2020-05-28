using System.Collections.Generic;

namespace Sales.Orders
{
    public class SillyCostCalculator : ICostCalculator
    {
        public decimal CostForProducts(IEnumerable<string> productIds) => 1999.99m;
    }
}