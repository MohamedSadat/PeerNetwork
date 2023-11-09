using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerLib.Data
{
    public class TransactionModel
    {
        public string PrivateKey { get; set; } = "6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b";
        public MessageModel Message { get; set; } = new MessageModel();
    }
}
