using PeerLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PeerLib.Services
{
    public class MsgSign
    {
        public static string Sign(MessageModel msg, string _privateKey)
        {
            //1 = 6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b
            //
            RSACryptoServiceProvider rsa = new();
            _privateKey = rsa.ToXmlString(false);
            msg.PublicKey = rsa.ToXmlString(true);
            rsa.FromXmlString(_privateKey);
            byte[] dataToEncrypt = Encoding.ASCII.GetBytes(msg.MsgHash);
            byte[] encryptedByteArray = rsa.Encrypt(dataToEncrypt, false).ToArray();
            var r = Convert.ToBase64String(encryptedByteArray);
            msg.Signature = r;
            return r;

        }
        public static bool Validate(MessageModel msg)
        {
            try
            {
                RSACryptoServiceProvider rsa = new();
                rsa.FromXmlString(msg.PublicKey);

                byte[] dataByte = Convert.FromBase64String(msg.Signature);
                byte[] decryptedByte = rsa.Decrypt(dataByte, false);
                var r = Encoding.UTF8.GetString(decryptedByte);
                if (r == msg.MsgHash)
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
