# Reservation APP API

This project designs the API of a booking application in great detail. The API allows users to make reservations, view existing reservations, and manage reservations. The project is developed using .NET Core and authenticates with JWT.

## Contents

- [Introduction](#introduction)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [User Registration and Login](#user-registration-and-login)
- [Reservation Procedures](#reservation-procedures)
- [Project Structure](#project-structure)
## Introduction

This project details the API of a booking application. The API provides various endpoints through which users can authenticate and perform booking transactions. It will be clearly helpful for anyone who wants to learn about this topic.
## Features

- User registration and authentication
- Secure authentication with JWT
- Create, view and cancel reservations
- User-specific reservation management
  ## Prerequisites

To run this project, you must have the following tools installed on your computer:

- [.NET SDK](https://dotnet.microsoft.com/download) (version 5.0 or later)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [Postman](https://www.postman.com/) or similar API client (optional)
  ## Installation

To run the project on your local machine, follow these steps:

1. Clone this project from GitHub:
git clone https://github.com/Gorlami5/GraduationThesis
cd booking-app-api

2. Run the following command to install the required dependencies:

dotnet restore

3. Run the project:

dotnet run
## Project Structure

The project is organized as follows:

```plaintext
GraduationThesis/ReservationApp
│
├── Controllers/
│ ├── AuthController.cs
│ ├── CityController.cs
│ ├── CompanyController.cs
│ ├── PhotoController.cs
│ ├── ReservationController.cs
├── BusinessUnit/
│ ├── Interfaces/
│ ├── AuthBusinessUnit.cs
│ ├── CityBusinessUnit.cs
│ ├── CompanyBusinessUnit.cs
│ ├── PhotoBusinessUnit.cs
│ ├── ReservationBusinessUnit.cs
├── Context/
│ ├── PostgreDbConnection.cs
├── DataAccessUnit/
│ ├── Interfaces/
│ ├── AuthDataAccess.cs
│ ├── CityDataAccess.cs
│ ├── CompanyDataAccess.cs
│ ├── PhotoDataAccess.cs
│ ├── ReservationDataAccess.cs
│ ├── UserDataAccess.cs
├── Dto/
│ ├── CityForDetailDto.cs
│ ├── CityForListDto.cs
│ ├── CompanyForListDto.cs
│ ├── CompanyPhotoForCreationDto.cs
│ ├── CompanyPhotoForReturnDto.cs
│ ├── PhotoForCreationDto.cs
│ ├── PhotoForReturnDto.cs
│ ├── ReservationForListDto.cs
│ ├── UserForLoginDto.cs
│ ├── UserForRegisterDto.cs
├── Extensions/
│ ├── CloudinaryInformation.cs
│ ├── ConstantsMessages.cs
│ ├── HashExtension.cs
├── Migrations/
│ ├── mig1/
├── Model/
│ ├── City.cs
│ ├── Company.cs
│ ├── CompanyPhoto.cs
│ ├── Photo.cs
│ ├── Reservation.cs
│ ├── Session.cs
│ ├── User.cs
├── Properties/
│ ├── launchSettings.json
├── Results/
│ ├── DataResult.cs
│ ├── ErrorDataResult.cs
│ ├── ErrorResult.cs
│ ├── IDataResult.cs
│ ├── IResult.cs
│ ├── Result.cs
│ ├── SuccessDataResult.cs
│ ├── SuccessResult.cs
├── Program.cs
├── ReservationApp.csproj
├── appsettings.json
└── appsettings.Development.json
