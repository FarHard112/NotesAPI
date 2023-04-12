# Notes API

The Notes API is a simple RESTful API built using C# and ASP.NET Core for managing personal notes. It provides endpoints to create, read, update, and delete notes, making it easy to integrate with various applications.

## Features

- RESTful API with endpoints for managing notes
- CRUD operations for creating, reading, updating, and deleting notes
- Secured endpoints using Authorization
- Implemented using AutoMapper for object mapping
- Built with C# and ASP.NET Core

## Endpoints

Below are the available API endpoints:

### Get All Notes

- `GET /api/note`
- Retrieves a list of all notes for the authorized user
- Requires Authorization

### Get Note By ID

- `GET /api/note/{id}`
- Retrieves a specific note by its ID for the authorized user
- Requires Authorization

### Create Note

- `POST /api/note`
- Creates a new note for the authorized user
- Requires Authorization

### Update Note

- `PUT /api/note`
- Updates an existing note for the authorized user
- Requires Authorization

### Delete Note

- `DELETE /api/note/{id}`
- Deletes a note by its ID for the authorized user
- Requires Authorization

## Usage

To use the API, you'll need to have the .NET SDK installed on your system and a computer running Windows, macOS, or Linux. Clone the repository, navigate to the project folder, and run the following commands to build and run the API:

