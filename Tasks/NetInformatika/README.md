# NetInformatika Task

### Task Info
Write a web app that will allow users to register/login with their account.
After successful login, show two input fields for start location and destination location. Use any free
autocomplete API for addresses.
After the user enters the addresses, show them on the map and draw the route between those two
points. Use the routing API of your choice.
After the markers and route are drawn on the map, allow user to press the "Play" button. Pressing
the "Play" button should start moving the start location marker on the map towards the destination
marker.
When the start location marker reaches the destination, notify the user.
After the “trip” is finished, save trip data in database of your choice (MSSQL is preferred). Data
should contain all the points of the route, date and time of when the trip started and ended.

### Setup
1) Create database inside Microsoft SQL Server.
2) Setup your connection to Microsoft SQL Server inside appsettings.json!
3) Place your Google Map API key inside appsettings.json!
4) Make migration on database and update it:
```
> add-migration "Initial Setup"
> update-database
```

### Endpoints
```
GET /index
GET /trips
GET /login
GET /register
POST /trips
POST /login
POST /register
POST /logout
```