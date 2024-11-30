namespace CoffeeBreak.Application.Common.Interfaces
{
    public interface ICryptographyService
    {
        string Encrypt(string text);
        string Decrypt(string text);
    }
}
