# Umbraco Headless Xamarin Forms iOS and Android sample

Xamarin Forms sample app for Umbraco Headless that lets you navigate the Content tree.

## Prerequisites

- [Xamarin Forms](https://dotnet.microsoft.com/apps/xamarin/xamarin-forms)

## Start the application

Before running the application, `src/Umbraco.Headless.Client.Samples.Xamarin.Forms/App.xaml.cs` needs to be updated with your Umbraco Headless
 project alias (the project alias can be found in the [Umbraco Cloud Portal](https://www.s1.umbraco.io)). If the Content Delivery API is protected the `ApiKey` also needs to be updated.

```cshorp
string projectAlias = "";
string apiKey = null;
```

In order to use the sample you will need an Umbraco Headless project with content, media and document types that correspond to those setup in the Views and Models of the sample website. You can use `demo-headless` as the project alias to get started with the sample. The Project behind this alias has been used as the source of the sample, so its a good place to start.

The `ApiKey` is not used in this sample and can thus be left blank. If you chose to protect the content exposed via the Content Delivery API then you will need an API-Key, but its an option that has to be actively turned on (or off - its off by default) via the Umbraco Backoffice in the Headless tree in the Settings section.

In your IDE select the project you want run, Android or iOS and click run. This should bring up an emulator for the selected device and open up the app.
