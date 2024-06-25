using Moq;
using VismaInternshipErnestasJ.Data;
using VismaInternshipErnestasJ.Models;
using VismaInternshipErnestasJ.Services;
using VismaInternshipErnestasJ.Services.VismaInternshipErnestasJ.Services;

namespace VismaInternshipErnestasJ.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IDataStorage> _mockDataStorage;
        private readonly IUserService _userService;
        private readonly List<User> _users;

        public UserServiceTests()
        {
            _mockDataStorage = new Mock<IDataStorage>();
            _users = [
                new User ("foo", "Foo", "password"),
                new User ("bar", "Bar", "password"),
                ];
            _mockDataStorage.Setup(ds => ds.LoadUsers()).Returns(_users);
            _userService = new UserService(_mockDataStorage.Object);
        }

        [Fact]
        public void Login_Should_Return_User_If_Credentials_Are_Correct()
        {
            var user = _userService.Login("foo", "password");
            Assert.NotNull(user);
            Assert.Equal("Foo", user.Name);
        }

        [Fact]
        public void Login_Should_Return_Null_If_Credentials_Are_Incorrect()
        {
            var user = _userService.Login("foo", "wrongpassword");
            Assert.Null(user);
        }
    }
}
