using CarDiagnostics.API.src.Modules.User.Core.Exceptions.ExceptionHandler;
using CarDiagnostics.API.src.Modules.User.Core.Services;
using CarDiagnostics.API.src.Modules.User.Data.Repositories;
using CarDiagnostics.API.src.Modules.User.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var inMemoryDatabase = builder.Configuration["inMemoryDatabase"];

if (inMemoryDatabase == null || bool.Parse(inMemoryDatabase))
{
    builder.Services.AddDbContext<UserDbContext>(options => options.UseInMemoryDatabase("User"));
}
else
{
    builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}

Log.Logger = new LoggerConfiguration()
    .WriteTo.File($"Logs/.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.AddSerilog();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();
app.UseMiddleware<GlobalExceptionHandler>();
try
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetService<UserDbContext>();
        dbContext?.Database.EnsureCreated();
        Log.Information("Connected with database.");
    }
}
catch (Exception ex)
{
    Log.Error(ex.Message);
    Environment.Exit(1);
}
app.UseCors("AllowAll");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();