# Mit-e-Kat

## Content

1. [Architectural notes](#Architectural-notes)
2. [How to run](#How-to-run)
3. [Todo](#Todo)

## Architectural notes

1. [Core concepts](#Core-concepts)
2. [Implementation](#Implementation)

### Core concepts

Application is devided into 3 main parts:
1. [Application core](#Application-core)
2. [Pluggable components](#Pluggable-components)
3. [Application entry point](#Application-entry-point)

#### Application core

Application core contains business-logic and should be independant from other
application parts. This is achieved through the use of ports and plug-in
components. Application core describes the protocol for interacting with an
external component and provides the ability to connect to itself any module that
meets this protocol. At the same time, the application core cannot bypass this
protocol and interact directly with the internal structure of this module.

#### Pluggable components

Pluggable components can implement any functionality required by the application
core. They should implement the protocol declared by the application core,
hiding implementation details and providing a way to connect themselfs to the
core, but should not connect on their own.

Typical representatives of puggable components are: a persistence module, an
authorization module, an encryption & compression module, a module for
interacting with an external system.

#### Application entry point

The application entry point is concerned with attaching pluggable components to
the application core and providing external access to the application.

Typical representatives of the entry point are: Web API & Tests.

### Implementation

The above concepts are implemented as follows:
1. Application core is the `Mitekat.Core project` project
2. Projects `Mitekat.Persistence` & `Mitekat.Helpers` are pluggable components
3. Entry point is the `Mitekat.RestApi` project

Illustration:
![Project Diagram](github/Project%20Diagram.jpg)

### Todo

1. [x] AccessToken + RefreshToken JWT auth
2. [x] OpenAPI spec + SwaggerUI
3. [x] MediatR handlers
4. [x] Utils for Failure and Success
5. [x] Request execution pipeline
6. [x] Utility for current user info retrieval
7. [ ] Mapping
8. [ ] Request validation
9. [ ] Configuration validation
10. [ ] Infrastructure for tests
11. [ ] Token versioning
12. [ ] Api versioning
13. [x] Automatic error handling
14. [x] Logging

## How to run

1. [Dependencies](#Dependencies)
2. [Clone and prepare](#Clone-and-restore)
3. [Run application](#Run-application)

### Dependencies

1. .Net 5
2. PostgreSQL 12+

### Clone and prepare

1. [Clone and restore](#Clone-and-restore)
2. [Specify app settings](#Specify-app-settings)
3. [Apply migrations](#Apply-migrations)

#### Clone and restore

You can download `.zip` archive righ now or use the following git cli command to
clone project:
```
git clone https://github.com/catman0745/Mit-e-Kat.git
```

Then run the following command in the **solution root**:
```
dotnet restore
```

#### Specify app settings

The easies way is to just create `application.Development.json` in the root of
the `Mitekat.RestApi` project:
```json
{
  "Auth": {
    "SecretKey": "32-plus-character-long-auth-secret-key",
    "AccessTokenLifetimeInMinutes": "15",
    "RefreshTokenLifetimeInDays": "7"
  },
  "Database": {
    "Server": "localhost",
    "Port": "5432",
    "Database": "db_name",
    "User": "db_user",
    "Password": "db_user_password"
  }
}
```
Note that the specified `db_user` should be able to create extensions in the
`db_name` database.

#### Apply migrations

You need `dotnet-ef` module to be installed on your system:
```
dotnet tool install --global dotnet-ef
```

Then you can just run the following command in the **solution root**:
```
dotnet ef database update -p ./Backend/Mitekat.Persistence -s ./Backend/Mitekat.RestApi
```

### Run application

Now you are good-to-go. You can run application in your favorite IDE or you can
just use the following command instead:
```
dotnet run -p ./Backend/Mitekat.RestApi
```

Assuming you didn't change the `launchSettings.json` config in the
`~/Backend/Mitekat.RestApi/Properties` folder, the application should be running
on [localhost#5000](http://localhost:5000).
