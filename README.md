# React4DotNet
We'll be going step by step to create a React app that can communitcate with an API made with DotNet and C# to perform full CRUD operations.

## Create the react app
npx create-react-app React4DotNet   
cd React4DotNet
npm start

## Clear out APP.js
We're going to replace the default app with a bare-bones react app for communicating with our API.

### App.js
We set up the App.js with a constant variable as the API URL to fetch data from, then we declare an empty useState variable to store our people in. Our useEffect method will fetch our list of people from the API and use the variable's set method to populate the data, once populated, the dom should refresh (React functionality).

Then return basic html and css to display and map the data we receive. This will serve as a great point to switch and create the DotNet API so we can start fetching data from it before we move to the other CRUD operations.

### API creation
Create the api with "dotnet new webapi -n api" we'll want to CD to it then install the required packages.

dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package System.IdentityModel.Tokens.Jwt

Npgsql — lets .NET talk to Postgres
EntityFrameworkCore.Design — Entity Framework tools for managing the database
JwtBearer — middleware that automatically checks incoming requests for a valid JWT token
System.IdentityModel.Tokens.Jwt — the library that actually creates and validates JWT tokens

After initializing, we'll need to set up our Person class, we'll put this in a "Models" folder inside the API folder.
Set up Data\AppDbContext.cs, this is like pool.js, allows the app to talk to the server and our tables.
