namespace VismaInternshipErnestasJ.Services
{
    using global::VismaInternshipErnestasJ.Data;
    using global::VismaInternshipErnestasJ.Models;
    using System;
    using System.Linq;

    namespace VismaInternshipErnestasJ.Services
    {
        public class UserService : IUserService
        {
            private readonly IDataStorage _dataStorage;
            private readonly List<User> _users;

            public UserService(IDataStorage dataStorage)
            {
                _dataStorage = dataStorage;
                _users = _dataStorage.LoadUsers();
            }

            public bool IsUserLoggedIn(UserContext userContext)
            {
                return userContext.CurrentUser != null;
            }

            public User? Login(string username, string password)
            {
                var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);

                if (user != null)
                {
                    Console.WriteLine("Login successful.");
                    return user;
                }

                Console.WriteLine("Invalid username or password.");
                return null;
            }

            public User? RegisterUser(string username, string name, string password)
            {

                var user = new User(username, name, password);

                var existingUser = _users.FirstOrDefault(u => u.Username == user.Username);
                if (existingUser != null)
                {
                    Console.WriteLine("User already exists.");
                    return null;
                }
                else
                {
                    _users.Add(user);
                    _dataStorage.SaveUsers(_users);
                    Console.WriteLine("User registered successfully.");
                    return user;
                }
            }
        }
    }

}
