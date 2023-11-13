using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PeerLib.Services
{
    public class KeyGeneratorService
    {
        public  string GeneratePrivateKey()
        {
            // Generate a new private key
            byte[] privateKeyBytes = new byte[32];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(privateKeyBytes);
            }
            string privateKeyHex = BitConverter.ToString(privateKeyBytes).Replace("-", "");
            return privateKeyHex;

        }
    }
}
