# LibraryMgmt
A simple library management api implemented using C# + .Net 8

This is deployed to Azure Container Apps via a github workflow. Future work should look to move the handling of the necessary cloud components to some form of iac (terraform, pulumi or bicep) for better repeatability.

[Live Swagger Page](https://librarymgmt-api.delightfulsea-08c8e5fe.uksouth.azurecontainerapps.io/swagger/index.html)

The in memory nature of the application + the scale to zero configuration of Container Apps will cause data to disappear when a cold start occurs. 

# Solution Structure
### LibraryMgmt
Web api project that provides a RESTful interface over a Library of Books. This utilises minimal apis & FluentValidation.

The project is structured around vertical slices. All Features related to the books context are located under `./LibraryMgmt/Books/Features`. Each feature corresponds to a RESTful endpoint.

A shared domain model is used by these features, the core of which is `Library.cs` which enforces the required business logic.

This can be built via the provided Dockerfile & deployed anywhere with a container runtime. The api will run as a non root user as per the base microsoft container. 

### LibraryMgmt.Test.Unit
Unit tests that cover the domain model

### Library.Test.Integration
Integration tests that cover the http endpoints. These all run against an in memory server, asserting the behaviour of the api front-to-back.

### Pipeline
`.github/workflows/cd` defines a barebones CD pipeline that 
1. Runs unit tests
2. Runs integration tests
3. Deploys to Azure Container Apps

This pipeline is set up to run manually.