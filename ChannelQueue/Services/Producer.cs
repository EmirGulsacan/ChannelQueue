using System.Threading.Channels;

namespace ChannelQueue.Services
{
    public class Producer
    {
        private readonly ChannelWriter<string> _channelWriter;

        public Producer(ChannelWriter<string> channelWriter)
        {
            _channelWriter = channelWriter;
        }

        public async Task ProduceAsync(string message)
        {
            await _channelWriter.WriteAsync(message);
        }

        public async Task ProduceAsync(IEnumerable<string> messages)
        {
            foreach (var message in messages)
            {
                await _channelWriter.WriteAsync(message);
            }
        }
    }
}
