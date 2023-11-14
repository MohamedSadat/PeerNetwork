using PeerLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerLib.Services
{
    public class MsgQueryService
    {
        private readonly BlovkChainAppModel app;
        private readonly MsgIndexService msgIndex;

        public MsgQueryService(BlovkChainAppModel app, MsgIndexService msgIndex)
        {
            this.app = app;
            this.msgIndex = msgIndex;
        }
        public Stack<MessageModel> GetMsgs()
        {
            return app.Node.Messages;
        }
        public List<MessageModel> GetMessages()
        {
            var messages = new List<MessageModel>();
            using (var stream = File.Open($"{app.DataPath}msg.dat", FileMode.Open))
            {
                //      Console.WriteLine("Size is {0}", stream.Length);
                using (var reader = new BinaryReader(stream, Encoding.UTF8))
                {
                    while (reader.PeekChar() > -1)
                    {
                        var msg = new MessageModel();
                        msg.NodePubKey = reader.ReadString();
                        msg.Sender = reader.ReadString();
                        msg.Receiver = reader.ReadString();
                        msg.PublicKey = reader.ReadString();

                        msg.Txt = reader.ReadString();
                        msg.Amount = reader.ReadUInt32();
                        msg.Fee = reader.ReadUInt32();
                        msg.Height = reader.ReadInt64();
                        msg.MsgHash = reader.ReadString();
                        msg.Signature = reader.ReadString();
                        msg.TimeStamp = reader.ReadInt64();

                        messages.Add(msg);
                    }
                }
            }
            return messages;

        }
        public List<MessageModel> GetMessagesByAddress(string key)
        {
            var indexes = msgIndex.GetIndexesByKey(key);
            if(indexes.Count==0)
            {
                return new List<MessageModel>();
            }

            var messages = new List<MessageModel>();
            using (var stream = File.Open($"{app.DataPath}msg.dat", FileMode.Open))
            {
                //      Console.WriteLine("Size is {0}", stream.Length);
                using (var reader = new BinaryReader(stream, Encoding.UTF8))
                {
                    stream.Position = 0;
                    foreach(var index in indexes)
                    {
                        stream.Position = index.Height;
                        var msg = new MessageModel();
                        msg.NodePubKey = reader.ReadString();
                        msg.Sender = reader.ReadString();
                        msg.Receiver = reader.ReadString();
                        msg.PublicKey = reader.ReadString();

                        msg.Txt = reader.ReadString();
                        msg.Amount = reader.ReadUInt32();
                        msg.Fee = reader.ReadUInt32();
                        msg.Height = reader.ReadInt64();
                        msg.MsgHash = reader.ReadString();
                        msg.Signature = reader.ReadString();
                        msg.TimeStamp = reader.ReadInt64();
                        messages.Add(msg);
                    }
             
                }
            }
            return messages;

        }
        
    }
}
