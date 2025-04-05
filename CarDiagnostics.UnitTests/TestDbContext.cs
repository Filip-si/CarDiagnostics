using Microsoft.EntityFrameworkCore;

namespace CarDiagnostics.UnitTests;

public static class TestDbContext
{
    public static T InitInMemoryDbContext<T>() where T : DbContext
    {
        var options = new DbContextOptionsBuilder<T>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return (T?)Activator.CreateInstance(typeof(T), options) ?? throw new ArgumentNullException();
    }
}
