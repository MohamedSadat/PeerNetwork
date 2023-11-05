using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerLib.Data
{
    public class MessageModel
    {
        public string Txt { get; set; } = "";
        public string Sender { get; set; } = "";
        public long Height { get; set; } = 0;
    }
}
