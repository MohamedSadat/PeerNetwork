using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerLib.Data
{
    public class BlockModel
    {
        public string PrevBlockHash { get; set; } = "";
        public string BlockHash { get; set; } = "";
        public long Height { get; set; } = 0;
        public long TimeStamp { get; set; } = DateTime.Now.Ticks;
        public string NodePubKey { get; set; } = "";
        public uint TotalAmount { get; set; } = 0;
        public uint TotalFee { get; set; } = 0;
        public uint TotalMsgs { get; set; } = 0;
        public List<MessageModel> Messages { get; set; } = new List<MessageModel>();

    }
}
