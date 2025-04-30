# Cogtive Technical Challenge

Welcome to Cogtive's technical challenge! This repository contains a simplified version of our industrial IoT platform architecture to help us evaluate your skills as a developer.

## About Cogtive

Cogtive is a SaaS platform for factory floor operations, focused on:
- Production tracking and efficiency monitoring (OEE)
- Equipment, process stages, and batch management
- Real-time data collection
- IoT device integration
- Offline operation with later synchronization

## Technology Stack

- **Backend**: .NET Core API with Entity Framework Core
- **Frontend Web**: React with TypeScript
- **Mobile**: Xamarin.Forms (with optional migration to .NET MAUI)
- **Databases**: SQLite (default), PostgreSQL (optional)
- **Containerization**: Docker & Docker Compose (optional)

## Repository Structure

- `/backend`: .NET Core API service
- `/web`: React frontend application
- `/mobile`: Xamarin.Forms mobile application for industrial operators
- `/iot-simulator`: IoT device simulator for generating metrics data (for senior level)
- `/scripts`: Utility scripts for environment reset and application startup
- `docker-compose.yml`: Configuration for running all services

## Getting Started

### Prerequisites
- .NET SDK (6.0 or later)
- Node.js (16 or later)
- Visual Studio or Visual Studio Code for mobile development
- (Optional) Docker & Docker Compose

### Environment Setup

For a quick start, use the provided scripts:

#### Unix/Mac/Linux
```bash
# Start the backend and frontend
./scripts/start-app.sh

# If you need to reset the environment first
./scripts/reset-environment.sh
```

#### Windows
```batch
# Start the backend and frontend
scripts\start-app.bat

# If you need to reset the environment first
scripts\reset-environment.bat
```

### Manual Setup

#### Backend API
```bash
cd backend
dotnet restore
dotnet run
```
The API will be available at the URL shown in the console output (typically `https://localhost:5211`).

API endpoints:
- GET `/api/machines` - Returns a list of industrial machines
- GET `/api/production-data` - Returns production metrics
- GET `/api/machines/{id}` - Returns a specific machine
- GET `/api/machines/{id}/production-data` - Returns production data for a specific machine
- POST `/api/production-data` - Adds new production data (for IoT simulator)

#### Frontend Web
```bash
cd web
npm install
npm start
```
The web app will be available at `http://localhost:3000`.

#### Mobile App
```bash
cd mobile
dotnet restore
# Open the .csproj in Visual Studio
```

The mobile app is designed for factory operators to record production data and interact with IoT devices on the shop floor. For Android emulator testing, it uses `10.0.2.2` to connect to the API on your host machine.

## Challenge Tasks

Choose the level that matches your experience. Each level includes all requirements from previous levels.

### Junior Level Tasks

**Objective:** Demonstrate your ability to understand and work with an existing codebase.

**Required Deliverables:**

1. **Working Environment Setup**
   - Get the backend API running with SQLite database
   - Get the frontend web application displaying machine data
   - Verify the connection between components

2. **Intentional Error Identification**
   - Find and document the intentional error in the data model (hint: check the `Efficiency` property in production data)
   - Create a brief markdown document explaining what's wrong and how it affects the application

3. **Basic Improvement (choose ONE)**
   - Add proper validation attributes to the backend models
   - Improve the UI of the machine listing in the frontend
   - Add a loading indicator to the frontend while data is being fetched

**Evaluation Focus:**
- Following setup instructions correctly
- Understanding basic code structure
- Attention to detail in finding the intentional error
- Clean, readable code for your improvement

### Mid-Level Tasks

**Objective:** Demonstrate your ability to implement better architecture and add meaningful features.

**Required Deliverables:**

1. **Data Model Fix**
   - Fix the intentional error in the data model (changing `Efficiency` from string to decimal/number)
   - Update all components to work with the fixed data model
   - Ensure proper type conversion in the API and frontend

2. **Error Handling**
   - Implement comprehensive error handling in the React frontend
   - Add appropriate user feedback (error messages, loading states)
   - Make sure the UI gracefully handles API failures

3. **Frontend Enhancements**
   - Implement filtering of machines by status (active/inactive)
   - Implement sorting of machines by name, type, and status
   - Add a search functionality to find machines by name or serial number

4. **Automated Testing**
   - Add unit tests for at least one backend component
   - Add unit tests for at least one frontend component
   - Ensure tests can be easily run with standard commands

**Evaluation Focus:**
- Clean architecture and code organization
- Proper implementation of state management
- User experience considerations
- Testing strategy and implementation

### Senior Level Tasks

**Objective:** Demonstrate your ability to architect complex systems and implement advanced features.

**Required Deliverables:**

1. **Database Integration**
   - Implement PostgreSQL integration instead of SQLite
   - Configure proper database migrations
   - Document the setup process in your submission README

2. **Containerization**
   - Update Docker configuration for all components
   - Ensure proper networking between Docker containers
   - Provide Docker Compose configuration for easy startup

3. **Mobile App Enhancement**
   - Migrate at least one component of the mobile app to .NET MAUI
   - Ensure the migrated component functions correctly
   - Document the migration process and any challenges faced

4. **IoT Integration**
   - Implement the IoT device simulator to generate random machine metrics
   - Establish connection between the simulator and API
   - Display real-time updates in the frontend when new data arrives
   - Add a visualization component for the metrics data

5. **Architecture Documentation**
   - Provide a detailed markdown document proposing architectural improvements
   - Include diagrams (can be simple) illustrating your proposed architecture
   - Address scalability, maintainability, and code organization concerns

**Evaluation Focus:**
- System architecture and design decisions
- Implementation quality of advanced features
- Documentation clarity and thoroughness
- DevOps and containerization approach
- Real-time data handling capabilities

## Evaluation Criteria

Your submission will be evaluated based on:

- **Code Quality**: Well-structured, readable, and maintainable code
- **Technical Understanding**: Proper use of technologies and design patterns
- **Problem Solving**: Ability to identify and fix issues effectively
- **Architecture**: Component organization and interaction (especially for senior level)
- **Documentation**: Clear explanations and setup instructions

## Troubleshooting

### Environment Reset

If you encounter issues with the database or environment, reset it using:

```bash
# Unix/Mac/Linux
./scripts/reset-environment.sh

# Windows
scripts\reset-environment.bat
```

For more detailed reset instructions, see [ENVIRONMENT-RESET.md](./ENVIRONMENT-RESET.md).

### Common Issues

- **Database Errors**: If you see SQLite errors like "no such table", use the reset script to recreate the database.
- **TypeScript Errors**: For React type issues, run `npm install --save-dev @types/react @types/react-dom`.
- **CORS Issues**: If the frontend can't connect to the backend, check CORS configuration in Program.cs.
- **Docker Issues**: Ensure all needed Dockerfiles exist and configuration is correct.
- **Mobile Connection**: Android emulators use `10.0.2.2` for localhost; iOS simulators use `localhost` or `127.0.0.1`.

## Submission Guidelines

1. Fork this repository
2. Implement your solution based on your chosen level
3. Submit a pull request OR send us a zip file of your solution
4. Include a README with:
   - Which level you completed
   - Your approach to solving the tasks
   - Any challenges you faced and how you solved them
   - Setup instructions for reviewing your solution
   - Any additional notes or considerations

Good luck, and we look forward to seeing your solution!
