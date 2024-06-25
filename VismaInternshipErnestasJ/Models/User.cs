
namespace VismaInternshipErnestasJ.Models
{
    public class User(string username, string name, string password)
    {
        public string Username { get; private set; } = username;
        public string Name { get; private set; } = name;
        public UserType UserType { get; private set; } = UserType.RegularUser;

        public string Password { get; private set; } = password;
    }
}
