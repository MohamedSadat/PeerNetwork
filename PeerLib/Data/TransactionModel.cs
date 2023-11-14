using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerLib.Data
{
    public class TransactionModel
    {
        public string PrivateKey { get; set; } = "";
        public string PrivateKeyXml { get; set; } = "";

        public string PublicKeyXML { get; set; } = "";
        public MessageModel Message { get; set; } = new MessageModel();
    }
}
