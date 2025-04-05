using CarDiagnostics.API.src.Modules.User.Api.Models;
using CarDiagnostics.API.src.Modules.User.Core.Exceptions;
using CarDiagnostics.API.src.Modules.User.Data;
using CarDiagnostics.API.src.Modules.User.Data.Entities;
using CarDiagnostics.API.src.Modules.User.Data.Repositories;

namespace CarDiagnostics.UnitTests;

[TestFixture]
public class UserRepositoryTests
{

    [Test]
    [TestCase("John", "Test", "john.test@gmail.com")]
    public void Should_AddUser_Successfully(string firstName, string lastName, string email)
    {
        //Given
        var request = new UserRequest(firstName, lastName, email);
        var dbContext = TestDbContext.InitInMemoryDbContext<UserDbContext>();
        var repository = new UserRepository(dbContext);

        //When
        repository.AddUser(request);
        var userCount = dbContext.Users.Count();
        var user = dbContext.Users.First();

        //Then
        Assert.That(userCount, Is.EqualTo(1));
        Assert.That(user.FirstName, Is.EqualTo(firstName));
        Assert.That(user.LastName, Is.EqualTo(lastName));
        Assert.That(user.Email, Is.EqualTo(email));
    }

    [Test]
    [TestCase("John", "Test", "john.test@gmail.com")]
    public void Should_AddUserThrowException_WhenUserWithThisEmailAlreadyAdded(string firstName, string lastName, string email)
    {
        //Given
        var request = new UserRequest(firstName, lastName, email);
        var dbContext = TestDbContext.InitInMemoryDbContext<UserDbContext>();
        var repository = new UserRepository(dbContext);

        //When
        repository.AddUser(request);

        //Then
        Assert.Throws<EmailAlreadyUsedException>(() => repository.AddUser(request));
    }

    [Test]
    public void Should_DeleteUser_Successfully()
    {
        //Given
        var dbContext = TestDbContext.InitInMemoryDbContext<UserDbContext>();
        var repository = new UserRepository(dbContext);

        //When
        dbContext.Users.Add(new UserEntity() { Email = "john.test@gmail.com", FirstName = "john", LastName = "test" });
        dbContext.SaveChanges();
        var userId = dbContext.Users.First().Id;

        repository.DeleteUser(userId);
        var userCount = dbContext.Users.Count();

        //Then
        Assert.That(userCount, Is.EqualTo(0));
    }

    [Test]
    public void Should_DeleteUserThrowException_WhenUserNotFound()
    {
        //Given
        Guid userId = Guid.NewGuid();
        var dbContext = TestDbContext.InitInMemoryDbContext<UserDbContext>();
        var repository = new UserRepository(dbContext);

        //When & Then
        Assert.Throws<UserNotFoundException>(() => repository.DeleteUser(userId));
    }

    [Test]
    public void Should_GetUser_Successfully()
    {
        //Given
        var dbContext = TestDbContext.InitInMemoryDbContext<UserDbContext>();
        var repository = new UserRepository(dbContext);

        //When
        dbContext.Users.Add(new UserEntity() { Email = "john.test@gmail.com", FirstName = "john", LastName = "test" });
        dbContext.SaveChanges();
        var userId = dbContext.Users.First().Id;
        var user = repository.GetUser(userId);

        //Then
        Assert.That(user.FirstName, Is.EqualTo("john"));
        Assert.That(user.LastName, Is.EqualTo("test"));
        Assert.That(user.Email, Is.EqualTo("john.test@gmail.com"));
    }

    [Test]
    public void Should_GetUserThrowException_WhenUserNotFound()
    {
        //Given
        Guid userId = Guid.NewGuid();
        var dbContext = TestDbContext.InitInMemoryDbContext<UserDbContext>();
        var repository = new UserRepository(dbContext);

        //When & Then
        Assert.Throws<UserNotFoundException>(() => repository.GetUser(userId));
    }

    [Test]
    public void Should_GetUsers_Successfully()
    {
        //Given
        var dbContext = TestDbContext.InitInMemoryDbContext<UserDbContext>();
        var repository = new UserRepository(dbContext);

        //When
        dbContext.Users.Add(new UserEntity() { Email = "john.test@gmail.com", FirstName = "john", LastName = "test" });
        dbContext.Users.Add(new UserEntity() { Email = "john2.test2@gmail.com", FirstName = "john2", LastName = "test2" });
        dbContext.SaveChanges();
        var users = repository.GetUsers();

        //Then
        Assert.That(users.Count, Is.EqualTo(2));
    }

    [Test]
    [TestCase("John", "Test", "john.test@gmail.com")]
    public void Should_UpdateUser_Successfully(string firstName, string lastName, string email)
    {
        //Given
        var dbContext = TestDbContext.InitInMemoryDbContext<UserDbContext>();
        var repository = new UserRepository(dbContext);

        //When
        dbContext.Users.Add(new UserEntity() { Email = email, FirstName = firstName, LastName = lastName });
        dbContext.SaveChanges();
        var id = dbContext.Users.First().Id;
        repository.UpdateUser(new UpdateUserRequest(id, "updated", "updated", "john.updated@gmail.com"));
        var user = dbContext.Users.First();

        //Then
        Assert.That(user.FirstName, Is.EqualTo("updated"));
        Assert.That(user.LastName, Is.EqualTo("updated"));
        Assert.That(user.Email, Is.EqualTo("john.updated@gmail.com"));
    }

    [Test]
    public void Should_UpdateUserThrowException_WhenUserNotFound()
    {
        //Given
        Guid userId = Guid.NewGuid();
        var dbContext = TestDbContext.InitInMemoryDbContext<UserDbContext>();
        var repository = new UserRepository(dbContext);
        //When & Then
        Assert.Throws<UserNotFoundException>(() => repository.UpdateUser(new UpdateUserRequest(userId, "updated", "updated", "john.updated@gmail.com")));
    }

    [Test]
    public void Should_UpdateUserThrowException_WhenUserWithThisEmailAlreadyAdded()
    {
        //Given
        Guid userId1 = Guid.NewGuid();
        Guid userId2 = Guid.NewGuid();
        var dbContext = TestDbContext.InitInMemoryDbContext<UserDbContext>();
        var repository = new UserRepository(dbContext);

        //When
        dbContext.Users.Add(new UserEntity() { Id = userId1, CreatedDate = DateTime.Now, Email = "john.updated@gmail.com", FirstName = "firstName", LastName = "lastName" });
        dbContext.Users.Add(new UserEntity() { Id = userId2, CreatedDate = DateTime.Now, Email = "john2.updated@gmail.com", FirstName = "firstName2", LastName = "lastName2" });
        dbContext.SaveChanges();

        //Then
        Assert.Throws<EmailAlreadyUsedException>(() => repository.UpdateUser(new UpdateUserRequest(userId2, "updated", "updated", "john.updated@gmail.com")));
    }
}

