using PeerLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PeerLib.Services
{
    public class NodeServices
    {
        private readonly AppModel app;

        public NodeServices(AppModel app)
        {
            this.app = app;
        }
        public List<NodeModel> GetNodes()
        {
            try
            {
                var messages = new List<NodeModel>();
                using (var stream = File.Open($"{app.DataPath}nodes.dat", FileMode.Open))
                {
                    //      Console.WriteLine("Size is {0}", stream.Length);
                    using (var reader = new BinaryReader(stream, Encoding.UTF8))
                    {
                        while (reader.PeekChar() > -1)
                        {
                            var msg = new NodeModel();
                            msg.NodeName = reader.ReadString();
                            msg.NodeAddress = reader.ReadString();
                            messages.Add(msg);
                        }
                    }
                }
                return messages;
            }
            catch { 
            return new List<NodeModel>();
            }
        }
        public async Task AddNode(NodeModel node)
        {
            CheckNodeFile();
            app.Node.Peers.Add(node);
            using (var stream = File.Open($"{app.DataPath}nodes.dat", FileMode.Append))
            {
                //Append will add to the end of the file
                using (var writer = new BinaryWriter(stream, Encoding.UTF8))
                {
                    //   writer.Seek(0, SeekOrigin.End);
                    writer.Write(node.NodeName);
                    writer.Write(node.NodeAddress);

                }
            }

        }
        public async Task PublishNode(NodeModel node)
        {
            //Call http service to send msg to peers
            foreach (var peer in app.Node.Peers)
            {
                using (var http = new HttpClient())
                {
                    http.BaseAddress = new Uri(peer.NodeAddress);
                    var response = await http.PostAsJsonAsync("/api/Node/PublishNode", node);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Node published");
                    }
                }


                //Use http
                //   peer.Messages.Push(msg);
            }
        }
        public void CheckNodeFile()
        {
            if (File.Exists($"{app.DataPath}msg.dat") == false)
            {
                var f = File.Create($"{app.DataPath}msg.dat");
                f.Close();

            }
        }
    
    }
}
