namespace PeerLib.Data
{
    public class NodeModel
    {
        public string NodeId { get; set; } = "";
        public string NodeAddress { get; set; } = "";
        public string NodePort { get; set; } = "";
        public string NodeName { get; set; } = "";
        public Stack<MessageModel> Messages { get; set; } = new Stack<MessageModel>();
        public List<NodeModel> Peers { get; set; } = new List<NodeModel>();
    }
}