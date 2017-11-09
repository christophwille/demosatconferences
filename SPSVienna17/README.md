# Graf Bobby entdeckt den Microsoft Graph 

The Microsoft Graph is the single endpoint for accessing cloud-stored information, either in organizational 
settings or for consumers. It allows you to find out about machines (think Intune), users and their relationships 
(think Outlook), as well as OneDrive, Excel and obviously Sharepoint. The Graph API is growing by leaps and bounds 
every month, and thus we cannot look at all API details. Instead, we are going take a look at the overarching 
concepts across all resource types, from authentication to common operations, to advanced concepts 
such as delta query and webhooks. 

## Links

* [Graph Explorer](http://aka.ms/ge)
* [Microsoft Graph on GitHub](https://github.com/MicrosoftGraph) - sample [Microsoft Graph Snippets Sample for ASP.NET 4.6](https://github.com/microsoftgraph/aspnet-snippets-sample/) demoed
* [Documentation](https://developer.microsoft.com/en-us/graph/docs/concepts/overview)
* [Official Samples](https://developer.microsoft.com/en-us/graph/code-samples-and-sdks)
* OfficeDev Training Content [Getting Started with the Microsoft Graph](https://github.com/OfficeDev/TrainingContent/tree/master/Graph)
* [Microsoft Graph Client Library for .NET](https://github.com/microsoftgraph/msgraph-sdk-dotnet)
* [StackOverflow microsoft-graph](https://stackoverflow.com/questions/tagged/microsoft-graph)
* Important v2 endpoint docs [What's different about the v2.0 endpoint?](https://docs.microsoft.com/en-us/azure/active-directory/develop/active-directory-v2-compare) and [Should I use the v2.0 endpoint?](https://docs.microsoft.com/en-us/azure/active-directory/develop/active-directory-v2-limitations)
* [Permissions reference](https://developer.microsoft.com/en-us/graph/docs/concepts/permissions_reference)
* [Azure Functions Microsoft Graph bindings](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-microsoft-graph)

## Videos (mostly Ignite)

* [Build smarter apps with Office using the Microsoft Graph](https://channel9.msdn.com/Events/Ignite/Microsoft-Ignite-Orlando-2017/BRK3080)
* [Build applications to secure and manage your enterprise using Microsoft Graph](https://channel9.msdn.com/Events/Ignite/Microsoft-Ignite-Orlando-2017/BRK3388)
* [Office development: Authentication demystified](https://channel9.msdn.com/Events/Ignite/Microsoft-Ignite-Orlando-2017/BRK3225)
* [Integrate OneDrive and SharePoint files, collaboration and sharing using Microsoft Graph](https://channel9.msdn.com/Events/Ignite/Microsoft-Ignite-Orlando-2017/BRK3039)
* [Microsoft Graph for the .NET Developer](https://channel9.msdn.com/Events/dotnetConf/2017/T229)
* [The keys to the cloud: Use Microsoft identities to sign in and access API from your mobile+web apps](https://channel9.msdn.com/Events/Ignite/Microsoft-Ignite-Orlando-2017/BRK3207)
* [Modern business processes with Microsoft Graph and Azure Functions](https://channel9.msdn.com/Events/Ignite/Microsoft-Ignite-Orlando-2017/BRK3202)
* [Navigating the Microsoft Graph with Azure Functions](https://channel9.msdn.com/Shows/Azure-Friday/Navigating-the-Microsoft-Graph-with-Azure-Functions)

## Tools and more

* [Azure Active Directory admin center](https://aad.portal.azure.com/)
* [Application Registration Portal](https://apps.dev.microsoft.com/) v2 endpoint
* [Postman](https://www.getpostman.com/) see article [Using Postman to call the Graph API using Azure Active Directory](https://blogs.msdn.microsoft.com/softwaresimian/2017/10/05/using-postman-to-call-the-graph-api-using-azure-active-directory-aad/)
* [ngrok](https://ngrok.com/) for tunneling to localhost (WebHooks scenario)
* [REST Client for VS Code](http://josephwoodward.co.uk/2017/10/rest-%20client-for-vs-Code-an-elegant-alternative-postman)
* [jwt.ms](http://jwt.ms/) never paste production tokens!
