using PeerLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PeerLib.Services
{
    public class MsgHashService
    {
        public static bool ValidateMsg(MessageModel msg)
        {
            return msg.MsgHash == HashAlgoStd(msg);
        }
        public static string HashAlgoStd(MessageModel mag)
        {
            return HashAlgoStd(mag.Txt + mag.PublicKey + mag.Height.ToString());
        }
        

        public static string HashAlgoStd(string msg, int outputSize = 32)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(msg));
                byte[] truncatedBytes = new byte[outputSize];
                Array.Copy(bytes, truncatedBytes, outputSize);
                StringBuilder sb = new StringBuilder();

                foreach (byte b in truncatedBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
