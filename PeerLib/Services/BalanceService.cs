using PeerLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerLib.Services
{
    public class BalanceService
    {
        private readonly BlovkChainAppModel app;
        private readonly MsgQueryService query;

        public BalanceService(BlovkChainAppModel app, MsgQueryService query)
        {
            this.app = app;
            this.query = query;
        }
        public long GetBalance(string account)
        {
         var sent=   query.GetMessagesByAddress(account).Where(s=>s.Sender==account).Sum(x => x.Amount);
            var received = query.GetMessagesByAddress(account).Where(s => s.Receiver == account).Sum(x => x.Amount);
            var balance = received - sent;
            return balance;
        }
    }
}
