using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerLib.Data
{
    public class WalletAppModel
    {
        public int FirstRun { get; set; } = 0;
        public string NodeAddress { get; set; } = "";
        public string PrivateKey { get; set; } = "";
        public string PrivateKeyXml { get; set; } = "";

        public string PublicKey { get; set; } = "";
        public string PublicKeyXML { get; set; } = "";
        public string WalletName { get; set; } = "Msg Wallet";

    }
}
