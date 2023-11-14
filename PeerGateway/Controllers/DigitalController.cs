using Microsoft.AspNetCore.Mvc;
using PeerLib.Data;
using PeerLib.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PeerGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DigitalController : ControllerBase
    {
        private readonly MsgService imsg;
        private readonly MsgSign sign;
        private readonly MsgHashService msgHash;

        public DigitalController(MsgService imsg,MsgSign sign,MsgHashService msgHash)
        {
            this.imsg = imsg;
            this.sign = sign;
            this.msgHash = msgHash;
        }
     

      


        // POST api/<MessageController>
        [HttpPost("Sign")]
        public async Task<ActionResult> Sign([FromBody] TransactionModel trans)
        {
            try
            {

             //   MsgHashService.HashAlgoStd(trans.Message);
              var r=  sign.SignEx(trans);
                trans.Message.Signature = r;
                return Ok(trans);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("MsgHash")]
        public async Task<ActionResult> MsgHash([FromBody] TransactionModel trans)
        {
            try
            {
                var r= msgHash.HashAlgoStd(trans.Message);
                return Ok(r);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("GeneratePubKeyXml")]
        public async Task<ActionResult> GeneratePubKeyXml([FromBody] WalletAppModel wallet)
        {
            try
            {
                var r = sign.GeneratePublicKeyXML(wallet);
                return Ok(wallet);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("EncryptMsg")]
        public async Task<ActionResult> EncryptMsg([FromBody] TransactionModel trans)
        {
            try
            {
                var r = sign.SignTextMsg(trans);
                return Ok(r);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("DecryptMsg")]
        public async Task<ActionResult> DecryptMsg([FromBody] TransactionModel trans)
        {
            try
            {
                var r = sign.DescryptTextMsg(trans);
                return Ok(r);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
