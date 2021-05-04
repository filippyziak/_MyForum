using System.Data;
using System.Security.Cryptography;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using MyForum.Core.Services.Interface;

namespace MyForum.Core.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly IDataProtectionProvider dataProtectionProvider;
        public IConfiguration Configuration { get; }
        public string ProtectorToken { get; }
        public CryptoService(IDataProtectionProvider dataProtectionProvider, IConfiguration configuration)
        {
            this.dataProtectionProvider = dataProtectionProvider;
            Configuration = configuration;
            ProtectorToken = Configuration.GetValue<string>("Constants:Token");
        }


        public string Encrypt(string plainText)
        {
            var dataProtector = dataProtectionProvider.CreateProtector(ProtectorToken);

            return dataProtector.Protect(plainText);
        }
        public string Decrypt(string cipherText)
        {
            try
            {
                var dataProtector = dataProtectionProvider.CreateProtector(ProtectorToken);

                return dataProtector.Unprotect(cipherText);
            }
            catch (CryptographicException)
            {
                throw new DataException();
            }
        }
    }
}