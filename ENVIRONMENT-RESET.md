# Development Environment: Reset Instructions

This document contains instructions for resetting your development environment when you encounter issues.

## Quick Reset

The fastest way to reset your environment is using the provided utility scripts:

### Unix/Mac/Linux
```bash
# Reset all components
./scripts/reset-environment.sh

# Reset specific component only
./scripts/reset-environment.sh backend    # Only reset backend
./scripts/reset-environment.sh web        # Only reset frontend
./scripts/reset-environment.sh mobile     # Only reset mobile app
./scripts/reset-environment.sh docker     # Only reset Docker containers
```

### Windows
```batch
# Reset all components
scripts\reset-environment.bat

# Reset specific component only
scripts\reset-environment.bat backend    # Only reset backend
scripts\reset-environment.bat web        # Only reset frontend
scripts\reset-environment.bat mobile     # Only reset mobile app
scripts\reset-environment.bat docker     # Only reset Docker containers
```

## Manual Reset Steps

If you prefer to reset components manually, follow these instructions:

### Backend (.NET) Cleanup

```bash
# Navigate to the backend folder
cd backend

# Remove build directories
rm -rf bin/ obj/        # Unix/Mac/Linux
rmdir /s /q bin obj     # Windows

# Remove SQLite database files
rm -f *.db *.db-shm *.db-wal        # Unix/Mac/Linux
del *.db *.db-shm *.db-wal          # Windows

# Restore packages
dotnet restore
```

### Frontend (React) Cleanup

```bash
# Navigate to the frontend folder
cd web

# Remove node_modules
rm -rf node_modules/                # Unix/Mac/Linux
rmdir /s /q node_modules            # Windows

# Remove lock file (if exists)
rm -f package-lock.json             # Unix/Mac/Linux
del package-lock.json               # Windows

# Reinstall dependencies
npm install
```

### Mobile (Xamarin) Cleanup

```bash
# Navigate to the mobile folder
cd mobile

# Remove build directories
rm -rf bin/ obj/                    # Unix/Mac/Linux
rmdir /s /q bin obj                 # Windows

# Restore packages
dotnet restore
```

### Docker Cleanup

If you're using Docker, you can completely clean the environment with:

```bash
# Stop and remove all containers, volumes, and networks
docker-compose down -v
```

## Database Initialization Fix

If you encounter the error `SQLite Error 1: 'no such table: Machines'`, you can modify the `Program.cs` file in the backend project to ensure proper database recreation:

```csharp
// In Program.cs, find this section:
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // Add these lines to force database recreation
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    // Rest of the code...
}
```

This forces the database to be rebuilt with the proper schema whenever the application starts.

## Starting the Application After Reset

After resetting the environment, start the application using:

### Unix/Mac/Linux
```bash
./scripts/start-app.sh
```

### Windows
```batch
scripts\start-app.bat
```

> **Note**: The reset scripts only clean the environment but do not start the services. Use the start scripts to run the application.
