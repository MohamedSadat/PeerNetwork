using Microsoft.AspNetCore.Mvc;
using PeerLib.Data;
using PeerLib.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PeerGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MsgService imsg;

        public MessageController(MsgService imsg)
        {
            this.imsg = imsg;
        }
        // GET: api/<MessageController>
        [HttpGet]
        public IEnumerable<MessageModel> Get()
        {
            return imsg.GetMessages();
        }
        [HttpGet("MsgHeight")]
        public ActionResult GetMsgHeight()
        {
            return Ok(imsg.GetMessagesHeighr());
        }


        // POST api/<MessageController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MessageModel msg)
        {
            try
            {
                await imsg.AddMsg(msg);
                await imsg.PublishMsg(msg);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<MessageController>
        [HttpPost("AddMsg")]
        public async Task<ActionResult> AddMsg([FromBody] MessageModel msg)
        {
            try
            {
                await imsg.AddMsg(msg);
             //   await imsg.PublishMsg(msg);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
