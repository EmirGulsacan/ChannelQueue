using System.Threading.Channels;

namespace ChannelQueue.Services
{
    public class Consumer : BackgroundService
    {
        private readonly ChannelReader<string> _channelReader;
        private readonly ILogger<Consumer> _logger;
        private readonly string[] _processedMessages;
        private int _index;

        public Consumer(ChannelReader<string> channelReader, ILogger<Consumer> logger, int capacity)
        {
            _channelReader = channelReader;
            _logger = logger;
            _processedMessages = new string[capacity]; // Set capacity
            _index = 0;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var message in _channelReader.ReadAllAsync(stoppingToken))
            {
                try
                {
                    // Message processing is done here
                    _logger.LogInformation($"Consumed: {message}");
                    await ProcessMessageAsync(message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing message");
                }
            }
        }

        private async Task ProcessMessageAsync(string message)
        {
            // Message processing logic is located here
            if (_index < _processedMessages.Length)
            {
                _processedMessages[_index++] = message;
            }
            else
            {
                _logger.LogWarning("Message array capacity reached. Message discarded.");
            }

            // Add 1 second cooldown. This section is included to observe the data processed in the queue from the console.
            // If you remove it, it will process data very quickly.
            await Task.Delay(1000);
        }
    }
}
