using Moq;
using VismaInternshipErnestasJ.Data;
using VismaInternshipErnestasJ.Models;
using VismaInternshipErnestasJ.Services;

namespace VismaInternshipErnestasJ.Tests.Services
{
    public class ShortageServiceTests
    {
        private static readonly User Alice = new("alice", "Alice", "password");
        private static readonly User Bob = new("bob", "Bob", "bobpsw");

        private readonly Mock<IDataStorage> _mockDataStorage;
        private readonly UserContext _mockUserContext = new() { CurrentUser = Alice };
        private readonly ShortageService _shortageService;
        private readonly List<Shortage> _shortages;

        public ShortageServiceTests()
        {
            _mockDataStorage = new Mock<IDataStorage>();
            _shortages = [];
            _mockDataStorage.Setup(ds => ds.LoadShortages()).Returns(_shortages);
            _mockDataStorage.Setup(ds => ds.SaveShortages(It.IsAny<List<Shortage>>())).Callback<List<Shortage>>(s => _shortages.AddRange(s));
            _shortageService = new ShortageService(_mockDataStorage.Object, _mockUserContext);
        }

        [Fact]
        public void RegisterShortage_Should_Add_New_Shortage()
        {
            // Arrange
            var shortage = new Shortage("Projector", "Projector shortage", Room.MeetingRoom, Category.Electronics, 5, [Alice]);

            // Act
            _shortageService.RegisterShortage(shortage);

            // Assert
            Assert.Contains(shortage, _shortages);
        }

        [Fact]
        public void RegisterShortage_Should_Override_If_Higher_Priority()
        {
            // Arrange
            var shortage1 = new Shortage("Projector", "Projector shortage", Room.MeetingRoom, Category.Electronics, 5, [Alice]);
            var shortage2 = new Shortage("Projector", "Projector shortage", Room.MeetingRoom, Category.Electronics, 8, [Alice]);

            // Act
            _shortageService.RegisterShortage(shortage1);
            _shortageService.RegisterShortage(shortage2);

            // Assert
            Assert.Equal(8, _shortages.First().Priority);
        }

        [Fact]
        public void DeleteShortage_Should_Remove_Shortage_If_Found()
        {
            // Arrange
            var shortage = new Shortage("Projector", "Projector shortage", Room.MeetingRoom, Category.Electronics, 5, [Alice]);
            _shortages.Add(shortage);

            // Act
            _shortageService.DeleteShortage("Projector", Room.MeetingRoom);

            // Assert
            Assert.Empty(_shortages);
        }

        [Fact]
        public void DeleteShortage_Should_Not_Remove_Shortage_If_Not_Found()
        {
            // Arrange
            var shortage = new Shortage("Projector", "Projector shortage", Room.MeetingRoom, Category.Electronics, 5, [Alice]);
            _shortages.Add(shortage);

            // Act
            _shortageService.DeleteShortage("Laptop", Room.MeetingRoom);

            // Assert
            Assert.Single(_shortages);
        }

        [Fact]
        public void DeleteShortage_Should_Not_Remove_Shortage_If_User_Does_Not_Have_Permission()
        {
            // Arrange
            var shortage = new Shortage("Projector", "Bob", Room.MeetingRoom, Category.Electronics, 5, [Bob]);
            _shortages.Add(shortage);

            // Act
            _shortageService.DeleteShortage("Projector", Room.MeetingRoom);

            // Assert
            Assert.Single(_shortages);
        }

        [Fact]
        public void ListShortages_Should_Return_Shortages_Created_By_Current_User()
        {
            // Arrange
            var shortage1 = new Shortage("Projector", "Projector shortage", Room.MeetingRoom, Category.Electronics, 5, [Alice]);
            var shortage2 = new Shortage("Laptop", "Bob", Room.Bathroom, Category.Electronics, 3, [Bob]);
            _shortages.Add(shortage1);
            _shortages.Add(shortage2);

            // Act
            var result = _shortageService.ListShortages();

            // Assert
            Assert.Single(result);
            Assert.Contains(shortage1, result);
        }

        [Fact]
        public void FilterByTitle_Should_Return_Shortages_With_Matching_Title()
        {
            // Arrange
            var shortage1 = new Shortage("Projector", "Projector shortage", Room.MeetingRoom, Category.Electronics, 5, [Alice]);
            var shortage2 = new Shortage("Laptop", "Bob", Room.Bathroom, Category.Electronics, 3, [Bob]);
            _shortages.Add(shortage1);
            _shortages.Add(shortage2);

            // Act
            var result = _shortageService.FilterByTitle("Projector");

            // Assert
            Assert.Single(result);
            Assert.Contains(shortage1, result);
        }

        [Fact]
        public void FilterByCreatedOnDate_Should_Return_Shortages_Created_Between_StartDate_And_EndDate()
        {
            // Arrange
            var startDate = new DateTime(2022, 1, 1);
            var endDate = new DateTime(2022, 1, 31);
            var shortage1 = new Shortage("Projector", "Projector shortage", Room.MeetingRoom, Category.Electronics, 5, [Alice])
            {
                CreatedOn = new DateTime(2022, 1, 15)
            };
            var shortage2 = new Shortage("Laptop", "Bob", Room.Bathroom, Category.Electronics, 3, [Bob])
            {
                CreatedOn = new DateTime(2022, 2, 1)
            };
            _shortages.Add(shortage1);
            _shortages.Add(shortage2);

            // Act
            var result = _shortageService.FilterByCreatedOnDate(startDate, endDate);

            // Assert
            Assert.Single(result);
            Assert.Contains(shortage1, result);
        }

        [Fact]
        public void FilterByCategory_Should_Return_Shortages_With_Matching_Category()
        {
            // Arrange
            var shortage1 = new Shortage("Projector", "Projector shortage", Room.MeetingRoom, Category.Electronics, 5, [Alice]);
            var shortage2 = new Shortage("Laptop", "Bob", Room.Bathroom, Category.Electronics, 3, [Bob]);
            _shortages.Add(shortage1);
            _shortages.Add(shortage2);

            // Act
            var result = _shortageService.FilterByCategory(Category.Electronics);

            // Assert
            Assert.Contains(shortage1, result);
            Assert.Contains(shortage2, result);
        }

        [Fact]
        public void FilterByRoom_Should_Return_Shortages_With_Matching_Room()
        {
            // Arrange
            var shortage1 = new Shortage("Projector", "Projector shortage", Room.MeetingRoom, Category.Electronics, 5, [Alice]);
            var shortage2 = new Shortage("Laptop", "Bob", Room.Bathroom, Category.Electronics, 3, [Bob]);
            _shortages.Add(shortage1);
            _shortages.Add(shortage2);

            // Act
            var result = _shortageService.FilterByRoom(Room.MeetingRoom);

            // Assert
            Assert.Single(result);
            Assert.Contains(shortage1, result);
        }
    }
}
