# Web platform server

For this project, you will implement a simple web server that can handle HTTP requests and serve static files.

Design this server as if you were creating a library that other developers can install and use in their applications. This solution is already setup to do that.

- The library you will create will be implemented in the `WebPlatformServer` project.
- The `WebPlatformServer.ConsoleRunner` project is a console application that will use the library to run the server. This project is already configured to import the library and run it.
- The server should be implemented in the `Server` class, and it should have a `Start()` method that starts the server and listens for incoming HTTP requests. 
	- Your server should be able to handle GET requests and serve static files from a specified directory.
	- The port should be configurable, and the default port should be 8080.
	- For any request that does not match a static file, the server should return a 404 Not Found response.
	- You are free to decide how to configure the server parameters (e.g., port, static file directory). Consider using either constructor parameters or properties for configuration; but any other approach is also acceptable as long as it is properly documented.
	- If no static file directory is specified, it should default to "./static", which is already provided in the project.
- The server should be able to handle multiple requests concurrently.