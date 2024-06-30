using System.Security.Cryptography;

namespace AdSpot.Api.Services;

public class KeyManager
{
    private byte[]? privateKey = null;

    public RSA RsaKey { get; }

    public KeyManager()
    {
        RsaKey = RSA.Create();
        if (privateKey is null)
        {
            privateKey = RsaKey.ExportRSAPrivateKey();
        }
        else
        {
            RsaKey.ImportRSAPrivateKey(privateKey, out _);
        }
    }
}
