using VismaInternshipErnestasJ.Models;

namespace VismaInternshipErnestasJ.Commands
{
    public class ExitCommand(UserContext userContext) : ICommand
    {
        public void Execute()
        {
            userContext.ExitRequested = true;
        }
    }
}
