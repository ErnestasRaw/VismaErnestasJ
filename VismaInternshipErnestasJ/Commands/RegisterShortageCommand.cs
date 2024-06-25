using VismaInternshipErnestasJ.Models;
using VismaInternshipErnestasJ.Services;

namespace VismaInternshipErnestasJ.Commands
{
    public class RegisterShortageCommand(IShortageService shortageService, UserContext userContext) : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Enter shortage title:");
            var title = Console.ReadLine();
            if (string.IsNullOrEmpty(title))
            {
                Console.WriteLine("Invalid title.");
                return;
            }

            Console.WriteLine("Enter name for the shortage:");
            var name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Invalid name.");
                return;
            }

            Console.WriteLine("Enter room (MeetingRoom/Kitchen/Bathroom):");
            if (!Enum.TryParse(Console.ReadLine(), out Room room))
            {
                Console.WriteLine("Invalid room.");
                return;
            }

            Console.WriteLine("Enter category (Electronics/Food/Other):");
            if (!Enum.TryParse(Console.ReadLine(), out Category category))
            {
                Console.WriteLine("Invalid category.");
                return;
            }

            Console.WriteLine("Enter priority (1-10):");
            if (!int.TryParse(Console.ReadLine(), out int priority) || priority < 1 || priority > 10)
            {
                Console.WriteLine("Invalid priority.");
                return;
            }

            var shortage = new Shortage(title, name, room, category, priority, [userContext.CurrentUser]);
            shortageService.RegisterShortage(shortage);
        }
    }
}
