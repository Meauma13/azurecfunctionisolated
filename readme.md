This is a an Azure C# sample isolated function running on Azure CosmosDB Core SQL API.

Had to change thr "functions_worker_runtime" setting to "dotnet-isolated" inorder to get it running locally after adding the Nuget package via the Terminal "dotnet add package Microsoft.Azure.Webjobs.Extensions.CosmosDB"

Credit to https://learn.microsoft.com/en-us/azure/azure-functions/create-first-function-vs-code-csharp
