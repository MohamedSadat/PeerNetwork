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
        private readonly MsgIndexService msgIndex;
        private readonly MsgHashService msgHash;
        private readonly MsgSign sign;

        public MsgService(AppModel app,NodeServices inode, MsgIndexService msgIndex,MsgHashService msgHash,MsgSign sign)
        {
            this.app = app;
            this.inode = inode;
            this.msgIndex = msgIndex;
            this.msgHash = msgHash;
            this.sign = sign;
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
                        msg.Amount=reader.ReadUInt32();
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
        public long GetMessagesHeight()
        {
            //get file size
            FileInfo fileInfo = new FileInfo($"{app.DataPath}msg.dat");
            long fileSizeInBytes = fileInfo.Length;
            return fileSizeInBytes;
         
        }
        public async Task AddMessages(List<MessageModel> lst)
        {
            foreach(var msg in lst)
            {
                await AddMsg(msg);
            }
        }
        
        public async Task<ResultModel<MessageModel>> AddMsg(MessageModel msg)
        {
            var r=new ResultModel<MessageModel>(msg);
            
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
                    writer.Write(app.Node.PublicKey);
                    writer.Write(msg.Sender);
                    writer.Write(msg.Receiver);
                    writer.Write(msg.PublicKey);
                    writer.Write(msg.Txt);
                    writer.Write(msg.Amount);
                    writer.Write(msg.Fee);
                    writer.Write(msg.Height);
                    msg.MsgHash = msgHash.HashAlgoStd(msg);
                    writer.Write(msg.MsgHash);
                  //  MsgSign.Sign(msg);
                    writer.Write(msg.Signature);
                    writer.Write(msg.TimeStamp);

                }
            }
            msgIndex.WriteIndex(msg);
            return r;
      
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
                    if (response.IsSuccessStatusCode)
                    {
                        
                        app.Logs.Add($"{peer.NodeAddress} ,Message published");
                    }
                    else
                    {
                        app.Logs.Add($"{peer.NodeAddress} ,Message not published");
                    }
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
                if (!msgHash.ValidateMsg(msg))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
