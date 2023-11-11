using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerLib.Data
{
    public class ResultModel<T>
    {
        public T Body { get; set; }
        public string Error { get; set; }
        public bool IsError { get; set; }
        public ResultModel(T body)
        {
            Body = body;
        }
    }
}
