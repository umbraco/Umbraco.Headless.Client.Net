# Umbraco Headless ASP.NET Core MVC sample

ASP.NET Core MVC sample site for Umbraco Headless - with custom routing and controller hijacking.

## Prerequisites

- [.NET Core SDK 2.2](https://www.microsoft.com/net/download/windows)

## Start the application

Before running the application,  `appsettings.json` needs to be updated with your Umbrace Headless
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

In the `Umbraco.Headless.Client.Samples.Web` folder run the following commands to restore the packagen and run the site.

```bat
> dotnet restore
> dotnet run
```

### 2. Using an IDE

Run the application in VSCode or Visual Studio by hitting `F5`

## Routing, controllers and views

By default the application will try to route the URL's to through Umbraco Headless by calling `https://cdn.umbraco.io/content/url?url={url}` and if the response is `200 OK` the `UmbracoContext.Content` is set to the response.

Then the router checks if there's a controller for the specific content type e.g. if the content type alias is `textPage` it'll look for an `IUmbracoController` named `TextPageController` if found, the `Index` action is called, otherwise the `DefaultUmbracoController` is called, it'll render a view named `Views/DefaultUmbraco/{contentTypeAlias}.cshtml` with `Umbraco.Headless.Client.Net.Delivery.Models.Content` as the model.
