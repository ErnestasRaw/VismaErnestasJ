using VismaInternshipErnestasJ.Models;
using VismaInternshipErnestasJ.Services;

namespace VismaInternshipErnestasJ.Commands
{
    public class FilterShortagesCommand(IShortageService shortageService) : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Filter by:\n1. Title\n2. CreatedOn Date\n3. Category\n4. Room");
            var filterOption = Console.ReadLine();

            switch (filterOption)
            {
                case "1":
                    Console.WriteLine("Enter title to filter by:");
                    var title = Console.ReadLine();

                    if (string.IsNullOrEmpty(title))
                    {
                        Console.WriteLine("Invalid title.");
                        return;
                    }

                    var titleResults = shortageService.FilterByTitle(title);
                    DisplayShortages(titleResults);
                    break;
                case "2":
                    Console.WriteLine("Enter start date (yyyy-MM-dd):");
                    var startDate = DateTime.Parse(Console.ReadLine() ?? "");

                    Console.WriteLine("Enter end date (yyyy-MM-dd):");
                    var endDate = DateTime.Parse(Console.ReadLine() ?? "");

                    var dateResults = shortageService.FilterByCreatedOnDate(startDate, endDate);
                    DisplayShortages(dateResults);
                    break;
                case "3":
                    Console.WriteLine("Enter category:");
                    var categoryOptions = string.Join("/", Enum.GetNames(typeof(Category)));
                    Console.WriteLine($"Options: {categoryOptions}");
                    if (Enum.TryParse(Console.ReadLine(), out Category category))
                    {
                        var categoryResults = shortageService.FilterByCategory(category);
                        DisplayShortages(categoryResults);
                    }
                    else
                    {
                        Console.WriteLine("Invalid category.");
                    }
                    break;
                case "4":
                    Console.WriteLine("Enter room:");
                    var roomOptions = string.Join("/", Enum.GetNames(typeof(Room)));
                    Console.WriteLine($"Options: {roomOptions}");
                    if (Enum.TryParse(Console.ReadLine(), out Room room))
                    {
                        var roomResults = shortageService.FilterByRoom(room);
                        DisplayShortages(roomResults);
                    }
                    else
                    {
                        Console.WriteLine("Invalid room.");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid filter option.");
                    break;
            }
        }

        private static void DisplayShortages(IEnumerable<Shortage> shortages)
        {
            foreach (var shortage in shortages)
            {
                Console.WriteLine($"Title: {shortage.Title}, Room: {shortage.Room}, Category: {shortage.Category}, Priority: {shortage.Priority}, Created On: {shortage.CreatedOn}");
            }
        }
    }
}
