using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerLib.Data
{
    public class BlovkChainAppModel
    {
        public string DataPath { get; set; } = "Server\\";
        public NodeModel Node { get; set; } = new NodeModel();
        public List<string> Logs { get; set; } = new List<string>();
    }
}
