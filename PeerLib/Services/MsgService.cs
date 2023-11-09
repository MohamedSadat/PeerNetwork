using PeerLib.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PeerLib.Services
{
    public class MsgService
    {
        private readonly AppModel app;
        private readonly NodeServices inode;

        public MsgService(AppModel app,NodeServices inode)
        {
            this.app = app;
            this.inode = inode;
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
                        msg.Sender = reader.ReadString();
                        msg.PublicKey = reader.ReadString();

                        msg.Txt = reader.ReadString();
                        msg.Height = reader.ReadInt64();
                        msg.MsgHash = reader.ReadString();
                        msg.Signature = reader.ReadString();

                        messages.Add(msg);
                    }
                }
            }
            return messages;

        }
        public long GetMessagesHeighr()
        {
            //get file size
            FileInfo fileInfo = new FileInfo($"{app.DataPath}msg.dat");
            long fileSizeInBytes = fileInfo.Length;
            return fileSizeInBytes;
         
      

        }

        public async Task AddMsg(MessageModel msg)
        {
            if(File.Exists($"{app.DataPath}msg.dat")==false)
            {
               var f= File.Create($"{app.DataPath}msg.dat");
                f.Close();

            }
            app.Node.Messages.Push(msg);
            using (var stream = File.Open($"{app.DataPath}msg.dat", FileMode.Append))
            {
                //Append will add to the end of the file
                using (var writer = new BinaryWriter(stream, Encoding.UTF8))
                {
                    msg.Height = stream.Length;
                    //   writer.Seek(0, SeekOrigin.End);
                    writer.Write(msg.Sender);
                    writer.Write(msg.PublicKey);

                    writer.Write(msg.Txt);
                    writer.Write(msg.Height);
                    msg.MsgHash = MsgHashService.HashAlgoStd(msg);
                    writer.Write(msg.MsgHash);


                  //  MsgSign.Sign(msg);
                    writer.Write(msg.Signature);




                }
            }
      
        }
        public async Task PublishMsg(MessageModel msg)
        {
            //Call http service to send msg to peers
            app.Node.Peers=  inode.GetNodes();
            foreach (var peer in app.Node.Peers)
            {
                using (var http = new HttpClient())
                {
                    http.BaseAddress = new Uri(peer.NodeAddress);
                    var response = await http.PostAsJsonAsync("/api/Message/AddMsg", msg);
                }


                //Use http
             //   peer.Messages.Push(msg);
            }
        }
        public async Task<bool> ValidateMsg()
        {
            var msgs = GetMessages();
            foreach (var msg in msgs)
            {
                if (!MsgHashService.ValidateMsg(msg))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
