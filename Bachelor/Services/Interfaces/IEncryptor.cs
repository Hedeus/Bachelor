namespace Bachelor.Services.Interfaces
{
    internal interface IEncryptor
    {
        void Encrypt(string SourcePath, string Destination, string Password, int BufferLength = 104200);

        bool Decrypt(string SourcePath, string Destination, string Passphrase, int BufferLength = 104200);
    }
}
