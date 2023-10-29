namespace ChatOn.Models
{
    public class Message : IMessageApiData
    {
        public int Id { get; set; }

        public string Content { get; set; } = string.Empty;

        public User? Sender { get; set; }

        public User? Receiver { get; set; }
    }

    public interface IMessageApiData
    {
        public int Id { get; set; }

        public string Content { get; set; }
    }
}
