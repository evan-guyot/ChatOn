namespace ChatOn.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; } = String.Empty;
        public User? Sender { get; set; }
        public User? Receiver { get; set; }
    }
}
