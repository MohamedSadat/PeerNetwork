using PeerLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerLib.Services
{
    public class ContractQueryService
    {
        private readonly BlovkChainAppModel app;

        public ContractQueryService(BlovkChainAppModel app)
        {
            this.app = app;
        }

        public ContractModel ReadContract(string contractname)
        {
            if (string.IsNullOrEmpty(contractname))
            {
                return new ContractModel { };
            }

            if (File.Exists($"{app.ContractsPath}\\{contractname}.dat") == false)
            {
                return new ContractModel {  };
            }

            var contract = new ContractModel();
            using (var stream = File.Open($"{app.ContractsPath}\\{contractname}.dat", FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8))
                {
                    contract.Name = reader.ReadString();
                    contract.Owner = reader.ReadString();
                    contract.Address = reader.ReadString();
                    contract.Balance = reader.ReadUInt32();
               
                }
            }
            return contract;
        }
    }
}
