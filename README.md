# Introduction

This code is a robust sample to exercise the Microsoft Azure Cognitive AI API content generation services. The code is written in C# Net 9. This is a fully working sample code and can be used as the basis to quickly create production code to implement Genertive AI into your existing products and web sites. The application is a Web API and supports Entra authentication. You can customise the returned results to return either structed or unstructured data depending on the "Query Name" you provde to the API. To use this sample you will need to register the application in Entra ID and create a KeyVault container for the Cognitive API Key. You can return tweak the query parameter to try different models and parameters. The query parameters are contained in a dictionary object and defined in the appsetting file. For structed output you will need to use the included schema file - feel free to cusomize per your needs.

# Getting Started

1. If this code works, I wrote it, otherwise I am not sure where it came from :) Seriously it should work! You can use the code as you see fit.
2. The use of 3rd party packages is minimal, I did include a reference to NewtonSoft to get a Swagger interface in Net 9
3. Postman scripts are included to help you get running quickly. You will need to adjust fields that have "YourTenantIdHere" by adding your Azure Tenant ID
4. This is a Web Api that calls the Microsoft Cognitive Services. You will need to configure your Azure subscription via AI Foundry to call the APi's. Additionally, you will need to register the Application in Entra Id if you plane to secure (suggested) the API.

# Build and Test

There are no unit test. This is a sample but could be used as the basis for a production system, however to be production ready, you should add logging and unit test. Also, beaware that calling the Microsoft AI serivces can get costly quickly so be aware.

# Contribute

I am planning to post another sample to demonstract RAG (Retrieval Augmented Generation).

# If you want to fast track your AI journey

If you would like to implement AI fearures in your application we would love to help. Or we can deliver production ready and deployed code in days and for the fraction of the cost of custom development. You will be given an unlimited license and full copies of source code so you control your destiny. We can upskill your staff to get them up to speed on implementing AI very quickly. If you want to add speach, image reconigition, content processing, or Generative AI to your products we can have you up and in production in a couple of days and for a fraction of the cost of in-house or costly consultants. We are establised and US based software firm specializing in Cloud Computing, SaaS, AI and security.

Reach me at: stephen.wheeler@wheelertech.us

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:

- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)
