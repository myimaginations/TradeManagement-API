**Trade Management API**
This project is a backend API for a trade management system, built using ASP.NET Core and MongoDB. It provides a set of RESTful APIs to manage users, track stock instruments, process buy and sell orders, and maintain user portfolios. The application is designed to demonstrate full CRUD (Create, Read, Update, Delete) functionality with a NoSQL database.

Features
User Management: Create, view, update, and delete user accounts. Each user has a unique ID, a username, and a balance.
Instrument Data: Stores a list of available stock instruments (e.g., AAPL, TSLA, BTC) with their current prices.
Order Processing: Users can place BUY and SELL orders for any available instrument. The API handles the transaction logic, updating the user's balance and holdings.
Portfolio Tracking: Maintains a record of each user's stock holdings and quantities.
Order History: Logs all buy and sell orders for each user, providing a complete transaction history.

Technology Stack
ASP.NET Core 6: The web framework for building the API.
C#: The primary programming language.
MongoDB: A flexible NoSQL database used for data persistence.
MongoDB.Driver: The official .NET library for connecting to MongoDB.
Swagger: Provides a user-friendly interface for documenting and testing the API endpoints.

Getting Started
Follow these steps to get the project up and running on your local machine.

Prerequisites
.NET 6 SDK
MongoDB Atlas Account
Git

Installation
Clone the repository to your local machine: git clone https://github.com/myimaginations/TradeManagement-API
Navigate to the project directory: cd TradeManagement
Configure your MongoDB connection string in the appsettings.json file.

Running the Application
After configuring the database, run the application from your terminal: dotnet run
This will start the API and open the Swagger UI in your browser, where you can test all the endpoints.

API Endpoints
The API is structured around the following endpoints:

Endpoint	HTTP Method	Description
/api/Users	POST	Creates a new user account.
/api/Users/{id}	GET	Retrieves a specific user by ID.
/api/Users	GET	Retrieves a list of all users.
/api/Users/{id}	PUT	Updates an existing user's information.
/api/Users/{id}	DELETE	Deletes a user account.
/api/Instruments	GET	Lists all available trading instruments.
/api/Orders	POST	Places a new buy or sell order.
/api/Orders/{userId}	GET	Retrieves a specific user's order history.
/api/Portfolio/{userId}	GET	Retrieves a specific user's current stock holdings and balance.
