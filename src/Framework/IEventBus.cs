using System.Threading.Tasks;

namespace Framework
{
    public interface IEventBus
    {
        Task Publish<T>(T message) where T : class;
        Task Publish(object message);
    }
}