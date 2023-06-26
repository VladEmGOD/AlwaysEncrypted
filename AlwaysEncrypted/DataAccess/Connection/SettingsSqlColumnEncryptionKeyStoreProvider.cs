using Microsoft.Data.SqlClient;
using System.Security.Cryptography;

namespace AlwaysEncrypted.DataAccess.Connection
{
    public class SettingsSqlColumnEncryptionKeyStoreProvider : SqlColumnEncryptionKeyStoreProvider
    {
        private readonly IConfiguration configuration;
        public SettingsSqlColumnEncryptionKeyStoreProvider(IConfiguration configuration) => this.configuration = configuration;

        public override byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey)
        {
            var a = Convert.ToBase64String(encryptedColumnEncryptionKey);

            var masterKey = configuration.GetValue<string>("MasterKey");
            using Aes aesAlgorithm = Aes.Create();

            aesAlgorithm.KeySize = 256;
            aesAlgorithm.Key = Convert.FromBase64String(masterKey);

            var b = aesAlgorithm.DecryptEcb(encryptedColumnEncryptionKey, PaddingMode.None);
            var b1 = Convert.ToBase64String(b);

            return b;
        }

        public override byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey)
        {
            var a = Convert.ToBase64String(columnEncryptionKey);

            var masterKey = configuration.GetValue<string>("MasterKey");
            using Aes aesAlgorithm = Aes.Create();

            aesAlgorithm.KeySize = 256;
            aesAlgorithm.Key = Convert.FromBase64String(masterKey);

            var b = aesAlgorithm.EncryptEcb(columnEncryptionKey, PaddingMode.None);
            var b1 = Convert.ToBase64String(b);
            return b;
        }
    }
}
