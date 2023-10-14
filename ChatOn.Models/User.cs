namespace ChatOn.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public ICollection<Message> SentMessages { get; set; } = new List<Message>();


        public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
    }
}
