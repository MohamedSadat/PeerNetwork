﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly MsgSign sign;
        private readonly MsgHashService msgHash;
        private readonly MsgQueryService msgQuery;
        private readonly MsgIndexService indexService;

        public MessageController(MsgService imsg,MsgSign sign,MsgHashService msgHash,MsgQueryService msgQuery,MsgIndexService indexService)
        {
            this.imsg = imsg;
            this.sign = sign;
            this.msgHash = msgHash;
            this.msgQuery = msgQuery;
            this.indexService = indexService;
        }
        // GET: api/<MessageController>
        [HttpGet]
        public IEnumerable<MessageModel> Get()
        {
            return msgQuery.GetMessages();
        }
        [HttpGet("MyMessage/{key}")]
        public IEnumerable<MessageModel> GetMyMessage(string key)
        {
            return msgQuery.GetMessagesByAddress(key);
        }
        [HttpGet("GetIndexes")]
        public IEnumerable<MsgIndexModel> GetIndexes()
        {
            return indexService.GetIndexes();
        }
        [HttpGet("MsgHeight")]
        public ActionResult GetMsgHeight()
        {
            return Ok(imsg.GetMessagesHeight());
        }
        [HttpGet("Validate")]
        public ActionResult GetValidate()
        {
            return Ok(imsg.ValidateMsg());
        }

        // POST api/<MessageController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TransactionModel  trans)
        {
            try
            {
                if (sign.Validate(trans) == false)
                {
                    return BadRequest("Invalid Signature");
                }

                await imsg.AddMsg(trans.Message);
                await imsg.PublishMsg(trans.Message);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<MessageController>
        [HttpPost("AddMsg")]
        public async Task<ActionResult> AddMsg([FromBody] TransactionModel trans)
        {
            try
            {
                if (sign.ValidateEx(trans) == false)
                {
                    return BadRequest("Invalid Signature");
                }

              var r=  await imsg.AddMsg(trans.Message);
                if(r.IsError)
                {
                    return BadRequest(r.Error);
                }
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
