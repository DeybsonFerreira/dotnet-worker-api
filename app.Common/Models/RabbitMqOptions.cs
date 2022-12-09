namespace app.Common.Models
{
    public class RabbitMqOptions 
    {
        public const string RabbitMQ = "RabbitMQ";
        public string HostName { get; set; } = String.Empty;
        public string UserName { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string Exchange { get; set; } = String.Empty;
    }
}