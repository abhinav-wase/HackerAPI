# HackerAPI
HackerAPI_CoreApp
# Hacker News API - ASP.NET Core Web API

This project is an ASP.NET Core Web API that serves as an interface to retrieve the newest stories from the Hacker News API. It includes a controller, `NewsController`, with an endpoint for fetching the newest stories with pagination support.

## Table of Contents

- [Getting Started](##getting-started)
  - [Prerequisites](###prerequisites)
  - [Installation](#installation)
- [Usage](##usage)
  - [Endpoint](###endpoint)
  - [Query Parameters](###query-parameters)
  - [Swagger UI](###swagger-ui)
    - [Exploring and Testing the API](###exploring-and-testing-the-api)
- [Configuration](##configuration)
- [Error Handling](##error-handling)
- [Testing](#testing)
- [Contributing](#contributing)
- [License](#license)

## Getting Started

### Prerequisites

To run this project, you need the following:

- [.NET Core SDK](https://dotnet.microsoft.com/download) installed on your machine.

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/yourusername/hacker-news-api.git
   ```
2. Navigate to the project directory:
 ```bash
cd hackernewsapi
```

3. Run the application:
 ```bash
dotnet run
```
The API should now be running locally.

## Usage

### Endpoint

The API provides an endpoint for retrieving the newest stories from Hacker News. (Default port : 5163|| IISExpress : 44398 for https)

```
Endpoint URL: http://localhost:<your-port>/api/News/newest
```


### Query Parameters

The GetNewestStories endpoint supports the following optional query parameters for pagination:

  1. page (default: 1): The page number of the results.
  2. pageSize (default: 10): The number of items to include per page.

Example Request: (Default port : 5163 || IISExpress : 44398 for https)

```http
GET http://localhost:<your-port>/api/News/newest?page=2&pageSize=15
```
![image](https://github.com/abhinav-wase/HackerAPI/assets/62688135/0ce64a98-e9e8-4552-99aa-951c8e11eb8d)


### Swagger UI
Explore and test the API using Swagger UI. Swagger UI provides an interactive documentation interface.


1. Run the application.

2. Open a web browser.

3. Navigate to the Swagger UI interface by visiting the following URL: (Default port : 5163 || IISExpress : 44398 for https)

```bash
http://localhost:<your-port>/swagger
```


### Exploring and Testing the API

In Swagger UI, you will see a list of available API endpoints.

  1. Click on the endpoint you want to explore, e.g., /api/News/newest.
     You will see details about the endpoint, including supported HTTP methods, parameters, request body (if applicable), and possible responses.

  2. Click the "Try it out" button, fill in any parameters, and click "Execute."

  3. Review the response details, including the HTTP status code, response body, headers, etc.
     
![image](https://github.com/abhinav-wase/HackerAPI/assets/62688135/2a446d9a-6c26-4a53-8993-8930ae6d9b2c)

## Configuration

If needed, you can modify the CORS policy in the Startup.cs file.

```csharp
[EnableCors("AllowAllDomainRequests")]
```

## Error Handling

If an error occurs during the retrieval of the newest stories, the API returns a 500 Internal Server Error with an error message in the response body.

Example Error Response:

```json
{
  "error": "An error occurred: [error message]"
}
```

# Testing
To run the tests for this project, follow these steps:

Open a terminal.

Navigate to the project directory:

```bash
cd HackerNewsApi_Test
```

Run the tests:

```bash
dotnet test
```

This will execute the test suite and provide feedback on the test results.

![image](https://github.com/abhinav-wase/HackerAPI/assets/62688135/02419786-085d-45ba-8a19-95290520d8fa)



## Contributing
Feel free to contribute to the project by opening issues or submitting pull requests. Your feedback and contributions are welcome.

## License
This project is licensed under the MIT License.
