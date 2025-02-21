BlogPostManager Project
This repository contains a Blog Post Manager application. The project is split into two main parts:

Backend: A RESTful API built with ASP.NET Core (.NET 8) that handles CRUD operations for blog posts.
Frontend: A modern Angular application that interacts with the backend API, allowing users to create, view, update, and delete blog posts.

Repository Structure:
BlogPostManager/
├── BlogPostManager.API/               # ASP.NET Core backend project
│   ├── Controllers/
│   │   └── PostsController.cs
│   ├── Data/
│   │   └── BlogContext.cs
│   ├── Models/
│   │   └── Post.cs
│   ├── Repositories/
│   │   ├── Interfaces/
│   │   │   └── IPostRepository.cs
│   │   └── PostRepository.cs
│   ├── Services/
│   │   ├── Interfaces/
│   │   │   └── IPostService.cs
│   │   └── PostService.cs
│   ├── Program.cs
│   ├── appsettings.json
│   └── BlogPostManager.API.csproj
├── blog-manager-frontend/             # Angular frontend project
│   ├── src/
│   │   ├── app/
│   │   │   ├── components/
│   │   │   │   ├── header/
│   │   │   │   │   ├── header.component.ts
│   │   │   │   │   ├── header.component.html
│   │   │   │   │   └── header.component.css
│   │   │   │   ├── blog-list/
│   │   │   │   │   ├── blog-list.component.ts
│   │   │   │   │   ├── blog-list.component.html
│   │   │   │   │   └── blog-list.component.css
│   │   │   │   ├── blog-detail/
│   │   │   │   │   ├── blog-detail.component.ts
│   │   │   │   │   ├── blog-detail.component.html
│   │   │   │   │   └── blog-detail.component.css
│   │   │   │   └── blog-form/
│   │   │   │       ├── blog-form.component.ts
│   │   │   │       ├── blog-form.component.html
│   │   │   │       └── blog-form.component.css
│   │   │   ├── app.routes.ts
│   │   │   ├── app.component.ts
│   │   │   ├── app.component.html
│   │   │   └── app.component.css
│   │   ├── main.ts
│   │   └── ...
│   ├── package.json
│   └── angular.json
└── README.md


How to Run the Project
Backend (ASP.NET Core API)
Prerequisites:

.NET 8 SDK
SQL Server or update the connection string in appsettings.json to your preferred database.

Setup and Run:

Open a terminal in the BlogPostManager.API directory.
Restore the packages and build the project:

dotnet restore
dotnet build

Run the migrations to create/update the database:

dotnet ef migrations add InitialCreate
dotnet ef database update

Start the API:
dotnet run
The API should now be running (e.g., at https://localhost:7252 in my case)and Swagger will pop up.

Frontend (Angular App)
Prerequisites:

Node.js (v18+)
Angular CLI
Setup and Run:

Open a terminal in the blog-manager-frontend directory.
Install dependencies:
npm install

Start the development server:
ng serve

Open your browser and navigate to http://localhost:4200.
Note: I have not invest much in css as this is custom per company I have tried to keep it basic(ugly).

Design Choices
Backend:
The API follows RESTful conventions, with separate layers (Controllers, Services, Repositories) adhering to SOLID principles for maintainability and testability.

Frontend:
The Angular app is built using standalone components, ensuring a modular design. Reactivity is handled through Angular's state management with services, and navigation is managed using Angular Router.

Error Handling & Security:
Both backend and frontend implement error handling and validation. The API returns proper HTTP status codes, and error messages are handled gracefully in the UI. CORS is configured to allow the frontend to communicate with the backend.

Additional Features
Search & Filtering:
The blog list view includes a search feature that filters posts in real time.

Routing:
React Router/Angular Router is implemented for navigation between the homepage, post details, and the add/edit forms.

I don't have experience with Docker; if I had known earlier, I would have learned and implemented it.
