# Umbraco Headless ASP.NET Core MVC sample

ASP.NET Core MVC sample site for Umbraco Headless - with custom routing and controller hijacking.

## Prerequisites

- [.NET Core SDK 2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2)

## Start the application

Before running the application,  `appsettings.json` needs to be updated with your Umbraco Headless
 project alias (the project alias can be found in the [Umbraco Cloud Portal](https://www.s1.umbraco.io)). If the Content Delivery API is protected the `ApiKey` also needs to be updated.

```json
{
  "Umbraco": {
    "ProjectAlias": "",
    "ApiKey": ""
  }
}
```

### 1. Using the command line

In the `Umbraco.Headless.Client.Samples.Web` folder run the following commands to restore the packages and run the site.

```bat
> dotnet restore
> dotnet run
```

### 2. Using an IDE

Run the application in VSCode or Visual Studio by hitting `F5`

## Routing, controllers and views

By default the application will try to route the URLs to through Umbraco Headless by calling `https://cdn.umbraco.io/content/url?url={url}` and if the response is `200 OK` the `UmbracoContext.Content` is set to the response.

Then the router checks if there's a controller for the specific content type e.g. if the content type alias is `textPage` it will look for a controller, marked with the `IUmbracoController` interface, named `TextPageController`, if found the `Index` action is called, otherwise the `DefaultUmbracoController` is called, it will then render a view named `Views/DefaultUmbraco/{contentTypeAlias}.cshtml` with `Umbraco.Headless.Client.Net.Delivery.Models.Content` as the model.
