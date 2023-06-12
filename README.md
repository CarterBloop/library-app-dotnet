# library-app-dotnet
.NET Core application for managing books, authors, etc. RESTful API for performing CRUD operations on books, authors, users, and loans.
Backend written in C#.

## Endpoints:

### Books:
  GET /api/books: Retrieve a list of all books.
  GET /api/books/{id}: Retrieve details of a specific book by its ID.
  POST /api/books: Create a new book.
  PUT /api/books/{id}: Update an existing book.
  DELETE /api/books/{id}: Delete a book.
### Authors:
  GET /api/authors: Retrieve a list of all authors.
  GET /api/authors/{id}/books: Retrieve all books written by a specific author.

## PostgreSQL Database

```sql
CREATE TABLE loan (
    bookid INTEGER,
    id INTEGER,
    startdate DATE,
    enddate DATE,
    userid INTEGER
);

CREATE TABLE book (
    id INTEGER,
    title VARCHAR(255),
    isbn VARCHAR(13)
);

CREATE TABLE bookauthor (
    bookid INTEGER,
    authorid INTEGER
);

CREATE TABLE author (
    id INTEGER,
    name VARCHAR(255)
);

CREATE TABLE user (
    id INTEGER,
    email VARCHAR(255),
    name VARCHAR(255)
);
```
