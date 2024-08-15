using Erni.University.Exceptions;
using Erni.University.Models;
using Erni.University.Repositories;
using Erni.University.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace Erni.University.UnitTests.Services;

[TestFixture]
public class UserServiceTest
{
    private Mock<IRepository<User>> _userRepositoryMock;

    [SetUp]
    public void SetUp()
    {
        _userRepositoryMock = new Mock<IRepository<User>>();
    }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Runs once before any tests in this fixture are run
    }

    [TearDown]
    public void TearDown()
    {
        // Runs after each test in this fixture
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        // Runs once after all tests in this fixture are run
    }

    private UserService CreateUnitUnderTest()
    {
        return new UserService(_userRepositoryMock.Object, Mock.Of<ILogger<UserService>>());
    }

    [Test]
    public void GetUsers_WhenUsersIsNotEmpty_ShouldReturnFailure()
    {
        List<User> expectedUsers =
        [
            new User
            {
                Name = "John",
                Email = "john@email.com"
            }
        ];
        var unitUnderTest = CreateUnitUnderTest();
        _userRepositoryMock.Setup(x => x.GetAll()).Returns(expectedUsers);

        var actualResult = unitUnderTest.GetUsers();
        var actualUsers = (actualResult as Success<IEnumerable<User>>)?.Value;

        actualResult.IsSuccess.ShouldBeTrue();
        actualUsers?.Count().ShouldBe(expectedUsers.Count);
        _userRepositoryMock.Verify(x => x.GetAll(), Times.Once);
    }

    [Test]
    public void GetUsers_WhenUsersIsEmpty_ShouldReturnFailure()
    {
        var unitUnderTest = CreateUnitUnderTest();
        _userRepositoryMock.Setup(x => x.GetAll()).Returns([]);

        var actualResult = unitUnderTest.GetUsers();
        var actualException = (actualResult as Failure<IEnumerable<User>>)?.Exception;

        actualResult.IsSuccess.ShouldBeFalse();
        actualException.ShouldBeOfType<NoUsersFoundException>();
        _userRepositoryMock.Verify(x => x.GetAll(), Times.Once);
    }

    [TestCase("John", "john@email.com")]
    [TestCase("Alice", "alice@test.com")]
    public void GetUser_WhenUserExists_ShouldReturnSuccess(string expectedName, string expectedEmail)
    {
        var expectedUser = new User
        {
            Name = expectedName,
            Email = expectedEmail
        };
        var unitUnderTest = CreateUnitUnderTest();
        _userRepositoryMock.Setup(x => x.GetAll()).Returns([expectedUser]);

        var actualResult = unitUnderTest.GetUser(expectedEmail);
        var actualUser = (actualResult as Success<User>)?.Value;

        actualResult.IsSuccess.ShouldBeTrue();
        actualUser.ShouldBe(expectedUser);
    }

    [Test]
    public void GetUser_WhenUserDoesNotExist_ShouldReturnFailure()
    {
        var unitUnderTest = CreateUnitUnderTest();
        _userRepositoryMock.Setup(x => x.GetAll()).Returns([])
            .Callback(() => { Console.WriteLine("GetAll was called"); });

        var actualResult = unitUnderTest.GetUser("john@email.com");
        var actualException = (actualResult as Failure<User>)?.Exception;

        actualResult.IsSuccess.ShouldBeFalse();
        actualException.ShouldBeOfType<UserNotFoundException>();
    }

    [Test]
    public void GetUser_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        var unitUnderTest = CreateUnitUnderTest();
        _userRepositoryMock.Setup(x => x.GetAll()).Throws<Exception>();

        var actualResult = unitUnderTest.GetUser("john@email.com");
        var actualException = (actualResult as Failure<User>)?.Exception;

        actualResult.IsSuccess.ShouldBeFalse();
        actualException.ShouldBeOfType<Exception>();
    }
}