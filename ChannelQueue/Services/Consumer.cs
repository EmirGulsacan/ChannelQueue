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
            _processedMessages = new string[capacity]; // Kapasiteyi ayarlarız
            _index = 0;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var message in _channelReader.ReadAllAsync(stoppingToken))
            {
                try
                {
                    // Mesaj işleme işlemleri burada yapılır
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
            // Mesaj işleme logic'i burada yer alır
            if (_index < _processedMessages.Length)
            {
                _processedMessages[_index++] = message;
            }
            else
            {
                _logger.LogWarning("Message array capacity reached. Message discarded.");
            }

            // 1 saniye bekleme süresi ekle. Bu kısım console'dan kuyrukta işlenen veriyi gözlemlemek için koyulmuştur.
            // Kaldırırsanız veriyi çok hızlı işleyecektir.
            await Task.Delay(1000);
        }
    }
}
