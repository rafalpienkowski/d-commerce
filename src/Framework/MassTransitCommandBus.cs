using System;
using System.Threading.Tasks;
using MassTransit;
using Newtonsoft.Json;

namespace Framework
{
    public class MassTransitCommandBus : ICommandBus
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public MassTransitCommandBus(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task Send<T>(T message) where T : class
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(typeof(T).GetUriForMessage());
            Console.WriteLine($"{typeof(T).GetUriForMessage()} send: {JsonConvert.SerializeObject(message)}");
            await sendEndpoint.Send(message);
        }
    }
}