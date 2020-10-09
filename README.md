# TVMaze
This is a webAPI with a hosted service.
The hosted service is responsible for scraping data from TVMaze once a day and add all new data to a SQL database.

The WebAPI can be used to retrieve this data from SQL.

# Decicions Made
I've decided to use .netCore 3.1 since 2.x support has either stopped or is close to End of support date.
Polly is used to handle service exceptions and retries.
EntityFramework and SQL is used for storage.
Swagger is used to document the API
# Getting Started
Ensure the migration script is ran to create all tables required for storage.

Start the WebAPI.
The WebAPI will start the hosted timer service which will insert all new data.
Using the SwaggerUI you can execute the Get after providing a PageNumber parameter.
Data will be retruned in json

# Example Result
[
  {
    "id": 53,
    "name": "Nashville",
    "castmembers": [
      {
        "id": 7214,
        "name": "Maisy Stella",
        "birthday": "2003-12-13T00:00:00"
      },
      {
        "id": 7213,
        "name": "Lennon Stella",
        "birthday": "1999-08-13T00:00:00"
      }
    ]
  }
]
