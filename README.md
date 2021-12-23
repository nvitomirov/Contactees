# Contactees

- Practice application - `asp.net mvc` and Restfull service

- Solution consists of two `asp.net core mvc` applications communicating with each other trought web services with JSON message format.

- Server application communicates with database using `EntityFramework`.

- Both applications can be installed independently on different locations (servers).

- Implementation of CRUD operations on server side for all tables and in web application trough forms.
- Web application has some basic checks of recievieng objects from the front-end, and sends information to server application.
- Server application receives messages, performs checks and manages data in database.
- Database consists of three entities: Person, Contacts, Addresses. 
- Person entity has properties Name, Surname, National ID Number, Contacts and Addressess
- Contact entity has properties Phone Number and E-mail
- Address entity has properties Street Name, Street Number, City, PostalCode and Country 
- One person can have several Contacts and Addresses. 
