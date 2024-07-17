using ChannelQueue.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChannelQueue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly Producer _producer;

        public MessageController(Producer producer)
        {
            _producer = producer;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<string> messages)
        {
            await _producer.ProduceAsync(messages);
            return Accepted();
        }
    }
}
