namespace ChatOn.Models
{
    public class Role : IRoleApiData
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool IsDeletable { get; set; } = true;
    
        public static readonly Role UnknownRole = new Role { Id = -1, Name = "Unknown" };
    }

    public interface IRoleApiData
    {
        int Id { get; set; }

        string Name { get; set; }
    }
}
