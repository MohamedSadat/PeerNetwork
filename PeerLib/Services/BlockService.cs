using PeerLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerLib.Services
{
    public class BlockService
    {
        private readonly BlovkChainAppModel app;
        private readonly BlockIndexService blockIndex;
        private readonly MsgHashService msgHash;

        public BlockService(BlovkChainAppModel app,BlockIndexService blockIndex,MsgHashService msgHash)
        {
            this.app = app;
            this.blockIndex = blockIndex;
            this.msgHash = msgHash;
        }
        public void AddBlock()
        {

        }
        public bool ValidateBlock()
        {
            return true;
        }
        public void WriteBlock(BlockModel block)
        {
          var lastblock=ReadPrevBlock();
            block.PrevBlockHash    = lastblock.BlockHash;
                if (File.Exists($"{app.DataPath}blockchain.dat") == false)
                {
                    var f = File.Create($"{app.DataPath}blockchain.dat");
                    f.Close();

                }
                //app.Node.Messages.Push(msg);
                using (var stream = File.Open($"{app.DataPath}blockchain.dat", FileMode.Append))
                {
                    //Append will add to the end of the file
                    using (var writer = new BinaryWriter(stream, Encoding.UTF8))
                    {
                    block.BlockHash = msgHash.HashBlock(block);

                    block.Height = stream.Length;
                        writer.Write(block.Height);
                       writer.Write(app.Node.PublicKey);
                        writer.Write(block.PrevBlockHash);
                        writer.Write(block.BlockHash);
                        writer.Write(block.TimeStamp);
                    

                    }
                }
            blockIndex.WriteIndex(block);

            
        }
        public BlockModel ReadBlock(long height)
        {
            var block=new BlockModel();
            return block;
        }
        public BlockModel ReadPrevBlock()
        {
            return blockIndex.ReadLastBlock();
        }
    }
}
