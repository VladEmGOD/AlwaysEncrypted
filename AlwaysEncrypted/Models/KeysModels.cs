namespace AlwaysEncrypted.Models;

public class KeysModel
{
    public string MaskterKey { get; set; }

    public string MaskterKeyBase64 { get; set; }

    public string EncryptionKey { get; set; }

    public string EncryptionKeyBase64 { get; set; }

    public string EncryptedEncryptionKey { get; set; }

    public string EncryptedEncryptionKeyBase64 { get; set; }

}
