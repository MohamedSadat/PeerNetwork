using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerLib.Data
{
    public class ContractModel
    {
        public string Owner { get; set; } = "";
        public string Address { get; set; } = "";
        public string Name { get; set; } = "";
        public uint Balance { get; set; } = 0;

    }
}
