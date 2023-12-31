﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerLib.Data
{
    public class MessageModel
    {
        public string NodePubKey { get; set; } = "";
        public long BlockHeight { get; set; } = 0;
        public string Txt { get; set; } = "";
       
        public string Sender { get; set; } = "";
        [Required]
        public string Receiver { get; set; } = "";
        public long Height { get; set; } = 0;
        public uint Amount { get; set; } = 0;
        public uint Fee { get; set; } = 0;
        //size 65
        public string MsgHash { get; set; } = "";
        public string PublicKey { get; set; } = "";
        
        public string Signature { get; set; } = "";
        public long TimeStamp { get; set; } = DateTime.Now.Ticks;
    }
}
