using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerLib.Data
{
    public class MsgIndexModel
    {
        public long Height { get; set; } = 0;
        public string Sender { get; set; } = "";
        public string Receiver { get; set; } = "";


    }
}
