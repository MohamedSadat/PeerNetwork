using PeerLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerLib.Services
{
    public class MsgIndexService
    {
        private readonly AppModel app;
        private readonly NodeServices inode;

        public MsgIndexService(AppModel app, NodeServices inode)
        {
            this.app = app;
            this.inode = inode;
        }
        public void WriteIndex(MessageModel msg)
        {
            if (File.Exists($"{app.DataPath}indexes.dat") == false)
            {
                var f = File.Create($"{app.DataPath}indexes.dat");
                f.Close();

            }

            using (var stream = File.Open($"{app.DataPath}indexes.dat", FileMode.Append))
            {
                //Append will add to the end of the file
                using (var writer = new BinaryWriter(stream, Encoding.UTF8))
                {
                    writer.Write(msg.Height);
                    writer.Write(msg.Sender);
                    writer.Write(msg.Receiver);
                }
            }
        }
        public List<MsgIndexModel> GetIndexes()
        {
            var msgindexes = new List<MsgIndexModel>();
            using (var stream = File.Open($"{app.DataPath}indexes.dat", FileMode.Open))
            {
                //      Console.WriteLine("Size is {0}", stream.Length);
                using (var reader = new BinaryReader(stream, Encoding.UTF8))
                {
                    while (reader.PeekChar() > -1)
                    {
                        var msg = new MsgIndexModel();
                        msg.Height = reader.ReadInt64();
                        msg.Sender = reader.ReadString();
                        msg.Receiver = reader.ReadString();
                        msgindexes.Add(msg);
                    }
                }
            }
            return msgindexes;

        }
        public List<MsgIndexModel> GetIndexesByKey(string key)
        {
            var msgindexes = new List<MsgIndexModel>();
            using (var stream = File.Open($"{app.DataPath}indexes.dat", FileMode.Open))
            {
                //      Console.WriteLine("Size is {0}", stream.Length);
                using (var reader = new BinaryReader(stream, Encoding.UTF8))
                {
                    while (reader.PeekChar() > -1)
                    {
                        var msg = new MsgIndexModel();
                        msg.Height = reader.ReadInt64();
                        msg.Sender = reader.ReadString();
                        msg.Receiver = reader.ReadString();
                        if (msg.Sender == key || msg.Receiver == key)
                        {
                            msgindexes.Add(msg);
                        }
                    }
                }
            }
            return msgindexes;

        }
    }
}
