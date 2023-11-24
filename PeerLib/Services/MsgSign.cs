using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using PeerLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1.Sec;

namespace PeerLib.Services
{
    public class MsgSign
    {
        public string GeneratePrivateXML(WalletAppModel wallet)
        {
            //1 = 6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b
            //
            var pubkey = wallet.PublicKey;
            var _privateKey = wallet.PrivateKey;
            RSACryptoServiceProvider rsa = new();
            _privateKey = rsa.ToXmlString(false);
            pubkey = rsa.ToXmlString(true);
            wallet.PublicKeyXML = pubkey;

            return pubkey;

        }
        public string GeneratePublicKeyXML(WalletAppModel wallet)
        {
            //1 = 6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b
            //
            var pubkey = wallet.PublicKey;
            var _privateKey = wallet.PrivateKey;
            RSACryptoServiceProvider rsa = new();
            _privateKey = rsa.ToXmlString(false);
            pubkey = rsa.ToXmlString(true);
            wallet.PublicKeyXML = pubkey;
            wallet.PrivateKeyXml = _privateKey;



            
            return pubkey;

        }
        public  string Sign(TransactionModel trans)
        {
            //1 = 6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b
            //
            var pubkey = trans.Message.PublicKey;
            var _privateKey = trans.PrivateKey;
            RSACryptoServiceProvider rsa = new();
            _privateKey = rsa.ToXmlString(false);
         pubkey = rsa.ToXmlString(true);
            trans.PublicKeyXML=pubkey;
            rsa.FromXmlString(_privateKey);
            byte[] dataToEncrypt = Encoding.ASCII.GetBytes(trans.Message.MsgHash);
            byte[] encryptedByteArray = rsa.Encrypt(dataToEncrypt, false).ToArray();
            var r = Convert.ToBase64String(encryptedByteArray);
            trans.Message.Signature = r;
            return r;

        }
        public string SignEx(TransactionModel trans)
        {
            //1 = 6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b
            //
            var pubkey = trans.Message.PublicKey;
            var _privateKey = trans.PrivateKey;
            RSACryptoServiceProvider rsa = new();
        
            rsa.FromXmlString(trans.PrivateKeyXml);
            byte[] dataToEncrypt = Encoding.ASCII.GetBytes(trans.Message.MsgHash);
            byte[] encryptedByteArray = rsa.Encrypt(dataToEncrypt, false).ToArray();
            var r = Convert.ToBase64String(encryptedByteArray);
            trans.Message.Signature = r;
            return r;

        }
        public string SignTextMsg(TransactionModel trans)
        {
            //1 = 6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b
            //
            RSACryptoServiceProvider rsa = new();
            
            rsa.FromXmlString(trans.PrivateKeyXml);
            byte[] dataToEncrypt = Encoding.ASCII.GetBytes(trans.Message.Txt);
            byte[] encryptedByteArray = rsa.Encrypt(dataToEncrypt, false).ToArray();
            var r = Convert.ToBase64String(encryptedByteArray);
          
            return r;

        }
        public string DescryptTextMsg(TransactionModel trans)
        {
            try
            {
                RSACryptoServiceProvider rsa = new();

                rsa.FromXmlString(trans.PublicKeyXML);

                byte[] dataByte = Convert.FromBase64String(trans.Message.Txt);
                byte[] decryptedByte = rsa.Decrypt(dataByte, false);
                var r = Encoding.UTF8.GetString(decryptedByte);
                return r;
            }
            catch (Exception ex)
            {
                return "";
            }

        }
        public  bool Validate(TransactionModel trans)
        {
            try
            {
                RSACryptoServiceProvider rsa = new();

                rsa.FromXmlString(trans.PublicKeyXML);

                byte[] dataByte = Convert.FromBase64String(trans.Message.Signature);
                byte[] decryptedByte = rsa.Decrypt(dataByte, false);
                var r = Encoding.UTF8.GetString(decryptedByte);
                if (r == trans.Message.MsgHash)
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
        public bool ValidateEx(TransactionModel trans)
        {
            try
            {
                RSACryptoServiceProvider rsa = new();

                rsa.FromXmlString(trans.PublicKeyXML);

                byte[] dataByte = Convert.FromBase64String(trans.Message.Signature);
                byte[] decryptedByte = rsa.Decrypt(dataByte, false);
                var r = Encoding.UTF8.GetString(decryptedByte);
                if (r == trans.Message.MsgHash)
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
