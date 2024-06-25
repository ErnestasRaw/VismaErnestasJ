using VismaInternshipErnestasJ.Models;

namespace VismaInternshipErnestasJ.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Checks if the user is logged in.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        /// <returns><c>true</c> if the user is logged in, otherwise <c>false</c>.</returns>
        bool IsUserLoggedIn(UserContext userContext);

        /// <summary>
        /// Logs in the user.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="username">The username.</param>
        /// <returns>The logged in user, or <c>null</c> if login fails.</returns>
        User? Login(string username, string password);

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="user">The user to be registered.</param>
        /// <returns>The registered user, or <c>null</c> if registration fails.</returns>
        User? RegisterUser(string username, string name, string password);
    }
}
