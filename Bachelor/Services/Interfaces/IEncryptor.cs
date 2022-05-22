using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bachelor.Services.Interfaces
{
    internal interface IEncryptor
    {
        void Encrypt(string SourcePath, string Destination, string Password, int BufferLength = 104200);

        bool Decrypt(string SourcePath, string Destination, string Passphrase, int BufferLength = 104200);

        Task EncryptAsync(string SourcePath, string Destination, string Password, int BufferLength = 104200, IProgress<double> Progress = null, CancellationToken Cancel = default);

        Task<bool> DecryptAsync(string SourcePath, string Destination, string Passphrase, int BufferLength = 104200, IProgress<double> Progress = null, CancellationToken Cancel = default);

    }
}
