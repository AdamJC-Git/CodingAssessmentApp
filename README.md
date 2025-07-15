# CodingAssessmentApp
tech test -
The test uses an API supplied by https://dummyjson.com/
 
Implement a C#.NET 8 Console application with the following features:
1. From the console, allow any user from https://dummyjson.com/users to log into the app using https://dummyjson.com/auth/login
2. Using the logged in user’s token, the application should find the three most expensive smartphones from https://dummyjson.com/auth/products, and display its brand, title and price from most to least expensive
3. The application should then ask the user to input a percentage value - and when entered, increase the price of these smartphones by the percentage supplied, via the API.
4. The application should then display the updated smartphones and their new prices.
 
The application should log user input and API calls and responses it should also have appropriate error handling, ie login failures, connection issues etc.
Please add any appropriate unit tests that you feel are appropriate
The application is relatively trivial but should allow you to demonstrate your ability to design and structure an application to ensure it meets the requirements, works as requested and can be maintained easily
