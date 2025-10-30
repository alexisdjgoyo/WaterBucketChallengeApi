# Water Bucket Challenge API (.NET 9)

This project implements a RESTful API in .NET 9 to solve the classic Water Bucket Challenge, meeting the requirements of finding the **optimal solution** (the shortest path) efficiently.

## 🚀 Start-up

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download) or higher.

### 1. Clone the Repository

```bash
git clone https://github.com/alexisdjgoyo/WaterBucketChallengeApi.git
cd WaterBucketChallengeApi/WaterBucketChallengeApi
```
### 2. Run project

```bash
dotnet run
```

### 3. Testing

```bash
dotnet test
```

## Aditional info

### 📂 Project Structure

The solution is organized into two primary projects (`.csproj`) coordinated by a single solution file (`.sln`). This structure adheres to the **Separation of Concerns (SoC)** and facilitates best practices like **Dependency Injection (DI)** and **Testing**.

| Directory / File                 | Type                 | Purpose                                                                                                                                                               |
| :------------------------------- | :------------------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `WaterBucketChallengeApi/`       | **Main API Project** | Contains the core application and logic.                                                                                                                              |
| ├── `Controllers/`               | Web Layer            | Contains the `WaterBucketController` which handles `POST /api/solve` requests, performs validation, and formats HTTP responses.                                       |
| ├── `Services/`                  | Business Logic       | Contains the concrete implementation (`WaterBucketSolverService.cs`), housing the **GCD** and **BFS** algorithms.                                                     |
| ├── `Interfaces/`                | Contracts            | Defines the contract (`IWaterBucketService`), crucial for the Dependency Inversion Principle (DIP) and **mocking during testing**.                                    |
| ├── `Models/`                    | Data Transfer        | Contains `WaterBucketRequestDto` (input model with validation annotations) and `SolutionStep` (the detailed JSON response format).                                    |
| ├── `Program.cs`                 | Configuration        | Application entry point where the middleware and Dependency Injection (`IWaterBucketService`) are configured.                                                         |
| ├──`WaterBucketChallengeApi.sln`    | Solution File        | Coordinates and manages the two related projects (`API` and `Tests`).                                                                                                 |
| `WaterBucketChallengeApi.Tests/` | **Testing Project**  | Verifies the integrity and correctness of the application.                                                                                                            |
| ├── `UnitTests/`                 | Tests                | Contains `SolverTest.cs`, which directly tests the **GCD** and **BFS** algorithm logic.                                                                               |
| ├── `IntegrationTests/`          | Tests                | Contains `IntegrationTests.cs`, which uses `WebApplicationFactory` to simulate HTTP requests, verifying the Controller, DTO validation, and the overall API pipeline. |
