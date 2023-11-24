using PeerLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerLib.Services
{
    public class BlockChainService
    {
        private readonly BlovkChainAppModel app;

        public BlockChainService(BlovkChainAppModel app)
        {
            this.app = app;
        }
        public void Initialize()
        {
            if (Path.Exists($"{app.DataPath}") == false)
            {
                var f = Directory.CreateDirectory($"{app.DataPath}");
               
            }
            if (Path.Exists(("contracts")) == false)
            {
                var f = Directory.CreateDirectory($"contracts");

            }
            if (File.Exists($"{app.DataPath}nodes.dat") == false)
            {
                var f = File.Create($"{app.DataPath}nodes.dat");
                f.Close();

            }
            if (File.Exists($"{app.DataPath}blockchain.dat") == false)
            {
                var f = File.Create($"{app.DataPath}blockchain.dat");
                f.Close();

            }
            if (File.Exists($"{app.DataPath}indexes.dat") == false)
            {
                var f = File.Create($"{app.DataPath}indexes.dat");
                f.Close();

            }
            if (File.Exists($"{app.DataPath}pageindex.dat") == false)
            {
                var f = File.Create($"{app.DataPath}pageindex.dat");
                f.Close();

            }

            if (File.Exists($"{app.DataPath}msg.dat") == false)
            {
                var f = File.Create($"{app.DataPath}msg.dat");
                f.Close();

            }
       
        }
    }
}
