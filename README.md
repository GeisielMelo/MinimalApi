## MinimalApi

Prototype API created using ASP.NET Core 8.0 and C# with the aim of providing an alternative version to my RESTful API built in Node.js, focusing on improved performance and scalability.

## Info

If you want to run this project on your local machine, you just need to create a .env file and follow the information available in .env.example.

## Routes

#### Public
- Auth - Create : POST /api/auth  { email, password }

- Users - Create : POST /api/users  { email, password }

#### Protected
- Users - Index  : GET /api/users

- Users - Show   : GET /api/users/id

- Users - Update : PUT   /api/users  { id, email, password }

- Users - Delete : DEL /api/users/id

## Author

- [@GeisielMelo](https://github.com/GeisielMelo)

## License

- [MIT](https://github.com/GeisielMelo/MinimalApi?tab=MIT-1-ov-file)
