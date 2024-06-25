using VismaInternshipErnestasJ.Models;
using VismaInternshipErnestasJ.Services;

namespace VismaInternshipErnestasJ.Middleware
{
    /// <summary>
    /// Represents the authentication middleware that checks if the user is authenticated before proceeding to the next middleware.
    /// </summary>
    public class AuthenticationMiddleware(Func<Task> next, IUserService userService)
    {
        public async Task Invoke(UserContext userContext)
        {
            if (userService.IsUserLoggedIn(userContext) == false)
            {
                Console.WriteLine("User is not authenticated. Please log in or register a new user.");

                Console.Write("Enter 'login' to log in or 'register' to register a new user: ");
                var input = Console.ReadLine();

                if (input == "login")
                {
                    Console.Write("Enter username: ");
                    var username = Console.ReadLine();

                    Console.Write("Enter password: ");
                    var password = Console.ReadLine();

                    var user = userService.Login(username, password);
                    if (user != null)
                    {
                        userContext.CurrentUser = user;
                    }
                    else
                    {
                        Console.WriteLine("Login failed.");
                    }
                }
                else if (input == "register")
                {
                    Console.Write("Enter username: ");
                    var username = Console.ReadLine();

                    Console.Write("Enter name: ");
                    var name = Console.ReadLine();

                    Console.Write("Enter password: ");
                    var password = Console.ReadLine();

                    var user = userService.RegisterUser(username, name, password);
                    if (user != null)
                    {
                        userContext.CurrentUser = user;
                    }
                    else
                    {
                        Console.WriteLine("Registration failed.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
            else
            {
                await next.Invoke();
            }

        }
    }
}
