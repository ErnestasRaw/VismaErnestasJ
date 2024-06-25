namespace VismaInternshipErnestasJ.Models

{
    public class Shortage
    {
        public string Title { get; private set; }
        public string Name { get; private set; }
        public Room Room { get; private set; }
        public Category Category { get; private set; }
        public int Priority { get; private set; }
        public DateTime CreatedOn { get; set; }
        public List<User> CreatedBy { get; private set; }

        public Shortage(string title, string name, Room room, Category category, int priority, List<User> createdBy)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Room = room;
            Category = category;
            SetPriority(priority);
            CreatedOn = DateTime.Now;
            CreatedBy = createdBy ?? throw new ArgumentNullException(nameof(createdBy));
        }

        public void UpdatePriority(int newPriority)
        {
            SetPriority(newPriority);
        }

        private void SetPriority(int priority)
        {
            if (priority < 1 || priority > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(priority), "Priority must be between 1 and 10.");
            }
            Priority = priority;
        }
    }
}