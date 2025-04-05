# CarDiagnostics Coding Task

## Build and run the app:

1. Navigate to folder Use command line CarDiagnostics\CarDiagnostics.API
2. Open command line here
3. Run commands one by one (set parameter inMemoryDatabase to false if you enter ConnectionString to database in appsettings.json):
   ```shell
    dotnet build
    dotnet run --inMemoryDatabase=true
   ```

## Using Swagger

After running, you can navigate to http://localhost:5299/swagger/index.html and see exposed endpoints