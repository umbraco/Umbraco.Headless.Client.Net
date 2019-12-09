# Umbraco Heartcore Blazor Web Assembly sample

Blazor sample site for Umbraco Headless - with example set up and content retrieval.

Read more about Blazor WebAssembly [here](https://docs.microsoft.com/en-gb/aspnet/core/blazor/hosting-models?view=aspnetcore-3.0#blazor-webassembly)

## Prerequisites

- [.NET Core SDK 3.0](https://dotnet.microsoft.com/download/dotnet-core/3.0)
- This sample is based on the base Blazor project when you "file-> new" Blazor project in Visual Studio. Recommended to run through [this](https://docs.microsoft.com/en-gb/aspnet/core/blazor/get-started?view=aspnetcore-3.0&tabs=visual-studio) tutorial if you are new to Blazor.

## Start the application

Before running the application,  Index.razor needs to be updated with your Umbraco Heartcore project alias (the project alias can be found in the [Umbraco Cloud Portal](https://www.s1.umbraco.io)). If the Content Delivery API is protected the `ApiKey` also needs to be updated.


In order to use the sample you will need an Umbraco Heartcore project with content, media and document types that correspond to those setup in the Views and Models of the sample website. You can use `demo-headless` as the project alias to get started with the sample. The Project behind this alias has been used as the source of the sample, so its a good place to start.

The `ApiKey` is not used in this sample and can thus be left blank. If you chose to protect the content exposed via the Content Delivery API then you will need an API-Key, but its an option that has to be actively turned on (or off - its off by default) via the Umbraco Backoffice in the Headless tree in the Settings section.

## Using Heartcore Services

In our razor page (Index.razor) we must include the Heartcore services:

```
@page "/"
@using Umbraco.Headless.Client.Net.Delivery;
```

On the code segment of the .razor file, we can call our service. The `OnInitalizedAsync` is called on page render automatically. Our `content` variable is then available to use 

```
@code {
    Umbraco.Headless.Client.Net.Delivery.Models.Content content = new Umbraco.Headless.Client.Net.Delivery.Models.Content();
    protected override async Task OnInitializedAsync()
    {
        var projectAlias = "demo-headless";
        var service = new ContentDeliveryService(projectAlias);
        var rootContentItems = await service.Content.GetRoot("en-US");
        content = rootContentItems.FirstOrDefault();
    }
}
```

This is a simple example, showing only a few content fields, but using this as a starting point you can extend to integrate more Umbraco content to your Blazor application.
