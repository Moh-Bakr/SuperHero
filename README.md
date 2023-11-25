# SuperHero Favorite List

Manage and organize your favorite superheroes with ease. Explore details, add to favorites.

## Table of Contents

- [Introduction](#introduction)
    - [Overview](#overview)

- [Project Overview](#project-overview)
    - [Architecture](#architecture)
    - [Components](#components)

- [Database](#database)
    - [Schema](#schema)
    - [Data Models](#data-models)

- [API Guide](#api)
    - [Request Flow](#request-flow)
    - [API Overview](#api-details)
    - [Auth Details](#auth-details)
    - [Super Hero Details](#super-hero)
    - [Favorite List Details](#favorite-list)
    - [Response Messages](#response-messages)

- [Integration Services](#integration-services)
    - [External Services](#external-services)
    - [Setup and Configuration](#setup-and-configuration)

- [Getting Started](#getting-started)
    - [Development Environment](#development-environment)
    - [Dependencies](#dependencies)

## Introduction

### Overview

Discover, search, and explore superheroes effortlessly. Get detailed information on status, power, and more. Easily add
your favorite superheroes to your list for quick access.

## Project Overview

### Architecture

The SuperHero Favorite List project follows the Repository Pattern in
conjunction with the Unit of Work. This architecture enhances modularity and maintainability by separating concerns and
providing a structured way to interact with the underlying data.

![Alt text](README/RepositoryPattern.png?raw=true)

### Components

#### Core

The Core component serves as the heart of the SuperHero Favorite List project, housing essential elements such as
controllers, app settings, and the main program entry point (`program.cs`). It acts as the central hub for the
application, providing the foundational structure and organization.

#### BAL (Business Access Layer)

The Business Access Layer (BAL) is a critical component of the SuperHero Favorite List project, responsible for
encapsulating the business logic and rules. This layer abstracts away the complexities of data processing, ensuring a
separation of concerns. It interacts with the Data Access Layer (DAL) to retrieve and persist data, orchestrating the
flow of information between the user interface and the data storage.

- **Service Layer:** Implements business logic and interacts with repositories through the Unit of Work. This layer
  encapsulates the application's core functionality, making it easily testable and maintainable.
- **DTOs (Data Transfer Objects):** Contains data transfer objects that are used to transfer data between the
  application and the API. This ensures that data is transferred consistently across the application, promoting data
  integrity.
- **Mappers:** Contains mappers that are used to map data between the application and the API. This ensures that data
  is mapped consistently across the application, promoting data integrity.
- **FluantValidation:** Contains fluent validation rules that are used to validate data. This ensures that data is
  validated consistently across the application.

#### DAL (Data Access Layer)

The Data Access Layer (DAL) is dedicated to managing the interaction between the application and the data source,
typically a database. It contains repositories that handle data retrieval, storage, and manipulation. The DAL ensures a
clear and standardized interface for accessing and managing the underlying data, promoting consistency and integrity.

- **Unit of Work:** Acts as a cohesive unit that oversees and coordinates multiple repositories. This ensures that
  transactions are managed consistently across different parts of the application, promoting data integrity.
- **Repositories:** Responsible for handling data access logic. Each entity (e.g., superheroes) has its dedicated
  repository, providing a clear and standardized interface for data operations.
- **Pagination:** Contains methods for paginating data. This ensures that data is presented in a structured and
  organized manner, making it easier to navigate and access.

#### Helper

The Helper component is a utility layer containing global methods that are utilized throughout the application. It
serves as a repository for commonly used functions, promoting code reusability and maintaining a centralized location
for shared functionalities.

- **Response:** Contains methods for generating consistent responses to API requests. This ensures that all responses
  follow a standardized format, making it easier to handle and process them.
- **FluentValidationErrorMessage:** Contains methods for formatting error messages. This ensures that error messages are
  formatted consistently across the application, promoting data integrity.
- **JwtToken:** Contains methods for generating and validating JWT tokens. This ensures that tokens are generated and
  validated consistently across the application, promoting data integrity.
- **SuperHeroApi:** Contains methods for calling the Superhero API. This ensures that data is retrieved consistently
  across the application, promoting data integrity.

## Database

### Schema

![Alt text](README/DataBaseDiagram.png?raw=true)

### Data Models

The SuperHero Favorite List project utilizes the following data models to structure and organize key information within
the system:

#### AspNetUsers

The `AspNetUsers` data model represents user information within the application. It typically includes details such as
user IDs, usernames, email addresses, and authentication-related data.

#### AspNetTokens

The `AspNetTokens` data model is responsible for storing tokens associated with user authentication and authorization
processes. This model may include fields such as token IDs, expiration dates, and related user information.

#### AspNetRoles

The `AspNetRoles` data model defines the roles available within the system. It includes information about each role,
such as role IDs and role names.

#### AspNetUsersRoles

The `AspNetUsersRoles` data model establishes the relationship between users and roles. It serves as a junction table
connecting user IDs with role IDs, facilitating the assignment of roles to individual users.

#### FavoriteList Table

The `FavoriteList` table within the SuperHero database serves as a repository for user-specific superhero favorites.
Here are the key fields of the table:

- **Id:** The unique identifier for each entry in the `FavoriteList` table.

- **SuperHeroId:** The identifier linking each favorite to a specific superhero within the application.

- **UserId:** The user identifier associated with the favorite entry, establishing ownership and connecting it to a
  specific user.

- **FullName:** The full name of the superhero added to the user's favorite list.

- **PlaceOfBirth:** The place of birth of the superhero.

- **ImageUrl:** The URL pointing to the image or avatar of the superhero.

## Api

### Request Flow

#### Sequence Diagram

![Alt text](README/SquenceDiagram.png?raw=true)

#### Services Diagram

![Alt text](README/ServicesDiagram.png?raw=true)

### Api Details

#### Overview

##### Base URL

```
  {{baseUrl}}/api/v1
```

| Reference                   | Method   | Purpose                                                 | Call External api         |
|-----------------------------|----------|---------------------------------------------------------|---------------------------|
| `/Auth/register`            | `POST`   | Allows users to register a new account.                 | `NO`                      |
| `/Auth/login`               | `POST`   | Allows users to log in to their account.                | `NO`                      |
| `/SuperHero/Search`         | `GET`    | Allows users to search for the superhero.               | `YES`  -> `Superhero API` |
| `/SuperHero/Details`        | `GET`    | Allows users to get all the details of the superhero.   | `YES`  -> `Superhero API` |
| `/FavoriteList/Create`      | `POST`   | Allows users to add a superhero to their favorite list. | `NO`                      |
| `/FavoriteList/GetByUserId` | `GET`    | Allows users to get all their favorite superhero.       | `NO`                      |
| `/FavoriteList/Delete`      | `DELETE` | Allows users to delete their favorite hero.             | `NO`                      |

#### Swagger UI

![Alt text](README/SwaggerUI.png?raw=true)

#### PostMan Collection

```
 https://documenter.getpostman.com/view/17382947/2s9Ye8faQP
```

### Auth Details

#### Register

##### Payload Details

| Field           | Type   | Nullable | Validation                                                                                               | Description                                 |
|-----------------|--------|----------|----------------------------------------------------------------------------------------------------------|---------------------------------------------|
| userName        | string | false    | Unique & Only allows characters '_', '-'                                                                 | The chosen username for registration.       |
| email           | string | false    | Unique & must be a valid email address.                                                                  | The user's email address for communication. |
| password        | string | false    | Must contain at least one uppercase letter, one lowercase letter, one number, and one special character. | The user's chosen password.                 |
| confirmPassword | string | false    | Must be the same as the password                                                                         | Confirmation of the chosen password.        |

#### Login

##### Payload Details

| Field    | Type   | Nullable | Validation                                                                                               | Description                           |
|----------|--------|----------|----------------------------------------------------------------------------------------------------------|---------------------------------------|
| userName | string | false    | Unique & Only allows characters '_', '-'                                                                 | The chosen username for registration. |
| password | string | false    | Must contain at least one uppercase letter, one lowercase letter, one number, and one special character. | The user's chosen password.           |

### Super Hero Details

#### Search Super Hero

##### Payload Details

| Field | Type   | Nullable | Validation | Description         |
|-------|--------|----------|------------|---------------------|
| Name  | string | false    | Required   | The super hero name |

#### Get Super Hero Details

##### Payload Details

| Field       | Type | Nullable | Validation | Description                                          |
|-------------|------|----------|------------|------------------------------------------------------|
| CharacterId | int  | false    | Required   | The CharacterId of the super hero to get the details |

### Favorite List

#### Add Super Hero to Favorite List

##### Payload Details

| Field        | Type    | Nullable | Validation                                | Description                                                        |
|--------------|---------|----------|-------------------------------------------|--------------------------------------------------------------------|
| superHeroId  | integer | false    | Required                                  | The identifier of the superhero.                                   |
| userId       | string  | false    | Required & must be associated with a user | The user identifier associating the favorite with a specific user. |
| fullName     | string  | false    | Required                                  | The full name of the superhero.                                    |
| placeOfBirth | string  | false    | Required                                  | The place of birth of the superhero.                               |
| imageUrl     | string  | false    | Required                                  | The URL pointing to the image/avatar of the superhero.             |

#### GET All Super Hero from Favorite List

##### Payload Details

| Field                  | Type | Nullable | Validation | Description                            |
|------------------------|------|----------|------------|----------------------------------------|
| id                     | int  | false    | Required   | The id of the super hero to delete     |
| pageNumber default(1)  | int  | true     | Required   | page number for pagination             |
| pageSize   default(10) | int  | true     | Required   | the size of the data for th pagination |

#### Remove Super Hero from Favorite List

##### Payload Details

| Field | Type | Nullable | Validation | Description                        |
|-------|------|----------|------------|------------------------------------|
| id    | int  | false    | Required   | The id of the super hero to delete |

### Response Messages

Note all the responses will have the following structure:

#### SuccessResponses

```
{
    "IsSuccess": true,
    "message": "string",
}
```

#### ErrorResponses

```
{
    "IsSuccess": false,
    "errors":[],
}
```

## Integration Services

### External Services

#### Superhero API

The Superhero API is a comprehensive and programmatically accessible data source containing information about
superheroes from various comic universes. Designed for easy integration with software applications, it provides
hassle-free access to detailed superhero data. The API offers a user-friendly and quantified format, making it an ideal
resource for retrieving information about your favorite superheroes.

**Documentation:** [Superhero API Documentation](https://superheroapi.com/)

**Purpose:** Retrieve detailed information about superheroes for integration with the SuperHero Favorite List
application.

**Authentication:** Check the [Superhero API Documentation](https://superheroapi.com/) for authentication requirements
and access details.

**Usage Guidelines:** Follow the guidelines provided in the [Superhero API Documentation](https://superheroapi.com/) for
proper usage and adherence to rate limits.

### Setup and Configuration

- login to Superhero API and get the access token
- add the access token to the appsettings.json file

## Getting Started

### Development Environment

- clone the repository
- open the solution in Visual Studio or any other IDE
- copy the appsettings.json file from the project and duplicate it to appsettings.Development.json
- add the connection string to the appsettings.Development.json file
- run the migration to create the database
- run the seed to add the roles
- add the jwt configuration to the appsettings.Development.json
- add the access token to the appsettings.Development.json
- run the project

### Dependencies

- .NET 8.0
- SQL Server