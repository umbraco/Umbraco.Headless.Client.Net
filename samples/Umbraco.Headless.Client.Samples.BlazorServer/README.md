# Umbraco Heartcore Blazor Server sample

Blazor sample site for Umbraco Headless - with example set up and content retrieval.

Read more about Blazor [here](https://docs.microsoft.com/en-gb/aspnet/core/blazor/?view=aspnetcore-3.0)

## Prerequisites

- [.NET SDK 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- This sample is based on the base Blazor project when you "file-> new" Blazor project in Visual Studio. Recommended to run through [this](https://docs.microsoft.com/en-gb/aspnet/core/blazor/get-started?view=aspnetcore-3.0&tabs=visual-studio) tutorial if you are new to Blazor.

## Start the application

Before running the application,  `appsettings.json` needs to be updated with your Umbraco Heartcore project alias (the project alias can be found in the [Umbraco Cloud Portal](https://www.s1.umbraco.io)). If the Content Delivery API is protected the `ApiKey` also needs to be updated.

```json
{
  "Umbraco": {
    "ProjectAlias": "",
    "ApiKey": ""
  }
}
```

To use the sample you will need an Umbraco Heartcore project with content, media and document types that correspond to those setup in the Views and Models of the sample website. You can use `demo-headless` as the project alias to get started with the sample. The Project behind this alias has been used as the source of the sample, so its a good place to start.

The `ApiKey` is not used in this sample and can thus be left blank. If you chose to protect the content exposed via the Content Delivery API then you will need an API-Key, but its an option that has to be actively turned on (or off - its off by default) via the Umbraco Backoffice in the Headless tree in the Settings section.

## Using Heartcore Services

Add the 'HeartcoreClientService.cs' class then the following snippet in the Program.cs to allow your app to use the Heartcore services and pick up your configuration from application.config as mentioned above.

```csharp
services.AddRazorPages();
services.AddServerSideBlazor();
services.AddSingleton<UmbracoService>(); //we'll get to this soon

//get your application.config
var umbracoConfig = Configuration.GetSection("umbraco");
var projectAlias = umbracoConfig.GetValue<string>("projectAlias");
var apiKey = umbracoConfig.GetValue<string>("apiKey");

//add the Umbraco content delivery service
services.AddUmbracoHeadlessContentDelivery(projectAlias, apiKey);
```

 ## Getting content

Since this this server side Blazor, I am calling Heartcore from the backend. I have created a UmbracoService.cs to do some (very simple for this example) content retrieval.

```csharp
public class UmbracoService
{
    private readonly ContentDeliveryService _contentDelivery;

    public UmbracoService(ContentDeliveryService contentDelivery)
    {
        _contentDelivery = contentDelivery ?? throw new ArgumentNullException(nameof(contentDelivery));
    }

    public async Task<Content> GetRoot()
    {
        var rootContentItems = await _contentDelivery.Content.GetRoot();
        return rootContentItems.FirstOrDefault();
    }
}
```

In our razor page (Index.razor) we must inject our service:

```
@page "/"
@using Umbraco.Headless.Client.Samples.BlazorServer.Heartcore;
@using Umbraco.Headless.Client.Net.Delivery.Models;

@inject UmbracoService UmbracoService
```

On the code segment of the .razor file, we can call our service. The `OnInitalizedAsync` is called on page render automatically. Our `root` variable is then available to use

```
@code {

    Content root = new Content();
    ContentCollection<Content> content = new ContentCollection<Content>();

    protected override async Task OnInitializedAsync()
    {
        root = await UmbracoService.GetRoot()

    }
}
```

This is a simple example, but using this as a starting point you can extend to integrate more Umbraco content to your Blazor application.
