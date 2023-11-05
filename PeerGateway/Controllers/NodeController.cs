using Microsoft.AspNetCore.Mvc;
using PeerLib.Data;
using PeerLib.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PeerGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NodeController : ControllerBase
    {
        private readonly AppModel app;
        private readonly NodeServices inode;

        public NodeController(AppModel app,NodeServices inode)
        {
            this.app = app;
            this.inode = inode;
        }
        // GET: api/<NodeController>
        [HttpGet]
        public IEnumerable<NodeModel> Get()
        {
            return inode.GetNodes();
        }

   

        // POST api/<NodeController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] NodeModel node)
        {
            try
            {
                await inode.AddNode(node);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
         
        }

    }
}
