namespace ChatOn.Models
{
    public class User : IUserApiData
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public Role Role { get; set; } = Role.UnknownRole;
        
        public ICollection<Message> SentMessages { get; set; } = new List<Message>();

        public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
    }

    public interface IUserApiData
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
