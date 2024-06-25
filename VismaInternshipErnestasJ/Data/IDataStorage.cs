using VismaInternshipErnestasJ.Models;

namespace VismaInternshipErnestasJ.Data
{
    /// <summary>
    /// Represents the interface for data storage operations.
    /// </summary>
    public interface IDataStorage
    {
        /// <summary>
        /// Loads the list of shortages from the data storage.
        /// </summary>
        /// <returns>The list of shortages.</returns>
        List<Shortage> LoadShortages();

        /// <summary>
        /// Saves the list of shortages to the data storage.
        /// </summary>
        /// <param name="shortages">The list of shortages to save.</param>
        void SaveShortages(List<Shortage> shortages);

        /// <summary>
        /// Loads the list of users from the data storage.
        /// </summary>
        /// <returns>The list of users.</returns>
        List<User> LoadUsers();

        /// <summary>
        /// Saves the list of users to the data storage.
        /// </summary>
        /// <param name="users">The list of users to save.</param>
        void SaveUsers(List<User> users);
    }
}
