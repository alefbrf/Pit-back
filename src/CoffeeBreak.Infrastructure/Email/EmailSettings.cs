namespace CoffeeBreak.Infrastructure.Email
{
    public class EmailSettings
    {
        public const string Section = "EmailSettings";

        public string Server { get; init; } = null!;
        public int Port { get; init; }
        public string User { get; init; } = null!;
        public string Password { get; init; } = null!;
    }
}
