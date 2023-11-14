using PeerLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace PeerLib.Services
{
    public class BlockIndexService
    {
        private readonly BlovkChainAppModel app;
    

        public BlockIndexService(BlovkChainAppModel app)
        {
            this.app = app;
         
        }

        public void WriteIndex(BlockModel block)
        {
            if (File.Exists($"{app.DataPath}blockindex.dat") == false)
            {
                var f = File.Create($"{app.DataPath}blockindex.dat");
                f.Close();

            }

            using (var stream = File.Open($"{app.DataPath}blockindex.dat", FileMode.Append))
            {
                //Append will add to the end of the file
                using (var writer = new BinaryWriter(stream, Encoding.UTF8))
                {
                    writer.Write(block.Height);
                    writer.Write(block.BlockHash);
                }
            }
        }
        public BlockModel ReadLastBlock()
        {
            var block = new BlockModel();
            var blockchainsize=GetBlockInexesHeight();
            if(blockchainsize == 0)
            {
                return block;
            }

            using (var stream = File.Open($"{app.DataPath}blockindex.dat", FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8))
                {
                    stream.Position=blockchainsize-73;
                        block.Height = reader.ReadInt64();
                    block.BlockHash = reader.ReadString();
                       
                }
            }
            return block;
        }
        public List<BlockModel> GetBlockInexes()
        {
            var blocks = new List<BlockModel>();
            using (var stream = File.Open($"{app.DataPath}blockindex.dat", FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8))
                {
                    while (reader.PeekChar() > -1)
                    {
                        var block = new BlockModel();
                        block.Height = reader.ReadInt64();
                    
                        blocks.Add(block);
                    }
                }
            }
            return blocks;

        }
        public long GetBlockInexesHeight()
        {
            //get file size
            FileInfo fileInfo = new FileInfo($"{app.DataPath}blockindex.dat");
            long fileSizeInBytes = fileInfo.Length;
            return fileSizeInBytes;

        }

    }
}
