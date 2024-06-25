using VismaInternshipErnestasJ.Models;
using VismaInternshipErnestasJ.Services;

namespace VismaInternshipErnestasJ.Commands
{
    public class DeleteShortageCommand(IShortageService shortageService) : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Enter shortage title to delete:");
            var title = Console.ReadLine();

            if (string.IsNullOrEmpty(title))
            {
                Console.WriteLine("Invalid title.");
                return;
            }

            Console.WriteLine("Enter room (MeetingRoom/Kitchen/Bathroom):");
            if (!Enum.TryParse(Console.ReadLine(), out Room room))
            {
                Console.WriteLine("Invalid room.");
                return;
            }

            shortageService.DeleteShortage(title, room);
        }
    }
}
