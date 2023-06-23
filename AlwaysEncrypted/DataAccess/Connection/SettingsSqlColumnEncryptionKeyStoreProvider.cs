using Microsoft.Data.SqlClient;

namespace AlwaysEncrypted.DataAccess.Connection
{
    public class SettingsSqlColumnEncryptionKeyStoreProvider : SqlColumnEncryptionKeyStoreProvider
    {
        public override byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey)
        {
            return encryptedColumnEncryptionKey;
        }

        public override byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey)
        {
            return columnEncryptionKey;
        }
    }
}
