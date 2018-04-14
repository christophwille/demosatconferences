# Reactive Programming mit Event Grid 

In modernen Lösungen sind Events überall - und obwohl Events so wichtig sind, muß man sehr viel Aufwand betreiben bevor man produktiv werden kann: das “plumbing” steht im Weg. Wie wäre es mit einer Lösung die das ganze Event Routing vom Publisher zum Subscriber übernehmen könnte? Inklusive Registrierung, Retry Logik, Monitoring, Delivery nahe Echtzeit und das ganze als Plattform Service der dynamisch skaliert?

Azure Event Grid bietet all das und wird mittlerweile - obwohl ein sehr “junger” Service - innerhalb von Azure bereits von einigen Services als Publisher bedient. Für viele Szenarien wird damit das Programmieren von “hammer polling” beziehungsweise “exponential backoff” überflüssig, was im Endeffekt Geld spart (Rechenzeit, Transkationen) - und das ist bei Cloud Lösungen ein gewichtiges Argument. Event Grid ist aber nicht auf Events von Azure selbst beschränkt: es ist auch für die eigene Applikation als Event backbone geeignet - WebHook Subscription Management ade, willkommen push-push!

[Global Azure Bootcamp 2018](https://coding-club-linz.github.io/global-azure-bootcamp-2018/)

## Azure Event Grid Official Links

* [Event Grid Homepage](https://azure.microsoft.com/en-us/services/event-grid/)
* [Docs: Overview](https://docs.microsoft.com/en-us/azure/event-grid/overview)
* [Docs: Choose between Azure services that deliver messages](https://docs.microsoft.com/en-us/azure/event-grid/compare-messaging-services) also check out [Events, Data Points, and Messages - Choosing the right Azure messaging service for your data](https://azure.microsoft.com/en-us/blog/events-data-points-and-messages-choosing-the-right-azure-messaging-service-for-your-data/) by Clemens Vasters

## Generic Articles and Videos

* [Article: Event-Driven Architecture in the Cloud with Azure Event Grid](https://msdn.microsoft.com/en-us/magazine/mt829271) with [code repo](https://github.com/dbarkol/AzureEventGrid)
* [On .NET: Cloud scale events with Azure Event Grid](https://channel9.msdn.com/Shows/On-NET/Cloud-scale-events-with-Azure-Event-Grid)
* [Azure Event Grid: Powering serverless through eventing](https://www.youtube.com/watch?v=SaOWhPTjHn0) also check out [Events, Data points and Messages](https://www.youtube.com/watch?v=ITrlLErsqzY) by Clemens Vasters
* [Article: An Introduction to Azure Event Grid](https://www.red-gate.com/simple-talk/cloud/cloud-development/introduction-azure-event-grid/)

## Sample Code

* [Azure Code Samples - Event Grid](https://azure.microsoft.com/en-us/resources/samples/?sort=0&service=event-grid)
* [EventGrid/Azure Function demo](https://www.codeproject.com/Articles/1220389/Azure-EventGrid-Azure-Function-demo)
* [Azure Event Grid Viewer with ASP.NET Core and SignalR](https://madeofstrings.com/2018/03/14/azure-event-grid-viewer-with-asp-net-core-and-signalr/)
* [Event-Grid-Glue](https://github.com/JeremyLikness/Event-Grid-Glue), article [Glue for the Internet](https://blog.jeremylikness.com/azure-event-grid-glue-for-the-internet-e770d94cc29)
* [Azure Event Grid extension for VS Code](https://github.com/Microsoft/vscode-azureeventgrid)
