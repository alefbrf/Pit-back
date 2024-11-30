namespace CoffeeBreak.Application.DTOs.Request.Email
{
    public record Email
    {
        public string RecipientEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
