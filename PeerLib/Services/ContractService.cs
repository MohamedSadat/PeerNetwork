using PeerLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerLib.Services
{
    public class ContractService
    {
        private readonly BlovkChainAppModel app;
        private readonly MsgService msgService;

        public ContractService(BlovkChainAppModel app, MsgService msgService)
        {
            this.app = app;
            this.msgService = msgService;
        }
        public void CreateContract(ContractModel contract)
        {
            if (Directory.Exists(app.ContractsPath) == false)
            {
                Directory.CreateDirectory(app.ContractsPath);
            }
            using (var stream = File.Create($"{app.ContractsPath}\\{contract.Name}.dat"))
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8))
                {
                    writer.Write(contract.Name);
                    writer.Write(contract.Owner);
                    writer.Write(contract.Address);
                    writer.Write(contract.Balance);
                }

            }
        }
        public async Task Deposit(ContractModel contract, TransactionModel transaction)
        {
            if (contract == null)
            {
                return;
            }
            contract.Balance += transaction.Message.Amount;
            using (var stream = File.Open($"{app.ContractsPath}\\{contract.Name}.dat", FileMode.Open))
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8))
                {
                    writer.Write(contract.Name);
                    writer.Write(contract.Address);
                    writer.Write(contract.Owner);
                    writer.Write(contract.Balance);
                }
            }
            await msgService.AddMsg(transaction.Message);

        }
        public async Task Withdraw(ContractModel contract, TransactionModel transaction)
        {
            if (transaction.Message.PublicKey == contract.Owner)
            {
                if (contract.Balance >= transaction.Message.Amount)
                {
                    contract.Balance -= transaction.Message.Amount;
                }
            }

            await msgService.AddMsg(transaction.Message);
            //  var contract = ReadContract(name);
            //   contract.Balance -= amount;
            //  CreateContract(contract);
          //  OnWithdraw();
        }

    }
}
