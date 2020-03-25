# Interswitch-interview
The project is designed using Visual studio 2019 as an editor, .Net core3.1 and c# as a language. It's a web API that uses Swagger UI to describe and test the two designed endpoints i.e
-/api/InterSwitch/validate-customer
-/api/InterSwitch/send-payment-notification

### Project dependencies
To run the project, the following are required
  1. SQL Server 2014 and above
  2. Visual Studio 2019 and above or Visual studio code (The solution is not tested with lower versions of visual studio)
  3. Internet access
  
To generate the database, in Visio studio under the package console manager, select InterviewApi.BusinessEntities then run the following command: "Update-Database". This will generate the full database schema based on code written in the business entities class library. Alternatively, you can manually run the db script added in the project directory in sql server

## Project structure
The project is classified into 4 class libraries i.e InterviewApi, InterviewApi.BusinessEntities, InterviewApi.BusinessLogic, InterviewApi.Common.
  1. InterviewApi. This is the entry point into the application and its a central point through which all class libraries integrate. The end-points are defined in this class library
  2. InterviewApi.BusinessEntities. This is the data access layer of the system. The application DB context and the generic repository is defined here through which data is accessed. A code first approach was used to designed and modify the database.
  3. InterviewApi.BusinessLogic. In this layer, all business logic is written. i.e The logic to access inter-switch API, and the services that are used to validate a customer, send payment notification and authenticating these requests
  4. InterviewApi.Common. All common utilities and supporting business extension methods are defined in this class library

## Question 1
A code first approach has been used to address question one. AspNetUser table has been extended by adding more fields and relationships that meet the question description. DTOs have been added referencing the user table. In the "ModelConfig" directory, relationships and required fields have been defined as instructed in the question. In the project directory are two files i.e "erd_diagram.png" (ER diagram showing the table relationships) and "Db structure.png" (Show the database table structure)

## Question 2
In the InterviewApi.BusinessLogic class library, services have been written to access Interswitch API as explained below:
  1. BaseClient. This is an HttpClient wrapper class. Wrapper classes are important when creating mock objects but also eases dependency injection
  2. IInterSwitchAuth service handles the generation of custom request headers. i.e Signature, Timestamp, authorization token, etc.
  3. IHttpClientWrapper is a generic request wrapper service through which Post and Get requests are done. Its basically used to separate API access logic from the business rules service logic
  4. ICustomerValidationService. In this service, the business rule to validate a customer is written. This class serves only one purpose which demonstrates the separation of concerns rule
  5. PaymentNotificationService contains the business rules to required to send notification logic to the Interswitch API
  6. End-points. In the "InterviewApi" class library under "Controllers" directory are two controllers. i.e "BaseController" (In which logic that is common to all controllers is written) and "InterSwitchController" (In this controller I define two end-points i.e "validate-customer" and "send-payment-notification". Swagger is used to provide an interactive UI to these two end-points by making simple post requests)

### Challenges.
Much as all this logic is written, I faced a challenge when generating valid signatures. The standard .net Cryptography methods that generate signatures don't match the corresponding versions of Java. I have added two sample methods that I've tried to generate a corresponding signature i.e "ToSHA1()" and "ToSha1_2()" all found in the "ObjectExtensions" class. I'll continue to look for a proper MessageDigest equivalence in c# that could help someone in the future.
