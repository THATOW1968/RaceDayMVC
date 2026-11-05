# RaceDay Project - Part 3 

## Project Overview
This project is the Part 3 submission for the RaceDay platform. It consists of two projects within one solution:
1. **RaceDayAPI (Part 2):** The RESTful backend built with ASP.NET Core Web API and Entity Framework Core.
2. **RaceDayMVC (Part 3):** The frontend web application built with ASP.NET Core MVC.

## What was implemented in Part 3

### 1. MVC Application Setup
*   The `RaceDayMVC` project consumes the `RaceDayAPI` using `HttpClient` (configured in `Program.cs` and handled via `ApiService.cs`).
*   The MVC project contains **no direct database access**. All data operations are sent to the API.

### 2. Authentication and Role-Based Navigation
*   Users can register and log in through the MVC application by communicating with the API's `/api/Auth` endpoints.
*   User sessions are maintained using `HttpContext.Session`. The `UserId`, `UserRole`, and `UserEmail` are stored securely upon login.
*   The `_Layout.cshtml` navbar dynamically changes based on the logged-in user's role. Organizers see "My Events" and "Create Event", while Participants see "Browse Events".
*   Unauthenticated users are automatically redirected to the Login page if they try to access protected pages (enforced via `OnActionExecuting`).
*   Users can securely log out, which clears the session.

### 3. Organiser Interface
*   Organisers have a dashboard to view all their created events.
*   Organisers can create new events (Name, Description, Date, Location, Distance).
*   Organisers can view a list of all Participants who have enrolled in a specific event, complete with a colour-coded status badge (Green for "Confirmed").
*   *Note: Azure Blob Storage is simulated. The API assigns a cloud-hosted image URL to new events to demonstrate how image rendering works, as we did not connect to a live Azure subscription.*

### 4. Participant Interface
*   Participants can browse all upcoming events.
*   Participants can click "Enrol Now" to register for an event. This sends a POST request to the API to record the enrolment.

### 5. UI Design
*   The UI reflects the RaceDay brand using a dark navbar, energetic red accents, and Bootstrap 5 for responsiveness.
*   Enrolment statuses are visually distinct (using Bootstrap badges).
*   The application handles invalid inputs gracefully (e.g., showing error alerts instead of crashing).

### 6. Database (SQLite)
To ensure the project runs perfectly on any computer without requiring SQL Server setup, the database was switched to **SQLite**. The `RaceDay.db` file is created automatically the first time the API runs.

## How to Run Locally
1. Open `RaceDayAPI.sln` in Visual Studio.
2. Right-click the Solution -> **Configure Startup Projects** -> Select **Multiple startup projects**.
3. Set both `RaceDayAPI` and `RaceDayMVC` to **Start**. Click OK.
4. Press **the green sign arrow**.
   * The API will launch Swagger on `https://localhost:5001`.
   * The MVC app will launch on `https://localhost:5002`.

