using VismaInternshipErnestasJ.Services;

namespace VismaInternshipErnestasJ.Commands
{
    public class ListShortagesCommand(IShortageService shortageService) : ICommand
    {
        public void Execute()
        {
            var shortages = shortageService.ListShortages();

            foreach (var shortage in shortages)
            {
                Console.WriteLine($"Title: {shortage.Title}, Room: {shortage.Room}, Category: {shortage.Category}, Priority: {shortage.Priority}, Created On: {shortage.CreatedOn}");
            }
        }
    }
}
