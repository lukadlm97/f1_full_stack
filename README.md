

Project for developing a full-stack app with appropriate documentation
------------------------------------------------------------------------------------------------------------------------

In this project, I will develop a lot of components using several theologies and technics. The purpose of this project is to learn some of them and involve all technics and technologies which I learn on my job.

The domain of the project is Formula 1 competition. I wanna create the application with some content of Formula 1 pieces of information - driver information, race, contract, news about F1 etc.

In the system, I will have more roles with implemented authentification and authorisation mechanisms.

Current status at trello:
![image](https://user-images.githubusercontent.com/36825550/172478993-342e3fcc-d1f3-43e0-a27b-7215c4e1bdad.png)


Timeline:
------------------------------------------------------------------------------------------------------------------------
1.	September 2021. - started project 
2.	end of September 2021. - thinking about project requrments and my wishis about concrete technology which i wanna using
3.	October 2021. - implemented logic for registrations users, JWT autorization, account managing etc.
4.	end of October 2021. - created drivers and country import from Kaggle web site (discovering seed potentals)
5.	23/11/2021 - migrate project to .NET 6, and created constructor details
6.	27/11/2021 - created repositories for drivers and constructors, and migrate repository logic to modern approach
7.	01/12/2021 - made more modern approach for doing logging on repository and executing operations on repository
8.	13/12/2021 - downgrade project to .net 5 (problems with entity and logging providers), added serilog for tracing, done get_all drivers functionality
9.	15/12/2021 - completed driver controller with all needed functionalities. started with doman part of contracts.
10.	19/12/2021 - finished constructor endpoints, created Swagger documentations for API, successfully ended testing of this endpoints.
11.	end of December 2021. - finished buissnes logic for racing result for drivers, constructors and contracts.
12.	16/01/2022 - tiny improvments on insert new driver logic (return created driver etc. for esier handling this action on front)
13.	18/01/2022 - completed part of API for CRUD on constructor, technical staff, and handling of contracts between constructors and technical staff with their role
14.	23/01/2022 - created endpoint for competition fetching and bugfix on racing result for constructors
15.	07/06/2022 - made comeback to project with better task organization and analyse of architecture to make part with competition, result, raceweeks, seasons, calendars more practical


Notes:
------------------------------------------------------------------------------------------------------------------------
* .NET Standard libraries (all libraries in this project are on this platform) still not support EntityFramworkCore packages 6.0.0. version (need to be 3.x.x)
