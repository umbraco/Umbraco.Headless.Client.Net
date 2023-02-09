# Umbraco Headless Console sample

Console application sample for Umbraco Headless - with options using the Content Delivery and the Content Management API.

## Prerequisites

- [.NET SDK 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

## Start the application

Run the application directly from Visual Studio by hitting F5 or open the folder with the `.csproj` file and use `dotnet` from the command line.

### 1. Using the command line

In the `Umbraco.Headless.Client.Samples.Console` folder run the following commands to restore the packages and run the Console application.

```bat
> dotnet restore
> dotnet run
```

### 2. Running the Console application

When the Console application is running you will first be prompted to "Enter the Project Alias of your Headless Project". To fully use the sample you will need an Umbraco Headless project with content and media. If you don't have one you can use `demo-headless` as the project alias for the options, which doesn't require an API Key.

In the list below you will find the options available in the Console application. Option A-E use the Umbraco Headless Content Delivery API and can be used for any Headless Project, which has public content. Option F uses the Content Management API, so an API-Key is required to run this part of the sample, as it will create a new folder in the Media Library and upload an image to a new Media item.

- [ A ] Fetch and show Content tree
- [ B ] Fetch and show Media tree
- [ C ] Show root Content
- [ D ] Show root Media
- [ E ] List Content URLs
- [ F ] Upload image to Media Library
- [ G ] Create content that references media
  - This case assumes that you have created a Document Type with a Media Picker property,
  - This case assumes that you have created a Media item (u can use case F before this case, to create a Media item).
- [ X ] Exit
