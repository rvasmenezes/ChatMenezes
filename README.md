#Chat system via browser, using C# language.

#Installation

1 - Open PowerShell;
2 - Navigate to the root folder, where docker-compose.yml is located.
3 - Run the command "docker-compose up" 


#Docker compose will be responsible for:

1 - Installing the RabbitMQ Message Broker;
2 - Installing the Postgres database;
3 - Running the database scripts in the init.sql file (creating tables and inserting the "Job" user)
4 - Run the Dockerfile to build the project image and start it on port 5533 (http://localhost:5533/)


#Usage

1 - Access the system
2 - Register with name, email and password
3 - Create a chat room
4 - Send messages simultaneously with other users
5 - Search for financial data by typing "/stock={stockCode}", where stockCode is the stock symbol
