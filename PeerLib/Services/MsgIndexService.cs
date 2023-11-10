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
                }
            }
        }
    }
}
