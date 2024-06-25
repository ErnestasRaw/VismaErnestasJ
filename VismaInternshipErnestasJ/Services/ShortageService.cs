using VismaInternshipErnestasJ.Data;
using VismaInternshipErnestasJ.Models;

// TODO: Update users on registration
namespace VismaInternshipErnestasJ.Services
{
    public class ShortageService : IShortageService
    {
        private readonly IDataStorage _dataStorage;
        private readonly UserContext _userContext;
        private readonly List<Shortage> _shortages;
        private User RequestingUser => _userContext.CurrentUser;

        public ShortageService(IDataStorage dataStorage, UserContext userContext)
        {
            _dataStorage = dataStorage;
            _userContext = userContext;
            _shortages = _dataStorage.LoadShortages();
        }

        public void RegisterShortage(Shortage shortage)
        {
            var existingShortage = _shortages.FirstOrDefault(s => s.Title == shortage.Title && s.Room == shortage.Room);
            if (existingShortage != null)
            {
                if (shortage.Priority > existingShortage.Priority)
                {
                    existingShortage.UpdatePriority(shortage.Priority);
                    _dataStorage.SaveShortages(_shortages);
                    Console.WriteLine("Existing shortage updated with higher priority.");
                }
                else
                {
                    Console.WriteLine("Shortage already exists with the same or higher priority.");
                }
            }
            else
            {
                _shortages.Add(shortage);
                _dataStorage.SaveShortages(_shortages);
                Console.WriteLine("Shortage registered successfully.");
            }
        }

        public void DeleteShortage(string title, Room room)
        {
            var shortage = _shortages.FirstOrDefault(s => s.Title == title && s.Room == room);
            if (shortage == null)
            {
                Console.WriteLine("Shortage not found.");
                return;
            }

            if (RequestingUser.UserType == UserType.Administrator || shortage.CreatedBy.Any(u => u.Username == RequestingUser.Username))
            {
                _shortages.Remove(shortage);
                _dataStorage.SaveShortages(_shortages);
                Console.WriteLine("Shortage deleted successfully.");
            }
            else
            {
                Console.WriteLine("You do not have permission to delete this shortage.");
            }
        }

        public IEnumerable<Shortage> ListShortages()
        {
            return RequestingUser.UserType == UserType.Administrator
                ? _shortages.OrderByDescending(s => s.Priority)
                : _shortages.Where(s => s.CreatedBy.Any(u => u.Username == RequestingUser.Username)).OrderByDescending(s => s.Priority);
        }

        public IEnumerable<Shortage> FilterByTitle(string title)
        {
            var shortages = ListShortages();
            return shortages.Where(s => s.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).OrderByDescending(s => s.Priority);
        }

        public IEnumerable<Shortage> FilterByCreatedOnDate(DateTime startDate, DateTime endDate)
        {
            var shortages = ListShortages();
            return shortages.Where(s => s.CreatedOn >= startDate && s.CreatedOn <= endDate).OrderByDescending(s => s.Priority);
        }

        public IEnumerable<Shortage> FilterByCategory(Category category)
        {
            var shortages = ListShortages();
            return shortages.Where(s => s.Category == category).OrderByDescending(s => s.Priority);
        }

        public IEnumerable<Shortage> FilterByRoom(Room room)
        {
            var shortages = ListShortages();
            return shortages.Where(s => s.Room == room).OrderByDescending(s => s.Priority);
        }



    }
}
