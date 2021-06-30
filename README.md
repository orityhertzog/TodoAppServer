# TodoAppServer
the todo-app is an end-to-end web application with [client side](https://github.com/orityhertzog/todo-app-project), server side and DB (JSON file/SQL server).
the server side of the Todo application, built using asp.net core web api, as REST api.

## Description
the Todo-app is an app to help manage todo's lists. 
the server side manages all the app CRUD using two different repositories services:
1. SQL server repository, using Entity Framework
2. JSON file - read/write from json file.

in the startup.cd under ConfigureServices you can choose which service to use (sql by default).

the app supports two types of controllers: the /lists controller which contains all the CRUD operations over the todo's lists,
and items controller which contains all the /items CRUD. every item belongs to one specific Todo list. the controllers return status codes depending on the situation.

TODOS: in the future, I intend to make changes in the entity's structure so that the items will be included inside the list entity (right now the item contains the list Id,
but the list is unaware of its items). 
also, I intend to transform some of the logic from the client to the server, and add some more functionality.







