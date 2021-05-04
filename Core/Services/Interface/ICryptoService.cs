namespace MyForum.Core.Services.Interface
{
    public interface ICryptoService
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}