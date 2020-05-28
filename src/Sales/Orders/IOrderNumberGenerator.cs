using System.Threading.Tasks;

namespace Sales.Orders
{
    public interface IOrderNumberGenerator
    {
        Task<string> GetNumber(string userId);
    }
}