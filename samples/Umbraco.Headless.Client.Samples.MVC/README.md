# Umbraco Headless ASP.NET Core MVC sample

.NET MVC sample site for Umbraco Headless - with custom routing and controller hijacking.

## Prerequisites

- [.NET SDK 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

## Start the application

Before running the application,  `appsettings.json` needs to be updated with your Umbraco Headless project alias (the project alias can be found in the [Umbraco Cloud Portal](https://www.s1.umbraco.io). If the Content Delivery API is protected, the `ApiKey` must be updated.

```json
{
  "Heartcore": {
    "ProjectAlias": "",
    "ApiKey": ""
  }
}
```

In order to use the sample, you will need a Umbraco Heartcore project with content, media and document types that correspond to those set up in the Views and Models of the sample website. You can use `demo-headless` as the project alias to get started with the sample. The Project behind this alias has been used as the sample source, so it is an excellent place to start.

The `ApiKey` is not used in this sample and can thus be left blank. Do note that you will need an API Key if you choose to protect the content exposed via the Content Delivery API. However, it is an option that has to be actively turned on (it is off by default) via the Umbraco Backoffice in the Headless tree in the Settings section.

### 1. Using the command line

In the `Umbraco.Headless.Client.Samples.MVC` you should run the following commands to restore the packages and run the site.

```bat
> dotnet restore
> dotnet run
```

### 2. Using an IDE

Run the application in VSCode or Visual Studio by hitting `F5`

## Routing, controllers and views

By default, the application will try to route the URLs through Umbraco Heartcore by calling `https://cdn.umbraco.io/content/url?url={url}` and if the response is `200 OK`, the `IUmbracoContext.Content` is set to the response.

Then the router checks if there is a controller for the specific content type, e.g. if the content type alias is `textPage`, it will look for a controller named `TextPageController` marked with the `UmbracoControllerAttribute` attribute. If found, the `Index` action is called; otherwise, the `DefaultUmbracoController` is called. It will render a view named `Views/{contentTypeAlias}/Index.cshtml` with `Umbraco.Headless.Client.Net.Delivery.Models.IContent` as the model.
