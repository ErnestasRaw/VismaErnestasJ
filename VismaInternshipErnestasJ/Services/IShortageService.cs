using VismaInternshipErnestasJ.Models;

namespace VismaInternshipErnestasJ.Services
{
    public interface IShortageService
    {
        /// <summary>
        /// Registers a new shortage.
        /// </summary>
        /// <param name="shortage">The shortage to be registered.</param>
        void RegisterShortage(Shortage shortage);

        /// <summary>
        /// Deletes an existing shortage.
        /// </summary>
        /// <param name="title">The title of the shortage to be deleted.</param>
        /// <param name="room">The room of the shortage to be deleted.</param>
        /// <param name="requestingUser">The user requesting the deletion.</param>
        void DeleteShortage(string title, Room room);

        /// <summary>
        /// Lists all shortages.
        /// </summary>
        /// <param name="requestingUser">The user requesting the list.</param>
        /// <returns>A list of shortages.</returns>
        IEnumerable<Shortage> ListShortages();

        /// <summary>
        /// Filters shortages by title.
        /// </summary>
        /// <param name="title">The title to filter by.</param>
        /// <param name="requestingUser">The user requesting the filter.</param>
        /// <returns>A list of shortages that match the title.</returns>
        IEnumerable<Shortage> FilterByTitle(string title);

        /// <summary>
        /// Filters shortages by the creation date range.
        /// </summary>
        /// <param name="startDate">The start date of the range.</param>
        /// <param name="endDate">The end date of the range.</param>
        /// <param name="requestingUser">The user requesting the filter.</param>
        /// <returns>A list of shortages that were created within the date range.</returns>
        IEnumerable<Shortage> FilterByCreatedOnDate(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Filters shortages by category.
        /// </summary>
        /// <param name="category">The category to filter by.</param>
        /// <param name="requestingUser">The user requesting the filter.</param>
        /// <returns>A list of shortages that match the category.</returns>
        IEnumerable<Shortage> FilterByCategory(Category category);

        /// <summary>
        /// Filters shortages by room.
        /// </summary>
        /// <param name="room">The room to filter by.</param>
        /// <param name="requestingUser">The user requesting the filter.</param>
        /// <returns>A list of shortages that match the room.</returns>
        IEnumerable<Shortage> FilterByRoom(Room room);
    }
}
