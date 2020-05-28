using System.Threading.Tasks;

namespace Framework
{
    public interface ICommandBus
    {
        Task Send<T>(T message) where T : class;
    }
}