namespace VismaInternshipErnestasJ.Models
{
    public class UserContext
    {
        public bool ExitRequested { get; set; } = false;
        public required User CurrentUser { get; set; }
    }
}