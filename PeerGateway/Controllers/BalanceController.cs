using Microsoft.AspNetCore.Mvc;
using PeerLib.Data;
using PeerLib.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PeerGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly MsgService imsg;
        private readonly BalanceService balanceService;

        public BalanceController(MsgService imsg,BalanceService balanceService)
        {
            this.imsg = imsg;
            this.balanceService = balanceService;
      
        }


        [HttpGet("GetBalance/{key}")]
        public ActionResult<long> GetMyMessage(string key)
        {
            return Ok( balanceService.GetBalance(key));
        }
     
    }
}
